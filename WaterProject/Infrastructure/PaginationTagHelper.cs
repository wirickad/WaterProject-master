﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterProject.Models.ViewModels;

namespace WaterProject.Infrastructure
{
    [HtmlTargetElement("div",Attributes = "page-model")] //asp-action is the attribute for the a tags, this is a custom attribute
    public class PaginationTagHelper : TagHelper
    {
        //Dynamically create the page links

        private IUrlHelperFactory uhf;

        public PaginationTagHelper (IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }


        public PageInfo PageModel { get; set; }
        public string PageAction { get; set; }
        public string PageClass { get; set; }
        public bool PageClassesEnabled { get; set; }

        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process (TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder final = new TagBuilder("div");

            for (int i=1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder tb = new TagBuilder("a");

                tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });
                //tb.AddCssClass("btn");
                if(PageClassesEnabled)
                {
                    tb.AddCssClass(PageClass);
                    tb.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                }
                tb.AddCssClass(PageClass);
                tb.InnerHtml.Append(i.ToString());

                final.InnerHtml.AppendHtml(tb);
            }

            tho.Content.AppendHtml(final.InnerHtml);

        }
    }
}
