using BudgetBack.Data.Utils;
using BudgetBack.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace BudgetBack.Data
{
    public class ProductRepositoryADO : IProductRepository
    {
        private SqlConnection _connection;

        public ProductRepositoryADO()
        {
            _connection = new SqlConnection(@"Data Source=172.16.10.196;Initial Catalog=db_almacen;User ID=alumno1w1;Password=alumno1w1;Trust Server Certificate=True");
        }


        public bool Delete(int id)
        {
            var parameters = new List<ParameterSQL>();
            parameters.Add(new ParameterSQL("@codigo", id));
            int rows = DataHelper.GetInstance().ExecuteSPDML("SP_REGISTRAR_BAJA_PRODUCTO", parameters);
            return rows == 1;
        }

        public List<Product> GetAll()
        {
            List<Product> lst = new List<Product>();
            var helper = DataHelper.GetInstance();
            var t = helper.ExecuteSPQuery("SP_RECUPERAR_PRODUCTOS", null);
            foreach (DataRow row in t.Rows)
            {
                int id = Convert.ToInt32(row["codigo"]);
                string nombre = row["n_producto"].ToString();
                int stock = Convert.ToInt32(row["stock"]);
                bool activo = Convert.ToBoolean(row["esta_activo"]);

                Product oProduct = new Product()
                {
                    Codigo = id,
                    Nombre = nombre,
                    Stock = stock,
                    Activo = activo
                };
                lst.Add(oProduct);
            }
            return lst;
        }

        public Product GetById(int id)
        {
            var parameters = new List<ParameterSQL>();
            parameters.Add(new ParameterSQL("@codigo", id));
            DataTable t = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_PRODUCTO_POR_CODIGO", parameters);
            
            if(t != null && t.Rows.Count == 1)
            {
                DataRow row = t.Rows[0];
                int codigo = Convert.ToInt32(row["codigo"]);
                string nombre = row["n_producto"].ToString();
                int stock = Convert.ToInt32(row["stock"]);
                bool activo = Convert.ToBoolean(row["esta_activo"]);

                Product oProduct = new Product()
                {
                    Codigo = codigo,
                    Nombre = nombre,
                    Stock = stock,
                    Activo = activo
                };
                return oProduct;

            }
            return null;
        }

        public bool Save(Product oProduct)
        {
            bool result = true;
            string query = "SP_GUARDAR_PRODUCTO";

            try
            {
                if (oProduct != null)
                {
                    _connection.Open();
                    var cmd = new SqlCommand(query, _connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo", oProduct.Codigo);
                    cmd.Parameters.AddWithValue("@nombre", oProduct.Nombre);
                    cmd.Parameters.AddWithValue("@stock", oProduct.Stock);
                    result = cmd.ExecuteNonQuery() == 1; //ExecuteNonQuery: cantidad de filas afectadas!
                }
            }
            catch (SqlException sqlEx)
            {
                result = false;
            }
            finally
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return result;
        }
    }
}
