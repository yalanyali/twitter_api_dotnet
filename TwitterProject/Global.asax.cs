using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TwitterProject.Models;

namespace TwitterProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //using (DenizContext db = new DenizContext())
            //{
            //    //Bu metod, eğer veritabanımız oluşturulmamış ise, oluşturulmasını sağlıyor
            //    db.Database.CreateIfNotExists();
            //}
            System.Data.Entity.Database.SetInitializer(new TwitterProject.Models.DenizInitializer());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
