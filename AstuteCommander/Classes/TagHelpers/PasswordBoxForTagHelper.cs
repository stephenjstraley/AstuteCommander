using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System;

namespace AstuteCommander.Classes.TagHelpers
{
    [HtmlTargetElement("password", Attributes = ForAttributeName, TagStructure = TagStructure.WithoutEndTag)]
    public class PasswordBoxForTagHelper : TagHelper
    {
        private const string ForAttributeName = "for";

        protected IHtmlGenerator Generator { get; set; }

        public PasswordBoxForTagHelper(IHtmlGenerator gen)
        {
            Generator = gen;
        }

        public override int Order => 0;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName(ForAttributeName)]

        public ModelExpression For { get; set; }

        public string Value { get; set; } = string.Empty;
        public string Width { get; set; }
        public string Class { get; set; }
        public string NoForm { get; set; }
        public string Small { get; set; }
        public string PlaceHolder { get; set; }

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

            var metadata = For.Metadata;
            var modelExplorer = For.ModelExplorer;
            var formPrefix = (!string.IsNullOrEmpty(NoForm)) ? string.Empty : "form-control";
            var sizePrefix = (!string.IsNullOrEmpty(Small)) ? "input-sm" : string.Empty;

            if (!string.IsNullOrEmpty(Class))
            {
                if (Class.Contains("narrow"))
                    Class = Class.Replace("narrow", "narrow-input");
            }

            if (!string.IsNullOrEmpty(Class))
                output.Attributes.SetAttribute("class", Class + $" {formPrefix} {sizePrefix} text-box single-line");
            else
                output.Attributes.SetAttribute("class", $"{formPrefix} {sizePrefix} text-box single-line");

            if (!string.IsNullOrEmpty(PlaceHolder))
                output.Attributes.SetAttribute("placeholder", PlaceHolder);

            output.Attributes.SetAttribute("type", "password");
            output.TagName = "input";
            output.Attributes.SetAttribute("value", For.Model);

            if (!string.IsNullOrEmpty(Width))
            {
                int newVal = Convert.ToInt32(Width);
                if (newVal > 0)
                    output.Attributes.SetAttribute("style", "width:" + Width + "px");
            }

            TagBuilder tagBuilder = GenerateTextBox(modelExplorer, string.Empty, "text");

            if (tagBuilder != null)
            {
                output.MergeAttributes(tagBuilder);
                if (tagBuilder.HasInnerHtml)
                {
                    output.Content.AppendHtml(tagBuilder.InnerHtml);
                }
            }
        }

        private TagBuilder GenerateTextBox(ModelExplorer modelExplorer, string inputTypeHint, string inputType)
        {

            var htmlAttributes = new System.Collections.Generic.Dictionary<string, object>
            {
                { "type", inputType }
            };


            return Generator.GenerateTextBox(
                ViewContext,
                modelExplorer,
                For.Name,
                value: modelExplorer.Model,
                format: null,
                htmlAttributes: htmlAttributes);
        }

    }

}
