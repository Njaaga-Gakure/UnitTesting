using EmployeeManagement.Business;
using EmployeeManagement.Controllers;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeManagement.Test
{
    public class InternalEmployeeControllerTests
    {
        private readonly InternalEmployeesController _internalEmployeesController; 
        public InternalEmployeeControllerTests()
        {
            var employeeServiceMock = new Mock<IEmployeeService>();
            employeeServiceMock.Setup(mock => mock.FetchInternalEmployeesAsync()).ReturnsAsync([
                new("Brian", "Gakure", 2, 3000, false, 2),
                new("Evans", "Mwangi", 2, 4000, false, 1),
                new("Joel", "Kores", 2, 5000, false, 3),
            ]);
            _internalEmployeesController = new InternalEmployeesController(employeeServiceMock.Object, null);
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
           
            Assert.IsType<IEnumerable<InternalEmployeeDto>>(((OkObjectResult)actionResult.Result)?.Value, exactMatch: false); 

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
    }
}
