using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TwitterProject.Models
{
    public class DenizInitializer:DropCreateDatabaseIfModelChanges<DenizContext>
    {
        protected override void Seed(DenizContext context)
        {
            var varTweets = new List<Tweet>
            {
                new Tweet{Username="Raphael", Body="Bu Twitter ne işe yarıyor çocuklar?"},
                new Tweet{Username="Donatello", Body="Leonardo di Caprio oscar alacak mı acaba?"},
                new Tweet{Username="Leonardo", Body="Arkadaşlarla espresso keyfi..."},
                new Tweet{Username="Michelangelo", Body="Pizza en sevdiğim yemektir."},
                new Tweet{Username="Deniz", Body="Denemeler Dünyası"}
            };

            varTweets.ForEach(s=> context.Tweets.Add(s));
            context.SaveChanges();

        }
    }
}