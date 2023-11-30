namespace UserManagment.API.Models.ExternalDataModels
{
    public class TodosModel
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
    }
}
