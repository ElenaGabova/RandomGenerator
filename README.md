# RandomGenerator
Тестовое задание, генерация массива чисел, посчет, и методы для работы с посчитанными числами.
Веб-апи проекта доступно в сваггер по ссылке: https://localhost:7005/swagger/index.html
Методы:
POST api/Numbers/AddGenerateNumbers - генерация последовательности чисел, сохранение в БД
POST api/Numbers/AddCountNumbers - подсчет повторов  сгенерированных чисел, сохранение в БД

GET api/Numbers/GetNumberRepetitionByNumber - отправка числа, получение из БД количества повторов числа
GET api/Numbers/GetNumberByNumberRepetitions - отправка числа, получение из БД количества чисел, с таким же повтором
GET api/Numbers/GetNumbersWithTopRepetitions - отправка числа повторов, получение из БД топ чисел с максимальным колиеством повторов

Структура проекта:

Constants      - общие константы. Настройка количества чисел в последовательности, максимального и минимального числа
Database       - классы, сервисы и интерфейсы для работы с базой данных.
Entities       - сущности в базе данных
Domain         - сущности в сервисах
Mapper         - описание маппинга для AutoMapper
Interface      - интерфейсы по работе с сущностями в сервисах
Repository     - репозитории по работе с сущностями в сервисах

RandomNumbersProject    - веб-апи для тестирования функционала
