using System;
using System.Collections.Generic;
using MyProWeb.Models.Domain;

namespace MyProWeb.Models;

public partial class Product
{
    public int Idpro { get; set; }

    public string NamePro { get; set; } = null!;

    public int? Price { get; set; }

    public int? Idcate { get; set; }

    public int? Idbrand { get; set; }

    public int? Iduser { get; set; }

    public string? ImgPro { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual Brand? IdbrandNavigation { get; set; }

    public virtual Category? IdcateNavigation { get; set; }

    public virtual User? IduserNavigation { get; set; }
}
