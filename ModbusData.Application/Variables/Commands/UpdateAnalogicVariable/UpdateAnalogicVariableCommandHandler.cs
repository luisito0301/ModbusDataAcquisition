using ModbusData.Application.Abstract;
using ModbusData.Contract; // Assuming this is where your repository interface is defined
using ModbusData.Domain.Entities.Variables; // Adjust the namespace as necessary
using System;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Application.Variables.Commands.UpdateAnalogicVariable;
using ModbusData.Contract.Variables;

namespace ModbusData.Application.Variables.Commands.UpdateAnalogicVariable
{
    public class UpdateAnalogicVariableCommandHandler : ICommandHandler<UpdateAnalogicVariableCommand, bool>
    {
        private readonly IVariableRepository<AnalogicVariable> _analogicVariableRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAnalogicVariableCommandHandler(
            IVariableRepository<AnalogicVariable> analogicVariableRepository,
            IUnitOfWork unitOfWork)
        {
            _analogicVariableRepository = analogicVariableRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(UpdateAnalogicVariableCommand request, CancellationToken cancellationToken)
        {
            // Find the existing AnalogicVariable
            var existingAnalogicVariable = _analogicVariableRepository.GetById(request.Id);

            if (existingAnalogicVariable == null)
            {
                return Task.FromResult(false); // Return false if the AnalogicVariable was not found
            }

            // Create a new instance of AnalogicVariable with updated values
            var updatedAnalogicVariable = new AnalogicVariable(
                existingAnalogicVariable.Id, // Keep the same ID
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

            // Update the AnalogicVariable in the repository
            _analogicVariableRepository.Update(updatedAnalogicVariable);
            _unitOfWork.SaveChanges(); // Save changes synchronously

            return Task.FromResult(true); // Return true if the update was successful
        }
    }
}