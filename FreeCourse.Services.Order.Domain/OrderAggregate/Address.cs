using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class Address:ValueObject
    {
        // Entityleri ulaşılabilir yapar.
        public Address(string province, string district, string street, string zipCode, string line)
        {
            Province = province;
            District = district;
            Street = street;
            ZipCode = zipCode;
            Line = line;
        }

        public string Province { get;private set; }
        public string District { get;private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }

        public string Line { get;private set; }

        // Direkt olarak veriyi döndürür. Ya da bussines ile döndürebilirsin aşşağıdaki setzipcode örneğindeki gibi
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Province;
            yield return District;
            yield return Street;
            yield return ZipCode;
            yield return Line;
        }


        // Geleneksel mimariden farklı olarak burada bussines kodu yazabiliriz. Örnek olarak belirli şartları sağlayanı return ederek set edebilir. Veritabanına veriyi set ederken
        // Büyük harfe dönüştürüp öyle kaydedebiliriz.
        public void SetZipCode(string zipCode)
        {
            // Business Code

            ZipCode = zipCode;
        }
    }
}
