using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test
{
    public class EmployeeFactoryTests
    {
        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500()
        {
            EmployeeFactory employeeFactory = new();
            var employee = (InternalEmployee) employeeFactory.CreateEmployee("Brian", "Gakure");
            Assert.Equal(2500, employee.Salary); 
        }
        
        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500()
        {
            EmployeeFactory employeeFactory = new();
            var employee = (InternalEmployee) employeeFactory.CreateEmployee("Brian", "Gakure");
            Assert.True(employee.Salary >= 2500 && employee.Salary <= 3500, "Salary is not in the acceptable range"); 
        } 
        
        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500Alternative()
        {
            EmployeeFactory employeeFactory = new();
            var employee = (InternalEmployee) employeeFactory.CreateEmployee("Brian", "Gakure");
            Assert.InRange(employee.Salary, 2500, 3500); 
        }

        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryPrecision()
        {
            EmployeeFactory employeeFactory = new();
            var employee = (InternalEmployee)employeeFactory.CreateEmployee("Brian", "Gakure");
            Assert.Equal(2500, employee.Salary, 0);
        } 
        [Fact]
        public void CreateEmployee_IsExternalIsTrue_ReturnTypeMustBeExternalEmployee()
        {
            // arrange
            EmployeeFactory employeeFactory = new();

            // act
            var employee =  employeeFactory.CreateEmployee("Brian", "Gakure", "Griffin", true);

            // assert 
            Assert.IsType<ExternalEmployee>(employee);

            // checks class and derived class
            //Assert.IsAssignableFrom<Employee>(employee);
           
        }
    }
}
