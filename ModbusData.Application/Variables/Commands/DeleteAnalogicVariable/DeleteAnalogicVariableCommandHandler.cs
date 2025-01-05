using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Variables; // Adjust the namespace as necessary
using ModbusData.Contract; // Assuming this is where your repository interface is defined
using System;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Variables.Commands.DeleteAnalogicVariable;
using ModbusData.Contract.Variables;

namespace ModbusData.Application.Variables.Commands.DeleteAnalogicVariable
{
    public class DeleteAnalogicVariableCommandHandler : ICommandHandler<DeleteAnalogicVariableCommand, bool>
    {
        private readonly IVariableRepository<AnalogicVariable> _analogicVariableRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAnalogicVariableCommandHandler(
            IVariableRepository<AnalogicVariable> analogicVariableRepository,
            IUnitOfWork unitOfWork)
        {
            _analogicVariableRepository = analogicVariableRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteAnalogicVariableCommand request, CancellationToken cancellationToken)
        {
            // Find the AnalogicVariable by ID
            var analogicVariableToDelete = _analogicVariableRepository.GetById(request.Id); // Assuming you have a method to get by ID

            if (analogicVariableToDelete == null)
            {
                return Task.FromResult(false); // Return false if the AnalogicVariable was not found
            }

            // Delete the AnalogicVariable
            _analogicVariableRepository.Delete(request.Id);
            _unitOfWork.SaveChanges(); // Save changes synchronously

            return Task.FromResult(true); // Return true if deletion was successful
        }
    }
}
