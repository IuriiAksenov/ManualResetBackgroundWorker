


# Фоновый поток с возможностью рестарта по востребованию / Background worker with the ability to restart on demand

(ENG is below)

Rus

Как правило фоновые потоки работают по определенному сценарию, например, запускаются раз в 5 часов. Однако, иногда необходимо принудительно сделать так, чтобы задача, которая была делегирована фоновому потоку выполнилась не по расписанию.
Данный код позволяет принудительно сбрасывать ожидание выполнения, чтобы поток выполнил свою задачу.  

### Структура проекта

### Core – логика класса по рестарту фонового потока

### Examples – пример использования
* Sample Server, который содержит фоновый процесс, печатающий цифры в консоль. Цифры печатаются через каждую минуту.
* Sample Client – позволяет вызвать метод контроллера Sample Server для отмены ожидания печатания цифр (не ждать минуту) 


### Поддержка
- По возникающим вопросам просьба обращаться на [iurii.aksenov@yandex.ru][support-email]
- Баги и feature-реквесты можно направлять в раздел [issues][issues]


---

Eng

Usually, background threads work according to a certain scenario, for example, they start every 5 hours. However, sometimes it is necessary to force a task that has been delegated to a background thread to run out of schedule.
This code allows you to forcibly reset the pending execution so that the thread will complete its task.

### Project structure

### Core - class logic for restarting the background thread

### Examples - usage example
* Sample Server which contains a background process that prints numbers to the console. The numbers are printed every minute.
* Sample Client - allows you to call the Sample Server controller method to cancel waiting for printing digits (do not wait a minute)

### Support
- If you have any questions, please contact [iurii.aksenov@yandex.ru][support-email]
- Bugs and feature requests can be sent to the [issues][issues] section

[support-email]: mailto:iurii.aksenov@yandex.ru
[issues]: https://github.com/IuriiAksenov/ManualResetBackgroundWorker/issues
