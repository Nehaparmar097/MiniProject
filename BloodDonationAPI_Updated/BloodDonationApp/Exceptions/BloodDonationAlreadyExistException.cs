using System.Runtime.Serialization;

namespace Job_Portal_API.Exceptions
{
    [Serializable]
    public class BloodDonationAlreadyExistException : Exception
    {
        public string message;
        public BloodDonationAlreadyExistException()
        {
            message = " already Donated";
        }

        public BloodDonationAlreadyExistException(string? message)
        {
            this.message = message;
        }

        public override string Message => message;
    }
}