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

            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Type a new Department name:");
            string newDepartment = Console.ReadLine();
            repo.InsertDepartment(newDepartment);
            IEnumerable<Department> departments = repo.GetAllDepartments();
            foreach ( var d in departments )
            {
                Console.WriteLine(d.Name);
                Console.WriteLine(d.DepartmentID);
                Console.WriteLine();
            }

        }
    }
}
