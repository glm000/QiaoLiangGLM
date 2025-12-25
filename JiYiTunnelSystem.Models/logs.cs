namespace JiYiTunnelSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("jiyitunnelsystem.logs")]
    public partial class logs:BaseEntity
    {


        public long UserId { get; set; }

        [StringLength(20)]
        public string Behavior { get; set; }

        

        public virtual users users { get; set; }
    }
}
