using System;
using ModbusData.Application.Abstract;
using ModbusData.Domain.Records;
using ModbusData.Domain.Types;
using System;
using System.Threading;
using System.Threading.Tasks;
using ModbusData.Application.Commands.CreateSample;
using ModbusData.Contract;
using ModbusData.DataAccess.Repositories;

namespace ModbusData.Application.Commands.CreateSample
{



    public class CreateSampleCommandHandler : ICommandHandler<CreateSampleCommand?, Domain.Records.Sample>
    {
        private readonly ISampleRepository _samplerepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSampleCommandHandler(
            ISampleRepository samplerepository,
            IUnitOfWork unitOfWork)
        {
            _samplerepository = samplerepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Domain.Records.Sample> Handle(CreateSampleCommand request, CancellationToken cancellationToken)
        {
            // Create a new instance of AnalogicVariable
            Domain.Records.Sample result = new Domain.Records.Sample
            {
                VariableId =Guid.Parse (request.VariableId),
               Date =DateTime.Parse( request.Date),
                Value = request.Value
            };

            // Add the new AnalogicVariable to the repository
            _samplerepository.AddSample(result);
            _unitOfWork.SaveChanges(); // Save changes asynchronously

            return Task.FromResult(result); // Return the created instance

        }
    }
}
