using System;
using System.Collections.Generic;

namespace NetCoreSession.Models.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public byte[]? Picture { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
