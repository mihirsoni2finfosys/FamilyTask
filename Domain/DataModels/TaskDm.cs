using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DataModels
{
    public class TaskDm
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? Member { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public Member MemberObj { get; set; }
    }
}

