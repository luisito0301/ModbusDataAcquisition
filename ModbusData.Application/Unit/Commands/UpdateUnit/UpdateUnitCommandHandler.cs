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

            // Update the existing Unit with new values
            existingUnit.ManufactererName = request.ManufactererName;
            existingUnit.Code = request.Code;
            existingUnit.AreaName = request.AreaName;
            existingUnit.UnitTypes = request.Type;

            // Update the Unit in the repository
            _unitRepository.Update(existingUnit);
            _unitOfWork.SaveChanges(); // Save changes

            return Task.FromResult(true); // Return true if the update was successful
        }
    }
}
