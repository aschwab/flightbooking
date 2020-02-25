using MediatR;
using System;

namespace FBS.Domain.Core
{
    public interface ICommand : INotification
    {
        Guid Id { get; set; }
    }
}