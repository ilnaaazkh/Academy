﻿using Academy.SharedKernel;
using CSharpFunctionalExtensions;

namespace Academy.Core.Abstractions
{
    public interface ICommandHandler<TResponse, in TCommand> where TCommand : ICommand  
    {
        Task<Result<TResponse, ErrorList>> Handle(TCommand command, CancellationToken cancellationToken = default);
    }

    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task<UnitResult<ErrorList>> Handle(TCommand command, CancellationToken cancellationToken = default);
    }
}
