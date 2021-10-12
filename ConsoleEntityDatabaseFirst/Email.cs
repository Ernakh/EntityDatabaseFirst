using System;
using System.Collections.Generic;

#nullable disable

namespace ConsoleEntityDatabaseFirst
{
    public partial class Email
    {
        public int Id { get; set; }
        public string Email1 { get; set; }
        public int PessoaId { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}
