using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace VotingApp_Net.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache _distributedCache;

        public HomeController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Vote()
        {
            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Choice(string vote)
        {
            ViewData["Vote"] = vote;
            var voterID = Guid.NewGuid();
            var co = ConfigurationOptions.Parse("redis:6379");
            co.ResolveDns = true;
            var redis = await ConnectionMultiplexer.ConnectAsync(co);

            string json = string.Format("{{\"voter_id\": \"{0}\", 'vote': \"{1}\"}}", voterID, vote);
            IDatabase db = redis.GetDatabase();
            db.ListLeftPush("votes", json);
            return View("Index");
        }
    }
}
