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

        public bool CrearFactura(Factura factura)
        {
            throw new NotImplementedException();
        }

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

            
            var result = new List<Factura>();

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_GET_ALL_FACTURAS2");

            foreach (DataRow row in dt.Rows)
            {
                result.Add(new Factura
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
                });
            }
            return result;
        }

        public Factura? GetByID(int id)
        {
            lst.Clear();

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", id)
            };
                       
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_GET_BY_ID", parameters);

            if(dt.Rows.Count == 0)
                return null;

            var r = dt.Rows[0];
            return new Factura
            {
                NroFactura = (int)r["nro_factura"],
                Fecha = (DateTime)r["fecha"],
                FormaPago = new FormaPago
                {
                    IDFormaPago = (int)r["ID Forma Pago"],
                    Nombre = r["Forma Pago"].ToString()
                },
                Cliente = new Cliente
                {
                    IDCliente = (int)r["ID Cliente"],
                    Nombre = r["nombre"].ToString(),
                    Apellido = r["apellido"].ToString()
                }
            };
        }

        public List<DetalleFactura> ObtenerDetalles()
        {
            throw new NotImplementedException();
        }

        public bool Save(Factura factura)
        {
            using (var cn = DataHelper.GetInstance().GetConnection())
            {
                cn.Open();
                using (var tx = cn.BeginTransaction())
                {
                    try
                    {
                        var pCab = new List<SqlParameter>
                        {
                            new SqlParameter("@fecha", factura.Fecha),
                            new SqlParameter("@id_forma_pago", factura.FormaPago.IDFormaPago),
                            new SqlParameter("@id_cliente", factura.Cliente.IDCliente),
                            new SqlParameter("@nro_generado", SqlDbType.Int) { Direction = ParameterDirection.Output }
                        };

                        DataHelper.GetInstance().ExecuteSPNonQuery("SP_SAVE_FACTURA", pCab, cn, tx); // corregir esto

                        int nroGenerado = (int)pCab.Last().Value; // obtiene el nro de factura generado por la BdD

                        factura.NroFactura = nroGenerado; // asigna el nro de factura al objeto factura

                        // Insertar los detalles de la factura

                        foreach (var d in factura.ListaDetalles)
                        {
                            var pDet = new List<SqlParameter>
                            {
                                new SqlParameter("@nro_factura", nroGenerado),
                                new SqlParameter("@id_producto", d.Articulo.IDArticulo),
                                new SqlParameter("@cantidad", d.Cantidad),
                                new SqlParameter("@precio_unitario", d.Articulo.Precio)
                            };
                            DataHelper.GetInstance().ExecuteSPNonQuery("SP_INSERT_DETALLE_FACTURA", pDet, cn, tx);
                        }

                        tx.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
