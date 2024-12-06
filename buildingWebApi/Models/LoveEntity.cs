using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buildingWebApi.Models

{
    [Table("love")]
    public class LoveEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }
        [ForeignKey("user_id")]
        public UserEntity? user { get; set; }

        [ForeignKey("building_id")]
        public BuildingEntity? building{ get; set; }
    }
}