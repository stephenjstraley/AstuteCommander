using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;


namespace AstuteCommander.Classes.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("checkbox", Attributes = ForAttributeName, TagStructure = TagStructure.WithoutEndTag)]

    public class CheckboxForTagHelper : TagHelper
    {
        private const string ForAttributeName = "for";

        private static readonly System.Collections.Generic.Dictionary<string, string> _defaultInputTypes =
            new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { nameof(Boolean), InputType.CheckBox.ToString().ToLowerInvariant() },
                { nameof(String), InputType.Text.ToString().ToLowerInvariant() }
            };

        protected IHtmlGenerator Generator { get; set; }

        public CheckboxForTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        public override int Order => 0;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName(ForAttributeName)]

        public ModelExpression For { get; set; }

        public string PostText { get; set; }

        public string Class { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var metadata = For.Metadata;
            var modelExplorer = For.ModelExplorer;

            string inputType;
            string inputTypeHint;

            // Note GetInputType never returns null.
            inputType = GetInputType(modelExplorer, out inputTypeHint);

            output.Attributes.SetAttribute("type", "checkbox");
            output.Attributes.SetAttribute("style", "width: 20px; height: 20px");

            if (!string.IsNullOrEmpty(Class))
                output.Attributes.SetAttribute("class", Class);

            output.TagName = "input";

            if (!string.IsNullOrEmpty(PostText))
                output.PostElement.SetHtmlContent(PostText);


            TagBuilder tagBuilder = GenerateCheckBox(modelExplorer, output);

            if (tagBuilder != null)
            {
                output.MergeAttributes(tagBuilder);
                if (tagBuilder.HasInnerHtml)
                {
                    output.Content.AppendHtml(tagBuilder.InnerHtml);
                }
            }
        }

        protected string GetInputType(ModelExplorer modelExplorer, out string inputTypeHint)
        {
            foreach (var hint in GetInputTypeHints(modelExplorer))
            {
                string inputType;
                if (_defaultInputTypes.TryGetValue(hint, out inputType))
                {
                    inputTypeHint = hint;
                    return inputType;
                }
            }

            inputTypeHint = InputType.Text.ToString().ToLowerInvariant();
            return inputTypeHint;
        }

        private TagBuilder GenerateCheckBox(ModelExplorer modelExplorer, TagHelperOutput output)
        {
            bool potentialBool = false;
            if (modelExplorer.ModelType == null)
            {
                if (modelExplorer.Model != null)
                {
                    potentialBool = false;
                }
            }
            else if (modelExplorer.ModelType == typeof(string))
            {
                if (modelExplorer.Model != null)
                {
                    var thing = modelExplorer.Model.ToString().ToUpper();
                    if (string.IsNullOrEmpty(thing))
                    {
                        potentialBool = false;
                    }
                    else
                    {
                        if (thing.Length > 0)
                            potentialBool = (thing.Substring(0, 1) == "Y" || thing.Substring(0, 1) == "T");
                        else
                            potentialBool = false;
                    }
                }
            }
            else if (modelExplorer.ModelType != typeof(bool))
            {
            }

            // hiddenForCheckboxTag always rendered after the returned element
            var hiddenForCheckboxTag = Generator.GenerateHiddenForCheckbox(ViewContext, modelExplorer, For.Name);
            if (hiddenForCheckboxTag != null)
            {
                var renderingMode =
                    output.TagMode == TagMode.SelfClosing ? TagRenderMode.SelfClosing : TagRenderMode.StartTag;
                hiddenForCheckboxTag.TagRenderMode = renderingMode;

                if (ViewContext.FormContext.CanRenderAtEndOfForm)
                {
                    ViewContext.FormContext.EndOfFormContent.Add(hiddenForCheckboxTag);
                }
                else
                {
                    output.PostElement.AppendHtml(hiddenForCheckboxTag);
                }
            }

            return Generator.GenerateCheckBox(
                ViewContext,
                modelExplorer,
                For.Name,
                isChecked: potentialBool,
                htmlAttributes: null);
        }

        private static IEnumerable<string> GetInputTypeHints(ModelExplorer modelExplorer)
        {
            if (!string.IsNullOrEmpty(modelExplorer.Metadata.TemplateHint))
            {
                yield return modelExplorer.Metadata.TemplateHint;
            }

            if (!string.IsNullOrEmpty(modelExplorer.Metadata.DataTypeName))
            {
                yield return modelExplorer.Metadata.DataTypeName;
            }

            // In most cases, we don't want to search for Nullable<T>. We want to search for T, which should handle
            // both T and Nullable<T>. However we special-case bool? to avoid turning an <input/> into a <select/>.
            var fieldType = modelExplorer.ModelType;
            if (typeof(bool?) != fieldType)
            {
                fieldType = modelExplorer.Metadata.UnderlyingOrModelType;
            }

            foreach (string typeName in TemplateRenderer.GetTypeNames(modelExplorer.Metadata, fieldType))
            {
                yield return typeName;
            }
        }
    }
}
