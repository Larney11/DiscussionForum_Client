using System;

namespace Forum_Client.Models
{
    // a user post in a forum
    public class UserPost
    {
        // subject title of the post
        public String Subject { get; set; }

        // content of the post
        public String Message { get; set; }
    }


    // a post made in a forum with assigned ID
    public class ForumPost
    {
        public int ID { get; set; }

        // date and time sent
        public DateTime Timestamp { get; set; }
        public UserPost UserPost { get; set; }

        public override string ToString()
        {
            return "ID: " + ID + " Subject: " + UserPost.Subject + " Message: " + UserPost.Message + " Timestamp: " + Timestamp.ToString("dd/MM/yy H:mm:ss zzz") + "\n";
        }
    }
}