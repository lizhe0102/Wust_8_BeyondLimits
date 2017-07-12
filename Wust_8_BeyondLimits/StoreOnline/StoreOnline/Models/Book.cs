using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreOnline.Models
{
    public class Book
    {
        [Key]
        public string Id { set; get; }
        public string BookName { set; get; }
        public string BookType { set; get; }
        public string BookIntroduce { set; get; }
        public double Price { set; get; }
        public string BookImg { set; get; }//使用文件框
    }
}