using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training.WebApi.Filters;

namespace Training.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [TimeTrackFilter]
    public class TestingController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult Testing()
        {
            return Ok("Testing");
        }

        [HttpGet]
        [Route("products")]
        [Authorize(Policy = "HasNationality")]
        public IActionResult GetProduct()
        {
            return Ok("products");
        }

        [HttpGet]
        [Route("categories")]
        [Authorize(Policy = "HasNameWithSpecificValue")]
        public IActionResult GetCategories()
        {
            return Ok("categories");
        }
        
        [HttpGet]
        [Route("movies")]
        [Authorize(Policy = "AtLeast18")]
        public IActionResult GetMovies()
        {
            return Ok("movies");
        }
        
        [HttpGet]
        [Route("nationality1")]
        [NationalityFilter("German")]
        public IActionResult GetNationality1()
        {
            return Ok("Nationality 1");
        }

        [HttpGet]
        [Route("nationality2")]
        [NationalityFilter("German,VietNam")]
        public IActionResult GetNationality2()
        {
            return Ok("Nationality 2");
        }
    }
}
