namespace UserManagment.Common.Requests
{
    public class CreateUserProfileRequest
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
    }
}
