#pragma checksum "C:\Users\SCS\source\repos\ASPNET - FromMicrosoftDoc\StudentManagement\Views\Home\About.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "aff34c3077b2021a5a853acbac47e064075e57aa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_About), @"mvc.1.0.view", @"/Views/Home/About.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\SCS\source\repos\ASPNET - FromMicrosoftDoc\StudentManagement\Views\_ViewImports.cshtml"
using StudentManagement;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\SCS\source\repos\ASPNET - FromMicrosoftDoc\StudentManagement\Views\_ViewImports.cshtml"
using StudentManagement.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aff34c3077b2021a5a853acbac47e064075e57aa", @"/Views/Home/About.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c7bb6102ef74a0920854243dc02f5efb2e491047", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_About : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<StudentManagement.Models.SchoolViewModel.EnrollmentDateGroup>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\SCS\source\repos\ASPNET - FromMicrosoftDoc\StudentManagement\Views\Home\About.cshtml"
  
	ViewData["Title"] = "Student Statistics";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>Student Statistics</h2>\r\n<table>\r\n\t<tr>\r\n\t\t<th>Enrollment Date</th>\r\n\t\t<th>Students</th>\r\n\t</tr>\r\n");
#nullable restore
#line 12 "C:\Users\SCS\source\repos\ASPNET - FromMicrosoftDoc\StudentManagement\Views\Home\About.cshtml"
     foreach (var item in Model) { 

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t<tr>\r\n\t\t\t<td>");
#nullable restore
#line 14 "C:\Users\SCS\source\repos\ASPNET - FromMicrosoftDoc\StudentManagement\Views\Home\About.cshtml"
           Write(Html.DisplayFor(m => item.EnrollmentDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\t\t\t<td>");
#nullable restore
#line 15 "C:\Users\SCS\source\repos\ASPNET - FromMicrosoftDoc\StudentManagement\Views\Home\About.cshtml"
           Write(item.studentCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\t\t</tr>\r\n");
#nullable restore
#line 17 "C:\Users\SCS\source\repos\ASPNET - FromMicrosoftDoc\StudentManagement\Views\Home\About.cshtml"
	}

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<StudentManagement.Models.SchoolViewModel.EnrollmentDateGroup>> Html { get; private set; }
    }
}
#pragma warning restore 1591
