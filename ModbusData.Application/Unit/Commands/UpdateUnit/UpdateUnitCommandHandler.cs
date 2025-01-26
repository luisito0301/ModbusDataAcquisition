using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Application.Abstract;
using ModbusData.Application.Unit.Commands.UpdateUnit;
using ModbusData.Contract;
using ModbusData.Contract.Units;
using ModbusData.DataAccess.Repositories.Common;
using ModbusData.Domain.Entities.Unit; // Asegúrate de que la ruta sea correcta
using ModbusData.Domain.Entities.Variables;


namespace ModbusData.Application.Units.Commands.UpdateUnit
{
    public class UpdateUnitCommandHandler : ICommandHandler<UpdateUnitCommand, bool>
    {
        private readonly IUnitRepository<ModbusData.Domain.Entities.Unit.Unit> _unitRepository; // Asegúrate de tener un repositorio para Unit
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUnitCommandHandler(
            IUnitRepository<ModbusData.Domain.Entities.Unit.Unit> unitRepository,
            IUnitOfWork unitOfWork)
        {
            _unitRepository = unitRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
        {
            // Find the existing Unit
            var existingUnit = _unitRepository.GetById(request.Id);

            if (existingUnit == null)
            {
                return Task.FromResult(false); // Return false if the Unit was not found
            }

            // Create a new instance of Unit
            var updatedUnit = new ModbusData.Domain.Entities.Unit.Unit(
                existingUnit.Id, // keep the ID
                request.ManufactererName,
                request.Code,
                request.AreaName,
                request.Type

            );

            // Add the new Unit to the repository
            _unitRepository.Add(updatedUnit);
            _unitOfWork.SaveChanges(); // Save changes asynchronously

            return Task.FromResult(true); // Return true if the update was successful
        }
    }
}
