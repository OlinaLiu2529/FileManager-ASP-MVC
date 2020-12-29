using FileManager_EF_Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager_EF.EF
{
   public class FileManagerEntities: DbContext
    {
        public FileManagerEntities() : base("name=FileManagerConnection")
        {
        }
        public virtual DbSet<Folder> Folders { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Extension> Extensions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Folder>().HasMany(e => e.Files).WithRequired(e => e.Folder).WillCascadeOnDelete(true);
        }
    }
}
