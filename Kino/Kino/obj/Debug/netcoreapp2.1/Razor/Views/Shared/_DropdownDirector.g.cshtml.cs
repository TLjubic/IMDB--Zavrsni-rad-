#pragma checksum "C:\Users\Tomislav\source\repos\Kino\Kino\Views\Shared\_DropdownDirector.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b20a48f56bb381b282b4721fb972a588d368734b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__DropdownDirector), @"mvc.1.0.view", @"/Views/Shared/_DropdownDirector.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/_DropdownDirector.cshtml", typeof(AspNetCore.Views_Shared__DropdownDirector))]
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
#line 1 "C:\Users\Tomislav\source\repos\Kino\Kino\Views\_ViewImports.cshtml"
using Kino;

#line default
#line hidden
#line 2 "C:\Users\Tomislav\source\repos\Kino\Kino\Views\_ViewImports.cshtml"
using Kino.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b20a48f56bb381b282b4721fb972a588d368734b", @"/Views/Shared/_DropdownDirector.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"df205999c203b7c097cfb1dbb724ed6ac27f2c63", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__DropdownDirector : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 551, true);
            WriteLiteral(@"
<ul class=""sub"">
    <div class=""menu_left"">
        <li><span class=""category_li"">- Awards -</span></li>
        <li><a href=""#"">Award 1</a></li>
        <li><a href=""#"">Award 2</a></li>
        <li><a href=""#"">Award 3</a></li>
    </div>
    <div class=""row menu_left"">
        <li><span class=""category_li"">- Users list -</span></li>
        <li><a href=""#"">People watching</a></li>
        <li><a href=""#"">Most popular</a></li>
        <li><a href=""#"">Most voted</a></li>
        <li><a href=""#"">Top rated</a></li>
    </div>
</ul>");
            EndContext();
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