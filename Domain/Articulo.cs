using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Domain
{
    public class Articulo
    {
        public int IDArticulo { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }

        public override string ToString()
        {
            return $"ID: {IDArticulo}, Articulo: {Nombre}, Precio: {Precio}";
        }

        public Articulo()
        {
            IDArticulo = Precio = 0;
            Nombre = string.Empty;
        }

        public Articulo(int id, string nombre, int precio)
        {
            this.IDArticulo = id;
            this.Nombre = nombre;
            this.Precio = precio;
        }
    }
}
