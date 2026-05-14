# Применение пакета чистых материалов ВКР

Пакет содержит complete replacement files для папок:

```text
vkr-clean/
presentation/
```

Тема ВКР:

> «Разработка web-приложения для автоматизации ведения документооборота и обработки клиентских заявок в сетевой компании ООО „ЗСК“»

PowerShell / VS Code Terminal:

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-clean-zsk-package.zip" -DestinationPath . -Force
git status
git diff
```

Если всё ок:

```powershell
git add vkr-clean presentation
git commit -m "Update VKR materials for ZSK document workflow topic"
```

После применения проверить:

1. Соответствует ли название организации требованиям кафедры.
2. Нужно ли указывать ООО «ЗСК» как условную организацию или как пример сетевой компании.
3. Какой статус функций сотрудника и email-уведомлений подтвержден кодом на момент финальной сдачи.
4. Должен ли финальный текст описывать только реализованную версию или также проектную целевую версию.
