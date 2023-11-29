using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using UserManagment.Common.Models.JSONPlaceHolderModels;
using UserManagment.Common.Services.Interfaces;

namespace UserManagment.Common.Services.Implementations
{
    public class JsonPlaceHolderService: IJsonPlaceHolderService
    {
        private readonly Uri uri;
        private readonly HttpClient httpClient;

        public JsonPlaceHolderService()
        {
            uri = new Uri("https://jsonplaceholder.typicode.com/");
            httpClient = new HttpClient()
            {
                BaseAddress = uri,
            };
        }

        public async Task<List<TodosModel>> GetUsersTodo(int userId)
        {
            try
            {
                var response = await httpClient.GetAsync("todos");
                var responseString = await response.Content.ReadAsStringAsync();
                var todos = JsonConvert.DeserializeObject<List<TodosModel>>(responseString)!.Where(x => x.UserId == userId).ToList();

                if (todos.Count == 0)
                {
                    throw new Exception("No record was found");
                }

                return todos;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error: {ex.Message}");
            }
        }

        public async Task<List<AlbumsModel>> GetUsersAlbums(int userId)
        {
            try
            {
                var response = await httpClient.GetAsync("albums");
                var responseString = await response.Content.ReadAsStringAsync();
                var albums = JsonConvert.DeserializeObject<List<AlbumsModel>>(responseString)!.Where(x => x.UserId == userId).ToList();

                if (albums.Count == 0)
                {
                    throw new Exception("No record was found");
                }

                return albums;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error: {ex.Message}");
            }
        }

        public async Task<List<PostsModel>> GetUsersPosts(int userId)
        {
            try
            {
                var response = await httpClient.GetAsync("posts?_embed=comments");
                var responseString = await response.Content.ReadAsStringAsync();
                var posts = JsonConvert.DeserializeObject<List<PostsModel>>(responseString)!.Where(x => x.UserId == userId).ToList();

                if (posts.Count == 0)
                {
                    throw new Exception("No record was found");
                }

                return posts;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error: {ex.Message}");
            }
        }
    }
}
