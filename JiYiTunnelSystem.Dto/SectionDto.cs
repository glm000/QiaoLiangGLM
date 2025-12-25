using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiYiTunnelSystem.Dto
{
    public class SectionDto
    {
        public long Id { get; set; }

        public string Shaft { get; set; }

        public long EngId { get; set; }

        public string EngName { get; set; }

        public string SectionNum { get; set; }

        public string[] SensorType { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
