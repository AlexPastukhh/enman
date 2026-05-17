# Restore start-exchange validator registration after document upload archive

This archive is a small repair patch for the case where the first upload archive was applied and removed the DI registration for `StartAgreementExchangeDtoValidator`.

It preserves the document upload implementation and restores:

```csharp
builder.Services.AddTransient<IValidator<StartAgreementExchangeDto>, StartAgreementExchangeDtoValidator>();
```

in `EnergyManagement.Server/Program.cs`.

It does not change API routes, domain behavior, migrations, OpenAPI or client generated files.
