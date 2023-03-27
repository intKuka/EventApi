# EventApi

АПИ для создания и редактирования мероприятий

!!! Docker-compose не может полностью функционировать из-за проблем с подключением к RabbitMQ !!!

!!! в compose не работает: events, images_service, spaces_service !!!

!!! как вариант можно построить compose и запустить нерабочие сервисы с VS, либо вовсе все в VS !!!

!!! В настоящий момент разбираюсь что не так !!!

Список сервисов для работы в VS:
- EventsApi
- ImagesService
- SpacesService
- PaymentService
- UsersService

## Начало работы
1. скопировать репоpиторий и загрузить submodules;
  ```
  git clone git@github.com:intKuka/EventApi.git
  git submodule init
  git submodule update
  ```
2. зайти в корневую папку проекта, если необходимо (например, compose)

3. построить контейнер RabbitMQ
```
docker run -d --hostname rmq --name rabbit-server -p 8080:15672 -p 5672:5672 rabbitmq:3-management
```

Далее нужно либо построить кусок compose `docker-compose up --build` с VS, либо только VS

Swagger также имеет возможность получения списков данных через Info, но результат представлен в качестве неформатированной строки

## Использование
При первом запуске будет создана локальная БД по строке `mongodb://localhost:27017`.

> Некоторые операции требуют свой набор работающих сервисов, т.ч. лучше их сразу все включить

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

### Info Controller
Пример полного пути запроса в Postman для работы заглушками:
> GET https://localhost:5001/api/info/images
или
> GET http://localhost:5211/api/info/spaces

`GET api/info/images` : возвращает все изображения

`GET api/info/spaces` : возвращает все пространства

`GET api/info/users` : возвращает всех пользователей

### Tickets Controller
Пример полного пути запроса в Postman для работы с БД:
> GET https://localhost:5001/api/tickets/check_ticket
или
> GET http://localhost:5211/api/tickets/check_ticket

`PATCH api/tickets/get_ticket` : принимает `eventId` и `userId`, устанавливает на следующий свободный билет `Guid пользователя`

> Если передаваемое событие имеет цену на билеты более нуля, тогда будет запущен метод симуляции денежных транзакций

`GET api/tickets/check_ticket` : принимает `Guid мероприятия` и `Guid пользователя`, выдает список билетов, которыми владеет пользователь в рамках заданного мероприятия

`GET api/tickets/check_seat` : принимает `Guid мероприятия` и `int номер места`, выдает `boolean` о том свободно ли данное место на заданном мероприятии


# Http заглушки с данными
!!! Перед использование следует запустить сервисы !!!

!!! https не работает на сервисах в compose !!!

Сервис изображений `http://localhost:5051/images` или `https://localhost:7014/images`

Сервис пространств `http://localhost:5093/spaces` или `https://localhost:7230/images`

Сервис пользователей `http://localhost:5018/users` или `https://localhost:7047/images`

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
