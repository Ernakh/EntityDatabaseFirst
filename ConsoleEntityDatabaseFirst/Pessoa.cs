using System;
using System.Collections.Generic;

#nullable disable

namespace ConsoleEntityDatabaseFirst
{
    public partial class Pessoa
    {
        public Pessoa()
        {
            Emails = new HashSet<Email>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Email> Emails { get; set; }
    }
}
