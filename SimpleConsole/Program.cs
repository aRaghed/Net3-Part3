using System.Threading.Tasks;

using static System.Console;

WriteLine("Hello world");
await Task.Delay(1000);
Testa();
return 0;

void Testa()
{
    WriteLine("Testar");
}