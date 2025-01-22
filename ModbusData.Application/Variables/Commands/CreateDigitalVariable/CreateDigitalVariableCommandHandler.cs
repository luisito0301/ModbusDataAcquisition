using MediatR;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Contract.Variables;
using ModbusData.Contract;
using System;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Application.Abstract;

namespace ModbusData.Application.Variables.Commands.CreateDigitalVariable
{
    public class CreateDigitalVariableCommandHandler : ICommandHandler<CreateDigitalVariableCommand, DigitalVariable>
    {
        private readonly IVariableRepository<DigitalVariable> _digitalVariableRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDigitalVariableCommandHandler(
            IVariableRepository<DigitalVariable> digitalVariableRepository,
            IUnitOfWork unitOfWork)
        {
            _digitalVariableRepository = digitalVariableRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<DigitalVariable> Handle(CreateDigitalVariableCommand request, CancellationToken cancellationToken)
        {
            var digitalVariable = new DigitalVariable(
                Guid.NewGuid(),
                request.Name,
                request.Type,
                request.IsMeasurement,
                request.Code,
                request.SamplingPeriod,
                request.ModbusAddress)
            {
                Value = request.Value,
                UnitId = request.UnitId
            };

            _digitalVariableRepository.Add(digitalVariable);
            _unitOfWork.SaveChanges();

            return Task.FromResult(digitalVariable);
        }
    }
}
