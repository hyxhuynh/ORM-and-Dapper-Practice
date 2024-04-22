using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ORM_and_Dapper_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);


            // Departments
            DapperDepartmentRepository departmentRepo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Type a new Department name:");
            string newDepartment = Console.ReadLine();
            departmentRepo.InsertDepartment(newDepartment);
            IEnumerable<Department> departments = departmentRepo.GetAllDepartments();
            foreach ( var d in departments )
            {
                Console.WriteLine(d.Name);
                Console.WriteLine(d.DepartmentID);
                Console.WriteLine();
            }

            // Products
            DapperProductRepository productRepo = new DapperProductRepository(conn);
            Console.WriteLine("Type a new Product name:");
            string inputProductName = Console.ReadLine();
            Console.WriteLine("Type a new Product price:");
            double inputProductPrice = double.Parse(Console.ReadLine());
            Console.WriteLine("Type a new Product ID:");
            int inputProductID = int.Parse(Console.ReadLine());
            Console.WriteLine("Type a new Category ID");
            int inputCategoryID = int.Parse(Console.ReadLine());

            productRepo.InsertProduct(inputProductName, inputProductPrice, inputProductID, inputCategoryID);
            IEnumerable<Product> products = productRepo.GetAllProducts();
            foreach (var p in products)
            {
                Console.WriteLine(p.Name);
                Console.WriteLine(p.Price);
                Console.WriteLine(p.ProductID);
                Console.WriteLine(p.CategoryID);
                Console.WriteLine();
            }

        }
    }
}
