#pragma checksum "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\Attendance\DetailAttendance.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b8d6c42cf572fb05c81f659d4e45dfa66b7c86fe"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Attendance_DetailAttendance), @"mvc.1.0.view", @"/Views/Attendance/DetailAttendance.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b8d6c42cf572fb05c81f659d4e45dfa66b7c86fe", @"/Views/Attendance/DetailAttendance.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"390e6b011c80c8aacfb8cb7c82a5633b478e25dc", @"/Views/_ViewImports.cshtml")]
    public class Views_Attendance_DetailAttendance : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b8d6c42cf572fb05c81f659d4e45dfa66b7c86fe3313", async() => {
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
  background-color: #4682B4;
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
#nullable restore
#line 29 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\Attendance\DetailAttendance.cshtml"
  
	Layout="_LayoutAdmin";

#line default
#line hidden
#nullable disable
#nullable restore
#line 32 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\Attendance\DetailAttendance.cshtml"
  
    var name = ViewBag.name;
    string nameDetailEmp="";
    foreach(var i in name)
    {
        nameDetailEmp=i.Name;
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"container-fluid\" id=\"container-wrapper\">\n  <div class=\"d-sm-flex align-items-center justify-content-between mb-4\">\n    <h1 class=\"h3 mb-0 text-gray-800\">Detail Attendance ");
#nullable restore
#line 42 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\Attendance\DetailAttendance.cshtml"
                                                   Write(nameDetailEmp);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\n      <ol class=\"breadcrumb\">\n        <li class=\"breadcrumb-item\"><a>Home / Attendance / Detail</a></li>\n      </ol>\n  </div>\n</div>\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b8d6c42cf572fb05c81f659d4e45dfa66b7c86fe5894", async() => {
                WriteLiteral("\n<table id=\"customers\" style=\"margin-left: 30px;width:800px;\">\n  <tr class=\"text-center\">\n    <th width=\"30px\">No</th>\n    <th>Date</th>\n    <th>Clock In</th>\n    <th>Clock Out</th>\n  </tr>\n");
#nullable restore
#line 56 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\Attendance\DetailAttendance.cshtml"
    
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
#line 64 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\Attendance\DetailAttendance.cshtml"
                           Write(counter);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\n        <td>");
#nullable restore
#line 65 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\Attendance\DetailAttendance.cshtml"
       Write(date);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\n        <td>");
#nullable restore
#line 66 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\Attendance\DetailAttendance.cshtml"
       Write(i.PresenceIn.ToString("H:mm"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\n        <td>");
#nullable restore
#line 67 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\Attendance\DetailAttendance.cshtml"
       Write(i.PresenceOut.ToString("H:mm"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\n      </tr>\n");
#nullable restore
#line 69 "C:\Users\Asus A456UQ\Downloads\Task-Final-HR-App-master\Views\Attendance\DetailAttendance.cshtml"
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
            WriteLiteral("\n</html>\n<script>\n  function SearchEmp(btn)\n  {\n    var input = document.getElementById(\"search\").value;\n    location.href=\'/Attendance/Index?search=\'+input;\n\n  }\n</script>");
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