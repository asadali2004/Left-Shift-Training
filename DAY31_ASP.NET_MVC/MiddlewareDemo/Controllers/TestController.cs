
using Microsoft.AspNetCore.Mvc;

public class TestController : Controller
{
    public IActionResult Echo(string q, string ans)
    {
        return Content($"You sent q = {q} and {ans}");
    }
}