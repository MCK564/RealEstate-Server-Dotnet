using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buildingWebApi.Models
{
    [Table("rentarea")]
    public class RentAreaEntity : BaseEntity
    {
        [Column("value")]
        public long value{ get; set; }

        [Column("building_id")]
        public long BuildingId{ get; set; }

        [ForeignKey("BuildingId")]
        public virtual BuildingEntity building{ get; set; }
    }
}