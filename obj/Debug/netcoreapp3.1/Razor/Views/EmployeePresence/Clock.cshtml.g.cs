#pragma checksum "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\EmployeePresence\Clock.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "768c8dae1d89ad7c6f1568096a1519c64980b1cc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_EmployeePresence_Clock), @"mvc.1.0.view", @"/Views/EmployeePresence/Clock.cshtml")]
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
#line 1 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\_ViewImports.cshtml"
using HR_App;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\_ViewImports.cshtml"
using HR_App.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"768c8dae1d89ad7c6f1568096a1519c64980b1cc", @"/Views/EmployeePresence/Clock.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"390e6b011c80c8aacfb8cb7c82a5633b478e25dc", @"/Views/_ViewImports.cshtml")]
    public class Views_EmployeePresence_Clock : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<!DOCTYPE html>\n<html>\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "768c8dae1d89ad7c6f1568096a1519c64980b1cc3288", async() => {
                WriteLiteral(@"
<style>
#customers {
  font-family: ""Trebuchet MS"", Arial, Helvetica, sans-serif;
  border-collapse: collapse;
  width: 100%;
}

#customers td, #customers th {
  border: 1px solid #ddd;
  padding: 8px;
}

#customers tr:nth-child(even){background-color: #f2f2f2;}

#customers tr:hover {background-color: #ddd;}

#customers th {
  padding-top: 12px;
  padding-bottom: 12px;
  text-align: left;
  background-color: #4CAF50;
  color: white;
}
</style>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "768c8dae1d89ad7c6f1568096a1519c64980b1cc4700", async() => {
                WriteLiteral("\n");
#nullable restore
#line 30 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\EmployeePresence\Clock.cshtml"
  
    Layout="LayoutEmp";

#line default
#line hidden
#nullable disable
                WriteLiteral("<h1 class=\"text-center\">Attendance</h1>\n<hr class=\"mb-4\">\n");
#nullable restore
#line 35 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\EmployeePresence\Clock.cshtml"
  
    var loginuser = ViewBag.iduser;

#line default
#line hidden
#nullable disable
                WriteLiteral("    <button class=\"btn btn-info text-right\"");
                BeginWriteAttribute("onclick", " onclick=\"", 662, "\"", 727, 3);
                WriteAttributeValue("", 672, "location.href=\'/EmployeePresence/ClockIn?id=", 672, 44, true);
#nullable restore
#line 37 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\EmployeePresence\Clock.cshtml"
WriteAttributeValue("", 716, loginuser, 716, 10, false);

#line default
#line hidden
#nullable disable
                WriteAttributeValue("", 726, "\'", 726, 1, true);
                EndWriteAttribute();
                WriteLiteral(">Clock In</button>\n    <button class=\"btn btn-info text-right\"");
                BeginWriteAttribute("onclick", " onclick=\"", 790, "\"", 856, 3);
                WriteAttributeValue("", 800, "location.href=\'/EmployeePresence/Clockout?id=", 800, 45, true);
#nullable restore
#line 38 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\EmployeePresence\Clock.cshtml"
WriteAttributeValue("", 845, loginuser, 845, 10, false);

#line default
#line hidden
#nullable disable
                WriteAttributeValue("", 855, "\'", 855, 1, true);
                EndWriteAttribute();
                WriteLiteral(">Clock Out</button>\n");
                WriteLiteral("<br>\n<br>\n<table id=\"customers\">\n  <tr class=\"text-center\">\n    <th width=\"30px\">No</th>\n    <th>Date</th>\n    <th>Clock In</th>\n    <th>Clock Out</th>\n  </tr>\n");
#nullable restore
#line 49 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\EmployeePresence\Clock.cshtml"
    
    var attendance = ViewBag.attendance;
    int counter =0;
    foreach(var i in attendance)
    {
      counter+=1;
      var date = @i.PresenceIn.ToString("dddd, dd MMMM yyyy");
      

#line default
#line hidden
#nullable disable
                WriteLiteral("<tr>\n        <td class=\"text-center\">");
#nullable restore
#line 57 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\EmployeePresence\Clock.cshtml"
                           Write(counter);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\n        <td>");
#nullable restore
#line 58 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\EmployeePresence\Clock.cshtml"
       Write(date);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\n        <td>");
#nullable restore
#line 59 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\EmployeePresence\Clock.cshtml"
       Write(i.PresenceIn.ToString("H:mm"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\n        <td>");
#nullable restore
#line 60 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\EmployeePresence\Clock.cshtml"
       Write(i.PresenceOut.ToString("H:mm"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\n      </tr>\n");
#nullable restore
#line 62 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\EmployeePresence\Clock.cshtml"
    }
  

#line default
#line hidden
#nullable disable
                WriteLiteral("</table>\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</html>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
