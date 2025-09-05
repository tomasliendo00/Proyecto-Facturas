using Proyecto.Data;
using Proyecto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Services
{
    public class FacturaService
    {
        private IFacturaRepository _repository;

        public FacturaService()
        {
            _repository = new FacturaRepository();
        }

        public List<Factura> GetFacturas()
        {
            return _repository.GetAll();
        }

        public Factura GetFacturaByID(int id)
        {
            return _repository.GetByID(id);
        }

        public bool SaveFactura(Factura factura)
        {
            return _repository.Save(factura);
        }
    }
}
