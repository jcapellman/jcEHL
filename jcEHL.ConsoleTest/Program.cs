using System;
using System.Collections.Generic;
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

        public static void BSONvsJSONSizeTest() {
            var person = new Person {
                FirstName = "Johnathan",
                LastName = "Doey"
            };

            Console.WriteLine($"Size of JSON {person.ToJSON().Length}");

            Console.WriteLine($"Size of BSON {person.ToBSON().Length}");
        }

        public static void JCONvsBSONvsJSONSizeTest() {
            var person = new Person {
                FirstName = "Jojo",
                LastName = "Doey"
            };

            Console.WriteLine($"Size of JSON {person.ToJSON().Length}");

            Console.WriteLine($"Size of BSON {person.ToBSON().Length}");

            var jcon = person.ToJCON();

            Console.WriteLine($"Size of JCON {jcon.Length}");
            Console.WriteLine($"JCON: {jcon}");
        }

        public static void BSONvsJSONCompressSizeTest() {
            var person = new Person {
                FirstName = "Johnathan",
                LastName = "Doey"
            };

            Console.WriteLine($"Size of JSON {person.ToJSON(false).Length}");
            Console.WriteLine($"Size of JSON (Compressed) {person.ToJSON().Length}");

            Console.WriteLine($"Size of BSON {person.ToBSON(false).Length}");
            Console.WriteLine($"Size of BSON (Compressed) {person.ToBSON().Length}");
        }

        public static void BSONvsJSONSpeedTest() {
            var content = new List<Person>();

            for (var x = 0; x < 1000; x++) {
                content.Add(new Person { FirstName = x.ToString(), LastName = (x*x).ToString()});
            }

            var start = DateTime.Now;

            foreach (var item in content) {
                item.ToJSON();
            }
            
            Console.WriteLine($"Time to JSON: {DateTime.Now.Subtract(start).TotalSeconds}");

            start = DateTime.Now;

            foreach (var item in content) {
                item.ToBSON();
            }

            Console.WriteLine($"Time to BSON: {DateTime.Now.Subtract(start).TotalSeconds}");
        }
        
        static void Main(string[] args) {
            Console.WriteLine("1> Copy Test");
            Console.WriteLine("2> BSON vs JSON (Size)");
            Console.WriteLine("3> BSON vs JSON (Speed)");
            Console.WriteLine("4> BSON vs JSON (Compressed vs Uncompressed)");
            Console.WriteLine("5> JCON vs BSON vs JSON (Size)");
            Console.WriteLine("6> Quit");

            var inputStr = Console.ReadLine();
            
            switch (Convert.ToInt32(inputStr)) {
                case 1:
                    CopyTest();
                    break;
                case 2:
                    BSONvsJSONSizeTest();
                    break;
                case 3:
                    BSONvsJSONSpeedTest();
                    break;
                case 4:
                    BSONvsJSONCompressSizeTest();
                    break;
                case 5:
                    JCONvsBSONvsJSONSizeTest();
                    break;
                case 6:
                    break;
            }

            Console.ReadKey();
        }
    }
}