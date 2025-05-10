using MediatR;


namespace ModbusData.Application.Abstract
{
    public interface ICommand : IRequest
    {

    }

    public interface ICommand<TResponse> : IRequest<TResponse>
    {

    }
}