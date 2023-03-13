using Core.Domain;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Infrastructure.TGTW_EF.Data
{
    public class SecuritySeedData : ISeedData
    {
        private readonly UserManager<IdentityUser> _userManager;

        public SecuritySeedData(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task EnsurePopulated(bool dropExisting = false)
        {
            const string USERNAME_EMPLOYEE = "Hubert";
            const string PASSWORD_EMPLOYEE = "EmployeePassword12!";
            const string EMAIL_EMPLOYEE = "Hubert@gmail.com";

            const string USERNAME_STUDENT = "Luca";
            const string PASSWORD_STUDENT = "StudentPassword12!";
            const string EMAIL_STUDENT = "Luca@gmail.com";

            const string USERNAME_STUDENT2 = "Laurens";
            const string PASSWORD_STUDENT2 = "StudentPassword12!";
            const string EMAIL_STUDENT2 = "Laurens@gmail.com";

            const string USERNAME_STUDENT3 = "Martijn";
            const string PASSWORD_STUDENT3 = "StudentPassword12!";
            const string EMAIL_STUDENT3 = "Martijn@gmail.com";

            if (dropExisting)
            {
                var existingUser = await _userManager.FindByNameAsync(USERNAME_EMPLOYEE);
                if (existingUser != null)
                    await _userManager.DeleteAsync(existingUser);

                existingUser = await _userManager.FindByNameAsync(USERNAME_STUDENT);
                if (existingUser != null)
                    await _userManager.DeleteAsync(existingUser);
            }

            IdentityUser Employee = await _userManager.FindByEmailAsync(EMAIL_EMPLOYEE);
            if (Employee == null)
            {
                Employee = new IdentityUser(USERNAME_EMPLOYEE)
                {
                    Email = EMAIL_EMPLOYEE,
                };

                await _userManager.CreateAsync(Employee, PASSWORD_EMPLOYEE);
                await _userManager.AddClaimAsync(Employee, new Claim("Employee", "true"));
            }

            IdentityUser Student = await _userManager.FindByEmailAsync(EMAIL_STUDENT);
            if (Student == null)
            {
                Student = new IdentityUser(USERNAME_STUDENT)
                {
                    Email = EMAIL_STUDENT,
                };

                await _userManager.CreateAsync(Student, PASSWORD_STUDENT);
                await _userManager.AddClaimAsync(Student, new Claim("Student", "true"));
            }

            IdentityUser Student2 = await _userManager.FindByEmailAsync(EMAIL_STUDENT2);
            if (Student2 == null)
            {
                Student2 = new IdentityUser(USERNAME_STUDENT2)
                {
                    Email = EMAIL_STUDENT2,
                };

                await _userManager.CreateAsync(Student2, PASSWORD_STUDENT2);
                await _userManager.AddClaimAsync(Student2, new Claim("Student", "true"));
            }

            IdentityUser Student3 = await _userManager.FindByEmailAsync(EMAIL_STUDENT3);
            if (Student3 == null)
            {
                Student3 = new IdentityUser(USERNAME_STUDENT3)
                {
                    Email = EMAIL_STUDENT3,
                };

                await _userManager.CreateAsync(Student3, PASSWORD_STUDENT3);
                await _userManager.AddClaimAsync(Student3, new Claim("Student", "true"));
            }
        }
    }
}