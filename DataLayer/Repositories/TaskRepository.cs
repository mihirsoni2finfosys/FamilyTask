using Core.Abstractions.Repositories;
using Domain.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataLayer
{
    public class TaskRepository : BaseRepository<Guid, TaskDm, TaskRepository>, ITaskRepository
    {
        public TaskRepository(FamilyTaskContext context) : base(context)
        {
            
        }
        public async new Task<IEnumerable<TaskDm>> ToListAsync(CancellationToken cancellationToken = default)
        {
            return await
                Query.Include(x => x.MemberObj).ToListAsync(cancellationToken);
        }

        ITaskRepository IBaseRepository<Guid, TaskDm, ITaskRepository>.NoTrack()
        {
            return base.NoTrack();
        }

        ITaskRepository IBaseRepository<Guid, TaskDm, ITaskRepository>.Reset()
        {
            return base.Reset();
        }


    }
}

