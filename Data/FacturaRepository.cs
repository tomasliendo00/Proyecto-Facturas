using Microsoft.Data.SqlClient;
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
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@nro_factura", id)
            };            

            try
            {
                int rows = DataHelper.GetInstance().ExecuteSPNonQuery("SP_DELETE_FACTURA", parameters);
                
                if(rows > 0)
                {
                    Console.WriteLine("Factura eliminada correctamente.");
                    return true;
                }
                else
                {
                    Console.WriteLine("No se encontró la factura con el ID especificado.");
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                // gestionar error
                Console.WriteLine("Error al eliminar la factura: " + ex.Message);
                return false;
            }            
        }

        public List<Factura> GetAll()
        {
            lst.Clear(); // limpia la lista para evitar duplicados en llamadas sucesivas

            //string consulta = "select * from VistaFacturas";

            //var dt = DataHelper.GetInstance().ExecuteQuery(consulta); // se conecta a la BdD y envia la consulta
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_GET_ALL_FACTURAS2");

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
                    }
                    //Detalles = new List<DetalleFactura>()
                    
                };
                lst.Add(factura);
            }
            return lst;
        }

        public Factura GetByID(int id)
        {
            lst.Clear();

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", id)
            };

            //var dt = DataHelper.GetInstance().ExecuteQuery($"SELECT * FROM Facturas WHERE nro_factura = {id}");
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_GET_BY_ID", parameters);

            Factura f = new Factura();
            f.NroFactura = (int)dt.Rows[0]["nro_factura"];
            f.Fecha = (DateTime)dt.Rows[0]["fecha"];
            f.FormaPago = new FormaPago();
                f.FormaPago.IDFormaPago = (int)dt.Rows[0]["ID Forma Pago"];
                f.FormaPago.Nombre = dt.Rows[0]["Forma Pago"].ToString();
            f.Cliente = new Cliente();
                f.Cliente.IDCliente = (int)dt.Rows[0]["ID Cliente"];
                f.Cliente.Nombre = dt.Rows[0]["nombre"].ToString();
                f.Cliente.Apellido = dt.Rows[0]["apellido"].ToString();
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
