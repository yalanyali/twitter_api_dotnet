using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterProject.Models
{
    public class Tweet
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Body { get; set; }
    }
}