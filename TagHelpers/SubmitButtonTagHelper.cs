using Microsoft.AspNetCore.Razor.TagHelpers;

namespace olympic_assignment.TagHelpers
{
    [HtmlTargetElement("button", Attributes = "[type=submit]")]
    public class SubmitButtonTagHelper : TagHelper
    {
        public string BsColor { get; set; } = "success";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string existingClass = output.Attributes.ContainsName("class")
                ? output.Attributes["class"].Value?.ToString() ?? ""
                : "";

            string btnClass = $"btn btn-{BsColor}";

            if (string.IsNullOrWhiteSpace(existingClass))
            {
                output.Attributes.SetAttribute("class", btnClass);
            }
            else if (!existingClass.Contains("btn"))
            {
                output.Attributes.SetAttribute("class", $"{btnClass} {existingClass}");
            }
        }
    }
}
