using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [Column("createddate")]
    public DateTime? CreatedDate { get; set; }

    [Column("createdby")]
    public string? CreatedBy { get; set; }

    [Column("modifieddate")]
    public DateTime? ModifiedDate { get; set; }

    [Column("modifiedby")]
    public string? ModifiedBy { get; set; }

    public BaseEntity()
    {
        CreatedDate = DateTime.Now;
        ModifiedDate = DateTime.Now;
    }

    public virtual void OnInsert()
    {
    CreatedDate = DateTime.Now;
    ModifiedDate = DateTime.Now;
    }

    public virtual void OnUpdate()
    {
    ModifiedDate = DateTime.Now;
    }
}
