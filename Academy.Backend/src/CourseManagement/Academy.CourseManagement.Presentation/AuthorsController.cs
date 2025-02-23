using Academy.Framework;
using Microsoft.AspNetCore.Mvc;

namespace Academy.CourseManagement.Presentation
{
    public class AuthorsController : ApplicationController
    {
        [HttpGet]
        public ActionResult HelloWorld()
        {
            return Ok("dscdsdcs");
        }
    }
}
