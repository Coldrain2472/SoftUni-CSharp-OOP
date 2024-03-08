using System.Collections.Generic;

namespace P03.DetailPrinter
{
    class Program
    {
        static void Main()
        {
            Employee employee = new Employee("Ivan The Programmer");
            ICollection<string> documents = new List<string>()
        {
            "Document One",
            "Document Two",
            "Document Three"
        };
            Employee manager = new Manager("Sasho The Manager", documents);

            List<Employee> employees = new List<Employee>();
            employees.Add(employee);
            employees.Add(manager);

            DetailsPrinter printer = new DetailsPrinter(employees);
            printer.PrintDetails();
        }
    }
}
