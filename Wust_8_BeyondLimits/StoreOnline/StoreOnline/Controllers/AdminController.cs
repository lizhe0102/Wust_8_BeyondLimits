using StoreOnline.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreOnline.Controllers
{
    /// <summary>
    /// 管理员控制器
    /// </summary>
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            //HttpContext
            return View();
        }
        /// <summary>
        /// 上传页面
        /// </summary>
        /// <returns>返回上传页面视图</returns>
        //[Authorize(Roles =Enum.GetName(typeof(Admit),Admit.Admin))]
        public ActionResult UpLoadBook()
        {
            return View();
        }


        /// <summary>
        /// 上传书籍
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpLoadBook(Book book)
        {
            Stream uploadStream = null;
            FileStream fs = null;
            string bookId = (Data.Data.GetData().GetBook().Max(i => Convert.ToInt32(i.Id)) + 1).ToString();
            try
            {
                //文件上传，一次上传1M的数据，防止出现大文件无法上传  
                HttpPostedFileBase postFileBase = HttpContext.Request.Files["BookAdress"];
                uploadStream = postFileBase.InputStream;
                int bufferLen = 1024;
                byte[] buffer = new byte[bufferLen];
                int contentLen = 0;

                string fileName = Path.GetFileName(bookId + ".jpg");
                string baseUrl = Server.MapPath("/");
                string uploadPath = baseUrl + @"Img\book\";
                fs = new FileStream(uploadPath + fileName, FileMode.Create, FileAccess.ReadWrite);

                while ((contentLen = uploadStream.Read(buffer, 0, bufferLen)) != 0)
                {
                    fs.Write(buffer, 0, bufferLen);
                    fs.Flush();
                }
                //
                book.Id = bookId;
                book.BookImg = bookId + ".jpg";
                Data.Data.GetData().GetBook().Add(book);
                ViewData["Message"] = "*成功添加！";
                //保存页面数据，上传的文件只保存路径  
                //string productImage = "/Images/Upload/Product/" + fileName;
                //p.ProductImage = productImage;
                //p.ProductId = Guid.NewGuid().ToString();
                //p.CreationDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                ViewData["Message"] = "*添加失败！"+ex.Message.ToString();
                ex.StackTrace.ToString();
            }
            finally
            {
                if (null != fs)
                {
                    fs.Close();
                }
                if (null != uploadStream)
                {
                    uploadStream.Close();
                }
            }
            return View();
        }


    }
}