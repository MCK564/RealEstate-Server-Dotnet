using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace buildingWebApi.Models
{
    [Table("buildingimage")]
    public class BuildingImageEntity :BaseEntity
    {
        [ForeignKey("building_id")]
        public BuildingEntity? building{ get; set; }

        [Column("image_url")]
        public string imageUrl{ get; set; } = string.Empty;

        public string description{ get; set; } = string.Empty;
    }
}