namespace jcEHL.ConsoleTest.TestClasses {
    public class Person : BaseEHLItem<Person> {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public Person(Person baseObject) : base(baseObject) { }

        public Person(string jconValue) : base(null) {
            Copy.InitT(this, jconValue);
        }

        public Person() : base(null) { }
    }
}