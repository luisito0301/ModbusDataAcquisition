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

namespace ModbusData.Application.Variables.Commands.CreateAnalogicVariable
{
   

   
        public class CreateAnalogicVariableCommandHandler : ICommandHandler<CreateAnalogicVariableCommand?, AnalogicVariable>
        {
            private readonly IVariableRepository<AnalogicVariable> _analogicvariablerepository;
            private readonly IUnitOfWork _unitOfWork;

            public CreateAnalogicVariableCommandHandler(
                IVariableRepository<AnalogicVariable> analogicvariablerepository,
                IUnitOfWork unitOfWork)
            {
                _analogicvariablerepository = analogicvariablerepository;
                _unitOfWork = unitOfWork;
            }

            public Task<AnalogicVariable> Handle(CreateAnalogicVariableCommand request, CancellationToken cancellationToken)
            {
                // Create a new instance of AnalogicVariable
                AnalogicVariable result = new AnalogicVariable(
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
                _analogicvariablerepository.Add(result);
                _unitOfWork.SaveChanges(); // Save changes asynchronously

                return Task.FromResult(result); // Return the created instance
            
        }
    }
}
