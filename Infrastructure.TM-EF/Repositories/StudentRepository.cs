using Core.Domain;
using Core.DomainServices;
using Infrastructure.TGTW_EF.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.TGTW_EF.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ToGoodToWasteDbContext _dbContext;
        public IEnumerable<Student> Students => _dbContext.Students
            .Include(s => s.Location)
            .ToList();

        public StudentRepository(ToGoodToWasteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Student? CreateStudent(Student student)
        {
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();

            return student;
        }

        public Student? ReadStudent(int studentId)
        {
            return _dbContext.Students.FirstOrDefault(c => c.StudentId == studentId);
        }

        public Student? UpdateStudent(Student student)
        {
            var entityToUpdate = _dbContext.Students.FirstOrDefault(c => c.StudentId == student.StudentId);
            if (entityToUpdate != null)
            {
                entityToUpdate.StudentId = student.StudentId;
                entityToUpdate.Name = student.Name;
                entityToUpdate.BirthDate = student.BirthDate;
                entityToUpdate.StudentNumber = student.StudentNumber;
                entityToUpdate.EmailAdress = student.EmailAdress;
                entityToUpdate.City = student.City;
                entityToUpdate.PhoneNumber = student.PhoneNumber;
                entityToUpdate.ReservedPackages = student.ReservedPackages;

                _dbContext.SaveChanges();
            }

            return entityToUpdate;
        }
        public Student? DeleteStudent(Student student)
        {
            var entityToRemove = _dbContext.Students.FirstOrDefault(c => c.StudentId == student.StudentId);
            if (entityToRemove != null)
            {
                _dbContext.Students.Remove(entityToRemove);
                _dbContext.SaveChanges();
            }

            return entityToRemove;
        }

        public Student? GetStudentByEmail(string email)
        {
            return Students
                .First(student => student.EmailAdress.Equals(email));
        }
    }
}
