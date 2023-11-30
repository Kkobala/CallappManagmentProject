using UserManagment.API.Models.ExternalDataModels;

namespace UserManagment.API.Services.Interfaces
{
    public interface IExternalDataService
    {
        Task<List<TodosModel>> GetUsersTodo(int userId);
        Task<List<AlbumsModel>> GetUsersAlbums(int userId);
        Task<List<PostsModel>> GetUsersPosts(int userId);
    }
}
