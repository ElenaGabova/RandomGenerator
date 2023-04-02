# RandomGenerator
Тестовое задание, генерация массива чисел, посчет, и методы для работы с посчитанными числами.
<br>
Веб-апи проекта доступно в сваггер по ссылке: https://localhost:7005/swagger/index.html
<br><br>
Методы:
<br>
POST api/Numbers/AddGenerateNumbers - генерация последовательности чисел, сохранение в БД
<br>
POST api/Numbers/AddCountNumbers - подсчет повторов  сгенерированных чисел, сохранение в БД
<br>
GET api/Numbers/GetNumberRepetitionByNumber - отправка числа, получение из БД количества повторов числа
<br>
GET api/Numbers/GetNumberByNumberRepetitions - отправка числа, получение из БД количества чисел, с таким же повтором
<br>
GET api/Numbers/GetNumbersWithTopRepetitions - отправка числа повторов, получение из БД топ чисел с максимальным колиеством повторов
<br><br>
Структура проекта:
<br>
Constants      - общие константы. Настройка количества чисел в последовательности, максимального и минимального числа
<br>
Database       - классы, сервисы и интерфейсы для работы с базой данных.
<br>
Entities       - сущности в базе данных
<br>
Domain         - сущности в сервисах
<br>
Mapper         - описание маппинга для AutoMapper
<br>
Interface      - интерфейсы по работе с сущностями в сервисах
<br>
Repository     - репозитории по работе с сущностями в сервисах
<br>
RandomNumbersProject - веб-апи для тестирования функционала
