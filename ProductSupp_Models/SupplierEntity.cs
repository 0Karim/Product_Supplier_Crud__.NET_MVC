using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProductSupp_Models
{
    public class SupplierEntity
    {
        //[Key]
        public int? ID { set; get; }

        [Required]
        [StringLength(100)]
        [MinLength(3, ErrorMessage = "Supplier Name must not less than 3 characters")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Supplier Name accept only characters")]
        public string SupplierName { set; get; }
    }
}
