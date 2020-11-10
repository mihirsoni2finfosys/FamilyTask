using Domain.Commands;
using Domain.Queries;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions.Services
{
    public interface ITaskService
    {
        Task<AddTaskCommandResult> AddTaskCommandHandler(AddTaskCommand command);
        Task<UpdateTaskCommandResult> UpdateTaskCommandHandler(UpdateTaskCommand command);
        Task<GetAllTasksQueryResult> GetAllTasksQueryHandler();
        Task<TaskVm> GetTaskQueryHandler(Guid id);
    }
}

