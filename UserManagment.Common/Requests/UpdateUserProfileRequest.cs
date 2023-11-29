namespace UserManagment.Common.Requests
{
    public class UpdateUserProfileRequest
    {
        //public int UserId { get; set; }
        public string OldFirstName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
    }
}
