namespace UserManagment.API.Models.ExternalDataModels
{
    public class PostsModel
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public List<CommentsModel> Comments { get; set; }
    }
}
