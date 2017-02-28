using drunk_texter.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;

namespace drunk_texter.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetMessages()
        {
            var allMessages = Message.GetMessages();
            return View(allMessages);
        }

        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message newMessage)
        {
            newMessage.Send();
            return RedirectToAction("Index");
        }
        public IActionResult GetForecast()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetForecast(string to, string city, string state)
        {
            const string fromNumber = "+19712511057";

            var weatherObs = Weather.GetForecast(city, state);
            Debug.WriteLine(weatherObs);
            var location = weatherObs.current_observation.display_location.full;
            var weather = weatherObs.current_observation.weather;
            //string weather = (string)weatherJson.SelectToken("current_observation.weather");
            //string location = (string)weatherJson.SelectToken("current_observation.display_location.full");
            string body = "The forecast in " + location + " is " + weather + ".";
            Message newMessage = new Message() { Body = body, To = to , From = fromNumber };
            Console.WriteLine(to);
            newMessage.Send();
            return View("ForecastResults", weatherObs);
        }

        public IActionResult GetCam()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetCam(string city, string state)
        {
            var camResult = Cam.GetCam(city, state);
            Console.WriteLine("TEST RESULT");
            Console.WriteLine(camResult.webcams[0].CURRENTIMAGEURL);
            Console.WriteLine("TEST RESULT");
            return View("CamResults", camResult);
        }
    }
}
