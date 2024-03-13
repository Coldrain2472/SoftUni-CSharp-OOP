using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class SubjectRepository : IRepository<ISubject>
    {
        private ICollection<ISubject> subjects;

        public SubjectRepository()
        {
            subjects = new List<ISubject>();
        }

        public IReadOnlyCollection<ISubject> Models => subjects as IReadOnlyCollection<ISubject>;

        public void AddModel(ISubject model)
        {
            subjects.Add(model);
        }

        public ISubject FindById(int id) => subjects.FirstOrDefault(s => s.Id == id);

        public ISubject FindByName(string name) => subjects.FirstOrDefault(s => s.Name == name);
    }
}
