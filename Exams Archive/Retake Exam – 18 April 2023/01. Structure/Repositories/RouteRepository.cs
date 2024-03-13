using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Repositories
{
    public class RouteRepository : IRepository<IRoute>
    {
        private List<IRoute> routes;

        public void AddModel(IRoute model)
        {
            routes.Add(model);
        }

        public IRoute FindById(string identifier)
        {
            IRoute route = routes.FirstOrDefault(r => r.RouteId == int.Parse(identifier));
            if (route != null)
            {
                return route;
            }

            return null;
        }

        public IReadOnlyCollection<IRoute> GetAll() => routes.AsReadOnly();

        public bool RemoveById(string identifier)
        {
            IRoute route = routes.FirstOrDefault(r => r.RouteId == int.Parse(identifier));
            if (route != null)
            {
                routes.Remove(route);
                return true;
            }

            return false;
        }
    }
}
