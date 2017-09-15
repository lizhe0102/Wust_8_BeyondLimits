using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreOnline.Models
{
    public class OrderShow
    {
        public string UserName { set; get; }
        public string BookName { set; get; }
        public string BookType { set; get; }
        public string Time { set; get; }
        public int Sum { set; get; }
    }
}