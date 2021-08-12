using GuruSoft.Business.Interface;
using GuruSoft.Infraestructure.DTO;
using GuruSoft.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GuruSoft.UI.Controllers
{
    public class HomeController : Controller
    {
        #region Member
        private readonly ILogger<HomeController> _logger;
        private readonly IProductBusiness _business;
        #endregion

        #region Ctor
        public HomeController(ILogger<HomeController> logger,
                             IProductBusiness business)
        {
            _logger = logger;
            _business = business;
        }
        #endregion

        #region Methods

        public IActionResult Index()
        {
            ProductViewModel model = new ProductViewModel();
            List<ProductDTO> ListaProducto = _business.GetAll();
            model.ListProduct = ListaProducto;
            return View(model);
        }

        public IActionResult NewProduct() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["message"] = "Los datos ingresados esta mal diligenciados ";
                return RedirectToAction("Index");
            }
            ProductDTO productDTO = ConvertToModelDTO(model);
            bool InsertProduct = _business.Insert(productDTO);
            if (InsertProduct)
                TempData["message"] = "Se Registro el producto correctamente";
            else
                TempData["message"] = "ocurrion un error al intentar registrar el producto";
            return RedirectToAction("Index");
        }

        public IActionResult EditProduct(Guid Id)
        {
            ProductDTO product = _business.GetById(Id);
            if (product == null)
            {
                TempData["message"] = "No se encontro el producto";
                return RedirectToAction("Index");
            }
            ProductViewModel model = ConvertToModelView(product);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["message"] = "Los datos ingresados esta mal diligenciados";
                return RedirectToAction("Index");
            }
            ProductDTO product = ConvertToModelDTO(model);
            bool updateProduct = _business.Update(product);
            if (updateProduct)
                TempData["message"] = "Se actualizaron el producto correctamente";
            else
                TempData["message"] = "ocurrion un error al intentar Actualizar el producto";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteProduct(Guid Id)
        {
            bool deleteProduct = _business.Delete(Id);
            if (deleteProduct)
                TempData["message"] = "Se elimino el producto correctamente";
            else
                TempData["message"] = "ocurrion un error al intentar eliminar el producto";
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

        #region Methods Private
        private ProductDTO ConvertToModelDTO(ProductViewModel model)
        {
            if (model != null)
                return new ProductDTO()
                {
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
        private ProductViewModel ConvertToModelView(ProductDTO model)
        {
            if (model != null)
                return new ProductViewModel()
                {
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
