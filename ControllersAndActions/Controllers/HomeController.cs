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
        //renders a form asking for user input of name and city
        public ViewResult Index()
        {
            return View("SimpleForm");
        }

 


        //items from form are retrieved from the Request object, from the Form property
        public ViewResult ReceiveFormData()
        {
            var name = Request.Form["name"];
            var city = Request.Form["city"];
            return View("Result", $"{name} lives in {city}");
        }
        //items from form are retrieved through the method params of the same name as input tag name values.
        public ViewResult ReceiveFormDataAsArgs(string name, string city)
        {
            return View("Result", $"{name} lives in {city}");
        }

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
        //now we can use that method with just the code below, returning an IActionResult -- just like all the other IActionResult methods in the frameworkS
        public IActionResult ReceiveFormMyCustomAction(string name, string city)
        {
            return new CustomHtmlResult{Content=$"{name} lives in {city}"};
        }
    }
}
