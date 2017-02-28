using drunk_texter.Models.CamHelper;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace drunk_texter.Models
{
    [JsonObject]
    public class Cam
    {
        public static Models.CamHelper.CamResponse GetCam(string city, string state)
        {
            RestClient client = new RestClient("http://api.wunderground.com/api/");
            RestRequest request = new RestRequest(EnvironmentVariables.WeatherAPIKey + "/webcams/q/" + state + "/" + city + ".json", Method.GET);
            RestResponse response = new RestResponse();

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();

            Models.CamHelper.CamResponse jsonResponse = JsonConvert.DeserializeObject<Models.CamHelper.CamResponse>(response.Content);
            return jsonResponse;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            TaskCompletionSource<IRestResponse> tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response =>
            {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }


    }
}
