
namespace BloodDonationApp.Exceptions
{
    [Serializable]
    public class ServiceException : Exception
    {
        public string message;
        public ServiceException()
        {
            message = "service not found";
        }

        public ServiceException(string? message)
        {
            this.message = message;
        }

        public override string Message => message;
    }
}