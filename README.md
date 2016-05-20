# twitter_api_dotnet
Basic Twitter client written in .Net Mvc 5 <br>

<b>Includes:</b> <br>
Tweetsharp (unofficial): https://www.nuget.org/packages/TweetSharp-Unofficial/<br>
Bootstrap: http://getbootstrap.com/<br>
Entity Framework<br><br>
 Use your own Api Key by changing this GlobalTweetService class: 

    public static class GlobalTweetService
    {
        public static TwitterService twitterService = new TwitterService("**Consumer Key **", "**Consumer Secret**", "**Access Token**", "**Access Token Secret**");
        public static IEnumerable<TwitterStatus> tweets;
    }

Contact: dnzyslrmk@gmail.com


