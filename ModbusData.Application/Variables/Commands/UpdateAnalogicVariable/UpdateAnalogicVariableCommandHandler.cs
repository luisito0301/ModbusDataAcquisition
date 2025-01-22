using ModbusData.Application.Abstract;
using ModbusData.Contract; // Asumiendo que este es donde se define la interfaz del repositorio
using ModbusData.Domain.Entities.Variables; // Ajustar el espacio de nombres si es necesario
using ModbusData.Domain.Types;
using System;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Contract.Variables;

namespace ModbusData.Application.Variables.Commands.UpdateAnalogicVariable
{
    public class UpdateVariableCommandHandler : ICommandHandler<UpdateVariableCommand, bool>
    {
        private readonly IVariableRepository<AnalogicVariable> _analogicVariableRepository;
        private readonly IVariableRepository<DigitalVariable> _digitalVariableRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateVariableCommandHandler(
            IVariableRepository<AnalogicVariable> analogicVariableRepository,
            IVariableRepository<DigitalVariable> digitalVariableRepository,
            IUnitOfWork unitOfWork)
        {
            _analogicVariableRepository = analogicVariableRepository;
            _digitalVariableRepository = digitalVariableRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(UpdateVariableCommand request, CancellationToken cancellationToken)
        {
            bool result;

            if (request.Type == VariableType.Analogic)
            {
                var existingVariable = _analogicVariableRepository.GetById(request.VariableId);
                if (existingVariable == null) return Task.FromResult(false);

                // Crear una nueva instancia de AnalogicVariable con valores actualizados
                var updatedVariable = new AnalogicVariable(
                    existingVariable.Id, // Mantener el mismo ID
                    request.Name,
                    request.Type,
                    request.IsMeasurement,
                    request.Code,
                    request.SamplingPeriod,
                    request.ModbusAddress)
                {
                    Value = (request as UpdateAnalogicVariableCommand).Value,
                    UnitId = request.UnitId
                };

                _analogicVariableRepository.Update(updatedVariable);
                result = true;
            }
            else if (request.Type == VariableType.Digital)
            {
                var existingVariable = _digitalVariableRepository.GetById(request.VariableId);
                if (existingVariable == null) return Task.FromResult(false);

                // Crear una nueva instancia de DigitalVariable con valores actualizados
                var updatedVariable = new DigitalVariable(
                    existingVariable.Id, // Mantener el mismo ID
                    request.Name,
                    request.Type,
                    request.IsMeasurement,
                    request.Code,
                    request.SamplingPeriod,
                    request.ModbusAddress)
                {
                    Value = (short)(request as UpdateDigitalVariableCommand).Value,
                    UnitId = request.UnitId
                };

                _digitalVariableRepository.Update(updatedVariable);
                result = true;
            }
            else
            {
                throw new ArgumentException("Unsupported variable type.");
            }

            _unitOfWork.SaveChanges();
            return Task.FromResult(result);
        }
    }
}
