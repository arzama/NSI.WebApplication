﻿using NSI.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.TaskRepository;
using NSI.Repository.Interfaces;
using NSI.Repository;
using NSI.BLL.Helpers;

namespace NSI.BLL
{
    public class TaskManipulation : ITaskManipulation
    {
        ITaskRepository _taskRepository;


        public TaskManipulation(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public TaskDto CreateTask(TaskDto taskDto)
        {
            return _taskRepository.CreateTask(taskDto);
        }

        public bool DeleteTaskById(int taskId)
        {
            return _taskRepository.DeleteTaskById(taskId);
        }

        public TaskDto EditTask(int taskId, TaskDto task)
        {
            return _taskRepository.EditTask(taskId, task);
        }

        public TaskDto GetTaksById(int taskId)
        {
            return _taskRepository.GetTaskById(taskId);
        }

        public ICollection<TaskDto> GetTasks(int? pageNumber = null, int? pageSize = null)
        {
            var tasks= _taskRepository.GetTasks();
            if (pageNumber != null && pageSize != null)
            {
               return PagingHelper<TaskDto>.PagedList(tasks, (int)pageNumber, (int)pageSize);
            }
            return tasks;
        }

        public ICollection<TaskDto> SearchTasks(TaskSearchCriteriaDto searchCriteria, int pageNumber, int pageSize)
        {
            return PagingHelper<TaskDto>.PagedList(_taskRepository.SearchTasks(searchCriteria), pageNumber, pageSize);
        }
    }
}