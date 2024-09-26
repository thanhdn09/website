using System.Collections.Generic;

namespace ProductManagementApp
{
    // Interface cho quản lý sản phẩm
    public interface IProductRepository
    {
        void AddProduct(Product product);
        void UpdateProduct(int id, string name, decimal price, int quantity);
        void DeleteProduct(int id);
        List<Product> GetProducts();
        Product FindProductById(int id);
        List<Product> SearchProducts(string keyword);
        void SaveToFile();
        void LoadFromFile();
    }
}
