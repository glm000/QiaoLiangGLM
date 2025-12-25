using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiYiTunnelSystem.Models
{
    public class BaseEntity
    {
        public long Id { get; set; } = new long();
        [Column(TypeName = "timestamp")]
        public DateTime? CreateTime { get; set; } = DateTime.Now;

        public sbyte? IsDeleted { get; set; } = 0;
    }
}
