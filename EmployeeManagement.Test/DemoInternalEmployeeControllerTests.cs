
using AutoMapper;
using EmployeeManagement.Business;
using EmployeeManagement.Controllers;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace EmployeeManagement.Test
{
    public class DemoInternalEmployeeControllerTests
    {
     
    // not to self: Add this test only when the controller does not have the [ApiController] attribute
        [Fact]
        public async Task CrreateInternalEmployee_InvalidInput_MustReturnBadRequest()
        {

          
            var internalEmployeeForCreationDtoMock = new Mock<InternalEmployeeForCreationDto>();
            var employeeServiceMock = new Mock<IEmployeeService>();
            var mapperMock = new Mock<IMapper>();
            var demoInternalEmployeeController = new DemoInternalEmployeeController(employeeServiceMock.Object, mapperMock.Object);
            demoInternalEmployeeController.ModelState.AddModelError("FirstName", "Required");

            // act
            var result = await demoInternalEmployeeController.CreateInternalEmployee(internalEmployeeForCreationDtoMock.Object);

            // assert 
            var actionResult = Assert.IsType<ActionResult<InternalEmployeeDto>>(result); 
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
      
        }

        [Fact]
        public void GetProtectedInternalEmployee_GetActionForUserInAndminRole_MustRedirectToGetInternalEmployeesOnProtectedInternalEmployees() {
            var employeeServiceMock = new Mock<IEmployeeService>();
            var mapperMock = new Mock<IMapper>();
            var demoInternalEmployeeController = new DemoInternalEmployeeController(employeeServiceMock.Object, mapperMock.Object);
            var userClaims = new List<Claim>() {
                new(ClaimTypes.Name, "Karen"),
                new(ClaimTypes.Role, "Admin"),
            };
            var claimsIdentity = new ClaimsIdentity(userClaims, "unitTest");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var httpContext = new DefaultHttpContext() { 
                User = claimsPrincipal
            };
            demoInternalEmployeeController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
            var result = demoInternalEmployeeController.GetProtectedInternalEmployee();
            var actionResult = Assert.IsType<IActionResult>(result, exactMatch: false);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("GetInternalEmployees", redirectToActionResult.ActionName);
            Assert.Equal("ProtectedInternalEmployee", redirectToActionResult.ControllerName); 


        }
        [Fact]
        public void GetProtectedInternalEmployee_GetActionForUserInAndminRole_MustRedirectToGetInternalEmployeesOnProtectedInternalEmployees_WithMoq() {
            var employeeServiceMock = new Mock<IEmployeeService>();
            var mapperMock = new Mock<IMapper>();
            var demoInternalEmployeeController = new DemoInternalEmployeeController(employeeServiceMock.Object, mapperMock.Object);
            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.Setup(principal => principal.IsInRole(It.Is<string>(role => role == "Admin"))).Returns(true);
            var httpContextMock = new Mock<HttpContext>(); 
            httpContextMock.Setup(httpContext => httpContext.User).Returns(mockPrincipal.Object);
            demoInternalEmployeeController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContextMock.Object
            };
            var result = demoInternalEmployeeController.GetProtectedInternalEmployee();
            var actionResult = Assert.IsType<IActionResult>(result, exactMatch: false);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("GetInternalEmployees", redirectToActionResult.ActionName);
            Assert.Equal("ProtectedInternalEmployee", redirectToActionResult.ControllerName); 
        }
    }
}
