Запуск приложения
=================

Краткая инструкция для воспроизведения демо находится в файле RUN_DEMO.md.

Самый простой запуск из корня проекта:

    .\scripts\start-demo.ps1

После запуска открыть:

    https://127.0.0.1:5173

Демонстрационные пользователи:

    Клиент:    e2e.client@example.com / ValidPassword111!
    Сотрудник: e2e.employee@example.com / ValidPassword111!

Для быстрой проверки сборки:

    dotnet restore
    dotnet build EnergyManagement.sln
    dotnet test Tests.EnergyManagement/Tests.EnergyManagement.csproj -p:ParallelizeAssemblies=false

Клиентская часть:

    cd energymanagement.client
    npm.cmd ci
    npm.cmd run build

Для E2E-тестов из корня проекта:

    npm.cmd ci
    npx.cmd playwright install
    npm.cmd run test:e2e
