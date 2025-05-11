using AutoMapper;
using EmployeeManagement.Business;
using EmployeeManagement.Controllers;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.MapperProfiles;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeManagement.Test
{
    public class InternalEmployeeControllerTests
    {
        private readonly InternalEmployeesController _internalEmployeesController; 
        private readonly InternalEmployee _firstEmployee; 
        public InternalEmployeeControllerTests()
        {
            var employeeServiceMock = new Mock<IEmployeeService>();
            _firstEmployee = new("Brian", "Gakure", 2, 3000, false, 2); 
            employeeServiceMock.Setup(mock => mock.FetchInternalEmployeesAsync()).ReturnsAsync([
               _firstEmployee,
                new("Evans", "Mwangi", 2, 4000, false, 1),
                new("Joel", "Kores", 2, 5000, false, 3),
            ]);
            //var mapperMock = new Mock<IMapper>();
            //mapperMock.Setup(m => m.Map<InternalEmployee, InternalEmployeeDto>(It.IsAny<InternalEmployee>())).Returns(new InternalEmployeeDto()); 
            var mapperConfiguration = new MapperConfiguration(configuration => configuration.AddProfile<EmployeeProfile>());

            var mapper = new Mapper(mapperConfiguration);
            _internalEmployeesController = new InternalEmployeesController(employeeServiceMock.Object, mapper);
        }

        [Fact]
        public async Task GetInternalEmployee_GetAction_MustReturnObjectResult() {
         
            var result = await _internalEmployeesController.GetInternalEmployees();
            var actionResult = Assert.IsType<ActionResult<IEnumerable<InternalEmployeeDto>>>(result); 
            Assert.IsType<OkObjectResult>(actionResult.Result); 

        }
        [Fact]
        public async Task GetInternalEmployee_GetAction_MustReturnIEnumerableOfInternalEmployeeDtoAsModelType() {
           
            var result = await _internalEmployeesController.GetInternalEmployees();
            var actionResult = Assert.IsType<ActionResult<IEnumerable<InternalEmployeeDto>>>(result);


            // exact match false is because we are asserting an interface not a concrete class
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<IEnumerable<InternalEmployeeDto>>(okResult.Value, exactMatch: false); 

        }
        [Fact]
        public async Task GetInternalEmployee_GetAction_MustReturnNumberOfInputtedInternalEmployees() {
           
            var result = await _internalEmployeesController.GetInternalEmployees();
            var actionResult = Assert.IsType<ActionResult<IEnumerable<InternalEmployeeDto>>>(result);


            // exact match false is because we are asserting an interface not a concrete class
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            //var employees = Assert.IsAssignableFrom<IEnumerable<InternalEmployeeDto>>(okResult.Value);
            var employees = Assert.IsType<IEnumerable<InternalEmployeeDto>>(okResult.Value, exactMatch: false);
            Assert.Equal(3, employees.Count());

        } 
        [Fact]
        public async Task GetInternalEmployee_GetAction_ObjectMustBeMappedCorrectly() {
           
            var result = await _internalEmployeesController.GetInternalEmployees();
            var actionResult = Assert.IsType<ActionResult<IEnumerable<InternalEmployeeDto>>>(result);


            // exact match false is because we are asserting an interface not a concrete class
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            //var employees = Assert.IsAssignableFrom<IEnumerable<InternalEmployeeDto>>(okResult.Value);
            var employees = Assert.IsType<IEnumerable<InternalEmployeeDto>>(okResult.Value, exactMatch: false);
            var firstEmployee = employees.First();
            Assert.Equal(_firstEmployee.Id, firstEmployee.Id);
            Assert.Equal(_firstEmployee.FirstName, firstEmployee.FirstName);
            Assert.Equal(_firstEmployee.LastName, firstEmployee.LastName);
            Assert.Equal(_firstEmployee.Salary, firstEmployee.Salary);
            Assert.Equal(_firstEmployee.SuggestedBonus, firstEmployee.SuggestedBonus);
            Assert.Equal(_firstEmployee.YearsInService, firstEmployee.YearsInService);

        }
    }
}
