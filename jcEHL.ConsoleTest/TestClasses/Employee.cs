namespace jcEHL.ConsoleTest.TestClasses {
    public class Employee : Person {
        public int YearsEmployed { get; set; }

        public Employee() { }

        public Employee(Person baseItem) : base(baseItem) { }
    }
}