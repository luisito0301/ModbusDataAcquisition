using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Application.Abstract;
using ModbusData.Application.Unit.Commands.CreateUnit;
using ModbusData.Contract;
using ModbusData.Contract.Units;
using ModbusData.DataAccess.Repositories.Common;
using ModbusData.Domain.Entities.Unit; // Asegúrate de que la ruta sea correcta
using ModbusData.Domain.Entities.Variables;

namespace ModbusData.Application.Units.Commands.CreateUnit
{
    public class CreateUnitCommandHandler : ICommandHandler<CreateUnitCommand, ModbusData.Domain.Entities.Unit.Unit>
    {
        private readonly IUnitRepository<ModbusData.Domain.Entities.Unit.Unit> _unitRepository; // Asegúrate de tener un repositorio para Unit
        private readonly IUnitOfWork _unitOfWork;

        public CreateUnitCommandHandler(
            IUnitRepository<ModbusData.Domain.Entities.Unit.Unit> unitRepository,
            IUnitOfWork unitOfWork)
        {
            _unitRepository = unitRepository;
            _unitOfWork = unitOfWork;
        }

        public  Task<ModbusData.Domain.Entities.Unit.Unit> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
        {
            // Create a new instance of Unit
            ModbusData.Domain.Entities.Unit.Unit unit = new ModbusData.Domain.Entities.Unit.Unit(
                Guid.NewGuid(), // Generate a new ID
                request.ManufactererName,
                request.Code,
                request.AreaName,
                request._variables // Assuming Variables is a List<AnalogicVariable>
            );

            // Add the new Unit to the repository
            _unitRepository.Add(unit);
            _unitOfWork.SaveChanges(); // Save changes asynchronously

            return Task.FromResult(unit); // Return the created instance
        }
    }
}