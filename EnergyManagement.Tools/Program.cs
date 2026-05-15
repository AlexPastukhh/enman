using EnergyManagement.Tools.ClientConstants;

var command = new GenerateClientConstantsCommand(
    new ClientConstantsPathResolver(),
    new ClientConstantsSnapshotFactory(),
    new ClientConstantsJsonSerializer(),
    new ClientConstantsWriter(),
    new ClientConstantsChecker());

return await command.ExecuteAsync(args);
