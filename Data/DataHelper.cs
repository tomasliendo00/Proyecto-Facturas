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
        public static DataHelper _instance;
        public SqlConnection _connection;

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

        public SqlConnection GetConnection()
        {
            return _connection;
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

        public int ExecuteSPNonQuery(string sp, List<SqlParameter> parameters, SqlConnection cn, SqlTransaction tx)
        {
            int rowsAffected = 0;
            SqlCommand cmd = null;
            try
            {                
                cmd = new SqlCommand(sp, cn, tx);
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null && parameters.Count > 0)
                    cmd.Parameters.AddRange(parameters.ToArray());

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {                
                Console.WriteLine("Error en la consulta SQL (Tx): " + ex.Message);
                rowsAffected = -1;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }
            return rowsAffected;
        }

    }
}
