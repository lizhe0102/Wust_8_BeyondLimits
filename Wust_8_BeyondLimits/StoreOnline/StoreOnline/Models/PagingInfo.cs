using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreOnline.Models
{
    public class PagingInfo
    {
        public int TotalItems { set; get; }
        public int ItemsPerPage { set; get; }
        public int CurrentPage { set; get; }
        public int TotalPage { get{
                return (int)Math.Ceiling((decimal)TotalItems/ItemsPerPage);
            } }
    }
}