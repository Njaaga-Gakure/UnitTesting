using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test
{
    public class EmployeeFactoryTests : IDisposable
    {
        private readonly EmployeeFactory _employeeFactory;

        // Contructor and Dispose
        public EmployeeFactoryTests()
        {
            _employeeFactory = new EmployeeFactory(); 
        }

        public void Dispose()
        {
            // clean up code he

        }
        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500()
        {
            //EmployeeFactory employeeFactory = new();
            var employee = (InternalEmployee) _employeeFactory.CreateEmployee("Brian", "Gakure");
            Assert.Equal(2500, employee.Salary); 
        }
        
        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500()
        {
            //EmployeeFactory employeeFactory = new();
            var employee = (InternalEmployee) _employeeFactory.CreateEmployee("Brian", "Gakure");
            Assert.True(employee.Salary >= 2500 && employee.Salary <= 3500, "Salary is not in the acceptable range"); 
        } 
        
        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500Alternative()
        {
            //EmployeeFactory employeeFactory = new();
            var employee = (InternalEmployee) _employeeFactory.CreateEmployee("Brian", "Gakure");
            Assert.InRange(employee.Salary, 2500, 3500); 
        }

        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryPrecision()
        {
            //EmployeeFactory employeeFactory = new();
            var employee = (InternalEmployee) _employeeFactory.CreateEmployee("Brian", "Gakure");
            Assert.Equal(2500, employee.Salary, 0);
        } 
        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_ReturnType")]
        public void CreateEmployee_IsExternalIsTrue_ReturnTypeMustBeExternalEmployee()
        {
            // arrange
            //EmployeeFactory employeeFactory = new();

            // act
            var employee =  _employeeFactory.CreateEmployee("Brian", "Gakure", "Griffin", true);

            // assert 
            Assert.IsType<ExternalEmployee>(employee);

            // checks class and derived class
            //Assert.IsAssignableFrom<Employee>(employee);
           
        }
    }
}
