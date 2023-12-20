using System;
using System.Collections.Generic;

namespace MyProWeb.Models;

public partial class User
{
    public int Iduser { get; set; }

    public int? PhoneNumber { get; set; }

    public string NameUser { get; set; } = null!;

    public string? PassUser { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
