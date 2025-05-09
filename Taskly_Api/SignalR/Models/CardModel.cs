﻿namespace Taskly_Api.SignalR.Models;

public record CardModel(Guid CardListId, Guid CardId, string Task, DateTime Deadline, Guid? UserId, string? UserAvatar, string? UserName);
