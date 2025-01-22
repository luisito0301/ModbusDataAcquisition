using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Variables;
using ModbusData.DataAccess.Repositories.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Contract.Variables;
using ModbusData.Contract;
using ModbusData.Domain.Types;

namespace ModbusData.Application.Variables.Commands.CreateAnalogicVariable
{
    public class CreateVariableCommandHandler : ICommandHandler<ICreateVariableCommand, Variable>
    {
        private readonly IVariableRepository<AnalogicVariable> _analogicVariableRepository;
        private readonly IVariableRepository<DigitalVariable> _digitalVariableRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateVariableCommandHandler(
            IVariableRepository<AnalogicVariable> analogicVariableRepository,
            IVariableRepository<DigitalVariable> digitalVariableRepository,
            IUnitOfWork unitOfWork)
        {
            _analogicVariableRepository = analogicVariableRepository;
            _digitalVariableRepository = digitalVariableRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Variable> Handle(ICreateVariableCommand request, CancellationToken cancellationToken)
        {
            Variable result;

            if (request.Type == VariableType.Analogic)
            {
                result = new AnalogicVariable(
                    Guid.NewGuid(),
                    request.Name,
                    request.Type,
                    request.IsMeasurement,
                    request.Code,
                    request.SamplingPeriod,
                    request.ModbusAddress)
                {
                    Value = (request as CreateAnalogicVariableCommand).Value,
                    UnitId = request.UnitId
                };

                _analogicVariableRepository.Add(result as AnalogicVariable);
            }
            else if (request.Type == VariableType.Digital)
            {
                result = new DigitalVariable(
                    Guid.NewGuid(),
                    request.Name,
                    request.Type,
                    request.IsMeasurement,
                    request.Code,
                    request.SamplingPeriod,
                    request.ModbusAddress)
                {
                    Value = (short)(request as CreateDigitalVariableCommand).Value,
                    UnitId = request.UnitId
                };

                _digitalVariableRepository.Add(result as DigitalVariable);
            }
            else
            {
                throw new ArgumentException("Unsupported variable type.");
            }

            _unitOfWork.SaveChanges();

            return Task.FromResult(result);
        }
    }
}
