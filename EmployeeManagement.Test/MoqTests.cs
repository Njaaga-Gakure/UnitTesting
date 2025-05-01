using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.DataAccess.Services;
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
            var employeeManagementTestDataRepositoryMock = new Mock<IEmployeeManagementRepository>();
            employeeManagementTestDataRepositoryMock.Setup(employeeManagementRepo => employeeManagementRepo.GetInternalEmployee(It.IsAny<Guid>())).Returns(new InternalEmployee("Brian", "Gakure", 2, 2500, false, 2) { 
                AttendedCourses = new List<Course>() {  new Course("Course One"), new Course("Course Two")}
            }); 
            var employeeFactoryMock = new Mock<EmployeeFactory>();
            var employeeService = new EmployeeService(employeeManagementTestDataRepositoryMock.Object, employeeFactoryMock.Object);

            var employee = employeeService.FetchInternalEmployee(Guid.Empty);

            Assert.Equal(400, employee.SuggestedBonus); 
        }
        [Fact]
        public async Task FetchInternalEmployee_EmployeeFetched_SuggestedBonusMustBeCalculated_Async() { 
            var employeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
            //var employeeFactory = new EmployeeFactory(); 
            var employeeManagementTestDataRepositoryMock = new Mock<IEmployeeManagementRepository>();
            employeeManagementTestDataRepositoryMock.Setup(employeeManagementRepo => employeeManagementRepo.GetInternalEmployeeAsync(It.IsAny<Guid>())).ReturnsAsync(new InternalEmployee("Brian", "Gakure", 2, 2500, false, 2) { 
                AttendedCourses = new List<Course>() {  new Course("Course One"), new Course("Course Two")}
            }); 
            var employeeFactoryMock = new Mock<EmployeeFactory>();
            var employeeService = new EmployeeService(employeeManagementTestDataRepositoryMock.Object, employeeFactoryMock.Object);

            var employee = await employeeService.FetchInternalEmployeeAsync(Guid.Empty);

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

            var employee = employeeService.CreateInternalEmployee("Jeff", "Gakure"); 

            Assert.Equal(suggestedBonus, employee.SuggestedBonus); 
        }
    }
}
