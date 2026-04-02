# ATM System

Серверная часть банкомата на ASP.NET Core Web API.  
Реализует управление счетами, сессиями (пользователь/админ), пополнение, снятие, просмотр баланса и истории операций.

## 1. Архитектура

Проект построен на принципах **чистой архитектуры** (Clean Architecture) и **Domain-Driven Design** (DDD):

- **Domain** — сущности, value-объекты, идентификаторы (Account, Session, Operation, Money, PinCode). Без внешних зависимостей.
- **Application.Abstractions** — интерфейсы репозиториев, persistence context, query objects.
- **Application** — реализация сервисов (AccountService, SessionService), маппинг в DTO, бизнес-логика.
- **Application.Contracts** — DTO и контракты операций (запросы/ответы) в виде record-типов.
- **Infrastructure** — in-memory реализация репозиториев (словари), DI-контейнер.
- **Presentation** — ASP.NET Core контроллеры, HTTP-запросы/ответы.
- **Main** — точка входа, настройка Swagger, DI, запуск.

## 2. Технологии

- .NET 9
- ASP.NET Core Web API
- Swagger / OpenAPI
- Dependency Injection (Microsoft.Extensions.DependencyInjection)
- In‑memory persistence

## 3. Функциональность

### Сессии
- Создание пользовательской сессии (по ID счета и PIN-коду)
- Создание административной сессии (по системному паролю)

### Работа со счетом (требует валидной сессии)
- Создание нового счета (только для администратора)
- Просмотр баланса
- Пополнение
- Снятие (с проверкой достаточности средств)
- История операций (дата, тип, сумма, баланс после операции)

### Ограничения
- PIN-код — строго 4 цифры
- Суммы неотрицательные
- Пользовательская сессия привязана к конкретному счету
- Администратор не привязан к счету, может создавать новые счета

## 4. Примеры запросов (Swagger)

После запуска откройте `https://localhost:5001/swagger`

### Создать административную сессию

```
POST /api/session/admin
{
"systemPassword": "admin"
}
```

Ответ: `{ "id": "guid", "type": "Admin" }`

### Создать пользовательскую сессию

```
POST /api/session/user
{
"accountId": 1,
"pin": "1234"
}
```

### Создать счёт (только для администратора)

```
POST /api/account/create
{
"sessionId": "guid-админ-сессии",
"pin": "4321"
}
```

### Пополнить счёт

```
POST /api/account/deposit
{
"sessionId": "guid-пользовательской-сессии",
"amount": 500.00
}
```

### Снять деньги

```
POST /api/account/withdraw
{
"sessionId": "guid-пользовательской-сессии",
"amount": 200.00
}
```

### Получить баланс

```
GET /api/account/balance?sessionId=guid
```

### Получить историю операций

```
GET /api/account/history?sessionId=guid
```

## 5.  Запуск

1. Установите .NET 9 SDK
2. Клонируйте репозиторий
3. В корне проекта выполните:

```
dotnet run --project Main
```

4. Откройте `https://localhost:5001/swagger`

Системный пароль администратора задаётся в `appsettings.json` (по умолчанию `"admin"`).

## 6. Структура решения

```
ATM/
├── Domain/                     # ядро, сущности и правила
├── Application.Abstractions/   # интерфейсы для инфраструктуры
├── Application/                # бизнес-логика и маппинг
├── Application.Contracts/      # DTO и контракты
├── Infrastructure/             # in-memory репозитории, DI
├── Presentation/               # контроллеры
└── Main/                       # композиция корня, запуск
```

