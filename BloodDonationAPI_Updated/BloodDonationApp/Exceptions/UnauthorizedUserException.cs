using System.Runtime.Serialization;

namespace Job_Portal_API.Exceptions
{
    [Serializable]
    public class UnauthorizedUserException : Exception
    {
        public string message;
        public UnauthorizedUserException()
        {
            message = "unautherized user , plz login with valid user details";
        }

        public UnauthorizedUserException(string? message) 
        {
            this.message = message;
        }

        public override string Message => message;
    }
}