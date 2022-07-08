﻿using token.Domain.Shared;
using Volo.Abp.Domain.Entities.Auditing;

namespace token.Domain.Records;

public class WordLogs : CreationAuditedEntity<Guid>
{
    public WordType Type { get; set; }

    /// <summary>
    /// 请求ip
    /// </summary>
    public string ip { get; set; }

    /// <summary>
    /// 请求设备
    /// </summary>
    public string Device { get; set; }

    public WordLogs()
    {
    }

    public WordLogs(Guid id, WordType type, string ip, string device) : base(id)
    {
        Type = type;
        this.ip = ip;
        Device = device;
    }
}