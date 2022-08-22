using System;
using System.Runtime.Serialization;

namespace FreeCourse.Web.Exceptions
{
    // Bu alanımız custom exception yapısıdır. 
    // Delegate ile istek yaptığımızda istek unAuthorize döndüyse eğer bu exception alanımıza yönlendirip. Buradan işlem ve yönlendirme yapacağız. 
    public class UnAuthorizeException : Exception
    {
        public UnAuthorizeException()
        {
        }

        public UnAuthorizeException(string message) : base(message)
        {
        }

        public UnAuthorizeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnAuthorizeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
