using System.Text.RegularExpressions;
using UserManagment.Common.Validations.Interfaces;

namespace UserManagment.Common.Validations.Implementations
{
    public class UserValidations: IUserValidations
    {
        public void ValidateEmailAddress(string emailAddress)
        {
            if (!Regex.IsMatch(emailAddress, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new Exception("Please enter a valid email address.");
            }
        }

        public void CheckPersonalNumberFormat(string personalNumber)
        {
            if (Regex.IsMatch(personalNumber, @"[A-Za-z]") || Regex.IsMatch(personalNumber, @"^(?=.*[\W_]).+$"))
            {
                throw new Exception("Private Number must contain only numbers");
            }

            if (!Regex.IsMatch(personalNumber, @"^(?=.*[0-9]).+$") || personalNumber.Length != 11)
            {
                throw new Exception("Invalid Private number format");
            }
        }
    }
}
