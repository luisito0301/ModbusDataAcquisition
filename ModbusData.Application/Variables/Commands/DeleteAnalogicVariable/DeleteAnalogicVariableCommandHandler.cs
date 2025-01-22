using ModbusData.Application.Abstract;
using ModbusData.Domain.Entities.Variables; // Ajustar el espacio de nombres si es necesario
using ModbusData.Contract; // Asumiendo que este es donde se define la interfaz del repositorio
using ModbusData.Domain.Types;
using System;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Contract.Variables;

namespace ModbusData.Application.Variables.Commands.DeleteAnalogicVariable
{
    public class DeleteVariableCommandHandler : ICommandHandler<DeleteVariableCommand, bool>
    {
        private readonly IVariableRepository<AnalogicVariable> _analogicVariableRepository;
        private readonly IVariableRepository<DigitalVariable> _digitalVariableRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteVariableCommandHandler(
            IVariableRepository<AnalogicVariable> analogicVariableRepository,
            IVariableRepository<DigitalVariable> digitalVariableRepository,
            IUnitOfWork unitOfWork)
        {
            _analogicVariableRepository = analogicVariableRepository;
            _digitalVariableRepository = digitalVariableRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteVariableCommand request, CancellationToken cancellationToken)
        {
            bool result;

            if (request.Type == VariableType.Analogic)
            {
                var variable = _analogicVariableRepository.GetById(request.VariableId);
                if (variable == null) return Task.FromResult(false);

                _analogicVariableRepository.Delete(variable.Id);
                result = true;
            }
            else if (request.Type == VariableType.Digital)
            {
                var variable = _digitalVariableRepository.GetById(request.VariableId);
                if (variable == null) return Task.FromResult(false);

                _digitalVariableRepository.Delete(variable.Id);
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
