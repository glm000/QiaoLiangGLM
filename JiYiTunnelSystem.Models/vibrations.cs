namespace JiYiTunnelSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("jiyitunnelsystem.vibrations")]
    public partial class vibrations:BaseEntity
    {
        

        public long SectionId { get; set; }

        public decimal? Data1 { get; set; }

        public decimal? Data2 { get; set; }

        public decimal? Data3 { get; set; }

        public decimal? Data4 { get; set; }

        public decimal? Data5 { get; set; }

        

        public virtual sections sections { get; set; }
    }
}
