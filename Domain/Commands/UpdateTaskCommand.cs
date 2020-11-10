using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands
{
    public class UpdateTaskCommand
    {
        public Guid Id { get; set; }
        public Guid? Member { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
    }
}

