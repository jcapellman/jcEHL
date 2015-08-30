using System;

using jcEHL.ConsoleTest.TestClasses;

namespace jcEHL.ConsoleTest {
    public class Program {
        public static void CopyTest() {
            var person = new Person {
                FirstName = "John",
                LastName = "Doe"
            };

            var employee = new Employee(person) { YearsEmployed = 10 };

            Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.YearsEmployed}");
        }

        static void Main(string[] args) {
            Console.WriteLine("1> Copy Test");

            switch (Convert.ToInt32(Console.ReadKey())) {
                case 1:
                    CopyTest();
                    break;
                case 2:
                    break;
            }

            Console.ReadKey();
        }
    }
}