using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories;

public class SubjectRepository : IRepository<ISubject>
{
    private List<ISubject> subjects;

    public SubjectRepository()
    {
        subjects = new List<ISubject>();
    }

    public IReadOnlyCollection<ISubject> Models => subjects;

    public void AddModel(ISubject model) => subjects.Add(model);

    public ISubject FindById(int id) => subjects.FirstOrDefault(s => s.Id == id);

    public ISubject FindByName(string name) => subjects.FirstOrDefault(s => s.Name == name);
}