
using AutoMapper;
using EmployeeManagement.Business;
using EmployeeManagement.Controllers;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeManagement.Test
{
    // not to self: Add this test only when the controller does not have the [ApiController] attribute
    public class DemoInternalEmployeeControllerTests
    {
        [Fact]
        public async Task CrreateInternalEmployee_InvalidInput_MustReturnBadRequest()
        {

            // arrange
            var employeeServiceMock = new Mock<IEmployeeService>();
            var mapperMock = new Mock<IMapper>();
            var demoInternalEmployeeController = new DemoInternalEmployeeController(employeeServiceMock.Object, mapperMock.Object);

            var internalEmployeeForCreationDtoMock = new Mock<InternalEmployeeForCreationDto>();
            demoInternalEmployeeController.ModelState.AddModelError("FirstName", "Required");

            // act
            var result = await demoInternalEmployeeController.CreateInternalEmployee(internalEmployeeForCreationDtoMock.Object);

            // assert 
            var actionResult = Assert.IsType<ActionResult<InternalEmployeeDto>>(result); 
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        
        }
    }
}
