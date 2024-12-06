using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buildingWebApi.Models
{
    [Table("user")]
    public class UserEntity : BaseEntity
    {
        public string? fullName { get; set; } 
        public string? password { get; set; } 
        public string? email{ get; set; } 
        public string? phone{ get; set; } 
        public string? avatar { get; set; } 

        [Column("post_quantity")]  
        public int? postQuantity{get; set;}

        public RoleEntity? role{ get; set; }
        public virtual ICollection<TokenEntity> tokens{get; set;}
        public virtual ICollection<BuildingEntity> buildings{get;set;}
        public virtual ICollection<PaymentEntity> payments{get; set;}
        
    }
}