using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace AstuteCommander
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("validate", Attributes = ValidationForAttributeName, TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ValidationHelper : TagHelper
    {
        private const string ValidationForAttributeName = "for";
        public override int Order => 0;
        public ValidationHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; }

        [HtmlAttributeName(ValidationForAttributeName)]
        public ModelExpression For { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            output.TagName = "span";
            output.Attributes.Add("class", "text-danger");

            if (For != null)
            {
                var tagBuilder = Generator.GenerateValidationMessage(
                    ViewContext,
                    For.ModelExplorer,
                    For.Name,
                    message: null,
                    tag: null,
                    htmlAttributes: null);

                if (tagBuilder != null)
                {
                    output.MergeAttributes(tagBuilder);

                    if (!output.IsContentModified)
                    {
                        var childContent = await output.GetChildContentAsync();
                        if (childContent.IsEmptyOrWhiteSpace)
                        {
                            if (tagBuilder.HasInnerHtml)
                            {
                                output.Content.SetHtmlContent(tagBuilder.InnerHtml);
                            }
                        }
                        else
                        {
                            output.Content.SetHtmlContent(childContent);
                        }
                    }
                }
            }

        }
    }
}
