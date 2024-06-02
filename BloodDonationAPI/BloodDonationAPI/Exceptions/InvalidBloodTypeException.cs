using System.Runtime.Serialization;

namespace Job_Portal_API.Exceptions
{
    [Serializable]
    internal class InvalidBloodTypeException : Exception
    {
        public string message;
        public InvalidBloodTypeException()
        {
            message = "Invalid Blood Type";
        }

        public InvalidBloodTypeException(string? message) 
        {
            this.message= message;  
        }

       public override string Message => message;
    }
}