using System.Runtime.Serialization;

namespace BloodDonationApp.Exceptions.RepositoryException
{

    public class UserRepositoryException : Exception
    {
        public UserRepositoryException()
        {
        }

        public UserRepositoryException(string message) : base(message)
        {
        }

        public UserRepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}