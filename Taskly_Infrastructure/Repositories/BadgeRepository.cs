﻿using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class BadgeRepository(TasklyDbContext tasklyDbContext) 
    : Repository<BadgeEntity>(tasklyDbContext), IBadgeRepository
{
}
