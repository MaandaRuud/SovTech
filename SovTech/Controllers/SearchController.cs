using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SovTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SovTech.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        [HttpPost]
        public ResponseModel Get(RequestModel model)
        {
            SearchResponseModel retData = new SearchResponseModel();
            try
            {
                SearchQuery search = JsonConvert.DeserializeObject<SearchQuery>("" + model.Id + "");
                if (search == null)
                {
                    search.Query = "";
                }
                if (search.Query.Length > 2)
                {
                    using (var client = new HttpClient())
                    {
                        var responseTask = client.GetAsync("https://api.chucknorris.io/jokes/search?query=" + search.Query);
                        responseTask.Wait();

                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var data = JsonConvert.DeserializeObject<JokeSearchModel>(result.Content.ReadAsStringAsync().Result);
                            retData.CnJokes = new ResponseModel()
                            {
                                Data = data.Result.OrderBy(o=>o.Id).Take(model.PageSize).Skip((model.Page-1)*model.PageSize).ToList(),
                                Message = "Success",
                                Status = (int)result.StatusCode,
                                Total = data.Total
                            };
                        }
                        else
                        {
                            retData.CnJokes = new ResponseModel()
                            {
                                Message = "Failed",
                                Status = (int)result.StatusCode,
                                Total = 0
                            };
                        }
                    }
                }
                else
                {
                    retData.CnJokes = new ResponseModel()
                    {
                        Message = "Search query must be at least be 3 characters",
                        Status = 400,
                        Total = 0
                    };
                }
                using (var client = new HttpClient())
                {
                    var responseTask = client.GetAsync("https://swapi.dev/api/people/?search=" + search.Query);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var data = JsonConvert.DeserializeObject<SwapiModel>(result.Content.ReadAsStringAsync().Result);
                        retData.SwPeople = new ResponseModel()
                        {
                            Data = data.Results,
                            Message = "Success",
                            Status = (int)result.StatusCode,
                            Total = data.Count
                        };
                    }
                    else
                    {
                        retData.SwPeople = new ResponseModel()
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
            return new ResponseModel()
            {
                Data = retData,
                Message = "Success",
                Status = 200
            };
        }
    }
}
