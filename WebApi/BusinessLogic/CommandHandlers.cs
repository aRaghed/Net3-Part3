using MediatR;

using System.Threading;
using System.Threading.Tasks;

using WebApi.CommandAndQueries;

namespace WebApi.BusinessLogic
{
    public class CommandHandlers : IRequestHandler<CreateOrder, string>,
                                   IRequestHandler<AddProduct, string>,
                                   IRequestHandler<RemoveProduct, string>,
                                   IRequestHandler<CancelOrder, string>
    {
        public Task<string> Handle(CreateOrder request, CancellationToken cancellationToken)
        {
            //Interact with Service, DatabaseContext, Stream or something else
            return Task.FromResult("Order created");
        }

        public Task<string> Handle(AddProduct request, CancellationToken cancellationToken)
        {
            //Interact with Service, DatabaseContext, Stream or something else
            return Task.FromResult("Product added");
        }

        public Task<string> Handle(RemoveProduct request, CancellationToken cancellationToken)
        {
            //Interact with Service, DatabaseContext, Stream or something else
            return Task.FromResult("Product removed");
        }

        public Task<string> Handle(CancelOrder request, CancellationToken cancellationToken)
        {
            //Interact with Service, DatabaseContext, Stream or something else
            return Task.FromResult("Order cancelled");
        }
    }
}