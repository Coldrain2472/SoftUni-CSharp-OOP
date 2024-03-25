using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Repositories;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private SubjectRepository subjects;
        private StudentRepository students;
        private UniversityRepository universities;

        public Controller()
        {
            subjects = new SubjectRepository();
            students = new StudentRepository();
            universities = new UniversityRepository();
        }

        public string AddStudent(string firstName, string lastName)
        {
            if (students.FindByName($"{firstName} {lastName}") != null)
            {
                return string.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
            }

            students.AddModel(new Student(students.Models.Count + 1, firstName, lastName));
            return string.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName, students.GetType().Name);
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            if (subjectType != nameof(TechnicalSubject) &&
                    subjectType != nameof(EconomicalSubject) &&
                    subjectType != nameof(HumanitySubject))
            {
                return string.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
            }

            if ((subjects.FindByName(subjectName) != null))
            {
                return string.Format(OutputMessages.AlreadyAddedSubject, subjectName);
            }

            ISubject subject;

            if (subjectType == nameof(TechnicalSubject))
            {
                subject = new TechnicalSubject(subjects.Models.Count + 1, subjectName);
            }
            else if (subjectType == nameof(EconomicalSubject))
            {
                subject = new EconomicalSubject(subjects.Models.Count + 1, subjectName);
            }
            else
            {
                subject = new HumanitySubject(subjects.Models.Count + 1, subjectName);
            }

            subjects.AddModel(subject);
            return string.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName, subjects.GetType().Name);
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if (universities.FindByName(universityName) != null)
            {
                return string.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }

            ICollection<int> subjectsIds = new List<int>();

            foreach (var subject in requiredSubjects)
            {
                subjectsIds.Add(subjects.FindByName(subject).Id);
            }

            universities.AddModel(new University(universities.Models.Count + 1, universityName, category, capacity, subjectsIds));

            return string.Format(OutputMessages.UniversityAddedSuccessfully, universityName, universities.GetType().Name);
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            string[] studentFullName = studentName.Split(" ");
            string studentFirstName = studentFullName[0];
            string studentLastName = studentFullName[1];

            if (students.FindByName(studentName) == null)
            {
                return string.Format(OutputMessages.StudentNotRegitered, studentFirstName, studentLastName);
            }

            if (universities.FindByName(universityName) == null)
            {
                return string.Format(OutputMessages.UniversityNotRegitered, universityName);
            }

            IStudent student = students.FindByName(studentName);
            IUniversity university = universities.FindByName(universityName);

            foreach (var currentSubject in university.RequiredSubjects)
            {
                if (!student.CoveredExams.Contains(currentSubject))
                {
                    return string.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
                }
            }

            if (student.University == university)
            {
                return string.Format(OutputMessages.StudentAlreadyJoined, studentFirstName, studentLastName, universityName);
            }

            student.JoinUniversity(university);
            return string.Format(OutputMessages.StudentSuccessfullyJoined, studentFirstName, studentLastName, universityName);
        }

        public string TakeExam(int studentId, int subjectId)
        {
            if (students.FindById(studentId) == null)
            {
                return string.Format(OutputMessages.InvalidStudentId);
            }

            if (subjects.FindById(subjectId) == null)
            {
                return string.Format(OutputMessages.InvalidSubjectId);
            }

            IStudent student = students.FindById(studentId);
            ISubject subject = subjects.FindById(subjectId);

            if (student.CoveredExams.Contains(subjectId))
            {
                return string.Format(OutputMessages.StudentAlreadyCoveredThatExam, student.FirstName, student.LastName, subject.Name);
            }

            student.CoverExam(subject);
            return string.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName, subject.Name);
        }

        public string UniversityReport(int universityId)
        {
            IUniversity university = this.universities.FindById(universityId);
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            sb.AppendLine($"Students admitted: {this.students.Models.Where(s => s.University == university).Count()}");
            sb.AppendLine($"University vacancy: {university.Capacity - this.students.Models.Where(s => s.University == university).Count()}");

            return sb.ToString().TrimEnd();
        }
    }
}
