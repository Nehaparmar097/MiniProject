using System.Runtime.Serialization;

namespace Job_Portal_API.Exceptions
{
    [Serializable]
    public class RecipientNotFoundException : Exception
    {
        public string message;
        public RecipientNotFoundException()
        {
            message = "Reciepient  not found";
        }

        public RecipientNotFoundException(string? message)
        {
            this.message = message;
        }

        public override string Message => message;  
    }
}