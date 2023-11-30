namespace UserManagment.Common.Validations.Interfaces
{
    public interface IUserValidations
    {
        void ValidateEmailAddress(string emailAddress);
        void CheckPersonalNumberFormat(string personalNumber);
    }
}
