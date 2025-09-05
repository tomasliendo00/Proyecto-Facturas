using Proyecto.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Data
{
    public class FacturaRepository : IFacturaRepository
    {

        List<Factura> lst = new List<Factura>(); // lista para almacenar los objetos Factura recuperados de la BdD

        public bool Delete(int id) // elimina una factura por su ID
        {
            try
            {
                DataHelper.GetInstance().ExecuteQuery($"DELETE FROM Facturas WHERE nro_factura = {id}");
                //DataHelper.GetInstance().ExecuteSPQuery("SP_DELETE_FACTURA");
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;                      
        }

        public List<Factura> GetAll()
        {
            lst.Clear(); // limpia la lista para evitar duplicados en llamadas sucesivas

            string consulta = "select * from VistaFacturas";

            var dt = DataHelper.GetInstance().ExecuteQuery(consulta); // se conecta a la BdD y envia la consulta
            //var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_GET_ALL_FACTURAS");

            foreach (DataRow row in dt.Rows) // recorre el resultado de la consulta y crea objetos Factura
            {
                var factura = new Factura
                {
                    NroFactura = (int)row["nro_factura"],
                    Fecha = (DateTime)row["fecha"],
                    FormaPago = new FormaPago
                    {
                        IDFormaPago = (int)row["ID Forma Pago"],
                        Nombre = row["Forma Pago"].ToString()
                    },
                    Cliente = new Cliente
                    {
                        IDCliente = (int)row["ID Cliente"],
                        Nombre = row["Nombre"].ToString(),
                        Apellido = row["Apellido"].ToString()
                    },
                    //Detalles = new List<DetalleFactura>()
                };
            }
            return lst;
        }

        public Factura GetByID(int id)
        {
            lst.Clear();

            var dt = DataHelper.GetInstance().ExecuteQuery($"SELECT * FROM Facturas WHERE nro_factura = {id}");
            //var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_GET_FACTURA_BY_ID");

            Factura f = new Factura();
            f.NroFactura = (int)dt.Rows[0]["nro_factura"];
            f.Fecha = (DateTime)dt.Rows[0]["fecha"];
            f.FormaPago.IDFormaPago = (int)dt.Rows[0]["id_forma_pago"];
            f.Cliente.IDCliente = (int)dt.Rows[0]["id_cliente"];
            //f.Detalles = new List<DetalleFactura>();
            lst.Add(f);

            return f;
        }

        public bool Save(Factura factura)
        {
            int rowsAffected = 0;

            rowsAffected = DataHelper.GetInstance().ExecuteQuery($"INSERT INTO Facturas (fecha, id_forma_pago, id_cliente) VALUES " +
                                                                 $"('{factura.Fecha.ToString("yyyy-MM-dd")}', {factura.FormaPago.IDFormaPago}," +
                                                                 $" {factura.Cliente.IDCliente})").Rows.Count;

            //rowsAffected = DataHelper.GetInstance().ExecuteSPQuery("SP_INSERT_FACTURA").Rows.Count;

            if (rowsAffected > 0)
                return true;
            else
                return false;
        }
    }
}
