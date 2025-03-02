using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test
{
    public class EmployeeTests
    {
        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_FullNameIsConcatenation() {
            // arrange 
            var employee = new InternalEmployee("Brian", "Gakure", 5, 39000, false, 2);

            // act 
            employee.FirstName = "Jeff";
            employee.LastName = "Ndegwa";

            // assert
            Assert.Equal("Jeff Ndegwa", employee.FullName); 
        } 
        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_FullNameStartsWithFirstName() {
            // arrange 
            var employee = new InternalEmployee("Brian", "Gakure", 5, 39000, false, 2);

            // act 
            employee.FirstName = "Jeff";
            employee.LastName = "Ndegwa";

            // assert
            Assert.StartsWith(employee.FirstName, employee.FullName); 
        }
        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_FullNameEndssWithLastName() {
            // arrange 
            var employee = new InternalEmployee("Brian", "Gakure", 5, 39000, false, 2);

            // act 
            employee.FirstName = "Jeff";
            employee.LastName = "Ndegwa";

            // assert
            Assert.EndsWith(employee.LastName, employee.FullName); 
        }
        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_FullNameContainsPartOfConcatenation() {
            // arrange 
            var employee = new InternalEmployee("Brian", "Gakure", 5, 39000, false, 2);

            // act 
            employee.FirstName = "Jeff";
            employee.LastName = "Ndegwa";

            // assert
            Assert.Contains("ff Ndeg", employee.FullName); 
        }
        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_FullNameSoundsLikeConcatenation() {
            // arrange 
            var employee = new InternalEmployee("Jeff", "Ndegwa", 5, 39000, false, 2);

            // act 
            employee.FirstName = "Brian";
            employee.LastName = "Gakure";

            // assert
            Assert.Matches("Br(i|y)an Gakure", employee.FullName); 
        }
    }
}
