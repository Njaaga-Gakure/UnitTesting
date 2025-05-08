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
        [Fact]
        public async Task GetInternalEmployee_GetAction_MustReturnObjectResult() {
            var employeeServiceMock = new Mock<IEmployeeService>();
            employeeServiceMock.Setup(mock => mock.FetchInternalEmployeesAsync()).ReturnsAsync(new List<InternalEmployee>() { 
                new("Brian", "Gakure", 2, 3000, false, 2),
                new("Evans", "Mwangi", 2, 4000, false, 1),
                new("Joel", "Kores", 2, 5000, false, 3),
            });
            var internalEmployeesController = new InternalEmployeesController(employeeServiceMock.Object, null);

            var result = await internalEmployeesController.GetInternalEmployees();


            var actionResult = Assert.IsType<ActionResult<IEnumerable<InternalEmployeeDto>>>(result); 
            var x = Assert.IsType<OkObjectResult>(actionResult.Result); 

        }
    }
}
