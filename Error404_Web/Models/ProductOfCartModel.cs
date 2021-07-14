using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Error404_Web.Models
{
    public class ProductOfCartModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Img { get; set; }
        public Nullable<double> ProductPrice { get; set; }
        public Nullable<int> ProductAmount { get; set; }
    }
}