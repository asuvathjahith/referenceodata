using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GrexelApi.Models;
[Serializable]
public partial class Organization
{
    [JsonProperty("OrganizationID")]
    public Guid OrganizationID { get; set; }

    public string Name { get; set; } = null!;

    public string BusinessID { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IsLocked { get; set; }

    public bool IsPhysicalPerson { get; set; }
    public Address BusinessAddress { get; set; } = new Address();

    public virtual ICollection<Address> Addresses { get; } = new List<Address>();
}
