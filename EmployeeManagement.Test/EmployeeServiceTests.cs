using EmployeeManagement.Business.EventArguments;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Fixtures;
using Xunit.Abstractions;


namespace EmployeeManagement.Test
{
    [Collection("EmployeeServiceCollection")]
    public class EmployeeServiceTests (EmployeeServiceFixture employeeServiceFixture, ITestOutputHelper testOutputHelper)
    {
     


        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourses_WithObject() 
        {
            var obligatoryCourse = employeeServiceFixture.EmployeeManagementRepository.GetCourse(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
            var internalEmployee = employeeServiceFixture.EmployeeService.CreateInternalEmployee("Brian", "Gakure");
            testOutputHelper.WriteLine($"After Act -> FirstName: {internalEmployee.FirstName}, LastName: {internalEmployee.LastName}");
            Assert.Contains(obligatoryCourse, internalEmployee.AttendedCourses); 
        } 

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourses_WithPredicate() 
        {
            var obligatoryCourse = employeeServiceFixture.EmployeeManagementRepository.GetCourse(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
            var internalEmployee = employeeServiceFixture.EmployeeService.CreateInternalEmployee("Brian", "Gakure");
            Assert.Contains(internalEmployee.AttendedCourses, course => course.Id == Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01")); 
        }

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourses_AttendedCourcesMustMatchObligatoryCourses() 
        {
            var obligatoryCourse = employeeServiceFixture.EmployeeManagementRepository.GetCourses(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"), Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));
            var internalEmployee = employeeServiceFixture.EmployeeService.CreateInternalEmployee("Brian", "Gakure");
            Assert.Equal(obligatoryCourse, internalEmployee.AttendedCourses); 

        }
        
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourses_AttendedCourcesMustNotBeNew() 
        {            
            var internalEmployee = employeeServiceFixture.EmployeeService.CreateInternalEmployee("Brian", "Gakure");
            Assert.All(internalEmployee.AttendedCourses, course => Assert.False(course.IsNew)); 
        }

        [Fact]
        public async Task CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourses_WithObject_Async()
        {

            var obligatoryCourse = await employeeServiceFixture.EmployeeManagementRepository.GetCourseAsync(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
            var internalEmployee = await employeeServiceFixture.EmployeeService.CreateInternalEmployeeAsync("Brian", "Gakure");
            Assert.Contains(obligatoryCourse, internalEmployee.AttendedCourses);
        }

        [Fact]
        public async Task GiveRaise_RaiseBelowMinimunGiven_EmployeeInvalidRaiseEceptionMustBeThrown() {

            var internalEmployee = new InternalEmployee("Brian", "Gakure", 5, 2500, false, 3);
            await Assert.ThrowsAsync<EmployeeInvalidRaiseException>(async () => await employeeServiceFixture.EmployeeService.GiveRaiseAsync(internalEmployee, 50));    
        }

        [Fact]
        public void NotifyOfAbsence_EmployeeIsAbsent_OnEmployeeIsAbsentMustBeTriggered()
        {
            var internalEmployee = new InternalEmployee("Brian", "Gakure", 5, 2500, false, 3);
            Assert.Raises<EmployeeIsAbsentEventArgs>(
                handler => employeeServiceFixture.EmployeeService.EmployeeIsAbsent += handler,
                handler => employeeServiceFixture.EmployeeService.EmployeeIsAbsent -= handler,
                () => employeeServiceFixture.EmployeeService.NotifyOfAbsence(internalEmployee)
            ); 

        }
    }
}
