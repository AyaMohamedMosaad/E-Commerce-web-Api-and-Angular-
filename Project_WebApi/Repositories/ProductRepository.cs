using Microsoft.EntityFrameworkCore;
using Project_WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using Project_WebApi.DTO;
using System;

namespace Project_WebApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        Entity db;
        public ProductRepository(Entity _db)
        {
            db = _db;
        }

        public List<ProductDTO> DisplyAppProducts()
        {
            List<ProductDTO> productDTOList = new List<ProductDTO>();
            List<Product> proList = db.Products.Include(C=>C.category).ToList();


            foreach(var item in proList)
            {
                ProductDTO productDTO = new ProductDTO();
                productDTO.productId = item.ProductID;
                productDTO.productName = item.ProductName;
                productDTO.productPrice = item.ProductPrice;
                productDTO.productDiscount = item.ProductDiscount;
                productDTO.productCategory = item.category.CategoryName;
                productDTO.productImage = item.ProductImage; 
                productDTO.Details = item.Details;

                productDTOList.Add(productDTO);
            }


            return(productDTOList);
        }

        public ProductDTO DisplayProductByID(int ProductId)
        {
            Product product = db.Products.Include(C=>C.category).FirstOrDefault(P => P.ProductID == ProductId);
            ProductDTO productDTO = new ProductDTO();
            productDTO.productId = product.ProductID;
            productDTO.productName = product.ProductName;
            productDTO.productPrice = product.ProductPrice;
            productDTO.productDiscount = product.ProductDiscount;
            productDTO.productCategory = product.category.CategoryName;
            productDTO.productImage = product.ProductImage;
            productDTO.Details = product.Details;

            return (productDTO);
        }


        public int AddProduct(ProductDTO NewProduct)
        {


            if (NewProduct.productName != null)
            {
                Product pro = new Product();

                pro.ProductName = NewProduct.productName;
                pro.ProductPrice = NewProduct.productPrice;
                pro.ProductDiscount = NewProduct.productDiscount;
                pro.ProductImage = NewProduct.productImage;
                //pro.category.CategoryName = NewProduct.productCategory;
                pro.CategoryId = NewProduct.categoryId;
                pro.Details = NewProduct.Details;

                db.Products.Add(pro);
                db.SaveChanges();
            }
            return 0;
        }





        //public int AddProduct(Product NewProduct)
        //{


        //    if (NewProduct.ProductName != null)
        //    {


        //        //pro.ProductName = NewProduct.productName;
        //        //pro.ProductPrice = NewProduct.productPrice;
        //        //pro.ProductDiscount = NewProduct.productDiscount;
        //        //pro.category.CategoryName = NewProduct.productCategory;
        //        //pro.CategoryId = NewProduct.categoryId;



        //        db.Products.Add(NewProduct);
        //        db.SaveChanges();
        //    }
        //    return 0;
        //}










        public int EditProduct(int id, Product Newproduct)
        {
            Product oldProduct = db.Products.Include(C => C.category).FirstOrDefault(P => P.ProductID == id);
            if (oldProduct != null)
            {
                oldProduct.ProductName = Newproduct.ProductName;
                oldProduct.ProductPrice = Newproduct.ProductPrice;
                oldProduct.ProductDiscount = Newproduct.ProductDiscount;
                oldProduct.CategoryId = Newproduct.CategoryId;
                oldProduct.ProductImage = Newproduct.ProductImage;
                oldProduct.Details = Newproduct.Details;


                //oldProduct.category.CategoryName = Newproduct.category.CategoryName;
                db.SaveChanges();

            }
            return 0;

        }
        public int DeleteProduct(int ProductId)
        {
            Product product = db.Products.FirstOrDefault(P => P.ProductID == ProductId);
            if (product != null)
            {
                
                    db.Products.Remove(product);
                    db.SaveChanges();
                
               
            }
            return 0;

        }




    }
}



