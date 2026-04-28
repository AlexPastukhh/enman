// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text.Json;
// using System.Threading.Tasks;
// using EnergyManagement.Server.Data;
// using static Domain.EnergyManagement.Common.Error;
// using static Domain.EnergyManagement.Common.Error.Errors;

// namespace EnergyManagement.Server.Infrastructure
// {
//     public class ConstantWriterService : IHostedService
//     {
//         private const string ErrorCodesPath 
//             = "C:\\Users\\Пользователь\\Downloads\\EnergyManagement\\Shared\\errorcodes.json";
//         private const string ConstantsPath 
//             = "C:\\Users\\Пользователь\\Downloads\\EnergyManagement\\Shared\\constants.json";
//         private readonly ErrorObject _errorCodes;
//         private readonly ConstantsToWrite _constants;

//         public ConstantWriterService(ErrorObject errorCodes, ConstantsToWrite constants)
//         {
//             _errorCodes = errorCodes;
//             _constants = constants;
//         }
//         private async Task WriteConstantsAsync(CancellationToken cancellationToken)
//         {
//             var json = JsonSerializer.Serialize(_constants, new JsonSerializerOptions { WriteIndented = true });
            
//             await File.WriteAllTextAsync(ConstantsPath, json, cancellationToken);
//         }
        
//         private async Task WriteErrorCodesAsync(CancellationToken cancellationToken)
//         {
//             // Serialize the object to JSON
//             var json = JsonSerializer.Serialize(_errorCodes, new JsonSerializerOptions { WriteIndented = true });

//             // Write the JSON to the file
//             await File.WriteAllTextAsync(ErrorCodesPath, json, cancellationToken);
//         }

//         public async Task StartAsync(CancellationToken cancellationToken)
//         {
//             // Serialize the object to JSON
//             await WriteConstantsAsync(cancellationToken);
//             await WriteErrorCodesAsync(cancellationToken);
//         }

//         public Task StopAsync(CancellationToken cancellationToken)
//         {
//             // No cleanup needed in this case
//             return Task.CompletedTask;
//         }
//     }
    
    
// }