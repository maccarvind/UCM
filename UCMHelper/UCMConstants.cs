using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMHelper
{
    public class UCMConstants
    {
        public DataTable FabricQuality { get; set; }
        public DataTable PackageType { get; set; }
        public DataTable Looms { get; set; }
        public DataTable InwardOrigin { get; set; }
        public DataTable FabricDefects { get; set; }
    }
}
