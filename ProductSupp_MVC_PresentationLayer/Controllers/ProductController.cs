using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductSupp_Models;
using Product_Supp_BusiniessLogicLayer;
using ProductSupp_MVC_PresentationLayer.ViewModels;
using Product_Supp_Common;

namespace ProductSupp_MVC_PresentationLayer.Controllers
{
    public class ProductController : Controller
    {
        ProductBusinessLogic product;

        public ProductController() { product = new ProductBusinessLogic(); }

        // GET: Product
        public ActionResult Index()
        {
            try
            {
                var product_List = ListAllProducts();

                return View(product_List);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, "Product_MVC_Index_action_method");

                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        public ActionResult AddProduct()
        {
            var supplier_list = product.LoadAllSuppliers();
            var Supplier_Product_ViewModel = new ViewModels.Suppliers_Product
            {
                Suppliers = supplier_list
            };
            return View(Supplier_Product_ViewModel);
        }

        [HttpPost]
        public ActionResult AddProduct(Suppliers_Product viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                InsertProduct(viewModel.Product);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, "Product_MVC_AddProduct_action_method");

                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        public ActionResult Edit(int? productID)
        {
            try
            {
                if(productID != null)
                {
                    var product_Edited = SelectProductByID(Convert.ToInt32(productID));

                    if(product_Edited != null)
                    {
                        var viewModel = new Suppliers_Product()
                        {
                            Product = product_Edited,
                            Suppliers = product.LoadAllSuppliers()
                        };

                        return View(viewModel);
                    }
                    else
                    {
                        throw new Exception("There is a problem when loading this Produt try again later");
                    }
                   
                }
                else
                {
                    throw new Exception("There is a Problem there is no such Product");
                }
            }catch(Exception ex)
            {
                ExceptionHandler.LogException(ex, "Product_MVC_Edit_action_method");

                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Suppliers_Product viewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                UpdateProduct(viewModel.Product);

                return RedirectToAction("Index");

            }catch(Exception ex)
            {
                ExceptionHandler.LogException(ex, "Product_MVC_Edit_action_method");

                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        public ActionResult Delete(int? productID)
        {
            try
            {
                if (productID != null)
                {
                    bool deleted = DeleteProduct(Convert.ToInt32(productID));
                    if (deleted == true)
                    {
                        return Json("deleted_Ok", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("not_deleted", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    throw new Exception("There is a Problem there is no such Supplier");
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, "Product_MVC_Delete_action_method");

                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }


        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Search_product(string searchBy, string searchTerm)
        {

            try
            {
                var productsList = ListAllProducts();

                if(searchBy == "Product Name")
                {
                    return Json(productsList.Where(p => p.Product.ProductName.ToLower().Contains(searchTerm.ToLower()) || searchTerm == null).ToList() , JsonRequestBehavior.AllowGet);
                }else if (searchBy == "Supplier Name")
                {
                    return Json(productsList.Where(s => s.Supplier.SupplierName.ToLower().Contains(searchTerm.ToLower()) || searchTerm == null).ToList() , JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, "Product_MVC_Search_action_method");

                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }

            return Json("" , JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadProducts()
        {
            try
            {
                return Json(ListAllProducts(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, "Product_MVC_LoadProducts_action_method");

                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }


        #region Private CRUD Operations

        private void InsertProduct(ProductEntity entity)
        {
            try
            {
                bool inserted = product.InsertProduct(entity);

            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, "Product_Presentation_Layer_Insert");
            }
        }

        private void UpdateProduct(ProductEntity entity)
        {
            try
            {
                bool updated = product.UpdateProduct(entity);

            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, "Product_Presentation_Layer_UpdateFunc");
            }
        }

        private bool DeleteProduct(int productID)
        {
            bool deleted = false;
            try
            {
                deleted = product.DeleteProductByID(productID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, "Product_Presentation_Layer_Delete_Func");
            }

            return deleted;
        }

        private ProductEntity SelectProductByID(int productID)
        {
            try
            {
                return product.SelectProductByID(productID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, "Product_Presentation_Layer_SelectprodByID");
            }
            return null;
        }

        private List<Products_List_ViewModel> ListAllProducts()
        {
            var products_list_ViewModel = new List<Products_List_ViewModel>();
            try
            {
                List<ProductEntity> products_list = product.SelectAllProducts();
                List<SupplierEntity> suppliers = product.LoadAllSuppliers();

                foreach(ProductEntity product in products_list)
                {
                    var entity = new Products_List_ViewModel()
                    {
                        Product = product,
                        Supplier = suppliers.Where(s => s.ID == product.SupplierID).SingleOrDefault()
                    };
                    products_list_ViewModel.Add(entity);
                }

                return products_list_ViewModel;
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex, "Product_Presentation_Layer_SelectProdByID");
            }
            return null;
        }
        #endregion
    }
}