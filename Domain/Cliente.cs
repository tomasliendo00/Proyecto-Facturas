using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Domain
{
    public class Cliente
    {
        public int IDCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public override string ToString()
        {
            return $"ID: {IDCliente} , Cliente:  {Apellido}, {Nombre}";
        }

        public Cliente()
        {
            IDCliente = 0;
            Nombre = Apellido = string.Empty;
        }

        public Cliente(int id, string nombre, string apellido)
        {
            this.IDCliente = id;
            this.Nombre = nombre;
            this.Apellido = apellido;
        }
    }
}
