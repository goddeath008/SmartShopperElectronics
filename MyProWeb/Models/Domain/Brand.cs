using System;
using System.Collections.Generic;

namespace MyProWeb.Models;

public partial class Brand
{
    public int Idbrand { get; set; }

    public string NameBrand { get; set; } = null!;

    public string? ImgBrand { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
