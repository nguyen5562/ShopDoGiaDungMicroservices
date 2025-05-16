﻿using AccountServices.DTO;
namespace AccountServices.Services.Interfaces
{
    public interface ILogService
    {
        Task<(IEnumerable<LogEntry> Logs, int TotalCount)> GetLogsAsync(int page, int pageSize);
        Task InsertLogAsync(string userId, string action, string objects, string ip);
    }
}
