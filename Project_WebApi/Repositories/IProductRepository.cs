using Project_WebApi.Models;
using System.Collections.Generic;

using Project_WebApi.DTO;

namespace Project_WebApi.Repositories
{
    public interface IProductRepository
    {
        int AddProduct(ProductDTO NewProduct);
       // int AddProduct(Product NewProduct);

        int DeleteProduct(int ProductId);
        ProductDTO DisplayProductByID(int ProductId);
        List<ProductDTO> DisplyAppProducts();
        int EditProduct(int id, Product Newproduct);
    }
}