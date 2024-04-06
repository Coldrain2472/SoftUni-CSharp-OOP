using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Repositories
{
    public class InfluencerRepository : IRepository<IInfluencer>
    {
        private readonly List<IInfluencer> influencers;

        public InfluencerRepository()
        {
            influencers = new List<IInfluencer>();
        }

        public IReadOnlyCollection<IInfluencer> Models => influencers;

        public void AddModel(IInfluencer model) => influencers.Add(model);

        public IInfluencer FindByName(string name) => influencers.FirstOrDefault(i => i.Username == name);

        public bool RemoveModel(IInfluencer model) => influencers.Remove(model);
    }
}
