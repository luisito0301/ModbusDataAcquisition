using ModbusData.Application.Abstract;
using ModbusData.Contract; // Assuming this is where your repository interface is defined
using ModbusData.Domain.Entities.Variables; // Adjust the namespace as necessary
using System;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Application.Variables.Commands.UpdateDigitalVariable;
using ModbusData.Contract.Variables;

namespace ModbusData.Application.Variables.Commands.UpdateDigitalVariable
{
    public class UpdateDigitalVariableCommandHandler : ICommandHandler<UpdateDigitalVariableCommand, bool>
    {
        private readonly IVariableRepository<DigitalVariable> _digitalVariableRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDigitalVariableCommandHandler(
            IVariableRepository<DigitalVariable> digitalVariableRepository,
            IUnitOfWork unitOfWork)
        {
            _digitalVariableRepository = digitalVariableRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(UpdateDigitalVariableCommand request, CancellationToken cancellationToken)
        {
            // Find the existing DigitalVariable
            var existingDigitalVariable = _digitalVariableRepository.GetById(request.Id);

            if (existingDigitalVariable == null)
            {
                return Task.FromResult(false); // Return false if the DigitalVariable was not found
            }

            // Create a new instance of DigitalVariable with updated values
            var updatedDigitalVariable = new DigitalVariable(
                existingDigitalVariable.Id, // Keep the same ID
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
            _digitalVariableRepository.Update(updatedDigitalVariable);
            _unitOfWork.SaveChanges(); // Save changes synchronously

            return Task.FromResult(true); // Return true if the update was successful
        }
    }
}
