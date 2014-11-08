using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    [Serializable]
    public class UnderWear
    {
        public int Uid { get;set;}
        public string UderWearName { get; set; }
        public DateTime AddTime { get;set;}
    }
}