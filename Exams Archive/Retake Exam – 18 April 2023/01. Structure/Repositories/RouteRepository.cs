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
        private readonly List<IRoute> routes;

        public RouteRepository()
        {
            routes = new List<IRoute>();
        }

        public void AddModel(IRoute model) => routes.Add(model);

        public IRoute FindById(string identifier)
        {
            int routeId = int.Parse(identifier);
            return routes.FirstOrDefault(r => r.RouteId == routeId);
        }

        public IReadOnlyCollection<IRoute> GetAll() => routes;

        public bool RemoveById(string identifier)
        {
            int routeId = int.Parse(identifier);
            var routeToRemove = routes.FirstOrDefault(r => r.RouteId == routeId);
            if (routeToRemove != null)
            {
                routes.Remove(routeToRemove);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
