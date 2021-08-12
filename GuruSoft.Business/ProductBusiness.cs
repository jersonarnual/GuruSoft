using GuruSoft.Business.Interface;
using GuruSoft.Data.Interface;
using GuruSoft.Data.Models;
using GuruSoft.Infraestructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuruSoft.Business
{
    public class ProductBusiness : IProductBusiness
    {
        #region Members
        private readonly IDefaultRepository<Product> _repository;
        #endregion

        #region Ctor
        public ProductBusiness(IDefaultRepository<Product> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public bool Delete(Guid id)
        {
            var item = _repository.GetById(id);
            if (item != null)
                return _repository.Delete(item);
            return false;
        }

        public List<ProductDTO> GetAll()
        {
            var lista = new List<ProductDTO>();
            var items = _repository.GetAll();
            if (items != null)
            {
                foreach (var item in items)
                {
                    lista.Add(ConvertToDTO(item));
                }
            }
            return lista;
        }


        public ProductDTO GetById(Guid id)
        {
            return ConvertToDTO(_repository.GetById(id));
        }

        public bool Insert(ProductDTO entity)
        {
            return _repository.Insert(ConvertToModel(entity));
        }

        public bool Update(ProductDTO entity)
        {
            var itemExists = _repository.GetById(entity.Id);
            if (itemExists != null)
            {
                itemExists.ProductCode = entity.ProductCode;
                itemExists.ProductName = entity.ProductName;
                itemExists.Description = entity.Description;
                itemExists.Price = entity.Price;
                itemExists.Quantity = entity.Quantity;
                itemExists.Total = entity.Total;
                itemExists.UpdateTime = entity.UpdateTime;
                return _repository.Update(itemExists);
            }
            return false;
        }
        #endregion

        #region Private methods
        private Product ConvertToModel(ProductDTO model)
        {
            if (model != null)
                return new Product()
                {
                    UpdateBy = model.UpdateBy,
                    CreateBy = model.CreateBy,
                    UpdateTime = model.UpdateTime,
                    CreateTime = model.CreateTime,
                    Id = model.Id,
                    ProductCode = model.ProductCode,
                    ProductName = model.ProductName,
                    Description = model.Description,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    Total = model.Total
                };
            return null;
        }

        private ProductDTO ConvertToDTO(Product model)
        {
            if (model != null)
                return new ProductDTO()
                {
                    UpdateBy = model.UpdateBy,
                    CreateBy = model.CreateBy,
                    UpdateTime = model.UpdateTime,
                    CreateTime = model.CreateTime,
                    Id = model.Id,
                    ProductCode = model.ProductCode,
                    ProductName = model.ProductName,
                    Description = model.Description,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    Total = model.Total
                };
            return null;
        }
        #endregion
    }
}
