using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Project_WebApi.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string ProductName { get; set; }

        public string Image { get; set; }

        public int Quantiy { get; set; }
        public int price { get; set; }

        public DateTime? DateCreated { get; set; } = DateTime.Now;

        //public decimal Price { get; set; }

        ////[ForeignKey("customer")]
        ////public string userName { get; set; }
        ////public Customer customer { get;set; }




        //[JsonIgnore]
        //public virtual List<OrderItem> orderItems { get; set; } = new List<OrderItem>();





        ////[ForeignKey("customer")]
        ////public int CustomrtId { get; set; }
        ////public virtual Customer customer { get; set; }


    }
}
