using System;

namespace KomodoRepository
{
    public class Developer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ID { get; }
        public bool HasPluralsight { get; set; }

        public Developer(string firstName, string lastName, int id)
        {
            FirstName = firstName;
            LastName = lastName;
            ID = id;
        }

        public Developer(string firstName, string lastName, int id, bool hasPluralSight)
            : this(firstName, lastName, id)
        {
            HasPluralsight = hasPluralSight;
        }
    }
}
