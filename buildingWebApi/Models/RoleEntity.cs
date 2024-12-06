using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buildingWebApi.Models
{
    [Table("role")]
    public class RoleEntity : BaseEntity
    {
        public string name{ get; set; }
        public string code{ get; set; }
        public virtual ICollection<UserEntity> users {get; set;}
    }
}