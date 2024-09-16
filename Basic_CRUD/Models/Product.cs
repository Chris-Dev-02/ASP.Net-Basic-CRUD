using System;
using System.Collections.Generic;

namespace Basic_CRUD.Models;

public partial class Product
{
    public long IdProducts { get; set; }

    public string? Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Brand { get; set; }

    public decimal? Price { get; set; }

    public string? Barcode { get; set; }

    public long? Category { get; set; }

    public virtual Category? CategoryNavigation { get; set; } = null!;
}
