using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Fixtures;

namespace EmployeeManagement.Test
{
    [Collection("EmployeeServiceCollection")]
    public class DataDrivenEmployeeServiceTests (EmployeeServiceFixture employeeServiceFixture) 
    {

        public static IEnumerable<object[]> ExampleTestDataForGivenRaise_WithProperty
        {
            get
            {
                return [[100, true], [200, false]];
            }
        }



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

        [Theory]
        [MemberData(nameof(ExampleTestDataForGivenRaise_WithProperty))]
        public async Task GiveRaise_RaiseGiven_EmployeeMinimumRaiseGivenMatches(int RaiseGiven, bool expectedValueForMinimumRaiseGiven)
        {
            var internalEmployee = new InternalEmployee("Brian", "Gakure", 4, 2500, false, 1);
            await employeeServiceFixture.EmployeeService.GiveRaiseAsync(internalEmployee, RaiseGiven); 
            Assert.Equal(expectedValueForMinimumRaiseGiven, internalEmployee.MinimumRaiseGiven); 
        } 
        
        [Theory]
        [MemberData(nameof(GetExampleTestDataForGivenRaise), 2)]
        public async Task GiveRaise_RaiseGiven_EmployeeMinimumRaiseGivenMatches_WithMethod(int RaiseGiven, bool expectedValueForMinimumRaiseGiven)
        {
            var internalEmployee = new InternalEmployee("Brian", "Gakure", 4, 2500, false, 1);
            await employeeServiceFixture.EmployeeService.GiveRaiseAsync(internalEmployee, RaiseGiven); 
            Assert.Equal(expectedValueForMinimumRaiseGiven, internalEmployee.MinimumRaiseGiven); 
        }

        [Theory]
        [InlineData("37e03ca7-c730-4351-834c-b66f280cdb01")]
        [InlineData("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e")]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedObligatoryCourses(Guid courseId)
        {
            var internalEmployee = employeeServiceFixture.EmployeeService.CreateInternalEmployee("Brian", "Gakure");
            Assert.Contains(internalEmployee.AttendedCourses, course => course.Id == courseId);
        }
        public static IEnumerable<object[]> GetExampleTestDataForGivenRaise(int testDataIntancesToProvide) {
            var testData = new List<object[]> { new object[] { 100, true }, new object[] { 200, false } };
            return  testData.Take(testDataIntancesToProvide);   
        }

    }
}
