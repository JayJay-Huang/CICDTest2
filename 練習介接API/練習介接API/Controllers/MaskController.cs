using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using 練習介接API.Models;
using 練習介接API.Service;

namespace 練習介接API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MaskController : ControllerBase
    {
        private readonly MaskService _maskService;
        public MaskController(MaskService maskService)
        {
            _maskService = maskService;
        }

        public async Task<IActionResult> Get()
        {
            var maskCount = await _maskService.GetMaskInfo();
            return Ok(maskCount);            
        }
    }
}

namespace 練習介接API.Service
{
    public class MaskService
    {
        public HttpClient Client { get; }

        public MaskService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://quality.data.gov.tw/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "QueryMask Sample");
            Client = client;
        }

        public async Task<IEnumerable<MaskInfo>> GetMaskInfo()
        {
            var response = await Client.GetAsync("dq_download_json.php?nid=116285&md5_url=2150b333756e64325bdbc4a5fd45fad1");
            response.EnsureSuccessStatusCode();
            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<MaskInfo>>(responseStream);
        }
    }
}