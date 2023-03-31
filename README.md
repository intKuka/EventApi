# EventApi

АПИ для создания и редактирования мероприятий

## Важно
Адреса при работе через docker compose работают только по HTTP

## Начало работы
1. скопировать репоpиторий
  ```
  git clone git@github.com:intKuka/EventApi.git
  ```
2. зайти в корневую папку проекта 

  ```
  cd EventApi
  ```

3. загрузить submoudules
  ```
  git submodule init  
  ```

  ```
  git submodule update
  ```

4. построить и запустить контейнер RabbitMQ
```
docker run -d --hostname rmq --name rabbit-server -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```

5. построить docker-compose
```
docker-compose up --build
```

### Возможные проблемы compose
Если по какой-то причине контейнер RabbitMQ был отключен, то для восстановления работы с ним следует снова запустить этот контейнер, и затем перезапусить весь compose. Так подключение к RMQ будет выполненно корректно

В ином случае сервисы остаются работоспособными, за исключением передачи событий в очередь, что может привести к расхождении информации

> Например, если удалить изображение, то репозиторий не обновит объекты с этим изображением на null

### Через Visual Studio
Во-первых нужно запустить контейнер RabbitMQ

Далее следует сделать мульти запуск сервисов

Список сервисов для полной работы в VS:
- EventsApi
- ImagesService
- SpacesService
- PaymentService
- UsersService

## Использование
### Events Controller
Пример полного пути запроса в Postman для работы с локалльной БД:
> GET https://localhost:5001/api/events
или
> GET http://localhost:5211/api/events

`GET api/events` : возвращает все мероприятия из БД

`GET api/events/{id:guid}` : возвращает мероприятие по `Guid`

`POST api/events` : принимает в тело запроса объект типа `Event` и добовляет мероприятие в БД

`PUT api/events` : принимает в тело запроса объект типа `Event` и заменяет собой объект с идентичным `Guid`

`DELETE api/events{id:guid}` : удаляет объект из БД по его `Guid`

### Tickets Controller
Пример корневого пути запроса в Postman для работы с БД:
> GET https://localhost:5001/api/tickets...
или
> GET http://localhost:5211/api/tickets...

`PATCH api/tickets?eventId={eventId:guid}&userId={userId:guid}` : принимает `Guid мероприятия` и `Guid пользователя`, устанавливает на следующий свободный билет `Guid пользователя`

> Если передаваемое событие имеет цену на билеты более нуля, тогда будет запущен метод симуляции денежных транзакций

`GET api/tickets/list?eventId={eventId:guid}&userId={userId:guid}` : принимает `Guid мероприятия` и `Guid пользователя`, выдает список билетов, которыми владеет пользователь в рамках заданного мероприятия

`GET api/tickets?eventId={eventId:guid}&seat={seat:int}` : принимает `Guid мероприятия` и `int номер места`, выдает `boolean` о том свободно ли данное место на заданном мероприятии


# Http заглушки с данными
Сервис изображений `http://localhost:5051/images` или `https://localhost:7014/images`

Сервис пространств `http://localhost:5093/spaces` или `https://localhost:7230/spaces`

Сервис пользователей `http://localhost:5018/users` или `https://localhost:7047/users`

Сервис транзакций `http://localhost:5234/payment` или `https://localhost:7073/payment`

## Images Service
Список изображений -- `GET http://localhost:5051/images`

Проверить наличие изображения по Guid -- `GET http://localhost:5051/images/{id:guid}` -- возвращает `Boolean`

Удалить изображение по Guid -- `DELETE http://localhost:5051/images/{id:guid}`

## Spaces Service
Список пространств -- `GET http://localhost:5093/spaces`

Проверить наличие пространства по Guid -- `GET http://localhost:5093/spaces/{id:guid}` -- возвращает `Boolean`

Удалить изображение по Guid -- `DELETE http://localhost:5093/spaces/{id:guid}`

## Users Service
Список пользователей -- `GET http://localhost:5018/users`

Проверить наличие пользователя по Guid -- `GET http://localhost:5018/users/{id:guid}` -- возвращает `Boolean`

## Payment Service
Получить состояние платежа -- `GET http://localhost:5234/payment` -- если платеж ни разу не создавался, тогда в ответе придет болванка

Остальные методы выполняются в процессе операции по покупке билета

# Дополнительно
## Минимальные данные для создания мероприятия
```
{
  "ends": "2024-03-30T05:44:35.832Z",
  "name": "string",
  "spaceId": "169a4f10-0914-4d8d-b922-3958621a72a5"
}
```

### Значения при null
**id** устанавливается автоматически

**starts** -- DateTime.UtcNow

**description** -- пустая строка

**ticketsQuantity** -- значение 0

**hasNumeration** -- значение false

**price** -- значение 0



