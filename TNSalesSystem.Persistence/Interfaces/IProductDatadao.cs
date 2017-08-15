using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNSalesSystem.Domain.Product_Details;

namespace TNSalesSystem.Persistence.Interfaces
{
    public interface IProductDatadao
    {
        bool InsertProductData(ProductDetails objPD);
    }
}
