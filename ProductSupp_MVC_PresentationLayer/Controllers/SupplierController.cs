using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductSupp_Models;
using Product_Supp_BusiniessLogicLayer;
using Product_Supp_Common;

namespace ProductSupp_MVC_PresentationLayer.Controllers
{
    public class SupplierController : Controller
    {

        SupplierBusinessLogic supplier;
        public SupplierController() { supplier = new SupplierBusinessLogic(); }

        // GET: Supplier
        public ActionResult Index()
        {
            return RedirectToAction("Add_Edit_Supplier");
        }

        public ActionResult Add_Edit_Supplier()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Edit_Supplier(SupplierEntity entity)
        {
            if(!ModelState.IsValid)
            {
                return View(entity);
            }

            try
            {
                if(entity.ID == 0)
                {
                    InsertSupplier(entity);
                }
                else
                {
                    UpdateSupplier(entity);
                }

                return RedirectToAction("Add_Edit_Supplier");

            }catch(Exception ex)
            {
                ExceptionHandler.LogException(ex, "Supplier_MVC_Add_Edit_action_method");

                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }


        public ActionResult LoadSuppliers()
        {
            try
            {
                var list = ListAllSuppliers();
                if(list != null)
                {
                    return Json(list , JsonRequestBehavior.AllowGet);
                }
                else
                {
                    throw new Exception("There is a problem when loading list try again later");
                }

            } catch(Exception ex)
            {
                ExceptionHandler.LogException(ex, "Supplier_MVC_LoadSuppliers_action_method");

                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        public ActionResult Delete(int? supplierID)
        {
            try
            {
                if(supplierID != null)
                {
                    bool deleted = DeleteSupplier(Convert.ToInt32(supplierID));
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
            catch(Exception ex)
            {
                ExceptionHandler.LogException(ex, "Supplier_MVC_Delete_action_method");

                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        #region Private CRUD Operations

        private void InsertSupplier(SupplierEntity entity)
        {
            try
            {
                bool inserted = supplier.InsertSupplier(entity);

            }catch(Exception ex)
            {
                ExceptionHandler.LogException(ex, "Supplier_Presentation_Layer_Insert");
            }
        }

        private void UpdateSupplier(SupplierEntity entity)
        {
            try
            {
                bool updated = supplier.UpdateSupplier(entity);

            }catch(Exception ex)
            {
                ExceptionHandler.LogException(ex, "Supplier_Presentation_Layer_UpdateFunc");
            }
        }

        private bool DeleteSupplier(int supplierID)
        {
            bool deleted = false;
            try
            {
                 deleted = supplier.DeleteSupplierByID(supplierID); // if value true => it mean that is deleted ,, if false it not deleted
            }catch(Exception ex)
            {
                ExceptionHandler.LogException(ex, "Supplier_Presentation_Layer_Delete_Func");
            }
            return deleted;
        }

        private SupplierEntity SelectSupplierByID(int supplierID)
        {
            try
            {
                return supplier.SelectSupplierByID(supplierID);
            }catch(Exception ex)
            {
                ExceptionHandler.LogException(ex, "Supplier_Presentation_Layer_SelectSupByID");
            }
            return null;
        }

        private List<SupplierEntity> ListAllSuppliers()
        {
            try
            {
                return supplier.SelectAllSuppliers();
            }catch(Exception ex)
            {
                ExceptionHandler.LogException(ex, "Supplier_Presentation_Layer_ListAll");
            }

            return null;
        }
        #endregion
    }
}