using System.Runtime.Serialization;

namespace Job_Portal_API.Exceptions
{
    [Serializable]
    public class BloodStockNotFoundException : Exception
    {
        public string mesg;
        public BloodStockNotFoundException()
        {
            mesg = "Blood Stock" +
                " Not Found";
        }

        public BloodStockNotFoundException(string? message) 
        {
            mesg = message;
        }

        public override string Message => mesg; 
    }
}