using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrnekNetCoreUyg2.Extensions
{
    static public class Extensions
    {
        //Herhangi bir yapı ya da değerin custom halini extensionlar ile oluşturabiliriz. 
        //CustomTextBox fonksiyonu mimarideki IHtmlHelper türleirne extension olarak eklenmiştir. 
        public static IHtmlContent CustomTextBox(this IHtmlHelper htmlHelper, string name, string value="inpıt area",string placeholder=null)
        {
            //new alanındaki değerler parametre olarak alınabilir ya da bir dto ile karşılanabilir. 
            return htmlHelper.TextBox(name, value, new { style = "background-color:green,color:white,font-size:11px;", @class = "form-input",placeholder=placeholder });
        }
    }
}
