// client for DiscussionForum RESTful web service

using Forum_Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Forum_Client
{
    // test
    class Client
    {
        static async Task GetsAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:52535/");          // base URL for API Controller

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // GET ../forum/all
                    // get all posts
                    Console.WriteLine("========================\nGetAllPosts() Response:");
                    HttpResponseMessage response = await client.GetAsync("/forum/all");
                    if (response.IsSuccessStatusCode)
                    {
                        // read results 
                        var posts = await response.Content.ReadAsAsync<IEnumerable<ForumPost>>();
                        foreach (var post in posts)
                        {
                            Console.WriteLine(post);
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }


                    // GET ../forum/id/2
                    // get post whoose id is 2
                    Console.WriteLine("========================\nGetPostById(2) Response:");
                    try
                    {
                        response = await client.GetAsync("/forum/id/2");

                        response.EnsureSuccessStatusCode();                         // throw exception if not success
                        var post = await response.Content.ReadAsAsync<ForumPost>();
                        Console.WriteLine(post);
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    // GET ../forum/last/3
                    // get last 3 posts
                    Console.WriteLine("========================\nGetLastPosts(3) Response:");
                    try
                    {
                        response = await client.GetAsync("/forum/last/3");

                        response.EnsureSuccessStatusCode();                         // throw exception if not success
                        var posts = await response.Content.ReadAsAsync<IEnumerable<ForumPost>>();
                        foreach (var post in posts)
                        {
                            Console.WriteLine(post);
                        }
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // add a post
        static async Task AddAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:52535/");
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // POST /forum with a user post serialised in request body
                    UserPost post = new UserPost() { Subject = "new post", Message = "new message" };
                    HttpResponseMessage response = await client.PostAsJsonAsync("forum", post);
                    if (response.IsSuccessStatusCode)
                    {
                        Uri postURI = response.Headers.Location;
                        var fp = await response.Content.ReadAsAsync<ForumPost>();
                        Console.WriteLine("URI for new resource: " + postURI.ToString());
                        Console.WriteLine(fp);
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        static void Main(string[] args)
        {
            AddAsync().Wait();
            AddAsync().Wait();
            AddAsync().Wait();
            AddAsync().Wait();
            AddAsync().Wait();

            GetsAsync().Wait();

            Console.ReadLine();
        }
    }
}