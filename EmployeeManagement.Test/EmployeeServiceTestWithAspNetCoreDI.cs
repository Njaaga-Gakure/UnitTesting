using EmployeeManagement.Test.Fixtures;

namespace EmployeeManagement.Test
{
    public class EmployeeServiceTestWithAspNetCoreDI : IClassFixture<EmployeeServiceWithAspNetCoreDIFixture>
    {
        private readonly EmployeeServiceWithAspNetCoreDIFixture _employeeServiceFixture;

        public EmployeeServiceTestWithAspNetCoreDI(EmployeeServiceWithAspNetCoreDIFixture employeeService)
        {
            _employeeServiceFixture = employeeService;  
        }

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourses_WithObject()
        {
            
            var obligatoryCourse = _employeeServiceFixture.EmployeeManagementRepository.GetCourse(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
            var internalEmployee = _employeeServiceFixture.EmployeeService.CreateInternalEmployee("Brian", "Gakure");
            Assert.Contains(obligatoryCourse, internalEmployee.AttendedCourses);
        }

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourses_WithPredicate()
        {
            var obligatoryCourse = _employeeServiceFixture.EmployeeManagementRepository.GetCourse(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
            var internalEmployee = _employeeServiceFixture.EmployeeService.CreateInternalEmployee("Brian", "Gakure");
            Assert.Contains(internalEmployee.AttendedCourses, course => course.Id == Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
        }
    }
}
