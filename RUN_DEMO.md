# Запуск и проверка приложения

Этот мини-гайд описывает текущее устройство запуска в репозитории. Для демо не нужно вручную применять EF-миграции: в проекте уже есть утилита `EnergyManagement.Tools`, которая создает локальную базу, приводит таблицы к нужной схеме и добавляет демонстрационные данные.

## Что внутри

- `Domain.EnergyManagement` - доменная модель.
- `EnergyManagement.Server` - ASP.NET Core backend, API и работа с данными.
- `energymanagement.client` - React + TypeScript frontend.
- `Tests.EnergyManagement` - доменные и интеграционные тесты.
- `tests/e2e` - Playwright E2E-тесты.
- `EnergyManagement.Testing` - тестовая инфраструктура и управление LocalDB.
- `EnergyManagement.Tools` - служебные команды, включая reset/seed демо-базы.
- `Shared` - общие API-артефакты.

Служебные папки вроде `node_modules`, `bin`, `obj`, `dist`, `playwright-report` и `test-results` не нужны для сдачи: они пересоздаются командами установки, сборки и тестов.

## Требования

- Windows.
- .NET SDK 8.
- Node.js 22.12 или новее.
- npm 10 или новее.
- SQL Server LocalDB.

Проверка версий:

```powershell
dotnet --version
node --version
npm --version
```

## Быстрая проверка проекта

Эти команды проверяют, что исходники восстанавливаются, собираются и проходят основные тесты:

```powershell
dotnet restore
dotnet build EnergyManagement.sln
dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj -p:ParallelizeAssemblies=false
```

Проверка клиентской части:

```powershell
cd energymanagement.client
npm.cmd ci
npm.cmd run build
```

## Демо-запуск одной командой

Из корня репозитория:

```powershell
.\scripts\start-demo.ps1
```

Скрипт делает следующее:

1. Проверяет доступность `dotnet`, `node` и `npm`.
2. Создает или очищает LocalDB-базу `TestEnergyManagement`.
3. Запускает `EnergyManagement.Tools -- seed-e2e-demo-data`.
4. Запускает backend на `https://localhost:7250`.
5. Запускает frontend на `https://127.0.0.1:5173`.

После запуска открыть:

```text
https://127.0.0.1:5173
```

## Демо-аккаунты

Эти пользователи создаются seed-командой:

```text
Клиент:    e2e.client@example.com / ValidPassword111!
Сотрудник: e2e.employee@example.com / ValidPassword111!
```

## Запуск без очистки базы

Если нужно сохранить текущие данные:

```powershell
.\scripts\start-demo.ps1 -SkipDatabaseReset
```

Seed-команда всё равно выполнится повторно, чтобы демо-логины были доступны.

## Ручной демо-запуск

Подготовить LocalDB-базу и демо-данные:

```powershell
dotnet run --project EnergyManagement.Tools -- reset-test-db
dotnet run --project EnergyManagement.Tools -- seed-e2e-demo-data
```

Запустить backend:

```powershell
$env:ASPNETCORE_ENVIRONMENT = "Development"
dotnet run --project EnergyManagement.Server/EnergyManagement.Server.csproj --no-launch-profile -- --urls https://localhost:7250
```

Запустить frontend в отдельном окне:

```powershell
cd energymanagement.client
npm.cmd ci
npm.cmd run dev -- --host 127.0.0.1
```

## E2E-тесты

Из корня репозитория:

```powershell
npm.cmd ci
npx.cmd playwright install
npm.cmd run test:e2e
```

Корневой скрипт `test:e2e` сам выполняет `reset-test-db`, а Playwright поднимает backend и frontend через `playwright.config.ts`.

## Как остановить демо

Закрыть два окна PowerShell, в которых запущены backend и frontend.

## Частая ошибка при проверке API

Не проверять GET-endpoint через `curl -I`: ключ `-I` отправляет `HEAD`, а часть endpoint-ов реализована только как `GET`. В такой ситуации можно получить `404` или `405`, хотя обычный GET работает.
