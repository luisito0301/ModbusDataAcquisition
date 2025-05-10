using MediatR;


namespace ModbusData.Application.Abstract
{
    public interface IQueryHandler<TQuery, TResponse>
        : IRequestHandler<TQuery, TResponse>
        where TQuery : IRequest<TResponse>
    {

    }
}
