using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductSupp_Models;
using Product_Supp_DataAccessLayer.Interfaces;
using System.Data.SqlClient;
using Product_Supp_Common;

namespace Product_Supp_DataAccessLayer
{
    public class ProductRepository : IRepository<ProductEntity>
    {
        private string _connectionString;
        private int row_affected;
        private SqlCommand _command;
        private SqlConnection _connection;

        public ProductRepository()
        {
            //Intiallize Connection String by call GetConnectionString()
            GetConnctionString();

            //Intiallize Sql Connection object by call CreateConnection()
            CreateConection();
        }


        private void GetConnctionString()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Product_Supplier_db"].ConnectionString;
        }

        private void CreateConection()
        {
            if (_connectionString != null)
                _connection = new SqlConnection(_connectionString);
        }

        public bool DeleteByID(int ID)
        {
            row_affected = 0;
            try
            {
                _command = new SqlCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "sp_CRUD_Operation_Procedure";
                _command.Connection = _connection;

                _command.Parameters.Clear(); // clear old paramters if found

                //here will bind supplier data from entity obj to the stored procedures
                _command.Parameters.AddWithValue("ID", ID);
                _command.Parameters.AddWithValue("tablename", "Products");
                _command.Parameters.AddWithValue("statment_type", "Delete");

                //Open Connection to database
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();

                row_affected = _command.ExecuteNonQuery();

                _connection.Close();

                if (row_affected == 0)
                {
                    throw new Exception("The Delete Method in Product Data Access Layer Have a problem in Execution and rows affected = " + row_affected);
                    //return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                //Log Exception
                ExceptionHandler.LogException(ex, "Product_Data_Access_Layer_Delete");


                throw new Exception("Product Repository :: Delete Method Error occured.", ex);
            }
        }

        public bool Insert(ProductEntity entity)
        {
            row_affected = 0;
            try
            {
                _command = new SqlCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "sp_CRUD_Operation_Procedure";
                _command.Connection = _connection;

                _command.Parameters.Clear(); // clear old paramters if found

                //here will bind supplier data from entity obj to the stored procedures
                _command.Parameters.AddWithValue("Product_Name", entity.ProductName);
                _command.Parameters.AddWithValue("Product_QunatityPerUnit", entity.QuantityPerUnit);
                _command.Parameters.AddWithValue("Product_ReorderLevel", entity.ReorderLevel);
                _command.Parameters.AddWithValue("Supplier_ID", entity.SupplierID);
                _command.Parameters.AddWithValue("Product_UnitPrice", entity.UnitPrice);
                _command.Parameters.AddWithValue("Product_UnitInStock", entity.UnitsInStock);
                _command.Parameters.AddWithValue("Product_UnitsOnOrder", entity.UnitsOnOrder);


                _command.Parameters.AddWithValue("tablename", "Products");
                _command.Parameters.AddWithValue("statment_type", "Insert");

                //Open Connection to database
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();

                row_affected = _command.ExecuteNonQuery();

                _connection.Close();

                if (row_affected == 0)
                {
                    throw new Exception("The Product Insert Method in Data Access Layer Have a problem in Execution and rows affected = " + row_affected);
                    //return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                //Log the Exception to a txt file
                ExceptionHandler.LogException(ex, "Product_Data_Access_Layer_Insert");

                throw new Exception("Product Repository :: Insert Method Error occured.", ex);
            }
        }

        public List<ProductEntity> SelectAll()
        {
            row_affected = 0;
            var Product_List = new List<ProductEntity>();

            try
            {

                _command = new SqlCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "sp_CRUD_Operation_Procedure";
                _command.Connection = _connection;

                _command.Parameters.Clear(); // clear old paramters if found

                //here will bind supplier data from entity obj to the stored procedures
                _command.Parameters.AddWithValue("tablename", "Products");
                _command.Parameters.AddWithValue("statment_type", "select_all");

                //Open Connection to database
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();

                using (var reader = _command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var entity = new ProductEntity();
                            entity.ID = reader.GetInt32(0);
                            entity.ProductName = reader.GetString(1);
                            entity.QuantityPerUnit = reader.GetInt32(2);
                            entity.ReorderLevel = reader.GetInt32(3);
                            entity.SupplierID = reader.GetInt32(4);
                            entity.UnitPrice = reader.GetInt32(5);
                            entity.UnitsInStock = reader.GetInt32(6);
                            entity.UnitsOnOrder = reader.GetInt32(7);

                            Product_List.Add(entity);
                        }
                    }
                }
                _connection.Close();

                return Product_List;
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, "Product_Data_Access_Layer_SelectAll");

                throw new Exception("Product Repository :: Select All Method Error occured.", ex);
            }
        }

        public ProductEntity SelectByID(int ID)
        {
            row_affected = 0;
            ProductEntity product = null;

            try
            {

                _command = new SqlCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "sp_CRUD_Operation_Procedure";
                _command.Connection = _connection;

                _command.Parameters.Clear(); // clear old paramters if found

                //here will bind supplier data from entity obj to the stored procedures
                _command.Parameters.AddWithValue("ID", ID);
                _command.Parameters.AddWithValue("tablename", "Products");
                _command.Parameters.AddWithValue("statment_type", "select");

                //Open Connection to database
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();

                using (var reader = _command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var entity = new ProductEntity();
                            entity.ID = reader.GetInt32(0);
                            entity.ProductName = reader.GetString(1);
                            entity.QuantityPerUnit = reader.GetInt32(2);
                            entity.ReorderLevel = reader.GetInt32(3);
                            entity.SupplierID = reader.GetInt32(4);
                            entity.UnitPrice = reader.GetInt32(5);
                            entity.UnitsInStock = reader.GetInt32(6);
                            entity.UnitsOnOrder = reader.GetInt32(7);

                            product = entity;

                            break;
                        }
                    }
                }
                _connection.Close();

                return product;
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, "Product_Data_Access_Layer_SelectByID");

                throw new Exception("Product Repository :: Select Method Error occured.", ex);
            }
        }

        public bool Update(ProductEntity entity)
        {
            row_affected = 0;
            try
            {
                _command = new SqlCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "sp_CRUD_Operation_Procedure";
                _command.Connection = _connection;

                _command.Parameters.Clear(); // clear old paramters if found

                //here will bind supplier data from entity obj to the stored procedures
                _command.Parameters.AddWithValue("ID", entity.ID);
                _command.Parameters.AddWithValue("Product_Name", entity.ProductName);
                _command.Parameters.AddWithValue("Product_QunatityPerUnit", entity.QuantityPerUnit);
                _command.Parameters.AddWithValue("Product_ReorderLevel", entity.ReorderLevel);
                _command.Parameters.AddWithValue("Supplier_ID", entity.SupplierID);
                _command.Parameters.AddWithValue("Product_UnitPrice", entity.UnitPrice);
                _command.Parameters.AddWithValue("Product_UnitInStock", entity.UnitsInStock);
                _command.Parameters.AddWithValue("Product_UnitsOnOrder", entity.UnitsOnOrder);

                _command.Parameters.AddWithValue("tablename", "Products");
                _command.Parameters.AddWithValue("statment_type", "Update");

                //Open Connection to database
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();

                row_affected = _command.ExecuteNonQuery();

                _connection.Close();

                if (row_affected == 0)
                {
                    throw new Exception("The Product Update Method in Data Access Layer Have a problem in Execution and rows affected = " + row_affected);
                }

                return true;

            }
            catch (Exception ex)
            {
                //Log the Exception to a txt file
                ExceptionHandler.LogException(ex, "Product_Data_Access_Layer_Update");

                //Bubble error to the caller

                throw new Exception("Product Repository :: Update Method Error occured.", ex);
            }
        }
    }
}
