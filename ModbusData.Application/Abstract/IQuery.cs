using MediatR;


namespace ModbusData.Application.Abstract
{
    public interface IQuery<TResponse> : IRequest<TResponse>
    {

    }
}
