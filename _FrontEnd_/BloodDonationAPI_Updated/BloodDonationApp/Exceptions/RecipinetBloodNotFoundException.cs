using System.Runtime.Serialization;

namespace Job_Portal_API.Exceptions
{
    [Serializable]
    public class RecipinetBloodNotFoundException : Exception
    {
        public string message;
        public RecipinetBloodNotFoundException()
        {
            message = "Recipient requied blood type is  not found";
        }

        public RecipinetBloodNotFoundException(string? message)
        {
            this.message = message;
        }

        public override string Message => message;
    }
}