using Core.Domain;

namespace Core.DomainServices
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Employees { get; }

        Employee? CreateEmployee(Employee employee);
        Employee? ReadEmployee(int employeeId);
        Employee? UpdateEmployee(Employee employee);
        Employee? DeleteEmployee(Employee employee);
        Employee GetEmployeeByEmail(string email);
    }
}