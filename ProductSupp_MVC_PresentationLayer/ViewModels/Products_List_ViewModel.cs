using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProductSupp_Models;

namespace ProductSupp_MVC_PresentationLayer.ViewModels
{
    public class Products_List_ViewModel
    {
        //IEnumerable<ProductEntity> Products { set; get; }
        //List<SupplierEntity> Suppliers { set; get; }

        //SupplierEntity Supplier { set; get; }

        public ProductEntity Product { set; get; }
        public SupplierEntity Supplier { set; get; }
    }
}