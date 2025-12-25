namespace JiYiTunnelSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("jiyitunnelsystem.alarmlogs")]
    public partial class alarmlogs : BaseEntity
    {

        public long SectionId { get; set; }

        public long SensorId { get; set; }
        public decimal? Data { get; set; }

        public sbyte Grade { get; set; }


        public virtual sections sections { get; set; }
        public virtual sensors sensors { get; set; }
    }
}
