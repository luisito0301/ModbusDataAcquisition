using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Unit; // Adjust the namespace as necessary
using ModbusData.Contract; // Assuming this is where your repository interface is defined
using ModbusData.Units.Commands.DeleteUnits;
using ModbusData.Contract.Units;


namespace ModbusData.Application.Variables.Commands.DeleteUnit
{
    public class DeleteUnitCommandHandler : ICommandHandler<DeleteUnitCommand, bool>
    {
        private readonly IUnitRepository<ModbusData.Domain.Entities.Unit.Unit> _unitRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUnitCommandHandler(
            IUnitRepository<ModbusData.Domain.Entities.Unit.Unit> unitRepository,
            IUnitOfWork unitOfWork)
        {
            _unitRepository = unitRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
        {
            // Find the unit by id
            var unitToDelete = _unitRepository.GetById(request.id); // Assuming you have a method to get by ID

            if (unitToDelete == null)
            {
                return Task.FromResult(false); // Return false if the unit was not found
            }

            // Delete the AnalogicVariable
            _unitRepository.Delete(request.id);
            _unitOfWork.SaveChanges(); // Save changes synchronously

            return Task.FromResult(true); // Return true if deletion was successful
        }
    }
}
