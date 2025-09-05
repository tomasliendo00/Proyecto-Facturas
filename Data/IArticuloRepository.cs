using Proyecto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Data
{
    public interface IArticuloRepository
    {
        List<Articulo> GetAll();

        Articulo GetByID(int id);

        bool Save(Articulo articulo);

        bool Delete(int id);
    }
}
