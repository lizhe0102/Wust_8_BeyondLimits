using StoreOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreOnline.Data
{
    public class Data
    {
        private static Data data = null;
        private static readonly Object objects = new Object();
        private List<User> userData = new List<User>();
        private Data()
        {
            userData.Add(new User() { UserName="wustzz",Password="1234",Email="1194717390@qq.com"});
            userData.Add(new User() { UserName="lizhe",Password="1234",Email="861775749@qq.com"});
            userData.Add(new User() { UserName = "boyaning", Password = "1234", Email = "1194717390@qq.com" });
        }
        public static Data GetData()
        {
            if (data == null)
            {
                lock (objects)
                {
                    if (data == null)
                        data = new Data();
                }

            }
            return data;
        }

        public List<User> GetUserData()
        {
            return userData;
        }
    }
}
