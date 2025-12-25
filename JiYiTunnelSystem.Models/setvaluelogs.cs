
namespace JiYiTunnelSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("jiyitunnelsystem.setvaluelogs")]
    public partial class setvaluelogs:BaseEntity
    {
        public long SensorId { get; set; }
        public long SectionId { get; set; }
        public decimal? InitialValue { get; set; }
        public virtual sensors sensors { get; set; }
        public virtual sections sections { get; set; }
    }
}
