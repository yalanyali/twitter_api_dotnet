using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TwitterProject.Models;
using TweetSharp;


namespace TwitterProject.Controllers
{
    public static class GlobalTweetService
    {
        public static TwitterService twitterService = new TwitterService("**Consumer Key **", "**Consumer Secret**", "**Access Token**", "**Access Token Secret**");
        public static IEnumerable<TwitterStatus> tweets;
    }

    public class AllowJsonGetAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var jsonResult = filterContext.Result as JsonResult;

            if (jsonResult == null)
                throw new ArgumentException("Action does not return a JsonResult, attribute AllowJsonGet is not allowed");

            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            base.OnResultExecuting(filterContext);
        }
    }

    public class TweetController : Controller
    {
        private DenizContext db = new DenizContext();


        public ActionResult Index()
        {
            return View(db.Tweets.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tweet tweet = db.Tweets.Find(id);
            if (tweet == null)
            {
                return HttpNotFound();
            }
            return View(tweet);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Create([Bind(Include="ID,Username,Body")] Tweet tweet)
        {
            if (ModelState.IsValid)
            {
                db.Tweets.Add(tweet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tweet);
        }

 
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tweet tweet = db.Tweets.Find(id);
            if (tweet == null)
            {
                return HttpNotFound();
            }
            return View(tweet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Username,Body")] Tweet tweet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tweet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tweet);
        }

 
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tweet tweet = db.Tweets.Find(id);
            if (tweet == null)
            {
                return HttpNotFound();
            }
            return View(tweet);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tweet tweet = db.Tweets.Find(id);
            db.Tweets.Remove(tweet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult GetTweets(string uname)
        {
            try
            {
                GlobalTweetService.tweets = GlobalTweetService.twitterService.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions() { Count = 10, ScreenName = uname });
                ViewBag.Tweets = GlobalTweetService.tweets;
                ViewBag.Username = uname;

            }
            catch (Exception)
            {
                
                return View("Hata");
            }

            return View();
        }
        
        [AllowJsonGet]
        public JsonResult GetTweetsJson(string uname)
        {
            try
            {
                GlobalTweetService.tweets = GlobalTweetService.twitterService.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions() { Count = 10, ScreenName = uname });
                ViewBag.Tweets = GlobalTweetService.tweets;
                ViewBag.Username = uname;

            }
            catch (Exception)
            {

                
            }

            List<Tweet> objeler = new List<Tweet>();
            

            foreach (var item in GlobalTweetService.tweets)
            {
                objeler.Add(new Tweet {Username = item.User.ScreenName, Body=item.Text });
            }

            return Json(objeler);
        }

        [HttpGet]
        public ActionResult SaveTweets(string uname)
        {
            try
            {
                GlobalTweetService.tweets = GlobalTweetService.twitterService.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions() { Count = 10, ScreenName = uname });
                ViewBag.Tweets = GlobalTweetService.tweets;
                ViewBag.Username = uname;

            }
            catch (Exception)
            {

                return View("Hata");
            }

            if (ModelState.IsValid)
            {
                foreach (var item in GlobalTweetService.tweets)
                {
                    db.Tweets.Add(new Tweet { Username = item.User.ScreenName, Body = item.Text });
                }
                db.SaveChanges();
                
            }

           

            return View();
        }
    }
}
