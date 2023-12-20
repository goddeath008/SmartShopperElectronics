using System;
using System.Collections.Generic;

namespace MyProWeb.Models;

public partial class Category
{
    public int Idcate { get; set; }

    public string NameCate { get; set; } = null!;

    public string? ImgCate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
