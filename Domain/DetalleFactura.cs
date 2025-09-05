using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Domain
{
    public class DetalleFactura
    {
        public int IDDetalle { get; set; } 
        public Articulo Articulo { get; set; }
        public int Cantidad { get; set; }

        public override string ToString()
        {
            return $"ID: {IDDetalle}, Articulo: [{Articulo}], Cantidad: {Cantidad}";
        }

        public DetalleFactura()
        {
            IDDetalle = Cantidad = 0;
            Articulo = new Articulo();
        }

        public DetalleFactura(int id, Factura factura, Articulo articulo, int cantidad)
        {
            this.IDDetalle = id;
            this.Articulo = articulo;
            this.Cantidad = cantidad;
        }
    }
}
