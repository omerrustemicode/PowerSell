using PowerSell.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime RegisteredDate { get; set; }
    public DateTime LastLogin { get; set; }
    public string UserType { get; set; }
    // Navigation property to Orders
    public virtual ICollection<Orders> Orders { get; set; }

}
