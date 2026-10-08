using bookStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace bookStore.Business.Services.IServices
{
    public interface IProductService
    {
        Task<Product?> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync(bool includeCategory=false);
        Task<Product> createProductAsync(Product product);
        Task updateProductAsync(Product product);
        Task deleteProductAsync(int id);

    }
}
