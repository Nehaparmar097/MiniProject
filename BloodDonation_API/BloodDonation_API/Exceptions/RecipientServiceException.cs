using System.Runtime.Serialization;

namespace BloodDonationApp.Exceptions
{
    [Serializable]
    public class RecipientServiceException : Exception
    {
        public string message;
        public RecipientServiceException(Exception ex)
        {
            message = "this recipient service has some error";
        }

        public RecipientServiceException(string? message, Exception ex)
        {
            this.message = message;
        }

        public override string Message => message;
    }
}