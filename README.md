# testQpd

## Описание
Сервис для стандартизации адресов с использованием API DaData.

## Технологии
- ASP.NET Core WebApi
- Serilog
- AutoMapper
- IHttpClientFactory
- Swagger

## Настройка
1. Укажите ApiKey и Secret DaData в `appsettings.json`

## Эндпоинты
- `GET /api/address?address=<адрес>`

## Описание результата
Пользователь отправляет адрес в свободной текстовой форме, а сервис возвращает стандартизированную информацию 
(страну, город, улицу, номер дома, номер квартиры).