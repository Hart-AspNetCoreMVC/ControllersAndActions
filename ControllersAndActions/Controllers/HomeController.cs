using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControllersAndActions.Inframstructure;
using Microsoft.AspNetCore.Mvc;



namespace ControllersAndActions.Controllers
{
    public class HomeController : Controller
    {
        //GET A FORM
        //renders a form asking for user input of name and city
        public ViewResult Index()
        {
            return View("SimpleForm");
        }

        //FORM DATA - MANUAL
         //items from form are retrieved from the Request object, from the Form property
        public ViewResult ReceiveFormData()
        {
            var name = Request.Form["name"];
            var city = Request.Form["city"];
            return View("Result", $"{name} lives in {city}");
        }

        //FORM DATA VIA PARAMS
        //items from form are retrieved through the method params of the same name as input tag name values.
        //uses ViewResult class which provides access to Razor engine to process .cshtml
        //View method overloaded to pass in name of file or object, file and object -- or nothing which it renders file with same name as action(or attribute).
        //Rendering file: in searching for .cshtml file, mvc looks in folder with same name as controller, then shared folder
        //passing object: passing an obj to View meth sets the ViewData.Model property of the the ViewResult object.
        //**PROBLEM: real world, a form should not render a view and instead redirect to a get req to avoid resubmission sees POST/REDIRECT/POST example
        public ViewResult ReceiveFormDataAsArgs(string name, string city)
        {
            return View("Result", $"{name} lives in {city}");
        }

        //RENDERING A VIEW - MANUALLY
        //manually sending html to the the browser, after recieveing the form data as args to the method;  UTH preview of what action methods do
        public void ManualReceiveForm(string name, string city)
        {
            Response.StatusCode = 200;
            Response.ContentType = "text/html";
            byte[] content = Encoding.ASCII.GetBytes($"<html><body>{name} lives in {city}</body>");
            Response.Body.WriteAsync(content, 0, content.Length);
        }
        //instead of manually writing the code above -- let's encapsulate it implementing the IActionResult interface, essentially making our own ActionResult
        //Goto the CustomHtmlResult file in the Infrastructure folder to see the same code encapsulated into a method using the interface
        //now we can use that method with just the code below, returning an IActionResult -- just like all the other IActionResult methods in the framework
        //essentially takes the code that produces the response out of the controller(put in infrastructure for code resuse)

        //RENDING A VIEW USING IACTIONRESULT
        public IActionResult ReceiveFormMyCustomAction(string name, string city)
        {
            return new CustomHtmlResult{Content=$"{name} lives in {city}"};
        }

        //POST/REDIRECT/GET pattern to prevent re-submission of form data
        //HttpPost attribute ensures that only post requests can be sent to method, not a browser reload; cannot hit this route by typing into browser
        // Redirect means new HTTP req is sent and form data is lost; USE TempData below to add persistance until read 
        //TempData returns Dictionary based off session; session must be enabled in startup.
        [HttpPost]
        public RedirectToActionResult ReceiveFormThenRedirect(string name, string city)
        {
            TempData["name"] = name;
            TempData["city"] = city;
            return RedirectToAction(nameof(Data));
        }
        public ViewResult Data()
        {
            string name = TempData["name"].ToString();
            string city = TempData["city"] as string;
            return View("Result", $"{name} lives in {city}");
        }
    }
}
