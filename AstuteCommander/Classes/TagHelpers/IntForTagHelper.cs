using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System;

namespace AstuteCommander.Classes.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("intbox", Attributes = ForAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class IntBoxForTagHelper : TagHelper
    {
        private const string ForAttributeName = "for";

        protected IHtmlGenerator Generator { get; set; }

        public IntBoxForTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        public override int Order => 0;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName(ForAttributeName)]

        public ModelExpression For { get; set; }

        public string Value { get; set; } = string.Empty;

        public string Width { get; set; }

        public string Digits { get; set; }

        public string Min { get; set; }
        public string Max { get; set; }

        public string Class { get; set; }

        public string NoForm { get; set; }

        public string Small { get; set; }

        public string Placeholder { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (!string.IsNullOrEmpty(Value))
            {
                output.CopyHtmlAttribute(nameof(Value), context);
                output.Attributes.SetAttribute("value", Value);
            }
            else
                output.Attributes.SetAttribute("value", For.Model);

            if (!string.IsNullOrEmpty(Min))
                output.Attributes.SetAttribute("min", Min);

            if (!string.IsNullOrEmpty(Max))
                output.Attributes.SetAttribute("max", Max);

            var metadata = For.Metadata;
            var modelExplorer = For.ModelExplorer;

            var formPrefix = string.Empty;
            if (!string.IsNullOrEmpty(NoForm))
                formPrefix = string.Empty;
            else
                formPrefix = "form-control";

            var sizePrefix = string.Empty;
            if (!string.IsNullOrEmpty(Small))
                sizePrefix = "input-sm";

            if (!string.IsNullOrEmpty(Class))
            {
                if (Class.Contains("narrow"))
                    Class = Class.Replace("narrow", "narrow-input");
            }

            if (!string.IsNullOrEmpty(Class))
                output.Attributes.SetAttribute("class", Class + $" {formPrefix} {sizePrefix}");
            else
                output.Attributes.SetAttribute("class", $"{formPrefix} {sizePrefix}");

            output.Attributes.SetAttribute("type", "number");
            output.TagName = "input";

            if (!string.IsNullOrEmpty(Width))
            {
                int newVal = Convert.ToInt32(Width);
                if (newVal > 0)
                    output.Attributes.SetAttribute("style", "width:" + Width + "px");
            }

            string format = null;
            // work on format
            if (!string.IsNullOrEmpty(Digits))
            {
                int digVal = Convert.ToInt32(Digits);
                format = string.Empty;
                format = format.PadRight(digVal, '0');

                format = "{0:" + format + "}";
            }

            TagBuilder tagBuilder = Generator.GenerateTextBox(
                ViewContext,
                modelExplorer,
                For.Name,
                value: modelExplorer.Model,
                format: format,
                htmlAttributes: null);

            if (tagBuilder != null)
            {
                output.MergeAttributes(tagBuilder);
                if (tagBuilder.HasInnerHtml)
                {
                    output.Content.AppendHtml(tagBuilder.InnerHtml);
                }
            }

        }
    }
}
