﻿using RoleServices.DTO;
namespace RoleServices.Services.Interfaces
{
    public interface ILogService
    {
        Task<(IEnumerable<LogEntry> Logs, int TotalCount)> GetLogsAsync(int page, int pageSize);
        Task InsertLogAsync(string userId, string action, string objects, string ip);
    }
}
