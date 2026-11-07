using System.Diagnostics;
using LabApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LabAsp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Routes([FromServices] IActionDescriptorCollectionProvider provider)
    {
        var routes = provider.ActionDescriptors.Items.OfType<ControllerActionDescriptor>().Select(r =>
        {
            return new RouteInfo()
            {
                Pattern = "/"(r.AttributeRouteInfo?.Template ?? "Konwencjonalna"),
                Action = r.ActionName,
                Controller = r.ControllerName,
                Methods = string.Join(",",
                    r.ActionConstraints?.OfType<HttpMethodActionConstraint>().SelectMany(c => c.HttpMethods ?? ["*"]) ??
                    Array.Empty<string>()
                ),
                Area = r.RouteValues.TryGetValue("area", out var area) ? area ?? null
            };
        });
        return View(routes);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
