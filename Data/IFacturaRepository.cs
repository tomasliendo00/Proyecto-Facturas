using Proyecto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Data
{
    public interface IFacturaRepository
    {
        List<Factura> GetAll();

        Factura GetByID(int id);

        bool Save(Factura factura);

        bool Delete(int id);
        List<DetalleFactura> ObtenerDetalles();
        bool CrearFactura(Factura factura);
    }
}
