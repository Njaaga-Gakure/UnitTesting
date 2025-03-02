using EmployeeManagement.Business;
using EmployeeManagement.Business.EventArguments;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Services;

namespace EmployeeManagement.Test
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourses_WithObject() 
        {

            // arrange
            var employeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());
            var obligatoryCourse = employeeManagementTestDataRepository.GetCourse(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

            // act
            var internalEmployee = employeeService.CreateInternalEmployee("Brian", "Gakure");
            Assert.Contains(obligatoryCourse, internalEmployee.AttendedCourses); 
        } 
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourses_WithPredicate() 
        {

            // arrange
            var employeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());
            var obligatoryCourse = employeeManagementTestDataRepository.GetCourse(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

            // act
            var internalEmployee = employeeService.CreateInternalEmployee("Brian", "Gakure");
            Assert.Contains(internalEmployee.AttendedCourses, course => course.Id == Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01")); 
        }
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourses_AttendedCourcesMustMatchObligatoryCourses() 
        {

            // arrange
            var employeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());
            var obligatoryCourse = employeeManagementTestDataRepository.GetCourses(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"), Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            // act
            var internalEmployee = employeeService.CreateInternalEmployee("Brian", "Gakure");
            Assert.Equal(obligatoryCourse, internalEmployee.AttendedCourses); 

        }
        
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourses_AttendedCourcesMustNotBeNew() 
        {

            // arrange
            var employeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());
            
            // act
            var internalEmployee = employeeService.CreateInternalEmployee("Brian", "Gakure");

            // assert 
            Assert.All(internalEmployee.AttendedCourses, course => Assert.False(course.IsNew)); 

        }

        [Fact]
        public async Task CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourses_WithObject_Async()
        {

            // arrange
            var employeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(employeeManagementTestDataRepository, new EmployeeFactory());
            var obligatoryCourse = await employeeManagementTestDataRepository.GetCourseAsync(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

            // act
            var internalEmployee = await employeeService.CreateInternalEmployeeAsync("Brian", "Gakure");
            Assert.Contains(obligatoryCourse, internalEmployee.AttendedCourses);
        }

        [Fact]
        public async Task GiveRaise_RaiseBelowMinimunGiven_EmployeeInvalidRaiseEceptionMustBeThrown() {

            // arrange
            var employeeService = new EmployeeService(new EmployeeManagementTestDataRepository(), new EmployeeFactory());
            var internalEmployee = new InternalEmployee("Brian", "Gakure", 5, 2500, false, 3);

            // act & assert 

            await Assert.ThrowsAsync<EmployeeInvalidRaiseException>(async () => await employeeService.GiveRaiseAsync(internalEmployee, 50));    
        }

        [Fact]
        public void NotifyOfAbsence_EmployeeIsAbsent_OnEmployeeIsAbsentMustBeTriggered()
        {
            // arrange
            var employeeService = new EmployeeService(new EmployeeManagementTestDataRepository(), new EmployeeFactory());
            var internalEmployee = new InternalEmployee("Brian", "Gakure", 5, 2500, false, 3);

            // Act & Assert

            Assert.Raises<EmployeeIsAbsentEventArgs>(
                handler => employeeService.EmployeeIsAbsent += handler,
                handler => employeeService.EmployeeIsAbsent -= handler,
                () => employeeService.NotifyOfAbsence(internalEmployee)
            ); 

        }
    }
}
