﻿using PowerSell.Models;
using System;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime RegisteredDate { get; set; }
    public DateTime LastLogin { get; set; }
    public string UserType { get; set; }


}