using System.Runtime.Serialization;
//exeptions
namespace Job_Portal_API.Exceptions
{
    [Serializable]
    internal class BloodDonationAlreadyExistException : Exception
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
