using System;

namespace FreeCourse.Web.Exceptions
{
    // Bu alanımız custom exception yapısıdır. 
    // Delegate ile istek yaptığımızda istek unAuthorize döndüyse eğer bu exception alanımıza yönlendirip. Buradan işlem ve yönlendirme yapacağız. 
    public class UnAuthorizeException : Exception
    {
    }
}
