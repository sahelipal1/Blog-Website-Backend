using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Blog_website.DAL.Entity;

[Table("users")]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("password")]
    [StringLength(50)]
    public string Password { get; set; } = null!;

    [Column("is_active")]
    public bool? IsActive { get; set; }

    [Column("createddate")]
    public DateOnly? Createddate { get; set; }

    [Column("updateddate")]
    public DateOnly? Updateddate { get; set; }

    [Column("createdby")]
    public int? Createdby { get; set; }

    [Column("updatedby")]
    public int? Updatedby { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column("phonenumber")]
    [StringLength(12)]
    public string? Phonenumber { get; set; }

    [Column("is_deleted")]
    public bool? IsDeleted { get; set; }

    [ForeignKey("Createdby")]
    [InverseProperty("InverseCreatedbyNavigation")]
    public virtual User? CreatedbyNavigation { get; set; }

    [InverseProperty("CreatedbyNavigation")]
    public virtual ICollection<User> InverseCreatedbyNavigation { get; set; } = new List<User>();

    [InverseProperty("UpdatedbyNavigation")]
    public virtual ICollection<User> InverseUpdatedbyNavigation { get; set; } = new List<User>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    [ForeignKey("Updatedby")]
    [InverseProperty("InverseUpdatedbyNavigation")]
    public virtual User? UpdatedbyNavigation { get; set; }
}
