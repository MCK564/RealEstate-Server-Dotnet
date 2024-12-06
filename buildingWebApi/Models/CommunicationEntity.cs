using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buildingWebApi.Models
{
    [Table("communication")]
    public class CommunicationEntity 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public string phone{ get; set; }
        public string note { get; set; }

        [ForeignKey("building_id")]
        public virtual BuildingEntity? building{ get; set; }
        
        [ForeignKey("sale_id")]
        public virtual UserEntity? owner{ get; set; }

        [ForeignKey("buyer_renter_id")]
        public virtual UserEntity? buyerRenter{get; set;}

    }
}