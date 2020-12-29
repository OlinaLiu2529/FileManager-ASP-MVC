using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager_EF_Models.Models.Base;

namespace FileManager_EF_Models.Models
{
    public partial class File:EntityBase
    {
      //  public int FileID { get; set; }

        public string FileName { get; set; }
        public string DescriptionFile { get; set; }
        public int FolderId { get; set; }

        public int ExtensionId { get; set; }

   
        public virtual Folder Folder { get; set; }
  
        public virtual Extension Extension { get; set; }
        public string Content { get; set; }

    }
}
