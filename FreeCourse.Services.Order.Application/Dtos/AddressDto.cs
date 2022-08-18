using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Dtos
{
    //Bire bir gösterim olsa dahi DTO lar ile çalışmak best practies'tir.
    // Bugün kullanıcıları göstermek istediğimiz bir şeyi yarın göstermek istemeyebiliriz. Bugün kullanıcıdan aldığımız bir veriyi yarın almak istemeyebiliriz.
    public class AddressDto
    {
        public string Province { get; private set; }
        public string District { get; private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }

        public string Line { get; private set; }
    }
}
