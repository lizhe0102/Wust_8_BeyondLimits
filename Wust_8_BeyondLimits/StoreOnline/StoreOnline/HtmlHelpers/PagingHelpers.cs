using StoreOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace StoreOnline.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,PagingInfo pagingInfo, Func<int,string> PageUrl)
        {
            StringBuilder result = new StringBuilder();
            for(int i=1;i<=pagingInfo.TotalPage;i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", PageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}