using StoreOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreOnline.Data
{
    public class Data
    {
        private static Data data = null;
        private static readonly Object objects = new Object();
        private List<User> userData = new List<User>();
        private List<Book> bookData=new List<Book>();
        private List<Order> orderData = new List<Order>(); 
        private Data()
        {
            userData.Add(new User() { UserName="wustzz",Password="1234",Email="1194717390@qq.com"});
            userData.Add(new User() { UserName="lizhe",Password="1234",Email="861775749@qq.com"});
            userData.Add(new User() { UserName = "boyaning", Password = "1234", Email = "1194717390@qq.com" });
            bookData.Add(new Book() {BookImg="01.jpg", Id = "01",Price=23.88,BookName="视屏学C++",BookType="IT技术",BookIntroduce= "本书以全面介绍了使用Visual C++进行程序开发的各种技术。在内容安排上由浅入深，让读者循序渐进掌握编程技术；在内容讲解上结合丰富的图解和形象的比喻，帮助读者理解“晦涩难懂”的技术；在内容形式上附有大量的提示、技巧、说明、编程信条等栏目，夯实读者编程技术，丰富编程经验。" });
            bookData.Add(new Book() { BookImg = "02.jpg", Id = "02", Price = 20.98, BookName = "C#开发宝典 ", BookType = "IT技术", BookIntroduce = "本书全面细致地讲解了使用c#语言进行编程和实际项目开发的各种技术，是学习c#编程的必备图书。全书以microsoft visual studio 2010开发环境为基础，分两大部分讲解了c#编程中的各种技术。" });
            bookData.Add(new Book() { BookImg = "03.jpg", Id = "03", Price = 19.88, BookName = "ASP.NET 从入门到精通", BookType = "IT技术", BookIntroduce = "本书介绍了如何使用ASP.NET 4.0和配套的Visual Studio 2010开发环境进行Web网站开发所要学习的技术、操作方法和使用技巧。" });
            bookData.Add(new Book() { BookImg = "04.jpg", Id = "04", Price = 22, BookName = "民国时期的总统们", BookType = "历史文学", BookIntroduce = "这是一本评价民国早期总统的通俗图书。包括北洋政府的5位总统（袁世凯、黎元洪、冯国璋、徐世昌、曹锟）和缔造民国、出任临时大总统的孙中山先生，共6位总统等。" });
            bookData.Add(new Book() { BookImg = "05.jpg", Id = "05", Price = 27.99, BookName = "货币战争", BookType = "经济文学", BookIntroduce = "2008年金融危机结束五年之后，对未来世界经济走势的观点仍然是众说纷纭。美国的量化宽松究竟有无效果？全球的货币超发到底是福是祸？金融市场是渐趋安全，还是越发危险？经济复苏是稳步向前，还是昙花一现？..." });
            bookData.Add(new Book() { BookImg = "06.jpg", Id = "06", Price = 20.88, BookName = "黑客攻防", BookType = "IT技术", BookIntroduce = "本书对目前流行的Windows黑客编程技术逐一进行讲解，揭开黑客编程 的神秘面纱。全书详细剖析了目前流行的各种黑客技术，包括文件操作技术 、后门编程技术、扫描嗅探技术、木马下载者技术、U盘小偷、密码盗窃、 验证码的破解、进程控制技术、NTFS数据流的检测与清除、系统监控技术、 API Hook与SPI等" });
            bookData.Add(new Book() { BookImg = "07.jpg", Id = "07", Price = 34.6, BookName = "虚拟现实设计", BookType = "IT技术", BookIntroduce = "本书全面介绍了计算机前沿科技——虚拟现实X3D（Extensible 3D）， 即虚拟现实三维立体网络程序设计语言。X3D作为第二代三维立体网络程序 设计语言，是目前计算机虚拟现实领域最前沿的一种新型语言。它是宽带网 络、多媒体、游戏设计、人性化动画设计、信息地理及人工智能相融合的高 科技产品，是把握未来网络、多媒体、游戏设计及人工智能的关键技术。  " });
            bookData.Add(new Book() { BookImg = "08.jpg", Id = "08", Price = 17, BookName = "普京大传", BookType = "人物传记", BookIntroduce = "本书以全新的视角深入地描述了普京的真实人生及俄罗斯的政治内幕。在任前两届总统期间，普京为俄罗斯创造了一个初步的奇迹：经济逐步走上正轨，军事力量增强，政治体制独具特色且日臻完善，外交风格鲜明。他重新让俄罗斯人找回了大国的自信，找回了精神力量。2" });
            bookData.Add(new Book() { BookImg = "09.jpg", Id = "09", Price = 38, BookName = "JAVA通用范例开发宝典", BookType = "IT技术", BookIntroduce = "本书以程序开发人员在编程中可能遇到的实际问题(案例)和开发中应 该掌握的技术为主线，全面介绍了运用Java语言进行程序开发的各方面的 应用案例和经验技巧。 " });
            bookData.Add(new Book() { BookImg = "10.jpg", Id = "10", Price = 25.88, BookName = "计算机操作系统", BookType = "IT技术", BookIntroduce = "本书详细介绍了计算机操作系统的基本概念、基本原理和典型实现技术，着重讲述了构造操作系统过程中面临的各种问题及其解决方法；特别讨论5了操作系统设计中的一些非常重要的进展，包括线程、实时系统、多处理器调度、进程迁移、分布计算模式、分布进程管理、中间件技术、微核技术、操作系统的安全性和网格操作系统等。" });
            orderData.Add(new Order() { UserName = "wustzz", No = 1, BookName = "视屏学C++", Time = DateTime.Now.ToShortDateString(), sum = 2, Price = 66 ,IsBuy=true });
            orderData.Add(new Order() { UserName = "wustzz", No = 2, BookName = "视屏学C++", Time = DateTime.Now.ToShortDateString(), sum = 2, Price = 66, IsBuy = true });
            orderData.Add(new Order() { UserName = "wustzz", No = 3, BookName = "视屏学C++", Time = DateTime.Now.ToShortDateString(), sum = 2, Price = 66, IsBuy = true });
            orderData.Add(new Order() { UserName = "wustzz", No = 4, BookName = "视屏学C++", Time = DateTime.Now.ToShortDateString(), sum = 2, Price = 66, IsBuy = true });
            orderData.Add(new Order() { UserName = "wustzz", No = 5, BookName = "视屏学C++", Time = DateTime.Now.ToShortDateString(), sum = 2, Price = 66, IsBuy = true });
            orderData.Add(new Order() { UserName = "wustzz", No = 6, BookName = "视屏学C++", Time = DateTime.Now.ToShortDateString(), sum = 2, Price = 66, IsBuy = true });
            orderData.Add(new Order() { UserName = "wustzz", No = 7, BookName = "视屏学C++", Time = DateTime.Now.ToShortDateString(), sum = 2, Price = 66, IsBuy = true });
            orderData.Add(new Order() { UserName = "wustzz", No = 8, BookName = "视屏学C++", Time = DateTime.Now.ToShortDateString(), sum = 2, Price = 66, IsBuy = true });
            orderData.Add(new Order() { UserName = "wustzz", No = 9, BookName = "视屏学C++", Time = DateTime.Now.ToShortDateString(), sum = 2, Price = 66, IsBuy = true });
            orderData.Add(new Order() { UserName = "wustzz", No = 10, BookName = "视屏学C++", Time = DateTime.Now.ToShortDateString(), sum = 2, Price = 66, IsBuy = true });
            orderData.Add(new Order() { UserName = "wustzz", No = 11, BookName = "视屏学C++", Time = DateTime.Now.ToShortDateString(), sum = 2, Price = 66, IsBuy = true });
            orderData.Add(new Order() { UserName = "wustzz", No = 12, BookName = "视屏学C++", Time = DateTime.Now.ToShortDateString(), sum = 2, Price = 66, IsBuy = true });
            orderData.Add(new Order() { UserName = "wustzz", No = 13, BookName = "视屏学C++", Time = DateTime.Now.ToShortDateString(), sum = 2, Price = 66, IsBuy = true });
            orderData.Add(new Order() { UserName = "wustzz", No = 14, BookName = "视屏学C++", Time = DateTime.Now.ToShortDateString(), sum = 2, Price = 66, IsBuy = true });
            orderData.Add(new Order() { UserName = "wustzz", No = 15, BookName = "视屏学C++", Time = DateTime.Now.ToShortDateString(), sum = 2, Price = 66, IsBuy = false });
            orderData.Add(new Order() { UserName = "wustzz", No = 16, BookName = "视屏学C++", Time = DateTime.Now.ToShortDateString(), sum = 2, Price = 66, IsBuy = false });
            orderData.Add(new Order() { UserName = "wustzz", No = 17, BookName = "视屏学C++", Time = DateTime.Now.ToShortDateString(), sum = 2, Price = 66, IsBuy = false });
            orderData.Add(new Order() { UserName = "wustzz", No = 18, BookName = "视屏学C++", Time = DateTime.Now.ToShortDateString(), sum = 2, Price = 66, IsBuy = false });
            orderData.Add(new Order() { UserName = "wustzz", No = 19, BookName = "视屏学C++", Time = DateTime.Now.ToShortDateString(), sum = 2, Price = 66, IsBuy = false });
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

        public List<Order> GetOrderData()
        {
            return orderData;
        }
        public List<Book> GetBook()
        {
            return bookData;
        }
    }
}