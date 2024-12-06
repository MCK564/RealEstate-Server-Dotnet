using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace buildingWebApi.Models
{
    [Table("building")]
    public class BuildingEntity : BaseEntity
    {
        public virtual ICollection<BuildingImageEntity> buildingImages {get; set;}

        public string name{ get; set; }
        public string street{ get; set; }
        public string ward{ get; set; }
        public string district{ get; set; }
        public string structure{ get; set; }

        [Column("numberofbasement")]
        public int numberOfBasement{ get; set; }

        [Column("floorarea")]
        public int floorArea{ get; set; }

        public string direction{ get; set; }
        public string level{ get; set; }
        
        [Column("rentprice")]            
        public int rentPrice{ get; set; }

        [Column("rentpricedescription")]
        public string rentPriceDescription{ get; set; }

        [Column("servicefee")]
        public string serviceFee{ get; set; }

        [Column("carfee")]
        public string carFee{ get; set; }

        [Column("motofee")]
        public string motoFee{ get; set; }

        [Column("overtimefee")]
        public string overtimeFee{ get; set; }

        [Column("waterfee")]
        public string waterFee{ get; set; }

        [Column("electricityfee")]
        public string electricityFee{ get; set; }

        
        public string deposit{ get; set; }
        public string payment{ get; set; }

        [Column("renttime")]
        public string rentTime{ get; set; }

        [Column("decorationtime")]
        public string decorationTime{ get; set; }

        [Column("brokeragetee")]
        public decimal brokerageFee { get; set; }

        
        public string note{ get; set; }
        public string map{ get; set; }
        public string avatar{ get; set; }
        
        [Column("managername")]
        public string managerName{ get; set; }
        
        [Column("managerphone")]
        public string managerPhone{ get; set; }
        public string type{ get; set; }
        public int status{ get; set; }

        [Column("onwer_id")]
        public long ownerId { get; set; }

    }
}