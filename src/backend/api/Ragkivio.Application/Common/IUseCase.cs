using FluentResults;

namespace Ragkivio.Application.Common;

public interface IUseCase<T_OUT,in T_IN>
where T_IN : class 
where T_OUT : class {
    Result<T_OUT> Handle(T_IN input);
}

public interface IUseCaseAsync<T_OUT, in T_IN>
where T_IN : class 
where T_OUT : class {
    Task<Result<T_OUT>> HandleAsync(T_IN input, CancellationToken token = default);
}

public interface IUseCaseAsync {
    Task<Result> HandleAsync(CancellationToken token = default);
}

public interface IUseCase {
    Result Handle();
}