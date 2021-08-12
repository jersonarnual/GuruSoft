using GuruSoft.Infraestructure.DTO;
using System;
using System.Collections.Generic;

namespace GuruSoft.Business.Interface
{
    public interface IProductBusiness
    {
        List<ProductDTO> GetAll();
        ProductDTO GetById(Guid id);
        bool Insert(ProductDTO entity);
        bool Update(ProductDTO entity);
        bool Delete(Guid entity);
    }
}
