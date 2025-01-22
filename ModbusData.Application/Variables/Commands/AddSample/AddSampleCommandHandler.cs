using MediatR;
using ModbusData.Domain.Entities.Variables; // Ajustar el espacio de nombres si es necesario
using ModbusData.Contract; // Asumiendo que este es donde se define la interfaz del repositorio
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Domain.Entities;
using ModbusData.Contract.Variables;
using ModbusData.Domain.Records;

namespace ModbusData.Application.Variables.Commands.AddSample
{
    public class AddSampleCommandHandler : IRequestHandler<AddSampleCommand, bool>
    {
        private readonly IVariableRepository<Variable> _variableRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddSampleCommandHandler(
            IVariableRepository<Variable> variableRepository,
            IUnitOfWork unitOfWork)
        {
            _variableRepository = variableRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(AddSampleCommand request, CancellationToken cancellationToken)
        {
            var variable = _variableRepository.GetById(request.VariableId);
            if (variable == null) return Task.FromResult(false);

            var sample = new Sample
            {
                VariableId = request.VariableId,
                Date = request.Date,
                
            };

            variable.Samples.Add(sample);
            _variableRepository.Update(variable);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
