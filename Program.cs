// See https://aka.ms/new-console-template for more information
using Proyecto.Data;

Console.WriteLine("Hello, World!");

var repo = new FacturaRepository();
var facturas = repo.GetAll();

foreach(var f in facturas)
{
    Console.WriteLine(f);
}

var id = repo.GetByID(2);

Console.WriteLine(id);

repo.Delete(8);
