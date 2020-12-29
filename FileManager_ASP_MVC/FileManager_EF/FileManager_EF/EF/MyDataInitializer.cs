using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager_EF_Models.Models;


namespace FileManager_EF.EF
{
    public class MyDataInitializer: DropCreateDatabaseAlways<FileManagerEntities>
    {
        protected override void Seed(FileManagerEntities context)
        {
            var extensions = new List<Extension>
            {
                new Extension{Type="txt", Icon="text"}
            };
            context.Extensions.AddOrUpdate(x => new { x.Type, x.Icon }, extensions.ToArray());

            var folders = new List<Folder>
            {
                new Folder{FolderName="System", ParentFolder="Windows" }
            };
            context.Folders.AddOrUpdate(x => new { x.FolderName, x.ParentFolder }, folders.ToArray());

        }
    }
}
