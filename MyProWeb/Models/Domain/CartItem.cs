using System;
using System.Collections.Generic;

namespace MyProWeb.Models;

public partial class CartItem
{
    public int Idcart { get; set; }

    public int? Idproduct { get; set; }

    public virtual Product? IdproductNavigation { get; set; }
}
