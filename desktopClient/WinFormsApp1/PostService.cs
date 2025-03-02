using System.Net.Http.Headers;
using System.Text.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    internal class PostService
    {
        static HttpClient client = new HttpClient();

        public void createConnection()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:8083/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public List<Post> GetPosts()
        {
            List<Post> posts = null;
            HttpResponseMessage response = client.GetAsync("post").Result;
            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("received : " + resultString);
                posts = JsonSerializer.Deserialize<List<Post>>(resultString);
                return posts;

            }
            return null;
        }

        public List<Post> GetPendingPosts()
        {
            var allPosts = this.GetPosts();
            return allPosts.Where(p => p.status == PostStatus.PENDING).ToList();
        }

        public List<User> GetActiveUsers()
        {
            List<Post> posts = this.GetPosts();
            return posts.Select(p => p.user).Where(u => u != null).DistinctBy(c => c.name).ToList();
        }

        public List<Post> GetPostsByKeyword(string keyword)
        {
            List<Post> posts = null;

            // Trimite cererea GET către server pentru căutare
            HttpResponseMessage response = client.GetAsync($"post/search?keyword={keyword}").Result;

            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Received response: " + resultString);  // Verifică răspunsul

                posts = JsonSerializer.Deserialize<List<Post>>(resultString);

                if (posts != null && posts.Count > 0)
                {
                    Console.WriteLine("Found posts: " + posts.Count);
                }
                else
                {
                    Console.WriteLine("No posts found for the given keyword.");
                }
            }
            else
            {
                Console.WriteLine("Request failed. Status code: " + response.StatusCode);
                MessageBox.Show("Request failed.");
            }

            return posts;
        }


    }
}
