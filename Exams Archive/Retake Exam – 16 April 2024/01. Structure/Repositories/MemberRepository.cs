﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Repositories.Contracts;

namespace TheContentDepartment.Repositories
{
    public class MemberRepository : IRepository<ITeamMember>
    {
        private readonly List<ITeamMember> teamMembers;

        public MemberRepository()
        {
            teamMembers = new List<ITeamMember>();
        }

        public IReadOnlyCollection<ITeamMember> Models => teamMembers;

        public void Add(ITeamMember model) => teamMembers.Add(model);

        public ITeamMember TakeOne(string modelName) => teamMembers.First(t => t.Name == modelName);
    }
}
