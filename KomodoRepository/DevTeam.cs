using System;
using System.Collections.Generic;
using System.Linq;

namespace KomodoRepository
{
    public class DevTeam
    {
        public string Name { get; set; }
        public int ID { get; }

        private readonly ISet<Developer> _members = new HashSet<Developer>();

        public DevTeam(string name, int id)
        {
            Name = name;
            ID = id;
        }

        public bool Add(Developer developer)
        {
            if (developer == null)
            {
                return false;
            }
            return _members.Add(developer);
        }

        public bool Remove(Developer developer)
        {
            return _members.Remove(developer);
        }

        public List<Developer> GetDevelopers()
        {
            return _members.ToList();
        }
    }
}