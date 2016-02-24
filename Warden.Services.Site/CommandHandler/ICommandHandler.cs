using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.Server.Services.Command;

namespace Warden.Server.Services.CommandHandler
{
    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        Task<TResult> Handle(TCommand command);
    }
}
