using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorAdvanced.TagHelpers
{
    //Muss in _ViewImports stehen -> @addTagHelper *, RazorAdvanced

    public static class MyHtmlTagHelper
    {
        public static IHtmlContent HelloWorldHTMLString(this IHtmlHelper htmlHelper)
        {
            return new HtmlString("<strong>Hello World</strong>");
        }

        public static string HelloWorldString(this IHtmlHelper htmlHelper)
        {
            return "<strong>Hello World</strong>";
        }

        public static IHtmlContent HelloWorld(this IHtmlHelper htmlHelper, string name)
        {
            TagBuilder span = new TagBuilder("span");

            span.InnerHtml.Append("Hello, " + name + "!");

            TagBuilder br = new TagBuilder("br") { TagRenderMode = TagRenderMode.SelfClosing };
            //<br/> = SelfClosing
            //<p> = Start-Tag
            //</p> = End-Tag
            string result;

            using (StringWriter writer = new StringWriter())
            {
                span.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                br.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);

                result = writer.ToString();
            }

            return new HtmlString(result);
        }
    }
}
