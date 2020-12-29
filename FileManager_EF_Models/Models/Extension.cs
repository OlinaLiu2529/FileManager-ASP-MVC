using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager_EF_Models.Models.Base;

namespace FileManager_EF_Models.Models
{
    public partial class Extension:EntityBase
    {
     
        public string Type { get; set; }
        public string Icon { get; set; }
    }
}
