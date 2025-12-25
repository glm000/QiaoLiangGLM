namespace JiYiTunnelSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("jiyitunnelsystem.engineeringsites")]
    public partial class engineeringsites:BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public engineeringsites()
        {
            sections = new HashSet<sections>();
        }



        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        public decimal? StrainAlarmValue { get; set; }

        public decimal? StrainContorlValue { get; set; }

        public decimal? OffsetAlarmValue { get; set; }

        public decimal? OffsetControlValue { get; set; }

        public decimal? VibrationAlarmValue { get; set; }

        public decimal? VibrationControlValue { get; set; }

        public decimal? SteelStressAlarmValue { get; set; }

        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sections> sections { get; set; }
    }
}
