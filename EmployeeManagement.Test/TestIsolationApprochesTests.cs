using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.DbContexts;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.DataAccess.Services;
using EmployeeManagement.Test.HttpMessageHandlers;
using EmployeeManagement.Test.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit.Sdk;

namespace EmployeeManagement.Test
{
    public class TestIsolationApprochesTests
    {

        // class or collection fixture approach should be considered for this setup as migrations are expensive
        [Fact]
        public async Task AttendedCourseAsync_CourseAttended_SuggestedBonusMustBeCorrectlyBeRecalculated() {

            // arrange 
            var connection = new SqliteConnection("Data Source=:memory:"); 
            connection.Open();
            var optionsBuilder = new DbContextOptionsBuilder<EmployeeDbContext>().UseSqlite(connection);    
            var dbContext = new EmployeeDbContext(optionsBuilder.Options);  
            dbContext.Database.Migrate();
            var employeeManagentDataRepository = new EmployeeManagementRepository(dbContext);
            var employeeService = new EmployeeService(employeeManagentDataRepository, new EmployeeFactory()); 
            var courseToAttend = await employeeManagentDataRepository.GetCourseAsync(Guid.Parse("844e14ce-c055-49e9-9610-855669c9859b"));
            var internalEmployee = await employeeManagentDataRepository.GetInternalEmployeeAsync(Guid.Parse("72f2f5fe-e50c-4966-8420-d50258aefdcb"));
            if (courseToAttend is null || internalEmployee is null)
            {
                throw new XunitException("Arranging the test failed");     
            }
            var expectedSugestedBonus = internalEmployee.YearsInService * (internalEmployee.AttendedCourses.Count + 1) * 100;

            // act 

            await employeeService.AttendCourseAsync(internalEmployee, courseToAttend);

            // assert
            Assert.Equal(expectedSugestedBonus, internalEmployee.SuggestedBonus);
        }
        [Fact]
        public async Task PromoteInternalEmployeeAsync_IsEligible_JobLevelMustBeIncreased()
        {

            // Arrange
            var httpClient = new HttpClient(new TestablePromotionEligibilityHandler(true));
            var internalEmployee = new InternalEmployee("Brian", "Gakure", 5, 2500, false, 1);
            var promotionService = new PromotionService(httpClient, new EmployeeManagementTestDataRepository());
            
            // Act

            await promotionService.PromoteInternalEmployeeAsync(internalEmployee);

            // Assert
            Assert.Equal(2, internalEmployee.JobLevel); 
        }
    }
}
