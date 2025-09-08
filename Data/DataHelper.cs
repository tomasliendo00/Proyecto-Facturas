using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Data
{
    public class DataHelper
    {
        private static DataHelper _instance;
        private SqlConnection _connection;

        private DataHelper()
        {
            _connection = new SqlConnection(Properties.Resources.CadenaConexion);
        }

        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }

        public DataTable ExecuteQuery(string query)     // para mandar consultas
        {
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(query, _connection);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException ex)
            {
                // gestionar error
                Console.WriteLine("Error en la consulta SQL: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return dt;
        }

        public DataTable ExecuteSPQuery(string sp)      // para mandar procedimientos almacenados
        {
            return ExecuteSPQuery(sp, null);
        }

        public DataTable ExecuteSPQuery(string sp, List<SqlParameter> parameters)
        {
            DataTable dt = new DataTable();

            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;
                // Agregar los parámetros al comando
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }
                }
                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException ex)
            {
                // gestionar error
                Console.WriteLine("Error en la consulta SQL: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return dt;
        }

        public int ExecuteSPNonQuery(string sp, List<SqlParameter> parameters)
        {
            int rowsAffected = 0;
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;

                if (parameters != null && parameters.Count > 0)
                    cmd.Parameters.AddRange(parameters.ToArray());

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                // gestionar error
                Console.WriteLine("Error en la consulta SQL: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return rowsAffected;
        }
    }
}
