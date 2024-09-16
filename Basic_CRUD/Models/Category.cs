using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Basic_CRUD.Models;

public partial class Category
{
    public long IdCategory { get; set; }

    public string? Title { get; set; } = null!;

    public string? Description { get; set; }

    [JsonIgnore]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
