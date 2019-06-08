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
    public class ProductBusinessLogic
    {
        //Define object of type Product Data Access Layer
        private ProductRepository _repository;


        public ProductBusinessLogic() { _repository = new ProductRepository(); }


        public bool InsertProduct(ProductEntity entity)
        {
            try
            {
                return _repository.Insert(entity);
            }catch(Exception ex)
            {
                //Log Exception Error
                ExceptionHandler.LogException(ex, "Product_Bussiness_Logic_Layer_Insert");

                throw new Exception("Business Logic :: Product Business :: Insert Function :: Error in Execution", ex);
            }
        }
        public bool UpdateProduct(ProductEntity entity)
        {
            try
            {
                return _repository.Update(entity);
            }
            catch (Exception ex)
            {
                //Log Exception Error
                ExceptionHandler.LogException(ex, "Product_Bussiness_Logic_Layer_Update");

                throw new Exception("Business Logic :: Product Business :: Update Function :: Error in Execution", ex);
            }
        }
        public bool DeleteProductByID(int productID)
        {
            try
            {
                return _repository.DeleteByID(productID);
            }
            catch (Exception ex)
            {
                //Log Exception Error
                ExceptionHandler.LogException(ex, "Product_Bussiness_Logic_Layer_Delete");

                throw new Exception("Business Logic :: Product Business :: Delete Function :: Error in Execution", ex);
            }
        }
        public ProductEntity SelectProductByID(int productID)
        {
            ProductEntity entity;
            try
            {
                entity = _repository.SelectByID(productID);

                return entity;
            }
            catch (Exception ex)
            {
                //Log Exception Error
                ExceptionHandler.LogException(ex, "Product_Bussiness_Logic_Layer_SelectByID");

                throw new Exception("Business Logic :: Product Business :: SelectbyID Function :: Error in Execution", ex);
            }
        }
        public List<ProductEntity> SelectAllProducts()
        {
            var allProduacts = new List<ProductEntity>();
            try
            {
                allProduacts = _repository.SelectAll();

                return allProduacts;
            }catch(Exception ex)
            {
                //Log Exception Error
                ExceptionHandler.LogException(ex, "Product_Bussiness_Logic_Layer_SelectAll");


                throw new Exception("Business Logic :: Product Business :: SelectAll Function :: Error in Execution", ex);
            }
        }

        public List<SupplierEntity> LoadAllSuppliers()
        {
            var supplier = new SupplierRepository();
            try
            {
                return supplier.SelectAll();
            }catch(Exception ex)
            {
                //Log Exception Error
                ExceptionHandler.LogException(ex, "Product_Bussiness_Logic_Layer_LoadAllSuppliers");


                throw new Exception("Business Logic :: Product Business :: Select All Supplier Function :: Error in Execution", ex);
            }
        }
    }
}
