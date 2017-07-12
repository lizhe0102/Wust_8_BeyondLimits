using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreOnline.Models
{
    public class BookAndPagingInfo
    {
        public List<Book> Books { set; get; }
        public PagingInfo PageInfo { set; get; }
    }
}