using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using 練習介接API.Models;

namespace 練習介接API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public IActionResult meth()
        {
            var comic = new BookInfo()
            {
                Name = "被討厭的勇氣",
                writer = "阿德勒",
                Price = 599
            };
            var da=JsonSerializer.Serialize(comic); //序列化

            return Ok(da);
        }
    }
}