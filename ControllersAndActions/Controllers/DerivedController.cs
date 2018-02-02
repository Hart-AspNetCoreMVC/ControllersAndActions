using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

//Deriving controller from the ANC.MVC controller class which defines methods and properties to access the MVC features in a cleaner way
namespace ControllersAndActions.Controllers
{
    public class DerivedController : Controller
    {
        /*same as ManualResult meth in Poco Controller, only using View method provided by controller base class - which under the hood, uses the values
         passed into the method to create the ViewResult object for me, along with all the other objects that need to be instantiated in the ManualResult version*/
        public ViewResult PassingAString() => View("Result", $"This is a derived controller");

        //Context Data is set of objects containing everything about the HTTP req; made available by the controller base class
        //This method queries into one of those objects to retrieve the header information sent over in the request
        //commonly used Conext Data objects are Request, Response, HttpContext, TouteData, ModelState, User (ie Request.Form provides all data entered into form)
        //These objects were created by ASP.NET and populated by various the middleware componenents as the request was processed
        //see manual version in poco controllers to see what this code encapsulates by using the controller class; this version is just more concise but does same thing
        public ViewResult GetReqHeaders() => View("DictionaryResult", 
            Request.Headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.First()));







    }
}
