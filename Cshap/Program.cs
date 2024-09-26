using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductManagementApp
{
    class Program
    {
        private static IProductRepository productRepository = new ProductRepository();
        private static UserRepository userRepository = new UserRepository();
        private static User currentUser;
        private static int currentId = 1; // Khai báo currentId tại đây


        static void Main(string[] args)
        {

            productRepository.LoadFromFile();

               // Thiết lập currentId dựa trên sản phẩm có id lớn nhất trong danh sách
            var products = productRepository.GetProducts();
            if (products.Any())
            {
                currentId = products.Max(p => p.Id) + 1; // Lấy giá trị lớn nhất và cộng thêm 1
            }
                
            if (!Login())
            {
                Console.WriteLine("Đăng nhập không thành công. Kết thúc chương trình.");
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Quản lý sản phẩm:");
                Console.WriteLine("1. Thêm sản phẩm");
                Console.WriteLine("2. Hiển thị danh sách sản phẩm");
                Console.WriteLine("3. Tìm kiếm sản phẩm");
                Console.WriteLine("4. Cập nhật sản phẩm");
                Console.WriteLine("5. Xóa sản phẩm");
                Console.WriteLine("6. Thoát");
                Console.Write("Chọn chức năng: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            if (currentUser.Role == UserRole.Admin) 
                            {
                                AddProduct();
                            }
                            else 
                            {
                                Console.WriteLine("Bạn không có quyền thêm sản phẩm.");
                            }
                            break;
                        case "2":
                            DisplayProducts();
                            break;
                        case "3":
                            SearchProducts();
                            break;
                        case "4":
                            if (currentUser.Role == UserRole.Admin) 
                            {
                                UpdateProduct();
                            }
                            else 
                            {
                                Console.WriteLine("Bạn không có quyền cập nhật sản phẩm.");
                            }
                            break;
                        case "5":
                            if (currentUser.Role == UserRole.Admin) 
                            {
                                DeleteProduct();
                            }
                            else 
                            {
                                Console.WriteLine("Bạn không có quyền xóa sản phẩm.");
                            }
                            break;
                        case "6":
                            return;
                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
                }
            }
        }

        private static bool Login()
        {
            Console.Write("Tên đăng nhập: ");
            string username = Console.ReadLine();
            Console.Write("Mật khẩu: ");
            string password = Console.ReadLine();

            currentUser = userRepository.Authenticate(username, password);
            return currentUser != null;
        }

        private static void AddProduct()
        {
            Console.WriteLine("Thêm sản phẩm mới:");
            Console.Write("Tên sản phẩm: ");
            string name = Console.ReadLine();

            Console.Write("Giá sản phẩm: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Số lượng: ");
            int quantity = int.Parse(Console.ReadLine());

            Product newProduct = new Product(currentId++, name, price, quantity);
            productRepository.AddProduct(newProduct);

            Console.WriteLine("Sản phẩm đã được thêm thành công!");
            Console.ReadLine();
        }

        private static void DisplayProducts()
        {
            Console.WriteLine("Danh sách sản phẩm:");
            foreach (var product in productRepository.GetProducts())
            {
                Console.WriteLine(product.ToString());
            }
            Console.ReadLine();
        }

        private static void SearchProducts()
        {
            Console.Write("Nhập từ khóa tìm kiếm: ");
            string keyword = Console.ReadLine();

            var searchResults = productRepository.SearchProducts(keyword);

            if (searchResults.Any())
            {
                foreach (var product in searchResults)
                {
                    Console.WriteLine(product.ToString());
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy sản phẩm.");
            }
            Console.ReadLine();
        }

        private static void UpdateProduct()
        {
            Console.Write("Nhập ID sản phẩm cần cập nhật: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Tên mới: ");
            string name = Console.ReadLine();

            Console.Write("Giá mới: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Số lượng mới: ");
            int quantity = int.Parse(Console.ReadLine());

            productRepository.UpdateProduct(id, name, price, quantity);
            Console.WriteLine("Cập nhật sản phẩm thành công!");
            Console.ReadLine();
        }

        private static void DeleteProduct()
        {
            Console.Write("Nhập ID sản phẩm cần xóa: ");
            int id = int.Parse(Console.ReadLine());

            productRepository.DeleteProduct(id);
            Console.WriteLine("Sản phẩm đã được xóa.");
            Console.ReadLine();
        }
    }
}
