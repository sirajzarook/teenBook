using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace teenBook.Data
{
    public class TeenbookContext : DbContext
    {
        public TeenbookContext() : base("DefaultConnection")
        {
            // do not load related child or parent records, instead going with eager loading
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<MultiChoice> MultiChoices { get; set; }
    }
}