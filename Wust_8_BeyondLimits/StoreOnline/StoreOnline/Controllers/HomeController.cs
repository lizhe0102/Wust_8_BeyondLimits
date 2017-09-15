using StoreOnline.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace StoreOnline.Controllers
{
    /// <summary>
    /// 主页控制器
    /// </summary>
    public class HomeController : Controller
    {
        private int PageSize = 4;
        private List<Book> books = Data.Data.GetData().GetBook();
       
        /// <summary>
        /// 为主业翻页动作提供服务
        /// </summary>
        /// <param name="page">想要到达的页数</param>
        /// <returns></returns>
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


        /// <summary>
        /// 为关于我们动作提供服务
        /// </summary>
        /// <returns></returns>
        public ActionResult AboutUs()
        {
            return View();
        }


        /// <summary>
        /// 留言
        /// </summary>
        /// <returns></returns>
        public ActionResult LeaveWord()
        {
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AnotherLink()
        {
            return View("Index");
        }
    }
}