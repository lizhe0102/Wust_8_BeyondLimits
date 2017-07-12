using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreOnline.Models
{
    public class UsreAndAdress
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { set; get; }
        public string tel { get; set; }
        public string Adr1 { set; get; }
        public string Adr2 { set; get; }
    }
}