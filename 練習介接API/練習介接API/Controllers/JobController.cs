using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using 練習介接API.Models;
using 練習介接API.Service;

namespace 練習介接API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        public async Task<IActionResult> Get()
        {

            string content = JobService.GetJsonContent("https://www.ktec.gov.tw/ktec_api.php?type=json");
            JobInfo Info = JsonConvert.DeserializeObject<JobInfo>(content);
            // 反序列化 將JSON格式字串轉為物件。 
            return Ok(Info);

        }

    }
}


namespace 練習介接API.Service
{
    public class JobService
    {

        public static string GetJsonContent(string Url)
        {
            string targetURI = Url;
            var request = System.Net.WebRequest.Create(targetURI);
            request.ContentType = "application/json; charset=utf-8";
            var response = request.GetResponse();
            string text;
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }
            return text;
        }
    }
}
