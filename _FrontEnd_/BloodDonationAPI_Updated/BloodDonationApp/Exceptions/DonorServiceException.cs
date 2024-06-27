using System.Runtime.Serialization;

namespace BloodDonationApp.Exceptions
{
    [Serializable]
    public class DonorServiceException : Exception
    {
        public string mesg;
        public DonorServiceException(Exception ex)
        {
            mesg = "service not available";
        }

        public DonorServiceException(string? message)
        {
            mesg = message;
        }

        public override string Message => mesg;
    }
}