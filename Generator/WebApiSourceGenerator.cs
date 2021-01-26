using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

using System.IO;
using System.Linq;
using System.Text;

namespace Generator
{
    [Generator]
    public class WebApiSourceGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new WebApiSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            WebApiSyntaxReceiver syntaxReceiver = (WebApiSyntaxReceiver)context.SyntaxReceiver;
            GenerateCommandClass(context, syntaxReceiver);
            GenerateQueryClass(context, syntaxReceiver);
        }

        private void GenerateQueryClass(GeneratorExecutionContext context, WebApiSyntaxReceiver syntaxReceiver)
        {
            string QueryClassTemplate = LoadTemplate(context, filename: "QueryClassTemplate.txt");

            StringBuilder commandSource = new StringBuilder();

            foreach (var query in syntaxReceiver.Queries)
            {
                var queryCommandName = query.Identifier.ValueText;
                var queryReturnType = LookupIRequestGenericType(query);
                var commandComments = query.GetLeadingTrivia().ToString();

                commandSource.AppendLine(@$"
        {commandComments}
        /// <param name=""command"">An instance of the {queryCommandName}</param> <returns>The
        /// returned result of this command</returns> <response code=""201"">Returns the newly
        /// created item</response> <response code=""400"">If the item is null</response>
        [HttpPost]
        [Produces(""application/json"")]
        [ProducesResponseType(typeof({queryReturnType}), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<{queryReturnType}> {queryCommandName}([FromBody]{queryCommandName} command)
                {{
                    return await _mediator.Send(command);
                }}

                ");
            }

            string finalSource = QueryClassTemplate.Replace("###Queries###", commandSource.ToString());

            SourceText sourceText = SourceText.From(finalSource, Encoding.UTF8);

            context.AddSource("GeneratedQueryController.cs", sourceText);
        }

        private void GenerateCommandClass(GeneratorExecutionContext context, WebApiSyntaxReceiver syntaxReceiver)
        {
            StringBuilder commandSource = new StringBuilder();
            string CommandClassTemplate = LoadTemplate(context, filename: "CommandClassTemplate.txt");

            foreach (var command in syntaxReceiver.Commands)
            {
                var commandName = command.Identifier.ValueText;
                var commandReturnType = LookupIRequestGenericType(command);
                var commandComments = command.GetLeadingTrivia().ToString();

                commandSource.AppendLine(@$"
        {commandComments}
        /// <param name=""command"">An instance of the {commandName}</param> <returns>The status of
        /// the operation</returns> <response code=""201"">Returns the newly created item</response>
        /// <response code=""400"">If the item is null</response>
        [HttpPost]
        [Produces(""application/json"")]
        [ProducesResponseType(typeof({commandReturnType}), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<{commandReturnType}> {commandName}([FromBody]{commandName} command)
                {{
                    return await _mediator.Send(command);
                }}

                ");
            }

            string finalSource = CommandClassTemplate.Replace("###Commands###", commandSource.ToString());

            SourceText sourceText = SourceText.From(finalSource, Encoding.UTF8);

            context.AddSource("GeneratedCommandController.cs", sourceText);
        }

        private string LookupIRequestGenericType(TypeDeclarationSyntax command)
        {
            foreach (var entry in command.BaseList.Types)
            {
                if (entry is SimpleBaseTypeSyntax basetype)
                {
                    if (basetype.Type is GenericNameSyntax type)
                    {
                        if ((type.Identifier.ValueText == "ICommand" || type.Identifier.ValueText == "IQuery") &&
                                   type.TypeArgumentList.Arguments.Count == 1)
                        {
                            return type.TypeArgumentList.Arguments[0].ToString();
                        }
                    }
                }
            }
            return "";
        }

        private string LoadTemplate(GeneratorExecutionContext context, string filename)
        {
            var addditionalfile = context.AdditionalFiles.FirstOrDefault(x => x.Path.EndsWith(filename));

            if (addditionalfile != null)
            {
                return File.ReadAllText(addditionalfile.Path);
            }
            else
                throw new FileNotFoundException("The file " + filename + " was not found");
        }
    }
}