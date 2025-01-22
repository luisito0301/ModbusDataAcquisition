using System;
using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Domain.Types;
using System;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.DataAccess.Repositories.Common;
using ModbusData.Contract;
using ModbusData.Contract.Variables;

namespace ModbusData.Application.Variables.Commands.CreateDigitalVariable
{



    public class CreateDigitalVariableCommandHandler : ICommandHandler<CreateDigitalVariableCommand?, DigitalVariable>
    {
        private readonly IVariableRepository<DigitalVariable> _digitalvariablerepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDigitalVariableCommandHandler(
            IVariableRepository<DigitalVariable> digitalvariablerepository,
            IUnitOfWork unitOfWork)
        {
            _digitalvariablerepository = digitalvariablerepository;
            _unitOfWork = unitOfWork;
        }

        public Task<DigitalVariable> Handle(CreateDigitalVariableCommand request, CancellationToken cancellationToken)
        {
            // Create a new instance of DigitalVariable
            DigitalVariable result = new DigitalVariable(
                Guid.NewGuid(), // Generate a new ID
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

            // Add the new AnalogicVariable to the repository
            _digitalvariablerepository.Add(result);
            _unitOfWork.SaveChanges(); // Save changes asynchronously

            return Task.FromResult(result); // Return the created instance

        }
    }
}
