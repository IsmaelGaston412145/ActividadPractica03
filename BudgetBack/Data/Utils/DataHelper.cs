

using BudgetBack.Data.Utils;
using System.Data;
using System.Data.SqlClient;

namespace BudgetBack.Data
{
    public class DataHelper
    {
        private static DataHelper _instancia;
        private SqlConnection _connection;

        private DataHelper()
        {
            _connection =  new SqlConnection (@"Data Source=PCESCRITORIO;Initial Catalog=db_almacen_uv;Integrated Security=True");
        }

        public static DataHelper GetInstance()
        {
            if (_instancia == null)
                _instancia = new DataHelper();

            return _instancia;
        }

        public DataTable ExecuteSPQuery(string sp, List<ParameterSQL>? parametros)
        {
            DataTable t = new DataTable();
            try
            {              
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var param in parametros)
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                }

                t.Load(cmd.ExecuteReader());
                _connection.Close();
            }
            catch(SqlException)
            {
                t = null;
            }
          
            return t;
        }


        public int ExecuteSPDML(string sp, List<ParameterSQL>? parametros)
        {
            int rows;
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if(parametros != null)
                {
                    foreach (var param in parametros)
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                }

                rows = cmd.ExecuteNonQuery();
                _connection.Close();
            }
            catch (SqlException)
            {
                rows = 0;
            }

            return rows;
        }



        public int ExecuteSPDMLTransact(string sp, List<ParameterSQL>? parametros, SqlConnection cnn,  SqlTransaction transaction, object parameterOut = null)
        {
            //TODO:
            return 0;
        }



        public SqlConnection GetConnection()
        {
            //devolver una connection
            return _connection;
        }


    }
}
