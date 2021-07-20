using System.Collections.Generic;
using System.Linq;

namespace KomodoRepository
{
    public class DeveloperRepo
    {
        private readonly IDictionary<int, Developer> dictionary = new Dictionary<int, Developer>();

        public bool Add(Developer developer)
        {
            int key = developer.ID;
            if (dictionary.ContainsKey(key))
            {
                return false;
            }
            else
            {
                dictionary.Add(key, developer);
                return true;
            }
        }

        public bool Remove(Developer developer)
        {
            return dictionary.Remove(developer.ID);
        }

        public bool Remove(int id)
        {
            return dictionary.Remove(id);
        }

        public Developer Get(int id)
        {
            //// This will check if the key exists twice
            //if (dictionary.ContainsKey(id))
            //{
            //    return dictionary[id];
            //}
            //else
            //{
            //    return null;
            //}

            if (dictionary.TryGetValue(id, out var developer))
            {
                return developer;
            }
            else
            {
                return null;
            }
        }

        public List<Developer> GetAll()
        {
            return dictionary.Values.ToList();
        }

        public List<Developer> GetAllDevelopersWithoutPluralSight()
        {
            var list = new List<Developer>();

            foreach (var developer in dictionary.Values)
            {
                if (developer.HasPluralsight == false)
                {
                    list.Add(developer);
                }
            }

            return list;
        }
    }
}
