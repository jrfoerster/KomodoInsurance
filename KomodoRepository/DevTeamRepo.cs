using System.Collections.Generic;
using System.Linq;

namespace KomodoRepository
{
    public class DevTeamRepo
    {
        private readonly List<DevTeam> _list = new List<DevTeam>();

        public bool Add(DevTeam devTeam)
        {
            foreach (var existingTeam in _list)
            {
                if (devTeam.ID == existingTeam.ID)
                {
                    return false;
                }
            }
            _list.Add(devTeam);
            return true;
        }

        public bool Remove(DevTeam devTeam)
        {
            return _list.Remove(devTeam);
        }

        public bool Remove(int id)
        {
            DevTeam devTeamToRemove = null;
            foreach (var devTeam in _list)
            {
                if (id == devTeam.ID)
                {
                    devTeamToRemove = devTeam;
                    break;
                }
            }
            return _list.Remove(devTeamToRemove);
        }

        public void RemoveDeveloperFromAllTeams(Developer developer)
        {
            foreach (var devTeam in _list)
            {
                devTeam.Remove(developer);
            }
        }

        public DevTeam Get(int id)
        {
            foreach (var devTeam in _list)
            {
                if (id == devTeam.ID)
                {
                    return devTeam;
                }
            }
            return null;
        }

        public List<DevTeam> GetAll()
        {
            return _list.ToList();
        }
    }
}
