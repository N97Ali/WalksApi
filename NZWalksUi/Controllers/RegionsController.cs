using Microsoft.AspNetCore.Mvc;
using NZWalksUi.Models;
using NZWalksUi.Models.DTO;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace NZWalksUi.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {

            this.httpClientFactory = httpClientFactory;
        }



        public async Task<IActionResult> Index()
        {
            List<RegionDto> Response = new List<RegionDto>();

            try
            {
                //Get all the regions from wev api
                var client = httpClientFactory.CreateClient();
                var httpReponseMessage = await client.GetAsync("https://localhost:7290/api/Regions");
                httpReponseMessage.EnsureSuccessStatusCode();
                Response.AddRange(await httpReponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());

            }
            catch (Exception ex)
            {
                // log the exception
            }
            return View(Response);
        }



        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionsViewModel addRegionsViewModel)
        {
            var client = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7290/api/Regions"),
                Content = new StringContent(JsonSerializer.Serialize(addRegionsViewModel), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();
                if (response is not null)
                {
                    return RedirectToAction("Index", "Regions");
                }
            }
            return View();
        }



        //--Edit 
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7290/api/Regions/{id.ToString()}");
            if (response is not null)
            {
                return View(response);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AddRegionsViewModel addRegionsViewModel)
        {
            var client = httpClientFactory.CreateClient();
            var httprequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7290/api/Regions/{addRegionsViewModel.Code}"),
                Content = new StringContent(JsonSerializer.Serialize(addRegionsViewModel), Encoding.UTF8, "application/json")

            };
            var httResponseMessage = await client.SendAsync(httprequestMessage);
            httResponseMessage.EnsureSuccessStatusCode();
            var response = await httResponseMessage.Content.ReadFromJsonAsync<RegionDto>();
            if (response is not null)
            {
                return RedirectToAction("Edit", "Regions");
            }
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Delete(AddRegionsViewModel addRegionsViewModel)

        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7290/api/Regions/{addRegionsViewModel.Code}");
                httpResponseMessage.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Regions");

            }
            catch (Exception ex)
            {

            }
            return View("Edit");
        }


    }

}
