using ModbusData.Contract;
using ModbusData.Contract.ModbusNetworks;
using ModbusData.Domain.Entities.Modbus_Network;
using ModbusData.Domain.Entities.Device;
using ModbusData.DataAccess.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModbusData.Application.Abstract;

namespace ModbusData.Application.ModbusNetwork.Commands.CreateModbusNetwork
{
    public class CreateModbusNetworkCommandHandler : ICommandHandler<CreateModbusNetworkCommand, Domain.Entities.Modbus_Network.ModbusNetwork>
    {
        private readonly IModbusNetworkRepository<Domain.Entities.Modbus_Network.ModbusNetwork> _modbusNetworkRepository; // Asegúrate de que la ruta sea correcta
        private readonly IUnitOfWork _unitOfWork;

        public CreateModbusNetworkCommandHandler(
            IModbusNetworkRepository<Domain.Entities.Modbus_Network.ModbusNetwork> modbusNetworkRepository,
            IUnitOfWork unitOfWork)
        {
            _modbusNetworkRepository = modbusNetworkRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Domain.Entities.Modbus_Network.ModbusNetwork> Handle(CreateModbusNetworkCommand request, CancellationToken cancellationToken)
        {
            // Crear una instancia de ModbusNetwork con la dirección IP del dispositivo maestro y la lista de dispositivos esclavos
            Domain.Entities.Modbus_Network.ModbusNetwork modbusNetwork = new Domain.Entities.Modbus_Network.ModbusNetwork(
                 Guid.NewGuid(),
                request.MasterIpAddress,
                request.Slaves
            );

            // Agregar la red Modbus a la base de datos
            _modbusNetworkRepository.Add(modbusNetwork);

            
            // Guardar los cambios en la base de datos
            _unitOfWork.SaveChanges();

            return Task.FromResult(modbusNetwork);
        }
    }
}