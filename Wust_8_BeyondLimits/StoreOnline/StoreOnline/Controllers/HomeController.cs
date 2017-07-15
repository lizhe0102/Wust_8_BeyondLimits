using StoreOnline.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace StoreOnline.Controllers
{
    public class HomeController : Controller
    {
        private int PageSize = 4;
        private List<Book> books = Data.Data.GetData().GetBook();
       
        public ActionResult Index(int page=1)
        {
            BookAndPagingInfo bp = new BookAndPagingInfo();
            
            if (books != null)
            {
                //books.Sort();
                int time = books.Count / PageSize;
                int mod = books.Count % PageSize;
                if (page <= time)
                {
                    bp.Books = books.GetRange((page - 1) * PageSize, PageSize);
                }
                else
                {
                    bp.Books = books.GetRange((page - 1) * PageSize, mod);
                }
                
                bp.PageInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = books.Count
                };
                return View(bp);
            }
            else
            {
                return View();
            }

        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult LeaveWord()
        {
            return View();
        }

        public ActionResult AnotherLink()
        {
            return View("Index");
        }
    }
}