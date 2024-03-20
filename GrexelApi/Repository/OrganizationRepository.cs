using GrexelApi.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GrexelApi.Repository;
public class OrganizationRepository : IOrganizationRepository
{
    private readonly GrexelContext dbContext;
    public OrganizationRepository(GrexelContext context)
    {
        dbContext = context;
    }
    public object AddOrganization(Organization organization)
    {
        // Validate the organization
        if (!IsValidOrganization(organization))
        {
            // Handle validation failure
            throw new InvalidOperationException("Invalid organization data");
        }
        var jsonDataParameter = new SqlParameter("@JsonData", SqlDbType.NVarChar)
        {
            Value = JsonConvert.SerializeObject(organization)
        };

        // Create a parameter for the output result (OrganizationID)
        var organizationIdParameter = new SqlParameter("@OrganizationID", SqlDbType.UniqueIdentifier)
        {
            Direction = ParameterDirection.Output
        };

        // Execute the stored procedure
        var result=dbContext.Database.ExecuteSqlRaw(
            "EXEC [dbo].[sp_PostOrganization] @JsonData, @OrganizationID OUTPUT",
            jsonDataParameter
        );

        // Retrieve the OrganizationID (GUID) from the output parameter
        if (organizationIdParameter.Value != DBNull.Value)
        {
            return (Guid)organizationIdParameter.Value;
        }

        // If the value is DBNull.Value, handle the case where no OrganizationID was returned
        throw new InvalidOperationException("OrganizationID was not returned by the stored procedure.");
    }

    public List<Organization> GetOrganization()
    {
        var organization = dbContext.Organizations.FromSqlRaw("EXEC [dbo].[sp_GetOrg]").ToList();
        return organization;
    }
    private bool IsValidOrganization(Organization organization)
    {
        if (string.IsNullOrEmpty(organization.Name) || string.IsNullOrEmpty(organization.BusinessID) || organization.StartDate == DateTime.MinValue)
        {
            return false;
        }
        return true;
    }
}

