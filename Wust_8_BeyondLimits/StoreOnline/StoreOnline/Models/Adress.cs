using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreOnline.Models
{
    public class Adress
    {
        [Key]
        public string UserName { get; set; }

        public string Tel { get; set; }

        public string Adr1 { set; get; }

        public string Adr2 { set; get; }
    }
}