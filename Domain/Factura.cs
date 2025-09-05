using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Domain
{
    public class Factura
    {
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public FormaPago FormaPago { get; set; }
        public Cliente Cliente { get; set; }
        //public List<DetalleFactura> Detalles { get; set; }

        public override string ToString()
        {
            return $"Factura nro: {NroFactura}, Fecha: {Fecha}, Forma de Pago: {FormaPago.Nombre}, Cliente: {Cliente.Apellido}, {Cliente.Nombre}";
        }

        public Factura()
        {
            NroFactura = 0;
            Fecha = DateTime.Now;
            FormaPago = new FormaPago();
            Cliente = new Cliente();
            //Detalles = new List<DetalleFactura>();
        }

        public Factura(int nroFactura, DateTime fecha, FormaPago formaPago, Cliente cliente/*, List<DetalleFactura> detalles*/)
        {
            this.NroFactura = nroFactura;
            this.Fecha = fecha;
            this.FormaPago = formaPago;
            this.Cliente = cliente;
            //this.Detalles = detalles;
        }
    }
}
