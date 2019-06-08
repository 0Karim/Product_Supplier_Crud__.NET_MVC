using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product_Supp_DataAccessLayer.Interfaces;
using ProductSupp_Models;
using System.Data.SqlClient;
using System.Configuration;
using Product_Supp_Common;

namespace Product_Supp_DataAccessLayer
{
    public class SupplierRepository : IRepository<SupplierEntity>
    {
        private string _connectionString;
        private int row_affected; 
        private SqlCommand _command;
        private SqlConnection _connection;


        private void GetConnctionString()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Product_Supplier_db"].ConnectionString;
        }

        private void CreateConection()
        {
            if (_connectionString != null)
                _connection = new SqlConnection(_connectionString);
        }

        public SupplierRepository()
        {
            //Intiallize Connection String by call GetConnectionString()
            GetConnctionString();

            //Intiallize Sql Connection object by call CreateConnection()
            CreateConection();
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
                _command.Parameters.AddWithValue("tablename", "Suppliers");
                _command.Parameters.AddWithValue("statment_type", "Delete");

                //Open Connection to database
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();
                

                row_affected = _command.ExecuteNonQuery();

                _connection.Close();

                if (row_affected == 0)
                {
                    throw new Exception("The Supplier Delete Method in Data Access Layer Have a problem in Execution and rows affected = " + row_affected);
                }

                return true;

            }
            catch (Exception ex)
            {
                //Log Exception
                ExceptionHandler.LogException(ex, "Supplier_Data_Access_Layer_Delete");

                //Bubble error to caller
                throw new Exception("Supplier Repository :: Delete Method Error occured.", ex);
            }
            //return true;
        }

        public bool Insert(SupplierEntity entity)
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
                _command.Parameters.AddWithValue("Supplier_Name", entity.SupplierName);
                _command.Parameters.AddWithValue("tablename", "Suppliers");
                _command.Parameters.AddWithValue("statment_type", "Insert");

                //Open Connection to database
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();


                row_affected = _command.ExecuteNonQuery();

                _connection.Close();

                if (row_affected == 0)
                {
                    throw new Exception("The Supplier Insert Method in Data Access Layer Have a problem in Execution and rows affected = " + row_affected);
                    //return false;
                }

                return true;

            }catch(Exception ex)
            {
                //Log the Exception to a txt file
                ExceptionHandler.LogException(ex, "Data_Access_Layer_Insert");

                //Bubble error to the caller 
                throw new Exception("Supplier Repository :: Insert Method Error occured.", ex);
            }
        }

        public List<SupplierEntity> SelectAll()
        {
            row_affected = 0;
            var Supplier_List = new List<SupplierEntity>();

            try
            {

                _command = new SqlCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "sp_CRUD_Operation_Procedure";
                _command.Connection = _connection;

                _command.Parameters.Clear(); // clear old paramters if found

                //here will bind supplier data from entity obj to the stored procedures
                _command.Parameters.AddWithValue("tablename", "Suppliers");
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
                            var entity = new SupplierEntity();
                            entity.ID = reader.GetInt32(0);
                            entity.SupplierName = reader.GetString(1);
                            Supplier_List.Add(entity);
                        }
                    }
                }
                _connection.Close();

                return Supplier_List;
            }
            catch(Exception ex)
            {
                ExceptionHandler.LogException(ex, "Supplier_Data_Access_Layer_SelectAll");

                throw new Exception("Supplier Repository :: Select All Method Error occured.", ex);
            }
        }

        public SupplierEntity SelectByID(int ID)
        {
            row_affected = 0;
            SupplierEntity supplier = null;

            try
            {

                _command = new SqlCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "sp_CRUD_Operation_Procedure";
                _command.Connection = _connection;

                _command.Parameters.Clear(); // clear old paramters if found

                //here will bind supplier data from entity obj to the stored procedures
                _command.Parameters.AddWithValue("ID", ID);
                _command.Parameters.AddWithValue("tablename", "Suppliers");
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
                            var entity = new SupplierEntity();
                            entity = new SupplierEntity();
                            entity.ID = reader.GetInt32(0);
                            entity.SupplierName = reader.GetString(1);

                            supplier = entity;

                            break;
                        }
                    }
                }
                _connection.Close();

                return supplier;
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, "Supplier_Data_Access_Layer_SelectByID");

                throw new Exception("Supplier Repository :: Select Method Error occured.", ex);
            }

        }

        public bool Update(SupplierEntity entity)
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
                _command.Parameters.AddWithValue("Supplier_Name", entity.SupplierName);
                _command.Parameters.AddWithValue("tablename", "Suppliers");
                _command.Parameters.AddWithValue("statment_type", "Update");

                //Open Connection to database
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();


                row_affected = _command.ExecuteNonQuery();

                _connection.Close();

                if (row_affected == 0)
                {
                    throw new Exception("The Supplier Update Method in Data Access Layer Have a problem in Execution and rows affected = " + row_affected);
                    //return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                //Log the Exception to a txt file
                ExceptionHandler.LogException(ex, "Supplier_Data_Access_Layer_Update");

                //Bubble error to the caller 
                throw new Exception("Supplier Repository :: Update Method Error occured.", ex);
            }
        }

        public int Check_Products_Count_With_SupplierID(int supplierID)
        {
            int products_count = 0;
            try
            {
                _command = new SqlCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "sp_Count_Products_with_SupplierID";
                _command.Connection = _connection;

                _command.Parameters.Clear(); // clear old paramters if found

                //here will bind supplier data from entity obj to the stored procedures
                _command.Parameters.AddWithValue("supplierID", supplierID);
                

                //Open Connection to database
                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();

                using (var reader = _command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            products_count = reader.GetInt32(0);
                            //break;
                        }
                    }
                }


                _connection.Close();
                

                return products_count;

            }
            catch(Exception ex)
            {
                ExceptionHandler.LogException(ex, "Supplier_Data_Access_Layer_Count_Supplier");

                throw new Exception("Supplier Repository :: Count Products with same Supplier_ID Method Error occured.", ex);
            }
        }
    }
}
