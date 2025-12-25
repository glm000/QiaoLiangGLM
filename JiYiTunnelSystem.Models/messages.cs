namespace JiYiTunnelSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("jiyitunnelsystem.messages")]
    public partial class messages : BaseEntity
    {
        [StringLength(4)]
        public string Comment { get; set; }
    }
}
