using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GraphKit.Web.Models;
using Microsoft.Extensions.Logging;
using GraphKit.Core;

namespace GraphKit.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IEmailService _emailService;

    public HomeController(ILogger<HomeController> logger, IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        ViewData["Message"] = "Your application description page.";

        return View();
    }

    public IActionResult Contact()
    {
        ViewData["Message"] = "Your contact page.";

        return View();
    }

    public IActionResult Email()
    {
        ViewData["Message"] = "ASP.NET Core mvc send email example";

        _emailService.Send("development@bgma.nl", "ASP.NET Core mvc send email example", "Send from asp.net core mvc action");

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
