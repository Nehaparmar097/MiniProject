using System.Runtime.Serialization;

namespace Job_Portal_API.Exceptions
{
    [Serializable]
    public class BloodDonationNotFoundException : Exception
    {
        public string message;
        public BloodDonationNotFoundException()
        {
            message = "Donation does not Exist";
        }

        public BloodDonationNotFoundException(string? message)
        {
            this.message = message;
        }
        public override string Message => message;


    }
}