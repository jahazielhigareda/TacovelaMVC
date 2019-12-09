using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Linq;
using Tacovela.MVC.Core.Enums;

namespace Tacovela.MVC.Core.TagHelpers
{
    [HtmlTargetElement("div", Attributes = MyValidationForAttributeName)]
    public class ValidationSummaryCustom : TagHelper
    {
        private const string MyValidationForAttributeName = "asp-valitadion-summary-custom";

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName(MyValidationForAttributeName)]
        public ValidationSummary For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            var classDiv = string.Empty;

            if (ViewContext.ModelState.Any(x => x.Key == TagHelperStatusEnum.Error.ToString()))
            {
                classDiv = "alert alert-danger text-danger small";
            }
            else if (ViewContext.ModelState.Any(x => x.Key == TagHelperStatusEnum.Success.ToString()))
            {
                classDiv = "alert alert-success text-success small";
            }

            output.Attributes.SetAttribute("class", classDiv);

            output.Content.AppendFormat(@"<button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close"">
                                    <span aria-hidden=""true"">&times;</span>
                                  </button>");

            var messages = ViewContext.ModelState.Where(x => x.Key == TagHelperStatusEnum.Error.ToString() ||
                                                           x.Key == TagHelperStatusEnum.Success.ToString())
                                                           .SelectMany(x => x.Value.Errors).ToList();
            foreach (var message in messages)
            {
                output.Content.AppendFormat("<li>{0}</li>", message.ErrorMessage);
            }

            if (messages.Any() == false)
                output.SuppressOutput();
        }
    }

    [HtmlTargetElement("div", Attributes = MyValidationForAttributeName)]
    public class MessageViewData : TagHelper
    {
        private const string MyValidationForAttributeName = "asp-msg-tempdata";

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName(MyValidationForAttributeName)]
        public ValidationSummary For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            var classDiv = string.Empty;

            var dictionary = (Dictionary<string, string>)ViewContext.TempData[GlobalApplicationEnum.TempDataMessage.ToString()];

            if (dictionary != null)
            {
                if (dictionary.Any(x => x.Key.Contains(TagHelperStatusEnum.Error.ToString())))
                {
                    classDiv = "alert alert-danger text-danger small";
                }
                else if (dictionary.Any(x => x.Key.Contains(TagHelperStatusEnum.Success.ToString())))
                {
                    classDiv = "alert alert-success text-success small";
                }

                output.Attributes.SetAttribute("class", classDiv);

                output.Content.AppendFormat(@"<button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close"">
                                    <span aria-hidden=""true"">&times;</span>
                                  </button>");

                var messages = dictionary.Where(x => x.Key.Contains(TagHelperStatusEnum.Error.ToString()) ||
                                                     x.Key.Contains(TagHelperStatusEnum.Success.ToString()))
                                                      .Select(x => x.Value).ToList();
                foreach (var message in messages)
                {
                    output.Content.AppendFormat("<li>{0}</li>", message);
                }

                if (messages.Any() == false)
                    output.SuppressOutput();
            }

            var modelState = ViewContext.ModelState;
            if (modelState != null && (modelState.Any(x => x.Key == TagHelperStatusEnum.Error.ToString())
                || modelState.Any(x => x.Key == TagHelperStatusEnum.Success.ToString())))
            {
                if (modelState.Any(x => x.Key == TagHelperStatusEnum.Error.ToString()))
                {
                    classDiv = "alert alert-danger text-danger small";
                }
                else if (modelState.Any(x => x.Key == TagHelperStatusEnum.Success.ToString()))
                {
                    classDiv = "alert alert-success text-success small";
                }

                output.Attributes.SetAttribute("class", classDiv);

                output.Content.AppendFormat(@"<button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close"">
                                    <span aria-hidden=""true"">&times;</span>
                                  </button>");

                var messages = modelState.Where(x => x.Key == TagHelperStatusEnum.Error.ToString() ||
                                                     x.Key == TagHelperStatusEnum.Success.ToString())
                                                      .SelectMany(x => x.Value.Errors).ToList();
                foreach (var message in messages)
                {
                    output.Content.AppendFormat("<li>{0}</li>", message.ErrorMessage);
                }

                if (messages.Any() == false)
                    output.SuppressOutput();
            }
        }
    }
}
