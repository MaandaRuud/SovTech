using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SovTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace SovTech.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SwapiController : ControllerBase
    {
        [HttpPost]
        public ResponseModel Get(RequestModel model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var responseTask = client.GetAsync("https://swapi.dev/api/people/");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var data = JsonConvert.DeserializeObject<SwapiModel>(result.Content.ReadAsStringAsync().Result);
                        return new ResponseModel()
                        {
                            Data = data.Results,
                            Message = "Success",
                            Status = (int)result.StatusCode,
                            Total = data.Count
                        };
                    }
                    else
                    {
                        return new ResponseModel()
                        {
                            Message = "Failed",
                            Status = (int)result.StatusCode,
                            Total = 0
                        };
                    }
                }
            }
            catch (Exception error)
            {
                return new ResponseModel()
                {
                    Status = 500,
                    Message = error.Message,
                    Data = error.StackTrace
                };
            }

        }
    }
}
