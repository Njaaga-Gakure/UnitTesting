using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Fixtures;

namespace EmployeeManagement.Test
{
    [Collection("EmployeeServiceCollection")]
    public class DataDrivenEmployeeServiceTests (EmployeeServiceFixture employeeServiceFixture) 
    {
        [Fact]
        public async Task GiveRaise_MinimunRaiseGiven_EmployeeMinimunRaiseGivenMustBeTrue()
        {
            var internalEmployee = new InternalEmployee("Brian", "Gakure", 4, 2500, false, 1);
            await employeeServiceFixture.EmployeeService.GiveRaiseAsync(internalEmployee, 100); 
            Assert.True(internalEmployee.MinimumRaiseGiven); 
        }

        [Fact]
        public async Task GiveRaise_MoreThanMinimumRaiseGiven_EmployeeMinimumRaiseGivenMustBeFalse()
        {
            var internalEmployee = new InternalEmployee("Brian", "Gakure", 4, 2500, false, 1);
            await employeeServiceFixture.EmployeeService.GiveRaiseAsync(internalEmployee, 200); 
            Assert.False(internalEmployee.MinimumRaiseGiven); 
        }
    }
}
