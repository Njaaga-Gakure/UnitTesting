﻿using AutoMapper;
using EmployeeManagement.Business;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    // no api controller atrribute
    [Route("api/demointernalemployees")]
    public class DemoInternalEmployeeController(IEmployeeService employeeService, IMapper mapper) : ControllerBase
    {

        private readonly IEmployeeService _employeeService = employeeService;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<ActionResult<InternalEmployeeDto>> CreateInternalEmployee(InternalEmployeeForCreationDto internalEmployeeForCreation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // create an internal employee entity with default values filled out
            // and the values inputted via the POST request
            var internalEmployee =
                    await _employeeService.CreateInternalEmployeeAsync(
                        internalEmployeeForCreation.FirstName, internalEmployeeForCreation.LastName);

            // persist it
            await _employeeService.AddInternalEmployeeAsync(internalEmployee);

            // return created employee after mapping to a DTO
            return CreatedAtAction("GetInternalEmployee", _mapper.Map<InternalEmployeeDto>(internalEmployee), new { employeeId = internalEmployee.Id });
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetProtectedInternalEmployee()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("GetInternalEmployees", "ProtectedInternalEmployee");
            }
            return RedirectToAction("GetInternalEmployees", "InternalEmployees");
        }

    }
}
