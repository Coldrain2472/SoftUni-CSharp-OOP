using Handball.Models.Contracts;
using Handball.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Repositories
{
    public class TeamRepository : IRepository<ITeam>
    {
        private readonly List<ITeam> teams;

        public TeamRepository()
        {
            teams = new List<ITeam>();
        }

        public IReadOnlyCollection<ITeam> Models => teams;

        public void AddModel(ITeam model)=>teams.Add(model);

        public bool ExistsModel(string name)=>teams.Any(t=>t.Name == name); 

        public ITeam GetModel(string name)=>teams.FirstOrDefault(t => t.Name == name);  

        public bool RemoveModel(string name)
        {
             ITeam team = teams.FirstOrDefault(t => t.Name == name);

            if (team != null)
            {
                teams.Remove(team);
                return true;
            }

            return false;
        }
    }
}
