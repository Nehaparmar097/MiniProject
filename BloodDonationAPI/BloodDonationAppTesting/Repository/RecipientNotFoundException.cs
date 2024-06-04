using System.Runtime.Serialization;

namespace Job_Portal_API.Exceptions
{
    [Serializable]
    internal class RecipientNotFoundException : Exception
    {
        public string message;
        public RecipientNotFoundException()
        {
            message = "Job Seeker not found";
        }

        public RecipientNotFoundException(string? message)
        {
            this.message = message;
        }

        public override string Message => message;  
    }
}