using Domain.Commands;
using Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebClient.Abstractions;
using Microsoft.AspNetCore.Components;
using Domain.ViewModel;
using Core.Extensions.ModelConversion;

namespace WebClient.Services
{
    public class TaskDataService: ITaskDataService
    {
        private readonly HttpClient httpClient;
        public TaskDataService(IHttpClientFactory clientFactory)
        {
            httpClient = clientFactory.CreateClient("FamilyTaskAPI");
            tasks = new List<TaskVm>();
            LoadTasks();
        }
        
        private IEnumerable<TaskVm> tasks;

        public IEnumerable<TaskVm> Tasks => tasks;
        public TaskVm SelectedTask { get; private set; }


        public event EventHandler TasksUpdated;
        public event EventHandler TaskSelected;
        public event EventHandler<string> UpdateTaskFailed;

        private async void LoadTasks()
        {
            tasks = (await GetAllTasks()).Payload;
            TasksUpdated?.Invoke(this, null);
        }

        private async Task<AddTaskCommandResult> Create(AddTaskCommand command)
        {
            return await httpClient.PostJsonAsync<AddTaskCommandResult>("tasks", command);
        }

        private async Task<UpdateTaskCommandResult> Update(UpdateTaskCommand command)
        {
            return await httpClient.PutJsonAsync<UpdateTaskCommandResult>($"tasks/{command.Id}", command);
        }

        private async Task<GetAllTasksQueryResult> GetAllTasks()
        {
            return await httpClient.GetJsonAsync<GetAllTasksQueryResult>("tasks");
        }

        public void SelectTask(Guid id)
        {
            SelectedTask = Tasks.SingleOrDefault(t => t.Id == id);
            TasksUpdated?.Invoke(this, null);
        }

        public async Task ToggleTask(Guid id)
        {
            foreach (var taskModel in Tasks)
            {
                if (taskModel.Id == id)
                {
                    taskModel.IsDone = !taskModel.IsDone;

                    var result = await Update(taskModel.ToUpdateTaskCommand());

                    Console.WriteLine(JsonSerializer.Serialize(result));

                    if (result != null)
                    {
                        var updatedList = (await GetAllTasks()).Payload;

                        if (updatedList != null)
                        {
                            tasks = updatedList;
                            TasksUpdated?.Invoke(this, null);
                            return;
                        }
                        UpdateTaskFailed?.Invoke(this, "The save was successful, but we can no longer get an updated list of members from the server.");
                    }

                    UpdateTaskFailed?.Invoke(this, "Unable to save changes.");
                }
            }
        }

        public async Task AddTask(TaskVm model)
        {
            var result = await Create(model.ToAddTaskCommand());
            if (result != null)
            {
                var updatedList = (await GetAllTasks()).Payload;

                if (updatedList != null)
                {
                    tasks = updatedList;
                    TasksUpdated?.Invoke(this, null);
                    return;
                }
                UpdateTaskFailed?.Invoke(this, "The creation was successful, but we can no longer get an updated list of members from the server.");
            }

            UpdateTaskFailed?.Invoke(this, "Unable to create record.");
        }
    }
}