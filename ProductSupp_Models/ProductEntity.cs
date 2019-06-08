using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductSupp_Models
{
    public class ProductEntity
    {
        public int? ID { set; get; }

        [Required(ErrorMessage = "Product Name Required Field !!")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Product Name must be in charters")]
        [MaxLength(100, ErrorMessage = "Product Name must not exceed 100 characters"), MinLength(3, ErrorMessage = "Product Name must not less than 3 characters")]
        public string ProductName { set; get; }

        [Required(ErrorMessage = "The Product Quantity is Required Field !!")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Product Quantity must be numbers and and non Zero")]
        public int QuantityPerUnit { set; get; }

        [Required(ErrorMessage = "Product Reorder Level Required Field !!")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Product Reorder Level must be numbers and non Zero")]
        public int ReorderLevel { set; get; }

        [Required(ErrorMessage = "Supplier is Required Field !!")]
        public int SupplierID { set; get; }

        [Required(ErrorMessage = "Product Unit Price Required Field !!")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Product Unit Price Level must be numbers and non Zero")]
        [Range(1, 100000, ErrorMessage = "Product Unit Price must be between 1 to 100000")]
        public int UnitPrice { set; get; }

        [Required(ErrorMessage = "Product Unit in stock Required Field !!")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Product Unit in stock must be numbers and non Zero")]
        public int UnitsInStock { set; get; }

        [Required(ErrorMessage = "Product Unit on order Required Field !!")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Product Unit on order must be numbers and non Zero")]
        public int UnitsOnOrder { set; get; }
    }
}
