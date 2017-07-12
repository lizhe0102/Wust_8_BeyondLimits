using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreOnline.Models
{
    public class Order
    {
        [Key]
        public int No { set; get; }
        public string Select { set; get; }
        public string UserName { set; get; }
        public string BookId { set; get; }
        public string BookName { set; get; }
        public string Time { set; get; }
        public bool IsBuy { set; get; }
        public int sum { set; get; }
        public double Price { set; get; }
        public bool HasPay { set; get; }
        public string  Delete { set; get; }
        public string Buy { set; get; }
    }
}