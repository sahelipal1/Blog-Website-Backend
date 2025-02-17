using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Blog_website.DAL.Entity;

[Table("category")]
[Index("CategoryName", Name = "category_category_name_key", IsUnique = true)]
public partial class Category
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("category_name")]
    [StringLength(50)]
    public string CategoryName { get; set; } = null!;

    [Column("category_description")]
    public string? CategoryDescription { get; set; }

    [InverseProperty("CategoryNavigation")]
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
