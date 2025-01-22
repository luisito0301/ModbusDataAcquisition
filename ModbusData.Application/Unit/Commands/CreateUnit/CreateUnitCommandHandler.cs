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

        public Task<ModbusData.Domain.Entities.Unit.Unit> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
        {
            // Crear una nueva instancia de Unit
            var unit = new ModbusData.Domain.Entities.Unit.Unit(
                Guid.NewGuid(), // Generar un nuevo ID
                request.ManufactererName,
                request.Code,
                request.AreaName,
                request.Type
            );

            // Añadir las variables a la unidad
            if (request._variables != null)
            {
                foreach (var variable in request._variables)
                {
                    unit.AddVariable(variable); // Asegúrate de que el método AddVariable esté implementado en la entidad Unit
                }
            }

            // Añadir la nueva unidad al repositorio
            _unitRepository.Add(unit);
            _unitOfWork.SaveChanges(); // Guardar los cambios

            return Task.FromResult(unit); // Devolver la instancia creada
        }
    }
}
