using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ProductManagementApp
{
    // Lớp quản lý sản phẩm và lưu trữ danh sách sản phẩm
    public class ProductRepository : IProductRepository
    {
        private List<Product> products = new List<Product>();
        private static string filePath = "products.json";

        public void AddProduct(Product product)
        {
            products.Add(product);
            SaveToFile();
        }

        public void UpdateProduct(int id, string name, decimal price, int quantity)
        {
            var product = FindProductById(id);
            if (product != null)
            {
                product.Name = name;
                product.Price = price;
                product.Quantity = quantity;
                SaveToFile();
            }
            else
            {
                throw new Exception("Không tìm thấy sản phẩm.");
            }
        }

        public void DeleteProduct(int id)
        {
            var product = FindProductById(id);
            if (product != null)
            {
                products.Remove(product);
                SaveToFile();
            }
            else
            {
                throw new Exception("Không tìm thấy sản phẩm.");
            }
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public Product FindProductById(int id)
        {
            return products.SingleOrDefault(p => p.Id == id);
        }

        public List<Product> SearchProducts(string keyword)
        {
            return products.Where(p => p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void SaveToFile()
        {
            var json = JsonConvert.SerializeObject(products, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public void LoadFromFile()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                products = JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
            }
        }
    }
}
