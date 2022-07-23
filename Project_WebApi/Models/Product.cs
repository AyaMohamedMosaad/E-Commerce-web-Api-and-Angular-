using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Project_WebApi.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }
        public int ProductDiscount { get; set; }
        public string ProductImage { get; set; }
        public string Details { get; set; }


        [ForeignKey("category")]
        public int CategoryId { get; set; }
        public virtual Category category { get; set; }


        //[JsonIgnore]
        //public virtual List<OrderItem> orderItems { get; set; } = new List<OrderItem>();


    }
}
