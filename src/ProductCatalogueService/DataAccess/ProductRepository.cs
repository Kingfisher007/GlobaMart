﻿using System.Linq;
using System.Collections.Generic;
using ProductCatalogueService.Models;
using System;

namespace ProductCatalogueService.DataAccess
{
    internal class ProductRepository : IProductRepository
    {
        ProductDataContext ProductData;

        public ProductRepository()
        {
            ProductData = new ProductDataContext();
        }

        public bool AddProduct(Product product)
        {
            if(!ProductData.Products.Contains(product))
            {
                ProductData.Products.Add(product);
                return true;
            }
            else
            {
                return false;
            }
        }

        public IList<Product> GetAllProducts()
        {
            return ProductData.Products;
        }

        public IList<Product> GetProducts(string type)
        {
            var products = from p in ProductData.Products
                           where p.Type.ToLower().Contains(type.ToLower())
                           select p;

            return products.ToList<Product>();
        }

        public IList<Product> GetProducts(string name, string type)
        {
            var products = from p in ProductData.Products
                           where p.Name.ToLower().Contains(name.ToLower()) && 
                                 (type.ToLower().Equals("all") || (!type.ToLower().Equals("all") && p.Type.ToLower().Contains(type.ToLower())))
                           select p;

            return products.ToList<Product>();
        }

        public IList<string> GetProductsType()
        {
            var types = from p in ProductData.Products
                        select p.Type;
            return types.Distinct().ToList();
        }
    }
}