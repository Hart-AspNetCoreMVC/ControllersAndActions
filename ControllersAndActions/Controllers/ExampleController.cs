using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControllersAndActions.Controllers
{
    public class ExampleController : Controller
    {
        //PASSING a datetime object to the index view in the Views Example folder
        public ViewResult Index()
        {
            return View(DateTime.Now);
        }
        //PASSING A STRING OBJECT: mvc will interpret below as a file name, not an object. Therefore, must cast to object first. Replace with: (object)"Hello World"
        public ViewResult Result() => View((object)"Hello World");


        //VIEWBAG: property of controller class; can send more than one object to view
        //ViewBag is dynamic therefore errors not revealed until runtime and no intellisence
        public ViewResult ViewBagDemo()
        {
            ViewBag.Message = "Hello World";
            ViewBag.Date = DateTime.Now;
            return View();
        }
        //Redirects send an HTTP code to brower -- 302: temp redirect; or 301: permanant redirect
        //redirect below returns a RedirectResult object (temporary) sending client to new url
        //can use RedirectPermanent for perm redirection
        public RedirectResult Redirect() => Redirect("https://google.com");


        //redirects to another action or /controller/action
        //Below is a wrapper for RedirectToRoute method in which you pass in an anon type (manually enter each property) to return a RedirectToTouteObject
        //use LocalRedirectResult when directing to urls from users to restrict what can be entered and prevent malicious attack to redirect pages to another location
        public RedirectToActionResult RedirectToActionDemo() => RedirectToAction(nameof(ViewBagDemo));

        //returning a Json object directly to the browser
        public JsonResult JsonDemo() => Json(new[] {"Alice", "Bob", "Joe"});


        //Response of a certain type; sends a string and takes an optional type; caveat is that client must be able to acept this MIME type(safer approach is content negotiation)
        public ContentResult ContentResultDemo() => Content("<h3>wowee</h3>", "text/html");

        //content negotiation: headers of HTTP req includes MIME types it will accept, content negotiation auto finds a compatible type, usually Json.
        //ObjectResult which is returned by OK method, performs content negotiation -- finds an acceptable format
        public ObjectResult ContentNegDemo() => Ok(new string[] {"Alice", "Bob", "Joe"});


        //File method can return FileContent, VirtualFile, FileStream, PhysicalFile based on needs. MVC uses StaticFile middleware to host common files/types
        public VirtualFileResult FileDemo() => File(@"/ReadMe.txt", "text/html");

        //SENDING SPECIFIC RESULT CODES-- typically these are already pre-written, but are available if more control is needed
        public StatusCodeResult StatusCodeDemo() => StatusCode(StatusCodes.Status403Forbidden);
    }
}
