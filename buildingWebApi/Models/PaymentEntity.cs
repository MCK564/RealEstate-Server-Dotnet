using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buildingWebApi.Models
{
    [Table("payment")]
    public class PaymentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }
        [ForeignKey("user_id")]
        public UserEntity? user { get; set; }

        public int status { get; set; }
        public float money{ get; set; }

        public long posts{ get; set; }

        [Column("created_at")]
        public DateTime createdAt{ get; set; }

        [Column("updated_at")]
        public DateTime updatedAt{ get; set;}
    }
}