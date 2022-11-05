using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel;
using System.Diagnostics;

namespace OrnekNetCoreUyg2.TagHelpers
{

    //    CUSTOM TAGHELPER
    //TagHelper'lar yapısal olarak birer sınıftırlar ve custom bir tag helpler oluşturmak için bir sınıf oluşturmak gerekmektedir. 
    //Compononet mantığında kullanmak gereken bir helper'a ihtiyaç varsa custom tag helper oluşturulabilmektedir 
    //Mimaride etiket mantığında olduğunda custom kısmı da bu mantıkta oluşturulmaktadır.
    //TagHelper sınıfları, taghelper isminin kendisidir ve isminin yanına TagHelper eklenmelidir ->EmailTagHelper
    //Bir sınıfın tag helper olabilmesi için adına tag helpler olması yetmez. bunun TagHelper'dan da türemiş olması gerekmektedir

    //[HtmlTargetElement("EmailYaz")] //class ismini tag ismi olarak kullanmak istenmiyorsa bu şekilde isim verilebilir
    public class EmailTagHelper : TagHelper
    {

        public string Mail { get; set; }
        public string DisplayName { get; set; }


        //Process metodu ovveride edildi
        //Tag helperin işlevsellik gösterebilmesi için process metodu ovveride edilmelidir 
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //context parametresi içerisinde ilgili tag helpera vermiş olduğunuz bütün değerleri getirir
            //context tag helperi getirir
            //output tag helperin yapacağı işlemleri sunmaktadır

            output.TagName = "a"; //Çıktı olarak bu taghelperin ne vereceği belirtilir. ->htmldeki a tagini oluşturacak
            output.Attributes.Add("href", $"mailto:{Mail}");
            output.Content.Append(DisplayName);

            //base.Process(context, output); //output tanımlandıysa base tetiklenmesine gerek yoktur.
        }

    }

    public class MailGonderTagHelper : TagHelper
    { }
}
