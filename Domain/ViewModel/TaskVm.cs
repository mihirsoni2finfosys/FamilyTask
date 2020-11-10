using Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModel
{
    public class TaskVm
    {
        public Guid Id { get; set; }
        public Guid? Member { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public MemberVm MemberObj { get; set; }
        public UpdateTaskCommand ToUpdateCommand()
        {
            throw new NotImplementedException();
        }
    }
}

