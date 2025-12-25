namespace JiYiTunnelSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("jiyitunnelsystem.sensors")]
    public partial class sensors:BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sensors()
        {
            alarmlogs = new HashSet<alarmlogs>();
            setvaluelogs = new HashSet<setvaluelogs>();
        }

        public long SectionId { get; set; }

        [Required]
        [StringLength(4)]
        public string SensorType { get; set; }

        [Required]
        [StringLength(10)]
        public string SensorNumber { get; set; }

        public decimal? InitialValue { get; set; }

        [StringLength(50)]
        public string Comment { get; set; }

        public decimal? K { get; set; }
        [Required]
        public sbyte IsAlarm { get; set; } = 0;

        public virtual sections sections { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<alarmlogs> alarmlogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<setvaluelogs> setvaluelogs { get; set; }
    }
}
