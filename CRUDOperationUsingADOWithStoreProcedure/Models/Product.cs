using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace CRUDOperationUsingADOWithStoreProcedure.Models
{
    public class Product
    {
        [Key]
        public int productID { get; set; }
        [Required]
        [DisplayName("Product Name:")]
        public string productName { get; set; }
        [Required]
        [DisplayName("Price:")]
        public int price { get; set; }
        [Required]
        [DisplayName("Qty:")]
        public decimal Qty { get; set; }
        [DisplayName("Remarks:")]
        public string Remarks { get; set; }

    }
}