namespace jcEHL.ConsoleTest.TestClasses {
    public class Person {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public Person() {  }

        public Person(Person basePerson) {
            Copy.Init(basePerson, this);
        }
    }
}