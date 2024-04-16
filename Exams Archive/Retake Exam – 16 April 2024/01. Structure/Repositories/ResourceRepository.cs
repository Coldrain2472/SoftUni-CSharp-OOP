using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Repositories.Contracts;

namespace TheContentDepartment.Repositories
{
    public class ResourceRepository : IRepository<IResource>
    {
        private readonly List<IResource> resources;

        public ResourceRepository()
        {
            resources = new List<IResource>();
        }

        public IReadOnlyCollection<IResource> Models => resources;

        public void Add(IResource model) => resources.Add(model);

        public IResource TakeOne(string modelName) => resources.FirstOrDefault(r => r.Name == modelName);
    }
}
