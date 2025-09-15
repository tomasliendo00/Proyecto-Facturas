using Proyecto.Data;
using Proyecto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Services
{
    public class FacturaService : IFacturaService
    {
        private IFacturaRepository _facturaRepo;

        public FacturaService(IFacturaRepository facturaRepo)
        {
            _facturaRepo = facturaRepo;
        }

        public List<DetalleFactura> ConsultarDetalle()
        {
            return _facturaRepo.ObtenerDetalles();
        }

        public bool RegistrarFactura(Factura factura)
        {
            return _facturaRepo.CrearFactura(factura); 
        }

        public bool EliminarFactura(int id)
        {
            if(id <= 0)
                throw new ArgumentException("El ID debe ser un número positivo.");
            return _facturaRepo.Delete(id);
        }
        public bool ActualizarFactura(Factura factura)
        {
            throw new NotImplementedException();
        }
    }
}
