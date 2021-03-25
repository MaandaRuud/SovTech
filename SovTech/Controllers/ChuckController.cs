using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SovTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace SovTech.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChuckController : ControllerBase
    {
        [HttpPost]
        public ResponseModel Get(RequestModel model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var responseTask = client.GetAsync("https://api.chucknorris.io/jokes/categories");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var data = JsonConvert.DeserializeObject<List<string>>(result.Content.ReadAsStringAsync().Result);
                        return new ResponseModel()
                        {
                            Data = data,
                            Message = "Success",
                            Status = (int)result.StatusCode,
                            Total = data.Count()
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
