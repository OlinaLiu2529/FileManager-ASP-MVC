using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager_EF_Models.Models.Base;

namespace FileManager_EF_Models.Models
{
    public partial class Folder:EntityBase
    {
        //public int FolderID { get; set; }
        public string FolderName { get; set; }
        public string ParentFolder { get; set; }

        public virtual ICollection<File> Files { get; set; } = new HashSet<File>();
    }
}
