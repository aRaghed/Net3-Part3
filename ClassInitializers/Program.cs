using System;

namespace ClassInitializers
{
    internal class Program
    {
        private static void Main()
        {
            #region Traditional method

            var personTraditionally = new PersonTraditionally { FirstName = "Lennart", LastName = "Jansson" };
            personTraditionally.LastName = "Persson";

            #endregion Traditional method

            #region ClassInitializer

            var personInit = new PersonInit { FirstName = "Lennart", LastName = "Jansson" };

            //personInit.LastName = "Persson"; // ERROR!

            #endregion ClassInitializer

            #region Record

            var personRecord = new PersonRecord { FirstName = "Lennart", LastName = "Jansson" };
            var anotherPerson = personRecord with { LastName = "Persson" };
            var originalPerson = anotherPerson with { LastName = "Jansson" };
            if (personRecord == originalPerson)
            {
            }
            /* We would now have ReferenceEquals(personRecord, originalPerson) = false (they aren’t the same object)
             * but Equals(personRecord, originalPerson) = true (they have the same value).
             * Along with the value-based Equals there’s also a value-based GetHashCode() override to go along with it.
             * Additionally, records implement IEquatable<T> and overload the == and != operators,
             * so that the value-based behavior shows up consistently across all those different equality mechanisms.
             * Records can inherit from other records
             */
            PersonRecord student = new StudentRecord { FirstName = "Lennart", LastName = "Jansson", Id = 129 };
            var otherStudent = student with { LastName = "Persson" };
            Console.WriteLine(otherStudent is StudentRecord);

            #endregion Record
        }
    }

    public class PersonTraditionally
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class PersonInit
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }
    }

    public class PersonReadonly
    {
        private readonly string firstName = "<unknown>";
        private readonly string lastName = "<unknown>";

        public string FirstName
        {
            get => firstName;
            init => firstName = (value ?? throw new ArgumentNullException(nameof(FirstName)));
        }

        public string LastName
        {
            get => lastName;
            init => lastName = (value ?? throw new ArgumentNullException(nameof(LastName)));
        }
    }

    public record PersonRecord
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }

    public record StudentRecord : PersonRecord
    {
        public int Id;
    }
}