using GrexelApi.Models;
using GrexelApi.Repository;

namespace GrexelApi.Repository;
public interface IOrganizationRepository
{
    object AddOrganization(Organization organization);
    List<Organization> GetOrganization();
}
