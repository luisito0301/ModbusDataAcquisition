using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Variables; // Adjust the namespace as necessary
using ModbusData.Contract; // Assuming this is where your repository interface is defined
using System;
using System.Threading;
using System.Threading.Tasks;

using ModbusData.Contract.Variables;

namespace ModbusData.Application.Variables.Commands.DeleteAnalogicVariable
{
    public class DeleteDigitalVariableCommandHandler : ICommandHandler<DeleteDigitalVariableCommand, bool>
    {
        private readonly IVariableRepository<DigitalVariable> _analogicVariableRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDigitalVariableCommandHandler(
            IVariableRepository<DigitalVariable> analogicVariableRepository,
            IUnitOfWork unitOfWork)
        {
            _analogicVariableRepository = analogicVariableRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteDigitalVariableCommand request, CancellationToken cancellationToken)
        {
            // Find the AnalogicVariable by ID
            var analogicVariableToDelete = _analogicVariableRepository.GetById(request.VariableId); // Assuming you have a method to get by ID

            if (analogicVariableToDelete == null)
            {
                return Task.FromResult(false); // Return false if the AnalogicVariable was not found
            }

            // Delete the AnalogicVariable
            _analogicVariableRepository.Delete(request.VariableId);
            _unitOfWork.SaveChanges(); // Save changes synchronously

            return Task.FromResult(true); // Return true if deletion was successful
        }
    }
}