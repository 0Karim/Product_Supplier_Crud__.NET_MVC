using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductSupp_Models;
using Product_Supp_DataAccessLayer;
using Product_Supp_Common;



namespace Product_Supp_BusiniessLogicLayer
{
    public class SupplierBusinessLogic
    {
        //Define object of type Supplier Data Access Layer
        private SupplierRepository _repository;

        public SupplierBusinessLogic() { _repository = new SupplierRepository(); }

        public bool InsertSupplier(SupplierEntity entity)
        {
            try
            {
                //bool inserted = _repository.Insert(entity);

                return _repository.Insert(entity);

            }catch(Exception ex)
            {
                //Log Exception Error
                ExceptionHandler.LogException(ex, "Supplier_Bussiness_Logic_Layer_Insert");

                throw new Exception("Business Logic :: Supplier Business :: Insert Function :: Error in Execution", ex);
            }
        }

        public bool UpdateSupplier(SupplierEntity entity)
        {
            try
            {
                return _repository.Update(entity);
            }catch(Exception ex)
            {
                //Log Exception Error
                ExceptionHandler.LogException(ex, "Supplier_Bussiness_Logic_Layer_Update");

                throw new Exception("Business Logic :: Supplier Business :: Update Function :: Error in Execution", ex);
            }
        }

        public bool DeleteSupplierByID(int supplierID)
        {
            int products_count;
            bool deleted = false;
            try
            {
                //here will check first if there is a products with the same supplier id to not make error in refrential integratiye constraint
                products_count = _repository.Check_Products_Count_With_SupplierID(supplierID);

                if(products_count == 0)
                {
                    deleted = _repository.DeleteByID(supplierID);
                }
                return deleted;
            }catch(Exception ex)
            {
                //Log Exception Error
                ExceptionHandler.LogException(ex, "Supplier_Bussiness_Logic_Layer_Delete");

                throw new Exception("Business Logic :: Supplier Business :: Delete Function :: Error in Execution", ex);
            }
        }

        public SupplierEntity SelectSupplierByID(int supplierID)
        {
            SupplierEntity supplierEntity;
            try
            {
                supplierEntity = _repository.SelectByID(supplierID);

                return supplierEntity;
            }catch(Exception ex)
            {
                //Log Exception Error
                ExceptionHandler.LogException(ex, "Supplier_Bussiness_Logic_Layer_SelectByID");

                throw new Exception("Business Logic :: Supplier Business :: SelectByID Function :: Error in Execution", ex);
            }
        }

        public List<SupplierEntity> SelectAllSuppliers()
        {
            var allSuppliers = new List<SupplierEntity>();
            try
            {
                allSuppliers = _repository.SelectAll();

                return allSuppliers;
            }catch(Exception ex)
            {
                //Log Exception Error
                ExceptionHandler.LogException(ex, "Supplier_Bussiness_Logic_Layer_SelectAll");

                throw new Exception("Business Logic :: Supplier Business :: SelectAll Function :: Error in Execution", ex);
            }
        }
    }
}
