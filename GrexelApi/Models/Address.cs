using System;
using System.Collections.Generic;

namespace GrexelApi.Models;

public partial class Address
{
    public Guid AddressID { get; set; }

    public string Street { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public Guid? OrganizationID { get; set; }

    public virtual Organization? Organization { get; set; }
}
