using Proyecto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Services
{
    public interface IFacturaService
    {
        List<DetalleFactura> ConsultarDetalle();

        bool RegistrarFactura(Factura factura);

        bool EliminarFactura(int id);
        
        bool ActualizarFactura(Factura factura);
    }
}
