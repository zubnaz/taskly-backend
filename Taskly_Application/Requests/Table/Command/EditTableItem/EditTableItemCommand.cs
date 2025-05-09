using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Command.EditTableItem;

public record EditTableItemCommand(
    Guid Id,
    string? Task,
    string Status,
    DateTime EndTime,
    string? Label) : IRequest<ErrorOr<TableItemEntity>>;