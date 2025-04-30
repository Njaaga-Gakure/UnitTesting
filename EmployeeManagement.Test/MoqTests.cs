using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Services;
using Moq;

namespace EmployeeManagement.Test
{
    public class MoqTests
    {
        [Fact]
        public void FetchInternalEmployee_EmployeeFetched_SuggestedBonusMustBeCalculated() { 
            var employeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
            //var employeeFactory = new EmployeeFactory(); 
            var employeeFactoryMock = new Mock<EmployeeFactory>(); 
            var employeeService = new EmployeeService(employeeManagementTestDataRepository, employeeFactoryMock.Object);

            var employee = employeeService.FetchInternalEmployee(Guid.Parse("72f2f5fe-e50c-4966-8420-d50258aefdcb"));

            Assert.Equal(400, employee.SuggestedBonus); 
        }
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_SuggestedBonusMustBeCalculated() { 
            var employeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
            var employeeFactoryMock = new Mock<EmployeeFactory>();
            //var employeeFactory = new EmployeeFactory(); 
            // only overridable methods can be mocked
            employeeFactoryMock.Setup(employeeFactory => employeeFactory.CreateEmployee("Brian", It.IsAny<string>(), null, false)).Returns(new InternalEmployee("Brian", "Njaaga", 5, 2500, false, 1)); 
            employeeFactoryMock.Setup(employeeFactory => employeeFactory.CreateEmployee("Sandy", It.IsAny<string>(), null, false)).Returns(new InternalEmployee("Sandy", "Wanjiru", 5, 2500, false, 1)); 
            employeeFactoryMock.Setup(employeeFactory => employeeFactory.CreateEmployee("Jeff", It.Is<string>(value => value.Contains('a')), null, false)).Returns(new InternalEmployee("Jeff", "Waaaah", 5, 2500, false, 1)); 
            var employeeService = new EmployeeService(employeeManagementTestDataRepository, employeeFactoryMock.Object);

            decimal suggestedBonus = 1000;

            var employee = employeeService.CreateInternalEmployee("Jeff", "Gkure"); 

            Assert.Equal(suggestedBonus, employee.SuggestedBonus); 
        }
    }
}
