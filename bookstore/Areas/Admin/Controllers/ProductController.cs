using System.Reflection;
using bookstore.Data;
using bookstore.Utility;
using bookStore.Business.Services;
using bookStore.Business.Services.IServices;
using bookStore.Models;
using bookStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bookstore.Areas.Customer.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.RoleAdmin)]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }
        
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<IActionResult> Upsert(int? id)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            ProductVM productVM = new()
            {
                CategoryList = categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = await _productService.GetProductByIdAsync(id.Value);
                return View(productVM);
            }
        }

        [Authorize(Roles = SD.RoleAdmin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Upsert")]
        public async Task<IActionResult> UpsertPost(ProductVM productVM, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine("images", "products");
                    string finalPath = Path.Combine(wwwRootPath, productPath);

                    if (!Directory.Exists(finalPath))
                    {
                        Directory.CreateDirectory(finalPath);
                    }

                    using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = Path.Combine(@"\", productPath, fileName).Replace("\\", "/");

                }

                if (productVM.Product.Id == null || productVM.Product.Id == 0)
                {
                    await _productService.createProductAsync(productVM.Product);
                }
                else
                {
                    await _productService.updateProductAsync(productVM.Product);
                }


                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                var categories = await _categoryService.GetAllCategoriesAsync();

                productVM = new()
                {
                    CategoryList = categories.Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }),
                    Product = new Product()
                };
                return View(productVM);
            }
        }

        #region API CALLS
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync(true);
            return Json(new { data = products });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return Json(new { success = false, message = "Invalid Id" });
            }

            var productTobeDeleted = await _productService.GetProductByIdAsync(id);
            if (productTobeDeleted == null)
            {
                return Json(new { success = false, message = "Invalid Id" });
            }

            //delete the product image
            if (!string.IsNullOrEmpty(productTobeDeleted.ImageUrl))
            {
                string imageUrl = productTobeDeleted.ImageUrl.TrimStart('/', '\\').Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);

                string imagePath = Path.Combine(
                    _webHostEnvironment.WebRootPath,
                    imageUrl
                );

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            await _productService.deleteProductAsync(id);
            return Json(new { success = false, message = "Delete Successful" });

        }

        #endregion
    }
}