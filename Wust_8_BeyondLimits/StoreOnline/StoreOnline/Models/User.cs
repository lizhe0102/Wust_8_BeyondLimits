using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreOnline.Models
{
    [DisplayName("会员信息")]
    [DisplayColumn("Name")]
    public class User
    {
        
        //public int Id { set; get; }
        //[DisplayName("会员账号")]
        //[Required(ErrorMessage = "请输入Email地址")]
        //[Description("Email地址为会员账号")]
        //[DataType(DataType.EmailAddress)]
        public string Email { set; get; }


        //[DisplayName("会员密码")]
        //[Required(ErrorMessage = "请输入密码")]
        //[MaxLength(40, ErrorMessage = "密码不超过40个字节")]
        //[Description("密码将以SHAI进行哈希运算")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        [Key]
        //[DisplayName("昵称")]
        //[Required(ErrorMessage = ("请输入网路昵称"))]
        //[MaxLength(10, ErrorMessage = "不超过十个字")]
        public string UserName { get; set; }

        public Admit Entry { set; get; }

    }

    public enum Admit
    {
        Admin,
        VIP,
        Visitor
    }
}