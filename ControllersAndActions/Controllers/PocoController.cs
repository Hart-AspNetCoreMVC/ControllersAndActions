using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControllersAndActions.Controllers
{
    //basic controller; DOES NOT DERIVE FROM ANY OTHER CLASS  
    
    public class PocoController
    {
        //'plain old CLR object"; most basic --  no dependencies on the asp.net API, just returns a string in browser
        public string Poco() => "This is a POCO controller";


        //manual action method; builds on previous method to utulize MVC functionality to render a view 
        //creates a ViewResult object manually which requires the setting various properties and creating other objects
        //MVC base Controller class encapsulates this process
        public ViewResult ManualResult() => new ViewResult()
        {
            ViewName="Result",
            ViewData= new ViewDataDictionary(
                new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = $"This is a POCO controller"
            }
            };

        //NEXT -- CREATE CONTROLLER CLASS DERIVED FROM CONTROLLER CLASS and return to next section when prompted

        //===============================================================
        //MANUAL GET HEADER CONTROLLER AS COMPARED TO ONE IN DERIVED CLASS

        //Manual controller to get header info w/o controller base class; what is happening under the hood when using action methods derived from controller class

        //ControllerContext is the base object that contains all the data needed for a request
        //we use an attribute decorator which tells the mvc to set the property with a Controller Context object describing  current request; ie dependency injection
        //without the decorator you get an error stating that there is no instance of the object.
        [ControllerContext]
        public ControllerContext ControllerContext { get; set; }

        public ViewResult ManualGetHeaders() => new ViewResult()
        {
            ViewName = "DictionaryResult",
            ViewData = new ViewDataDictionary(
                new EmptyModelMetadataProvider(),new ModelStateDictionary())
            {
                Model = ControllerContext.HttpContext.Request.Headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.First())

            }
        };

        }


    }

