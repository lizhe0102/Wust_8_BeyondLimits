using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreOnline.Models
{
    public class CarAndPagingInfo
    {
        public List<Order> Orders { set; get; }
        public PagingInfo pagingInfo { set; get; }
    }
}