using System.Collections.Generic;
using System;

public class ClientServices
{
    public int ServiceId { get; set; }
    public string ServiceName { get; set; }
    public double ServicePrice { get; set; }
    public string ServiceCategory { get; set; }
    public DateTime ServiceDate { get; set; }

    // Collection of users associated with this service
    public virtual ICollection<User> Workers { get; set; } = new List<User>();
}
