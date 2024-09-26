using System;

namespace ProductManagementApp
{
    // Định nghĩa lớp trừu tượng ProductBase để kế thừa
    public abstract class ProductBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public ProductBase(int id, string name, decimal price, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Tên: {Name}, Giá: {Price:C}, Số lượng: {Quantity}";
        }
    }

    // Lớp Product kế thừa từ ProductBase
    public class Product : ProductBase
    {
        public Product(int id, string name, decimal price, int quantity)
            : base(id, name, price, quantity) { }
    }
}
