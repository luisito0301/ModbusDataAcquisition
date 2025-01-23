using ModbusData.GrpcProtos; // Asegúrate de tener el namespace correcto para tus gRPC Protos
using Grpc.Net.Client;
using System;

namespace ModbusData.ModbusNetworkConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Presione una tecla para conectar");
            Console.ReadKey();

            Console.WriteLine("Creating channel and client");

            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var channel = GrpcChannel.ForAddress(
                "http://localhost:5051",
                new GrpcChannelOptions { HttpHandler = httpHandler });

            if (channel == null)
            {
                Console.WriteLine("Cannot connect");
                return;
            }

            var modbusNetworkClient = new ModbusNetworkService.ModbusNetworkServiceClient(channel);

            // Crear una red Modbus
            Console.WriteLine("Presione una tecla para crear una red Modbus");
            Console.ReadKey();

            var createModbusNetworkRequest = new CreateModbusNetworkRequest
            {
                MasterIpAddress = "192.168.1.100", // Ejemplo de IP
                Slaves = {  } // Ejemplo de dispositivos esclavos, puedes reemplazar estos ID con los correctos
            };

            var createModbusNetworkResponse = modbusNetworkClient.CreateModbusNetwork(createModbusNetworkRequest);

            if (createModbusNetworkResponse == null)
            {
                Console.WriteLine("Cannot create Modbus network");
                return;
            }
            else
            {
                Console.WriteLine($"Creación exitosa de la red Modbus. ID: {createModbusNetworkResponse.Id}");
            }

            // Obtener la red Modbus
            Console.WriteLine("Presione una tecla para obtener la red Modbus creada");
            Console.ReadKey();

            var getModbusNetworkResponse = modbusNetworkClient.GetModbusNetwork(new GetRequest
            {
                Id = createModbusNetworkResponse.Id
            });

            if (getModbusNetworkResponse == null)
            {
                Console.WriteLine("Cannot get Modbus network");
                return;
            }
            else
            {
                Console.WriteLine($"Red Modbus obtenida: Master IP: {getModbusNetworkResponse.ModbusNetwork.MasterIpAddress}, Slaves: {string.Join(", ", getModbusNetworkResponse.ModbusNetwork.Slaves)}");
            }

            // Actualizar la red Modbus
            Console.WriteLine("Presione una tecla para actualizar la red Modbus");
            Console.ReadKey();

            var updateModbusNetworkResponse = modbusNetworkClient.UpdateModbusNetwork(new ModbusNetworkDTO
            {
                Id = createModbusNetworkResponse.Id,
                MasterIpAddress = "192.168.1.101", // Actualización del IP
                Slaves = {  } // Ejemplo de actualización de dispositivos esclavos
            });

            if (updateModbusNetworkResponse == null)
            {
                Console.WriteLine("Cannot update Modbus network");
                return;
            }
            else
            {
                Console.WriteLine("Red Modbus actualizada exitosamente");
            }

            // Eliminar la red Modbus
            Console.WriteLine("Presione una tecla para eliminar la red Modbus");
            Console.ReadKey();

            var deleteModbusNetworkResponse = modbusNetworkClient.DeleteModbusNetwork(new DeleteRequest
            {
                Id = createModbusNetworkResponse.Id
            });

            if (deleteModbusNetworkResponse == null)
            {
                Console.WriteLine("Cannot delete Modbus network");
                return;
            }
            else
            {
                Console.WriteLine("Red Modbus eliminada exitosamente");
            }
            var unitClient = new Unit.UnitClient(channel);
            var analogicVariableClient = new AnalogicVariable.AnalogicVariableClient(channel);
            var digitalVariableClient = new DigitalVariable.DigitalVariableClient(channel);

            // Crear una unidad
            Console.WriteLine("Presione una tecla para crear una unidad");
            Console.ReadKey();

            var createUnitResponse = unitClient.CreateUnit(new CreateUnitRequest
            {
                ManufactererName = "Test Manufacturer",
                Code = "TU001",
                AreaName = "Test Area"
            });

            if (createUnitResponse == null)
            {
                Console.WriteLine("Cannot create unit");
                return;
            }
            else
            {
                Console.WriteLine($"Creación de unidad exitosa. ID: {createUnitResponse.Id}");
            }

            // Crear una variable analógica y asignar el ID de la unidad
            Console.WriteLine("Presione una tecla para crear una variable analógica");
            Console.ReadKey();

            var createAnalogicVariableResponse = analogicVariableClient.CreateAnalogicVariable(new CreateAnalogicVariableRequest
            {
                Name = "Test Analogic Variable",
                Type = VariableType.Analogic,
                IsMeasurement = true,
                Code = "AV001",
                SamplingPeriod = "00:00:01",
                ModbusAddress = 123,
                Unitid = createUnitResponse.Id,

            });

            if (createAnalogicVariableResponse == null)
            {
                Console.WriteLine("Cannot create analogic variable");
                return;
            }
            else
            {
                Console.WriteLine($"Creación exitosa de la variable analógica. ID: {createAnalogicVariableResponse.Id}");
            }

            // Crear una variable digital y asignar el ID de la unidad
            Console.WriteLine("Presione una tecla para crear una variable digital");
            Console.ReadKey();

            var createDigitalVariableResponse = digitalVariableClient.CreateDigitalVariable(new CreateDigitalVariableRequest
            {
                Name = "Test Digital Variable",
                Type = VariableType.Digital,
                IsMeasurement = true,
                Code = "DV001",
                SamplingPeriod = "00:00:01",
                ModbusAddress = 124,
                Unitid = createUnitResponse.Id
            });

            if (createDigitalVariableResponse == null)
            {
                Console.WriteLine("Cannot create digital variable");
                return;
            }
            else
            {
                Console.WriteLine($"Creación exitosa de la variable digital. ID: {createDigitalVariableResponse.Id}");
            }

            // Obtener la unidad
            Console.WriteLine("Presione una tecla para obtener la unidad creada");
            Console.ReadKey();

            var getUnitResponse = unitClient.GetUnit(new GetRequest
            {
                Id = createUnitResponse.Id
            });

            if (getUnitResponse == null)
            {
                Console.WriteLine("Cannot get unit");
                return;
            }
            else
            {
                Console.WriteLine($"Unidad obtenida: {getUnitResponse.Unit.ManufactererName}, {getUnitResponse.Unit.Code}, {getUnitResponse.Unit.AreaName}");
            }

            // Obtener la variable analógica
            Console.WriteLine("Presione una tecla para obtener la variable analógica creada");
            Console.ReadKey();

            var getAnalogicVariableResponse = analogicVariableClient.GetAnalogicVariable(new GetRequest
            {
                Id = createAnalogicVariableResponse.Id
            });

            if (getAnalogicVariableResponse == null)
            {
                Console.WriteLine("Cannot get analogic variable");
                return;
            }
            else
            {
                Console.WriteLine($"Variable analógica obtenida: {getAnalogicVariableResponse.AnalogicVariable.Name}, {getAnalogicVariableResponse.AnalogicVariable.Code}");
            }

            // Obtener la variable digital
            Console.WriteLine("Presione una tecla para obtener la variable digital creada");
            Console.ReadKey();

            var getDigitalVariableResponse = digitalVariableClient.GetDigitalVariable(new GetRequest
            {
                Id = createDigitalVariableResponse.Id
            });

            if (getDigitalVariableResponse == null)
            {
                Console.WriteLine("Cannot get digital variable");
                return;
            }
            else
            {
                Console.WriteLine($"Variable digital obtenida: {getDigitalVariableResponse.DigitalVariable.Name}, {getDigitalVariableResponse.DigitalVariable.Code}");
            }

            // Actualizar la unidad
            Console.WriteLine("Presione una tecla para actualizar la unidad");
            Console.ReadKey();

            var updateUnitResponse = unitClient.UpdateUnit(new UnitDTO
            {
                Id = createUnitResponse.Id,
                ManufactererName = "Updated Manufacturer",
                Code = "TU001_UPDATED",
                AreaName = "Updated Area"
            });

            if (updateUnitResponse == null)
            {
                Console.WriteLine("Cannot update unit");
                return;
            }
            else
            {
                Console.WriteLine("Unidad actualizada exitosamente");
            }

            // Eliminar la unidad
            Console.WriteLine("Presione una tecla para eliminar la unidad");
            Console.ReadKey();

            var deleteUnitResponse = unitClient.DeleteUnit(new DeleteRequest
            {
                Id = createUnitResponse.Id
            });

            if (deleteUnitResponse == null)
            {
                Console.WriteLine("Cannot delete unit");
                return;
            }
            else
            {
                Console.WriteLine("Unidad eliminada exitosamente");
            }

            // Eliminar la variable analógica
            Console.WriteLine("Presione una tecla para eliminar la variable analógica");
            Console.ReadKey();

            var deleteAnalogicVariableResponse = analogicVariableClient.DeleteAnalogicVariable(new DeleteRequest
            {
                Id = createAnalogicVariableResponse.Id
            });

            if (deleteAnalogicVariableResponse == null)
            {
                Console.WriteLine("Cannot delete analogic variable");
                return;
            }
            else
            {
                Console.WriteLine("Variable analógica eliminada exitosamente");
            }

            // Eliminar la variable digital
            Console.WriteLine("Presione una tecla para eliminar la variable digital");
            Console.ReadKey();

            var deleteDigitalVariableResponse = digitalVariableClient.DeleteDigitalVariable(new DeleteRequest
            {
                Id = createDigitalVariableResponse.Id
            });

            if (deleteDigitalVariableResponse == null)
            {
                Console.WriteLine("Cannot delete digital variable");
                return;
            }
            else
            {
                Console.WriteLine("Variable digital eliminada exitosamente");
            }
            var slaveDeviceClient = new SlaveDeviceService.SlaveDeviceServiceClient(channel);

            // Crear un dispositivo esclavo
            Console.WriteLine("Presione una tecla para crear un dispositivo esclavo");
            Console.ReadKey();

            var createSlaveDeviceRequest = new CreateSlaveDeviceRequest
            {

                IpAddress = "192.168.1.2", // Usamos string para la IP
                Variables = { } // Ejemplo de Variables, puedes reemplazar estos ID con los correctos
            };

            var createSlaveDeviceResponse = slaveDeviceClient.CreateSlaveDevice(createSlaveDeviceRequest);

            if (createSlaveDeviceResponse == null)
            {
                Console.WriteLine("Cannot create slave device");
                return;
            }
            else
            {
                Console.WriteLine($"Creación exitosa del dispositivo esclavo. ID: {createSlaveDeviceResponse.Id}");
            }

            // Obtener el dispositivo esclavo
            Console.WriteLine("Presione una tecla para obtener el dispositivo esclavo creado");
            Console.ReadKey();

            var getSlaveDeviceResponse = slaveDeviceClient.GetSlaveDevice(new GetRequest
            {
                Id = createSlaveDeviceResponse.Id
            });

            if (getSlaveDeviceResponse == null)
            {
                Console.WriteLine("Cannot get slave device");
                return;
            }
            else
            {
                Console.WriteLine($"Dispositivo esclavo obtenido: IP: {getSlaveDeviceResponse.SlaveDevice.IpAddress}");
            }

            // Actualizar el dispositivo esclavo
            Console.WriteLine("Presione una tecla para actualizar el dispositivo esclavo");
            Console.ReadKey();

            var updateSlaveDeviceResponse = slaveDeviceClient.UpdateSlaveDevice(new SlaveDeviceDTO
            {
                Id = createSlaveDeviceResponse.Id,

                IpAddress = "192.168.1.3", // Actualización del IP
                Variables = { } // Ejemplo de Variables, puedes reemplazar estos ID con los correctos
            });

            if (updateSlaveDeviceResponse == null)
            {
                Console.WriteLine("Cannot update slave device");
                return;
            }
            else
            {
                Console.WriteLine("Dispositivo esclavo actualizado exitosamente");
            }

            // Eliminar el dispositivo esclavo
            Console.WriteLine("Presione una tecla para eliminar el dispositivo esclavo");
            Console.ReadKey();

            var deleteSlaveDeviceResponse = slaveDeviceClient.DeleteSlaveDevice(new DeleteRequest
            {
                Id = createSlaveDeviceResponse.Id
            });

            if (deleteSlaveDeviceResponse == null)
            {
                Console.WriteLine("Cannot delete slave device");
                return;
            }
            else
            {
                Console.WriteLine("Dispositivo esclavo eliminado exitosamente");
            }


            channel.Dispose();
            Console.WriteLine("Pruebas CRUD  finalizadas.");
        }
    }
}
