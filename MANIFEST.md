# SL-AUTH-REG-EMAIL-001 — Send Registration Email Notification

Merge-ready archive without wrapper folder.

## Implemented

- Registration success triggers registration email notification.
- Application-level notification abstraction:
  - `IRegistrationEmailNotificationService`
  - `IEmailSender`
  - `EmailMessage`
- Infrastructure SMTP adapter using native `System.Net.Mail.SmtpClient`:
  - `SmtpEmailOptions`
  - `SmtpEmailSender`
- Registration handler calls notification only after account is persisted.
- Email send failure is logged and does not roll back/suppress successful registration response.
- Tests override `IEmailSender` with fake sender.
- Integration tests cover success, validation failure, duplicate failure, and sender failure behavior.

## Not changed

- No API contract shape change.
- No OpenAPI/generated TypeScript artifacts.
- No domain changes.
- No migrations.
- No docs/planning/client UI changes.
- No outbox/background worker.
