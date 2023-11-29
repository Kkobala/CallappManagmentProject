namespace UserManagment.Common.Models.JSONPlaceHolderModels
{
    public class UsersModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public AddressModel Address { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public CompanyModel Company { get; set; }
    }
}
