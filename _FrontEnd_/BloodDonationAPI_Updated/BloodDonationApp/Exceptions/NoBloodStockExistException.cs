
using System.Runtime.Serialization;

namespace BloodDonationApp.Exceptions
{
    [Serializable]
    public class NoBloodStockExistException : Exception
    {
        public string message;
        public NoBloodStockExistException()
        {
            message = "no such blood stock esist";
        }

        public NoBloodStockExistException(string? message)
        {
            this.message = message;
        }
        public override string Message => message;

    }
}