using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Threading.Tasks;
using System;

namespace AstuteCommander
{
    [HtmlTargetElement("lbl", Attributes = ForAttributeName)]
    public class LabelForTagHelper : TagHelper
    {
        private const string ForAttributeName = "for";
        protected IHtmlGenerator Generator { get; set; }


        public LabelForTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        public override int Order => 0;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public string Text { get; set; }


        [HtmlAttributeName(ForAttributeName)]

        public ModelExpression For { get; set; }

        public string Class { get; set; }

        public string ID { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            TagBuilder tagBuilder = Generator.GenerateLabel(
                ViewContext,
                For.ModelExplorer,
                For.Name,
                labelText: null,
                htmlAttributes: null
                );

            output.TagName = "label";

            if (!string.IsNullOrEmpty(ID))
                output.Attributes.Add("id", ID);

            if (!string.IsNullOrEmpty(Class))
                if (Class.Contains("narrow"))
                    Class = Class.Replace("narrow", "narrow-label");

            if (!string.IsNullOrEmpty(Class))
                tagBuilder.AddCssClass(Class + " control-label");
            else
                tagBuilder.AddCssClass("control-label");

            if (tagBuilder != null)
            {
                output.MergeAttributes(tagBuilder);

                if (!output.IsContentModified)
                {
                    var childContext = await output.GetChildContentAsync();
                    if (childContext.IsEmptyOrWhiteSpace)
                    {
                        if (tagBuilder.HasInnerHtml)
                            output.Content.SetHtmlContent(tagBuilder.InnerHtml);
                    }
                    else
                    {
                        output.Content.SetHtmlContent(childContext);
                    }
                }

                if (!string.IsNullOrEmpty(Text))
                    output.Content.Append(Text);
            }
        }
    }
}
