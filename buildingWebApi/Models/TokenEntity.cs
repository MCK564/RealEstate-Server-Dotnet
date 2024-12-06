using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buildingWebApi.Models
{
    [Table("tokens")]
    public class TokenEntity 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id{get; set;}
        public string token{ get; set; }

        [Column("refresh_token")]
        public string refreshToken{ get; set; }

        [Column("token_type")]        
        public string tokenType{ get; set; }

        [Column("expiration_date")]
        public DateTime expirationDate{ get; set; }

        [Column("refresh_expiration_date")]
        public DateTime refreshExpirationDate{ get; set; }
        
        [Column("is_mobile")]
        public bool isMobile{ get; set; }

        public bool revoked{ get; set; }
        public bool expired{ get; set; }

        public UserEntity? user{ get; set; }

    }
}