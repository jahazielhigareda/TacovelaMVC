using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
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

            if (ViewContext.ModelState.Any(x => x.Key == TagHelperStatusEnums.Error.ToString()))
            {
                classDiv = "alert alert-danger text-danger small";
            }
            else if (ViewContext.ModelState.Any(x => x.Key == TagHelperStatusEnums.Success.ToString()))
            {
                classDiv = "alert alert-success text-success small";
            }

            output.Attributes.SetAttribute("class", classDiv);

            output.Content.AppendFormat(@"<button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close"">
                                    <span aria-hidden=""true"">&times;</span>
                                  </button>");

            var messages = ViewContext.ModelState.Where(x => x.Key == TagHelperStatusEnums.Error.ToString() ||
                                                           x.Key == TagHelperStatusEnums.Success.ToString())
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
