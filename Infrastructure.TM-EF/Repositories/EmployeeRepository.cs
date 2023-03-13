using Core.Domain;
using Core.DomainServices;
using Infrastructure.TGTW_EF.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.TGTW_EF.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ToGoodToWasteDbContext _dbContext;

        public EmployeeRepository(ToGoodToWasteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Employee> Employees => _dbContext.Employees
            .Include("Location")
            .Include("Canteen")
            .ToList();

        public Employee? CreateEmployee(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();

            return employee;
        }

        public Employee? ReadEmployee(int employeeId)
        {
            return _dbContext.Employees.FirstOrDefault(c => c.EmployeeId == employeeId);
        }

        public Employee? UpdateEmployee(Employee employee)
        {
            var entityToUpdate = _dbContext.Employees.FirstOrDefault(c => c.EmployeeId == employee.EmployeeId);
            if (entityToUpdate != null)
            {
                entityToUpdate.EmployeeId = employee.EmployeeId;
                entityToUpdate.Name = employee.Name;
                entityToUpdate.Location = employee.Location;
                entityToUpdate.Canteen = employee.Canteen;

                _dbContext.SaveChanges();
            }

            return entityToUpdate;
        }
        public Employee? DeleteEmployee(Employee employee)
        {
            var entityToRemove = _dbContext.Employees.FirstOrDefault(c => c.EmployeeId == employee.EmployeeId);
            if (entityToRemove != null)
            {
                _dbContext.Employees.Remove(entityToRemove);
                _dbContext.SaveChanges();
            }

            return entityToRemove;
        }

        public Employee GetEmployeeByEmail(string email)
        {
            return Employees
                .First(employee => employee.EmailAdress.Equals(email));
        }
    }
}
