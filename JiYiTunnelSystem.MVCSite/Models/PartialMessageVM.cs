using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiYiTunnelSystem.MVCSite.Models
{
    public class PartialMessageVM
    {
        public string Message { get; set; }
        public string Icon { get; set; } = "warning";
        public string Url { get; set; }
    }
}