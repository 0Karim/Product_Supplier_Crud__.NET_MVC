using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProductSupp_Models;

namespace ProductSupp_MVC_PresentationLayer.ViewModels
{
    public class Suppliers_Product
    {
        public ProductEntity Product { get; set; }
        public IEnumerable<SupplierEntity> Suppliers { get; set; }
    }
}