# Apply

From repository root:

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-auth-reg-email-001-registration-email-notification-v38.zip" -DestinationPath . -Force
```

## Check

```powershell
dotnet build .\EnergyManagement.Server\EnergyManagement.Server.csproj
dotnet test .\Tests.EnergyManagement\Tests.EnergyManagement.csproj
```

No OpenAPI regeneration is expected because the registration API shape is unchanged.

## SMTP configuration

Configure real SMTP transport through configuration section:

```json
{
  "Email": {
    "Smtp": {
      "Host": "smtp.example.com",
      "Port": 587,
      "EnableSsl": true,
      "UserName": "...",
      "Password": "...",
      "From": "noreply@example.com"
    }
  }
}
```

If SMTP is not configured, registration still succeeds and email notification failure is logged.
