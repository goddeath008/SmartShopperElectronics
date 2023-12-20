using System;
using System.Collections.Generic;

namespace MyProWeb.Models.Domain;

public partial class Favorite
{
    public int Idfavo { get; set; }

    public int? Idpro { get; set; }

    public virtual Product? IdproNavigation { get; set; }
}
