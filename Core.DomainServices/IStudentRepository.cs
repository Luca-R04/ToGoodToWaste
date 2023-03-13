using Core.Domain;

namespace Core.DomainServices
{
    public interface IStudentRepository
    {
        IEnumerable<Student> Students { get; }

        Student? CreateStudent(Student student);
        Student? ReadStudent(int studentId);
        Student? UpdateStudent(Student student);
        Student? DeleteStudent(Student student);
        Student? GetStudentByEmail(string email);
    }
}