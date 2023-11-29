using UserManagment.Common.Models.JSONPlaceHolderModels;

namespace UserManagment.Common.Services.Interfaces
{
    public interface IJsonPlaceHolderService
    {
        Task<List<TodosModel>> GetUsersTodo(int userId);
        Task<List<AlbumsModel>> GetUsersAlbums(int userId);
        Task<List<PostsModel>> GetUsersPosts(int userId);
    }
}
