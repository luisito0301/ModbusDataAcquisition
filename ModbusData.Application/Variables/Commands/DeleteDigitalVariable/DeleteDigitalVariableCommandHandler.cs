using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Variables; // Adjust the namespace as necessary
using ModbusData.Contract; // Assuming this is where your repository interface is defined
using System;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Variables.Commands.DeleteDigitalVariable;
using ModbusData.Contract.Variables;

namespace ModbusData.Application.Variables.Commands.DeleteDigitalVariable
{
    public class DeleteDigitalVariableCommandHandler : ICommandHandler<DeleteDigitalVariableCommand, bool>
    {
        private readonly IVariableRepository<DigitalVariable> _digitalVariableRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDigitalVariableCommandHandler(
            IVariableRepository<DigitalVariable> digitalVariableRepository,
            IUnitOfWork unitOfWork)
        {
            _digitalVariableRepository = digitalVariableRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteDigitalVariableCommand request, CancellationToken cancellationToken)
        {
            // Find the DigitalVariable by ID
            var digitalVariableToDelete = _digitalVariableRepository.GetById(request.Id); // Assuming you have a method to get by ID

            if (digitalVariableToDelete == null)
            {
                return Task.FromResult(false); // Return false if the AnalogicVariable was not found
            }

            // Delete the AnalogicVariable
            _digitalVariableRepository.Delete(request.Id);
            _unitOfWork.SaveChanges(); // Save changes synchronously

            return Task.FromResult(true); // Return true if deletion was successful
        }
    }
}
