using System;
using System.Collections.Generic;
using System.IO;
using jcEHL.ConsoleTest.TestClasses;
using jcEHL.JCON;
using Newtonsoft.Json;

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

        public static void JCONvsBSONvsJSONListSizeTest() {
            var person = new Person {
                FirstName = "Jojo",
                LastName = "Doey"
            };

            var tmp = new List<Person>();

            for (var x = 0; x < 100; x++) {
                tmp.Add(person);
            }

            var json = JsonConvert.SerializeObject(tmp);
            var jcon = JCONConvert.SerializeObject(tmp);

            Console.WriteLine($"Size of JSON {json.Length}");

            Console.WriteLine($"Size of JCON {jcon.Length}");

            using (var sw = new StreamWriter("jcon.txt")) {
                sw.Write(jcon);
            }
            
            var jconList = JCONConvert.DeserializeObject<List<Person>>(jcon);

            using (var sw = new StreamWriter("json.txt")) {
                foreach (var item in jconList) {
                    Console.WriteLine(item.FirstName + " " + item.LastName);
                }
            }
        }

        public static void JCONTests() {
            var person = new Person {
                FirstName = "Jojo",
                LastName = "Doey"
            };

            var jconString = person.ToJCON();

            Console.WriteLine($"Size of JCON {jconString.Length}");

            var jconObj = JCONConvert.DeserializeObject<Person>(jconString);

            Console.WriteLine($"FirstName: {jconObj.FirstName}");
            Console.WriteLine($"LastName: {jconObj.LastName}");

            var content = new List<Person>();

            for (var x = 0; x < 1000; x++) {
                content.Add(new Person { FirstName = x.ToString(), LastName = (x * x).ToString() });
            }

            var serialized = JCONConvert.SerializeObject(content);

            Console.WriteLine($"Size of JCON {serialized.Length}");
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

        public static void BSONvsJSONvsJCONSpeedTest() {
            var content = new List<Person>();

            for (var x = 0; x < 1000; x++) {
                content.Add(new Person { FirstName = x.ToString(), LastName = (x * x).ToString() });
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

            start = DateTime.Now;

            foreach (var item in content) {
                item.ToJCON();
            }

            Console.WriteLine($"Time to JCON: {DateTime.Now.Subtract(start).TotalSeconds}");
        }

        static void Main(string[] args) {
            Console.WriteLine("1> Copy Test");
            Console.WriteLine("2> BSON vs JSON (Size)");
            Console.WriteLine("3> BSON vs JSON (Speed)");
            Console.WriteLine("4> BSON vs JSON (Compressed vs Uncompressed)");
            Console.WriteLine("5> JCON vs BSON vs JSON (Size)");
            Console.WriteLine("6> Object to JCON and string to JCON");
            Console.WriteLine("7> JSON vs JCON List Size");
            Console.WriteLine("8> Quit");

            var inputStr = Console.ReadLine();

            switch (Convert.ToInt32(inputStr)) {
                case 1:
                    CopyTest();
                    break;
                case 2:
                    BSONvsJSONSizeTest();
                    break;
                case 3:
                    BSONvsJSONvsJCONSpeedTest();
                    break;
                case 4:
                    BSONvsJSONCompressSizeTest();
                    break;
                case 5:
                    JCONvsBSONvsJSONSizeTest();
                    break;
                case 6:
                    JCONTests();
                    break;
                case 7:
                    JCONvsBSONvsJSONListSizeTest();
                    break;
                case 8:
                    break;
            }

            Console.ReadKey();
        }
    }
}