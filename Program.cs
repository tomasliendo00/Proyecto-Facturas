// See https://aka.ms/new-console-template for more information
using Proyecto.Data;

Console.WriteLine("Hello, World!");

var repo = new FacturaRepository();
var facturas = repo.GetAll();

foreach(var f in facturas)
{
    Console.WriteLine(f);
}
