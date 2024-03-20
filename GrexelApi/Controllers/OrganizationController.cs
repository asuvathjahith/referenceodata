using System.Data;
using System.Xml.Linq;
using GrexelApi.Models;
using GrexelApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace GrexelApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class OrganizationController : ControllerBase
{
    private readonly SqlConnection _sqlConnection;
    private readonly IOrganizationRepository _repo;

    public OrganizationController(IOrganizationRepository repo)
    {
        _repo = repo;
    }
    //Add Organization and Address
    [HttpPost]
    public IActionResult AddOrganization([FromBody] Organization model)
    {
        //Check for Model is Empty
        if (model == null)
        {
            return BadRequest("Invalid data");
        }
        try
        {
            var data=_repo.AddOrganization(model);
            return Ok(data);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, ex);
        }

    }
    //Getall Organization
    [HttpGet]
    public IActionResult GetOrganization()
    {
        try
        {
            var data = _repo.GetOrganization();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}