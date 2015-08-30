namespace jcEHL.ConsoleTest.TestClasses {
    public class Person : BaseEHLItem<Person> {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public Person(Person baseObject) : base(baseObject) { }

        public Person() : base(null) { }
    }
}