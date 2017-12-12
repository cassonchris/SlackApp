using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlackApp.Models;

namespace SlackApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Status")]
    public class StatusController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromForm] SlashCommand slashCommand)
        {
            return Ok(slashCommand.Text);
        }
    }
}