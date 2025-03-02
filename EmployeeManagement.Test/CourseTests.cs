using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test
{
    public class CourseTests
    {
        [Fact]
        public void CourseConstructor_ConstructCourse_IsNewMutBeTrue() {
            Course course = new("xUnit");

            Assert.True(course.IsNew); 
        
        }
    }
}
