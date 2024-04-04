using Handball.Models.Contracts;
using Handball.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Repositories
{
    public class PlayerRepository : IRepository<IPlayer>
    {
        private readonly List<IPlayer> players;

        public PlayerRepository()
        {
            players = new List<IPlayer>();
        }

        public IReadOnlyCollection<IPlayer> Models => players;

        public void AddModel(IPlayer model)=>players.Add(model);

        public bool ExistsModel(string name)=>players.Any(p=>p.Name==name);

        public IPlayer GetModel(string name)=>players.FirstOrDefault(p=>p.Name == name);

        public bool RemoveModel(string name)
        {
             IPlayer player = players.FirstOrDefault(p => p.Name == name);

            if (player != null)
            {
                players.Remove(player);
                return true;
            }

            return false;
        }
    }
}
