using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Domain
{
    public class FormaPago
    {
        public int IDFormaPago { get; set; }
        public string Nombre { get; set; }
        //public override string ToString()
        //{
        //    return $"ID: {IDFormaPago}, Forma de pago: {Nombre}";
        //}

        public FormaPago()
        {
            IDFormaPago = 0;
            Nombre = string.Empty;
        }
        public FormaPago(int id, string nombre)
        {
            this.IDFormaPago = id;
            this.Nombre = nombre;
        }
    }
}
