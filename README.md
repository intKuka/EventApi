# EventApi

АПИ для создания и редактирования мероприятий

## Начало работы
1. скопировать репоpиторий и загрузить submodules;
  ```
  git clone git@github.com:intKuka/EventApi.git
  git submodule init
  git submodule update
  ```
2. зайти в корневую папку проекта

3. построить docker-compose
```
docker-compose up --build
```

Далее можно использовать либо браузер или Postman для работы с compose, либо swagger через запуск Visual Studio.
> Важное отличие методов - они работают с разными БД (compose - host:mongodb, swagger - host:localhost)

## Работа в Swagger
При первом запуске будет создана локальная БД по строке `mongodb://localhost:27017`.

Есть три контроллера:
- работа с мероприятиями;
- информация о билетах;
- данные заглушек.

### Events Controller
Пример полного пути запроса в Postman для работы с локалльной БД:
> GET https://localhost:5001/api/events
или
> GET http://localhost:5002/api/events

`GET api/events` : возвращает все мероприятия из БД

`GET api/events/{id:guid}` : возвращает мероприятие по `Guid`

`POST api/events` : принимает в тело запроса объект типа `Event` и добовляет мероприятие в БД

`PUT api/events` : принимает в тело запроса объект типа `Event` и заменяет собой объект с идентичным `Guid`

`DELETE api/events{id:guid}` : удаляет объект из БД по его `Guid`

### Info Controller
Пример полного пути запроса в Postman для работы заглушками:
> GET https://localhost:5001/api/info/images
или
> GET http://localhost:5002/api/info/spaces

`GET api/info/images` : возвращает все изображения

`GET api/info/images{id:guid}` : возвращает изображение по `Guid`

`GET api/info/spaces` : возвращает все пространства

`GET api/info/spaces{id:guid}` : возвращает пространство по `Guid`

`GET api/info/users` : возвращает всех пользователей

`GET api/info/users{id:guid}` : возвращает пользователя по `Guid`

### Tickets Controller
Пример полного пути запроса в Postman для работы с БД:
> GET https://localhost:5001/api/tickets/checkTicket
или
> GET http://localhost:5002/api/tickets/checkTicket

`PATCH api/tickets/giveTicket` : принимает `Guid мероприятия` и `Guid пользователя`, устанавливает на следующий свободный билет `Guid пользователя`

`GET api/tickets/checkTicket` : принимает `Guid мероприятия` и `Guid пользователя`, выдает список билетов, которыми владеет пользователь в рамках заданного мероприятия

`GET api/tickets/checkSeat` : принимает `Guid мероприятия` и `int номер места`, выдает `boolean` о том свободно ли данное место на заданном мероприятии

## Работа в compose
За исключением сваггера, работа со ссылками остается прежней, но:
- Порт изменяется на `8080` и доступ запросы отправляются по `http`

> К примеру http://localhost:8080/api/events

## Несколько сэмплов
### Создать мероприятие
`POST https://localhost:5001/api/events`

> { "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", "starts": "2023-03-20T10:25:39.507Z", "ends": "2023-03-20T15:25:39.507Z", "name": "Event", "description": "Fun event", "imageId": "4c8ebbeb-ffba-4851-8300-ffd192e99372", "spaceId": "169a4f10-0914-4d8d-b922-3958621a72a5", "ticketsQuantity": 3, "hasNumeration": true }

### Изменить мероприятие
`PUT https://localhost:5001/api/events`

> { "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", "starts": "2023-03-20T10:25:39.507Z", "ends": "2023-03-20T15:25:39.507Z", "name": "Event", "description": "not so fun event(", "imageId": "4c8ebbeb-ffba-4851-8300-ffd192e99372", "spaceId": "169a4f10-0914-4d8d-b922-3958621a72a5", "ticketsQuantity": 3, "hasNumeration": false }

### Выдать билет
`PATCH https://localhost:5001/api/tickets/giveTicket?eventId=3fa85f64-5717-4562-b3fc-2c963f66afa6&userId=4bf981b9-fdd5-4854-b438-af792493a221`

### Удалить мероприятие
`DELETE https://localhost:5001/api/events/3fa85f64-5717-4562-b3fc-2c963f66afa6`
