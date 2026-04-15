using Meetup.Domain.Entities;
using Meetup.Domain.Repositories;
using Meetup.ValueObjects;

namespace Meetup.DomainApp;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine(" Meetup Platform - Domain Testing \n");

        // 1. Создаём организатора
        var username = new Username("ivan_petrov");
        var entityType = new EntityType("individual");
        var companyName = new CompanyName(null);

        var organizer = new Organizer(username, entityType, companyName);
        Console.WriteLine($" Создан организатор: {organizer.Username.Value} (ID: {organizer.Id})");

        // 2. Создаём тип мероприятия
        var eventTypeName = new EventTypeName("Конференция");
        var eventType = new EventType(eventTypeName, "IT-конференция", "#FF5733");
        Console.WriteLine($" Создан тип мероприятия: {eventType.Name.Value}");

        // 3. Создаём мероприятие
        var title = new EventTitle("C# Meetup 2025");
        var description = new EventDescription("Встреча разработчиков C#");
        var date = new EventDate(DateTime.Now.AddDays(30));
        var location = new EventLocation("Москва, ул. Ленина 1");
        var maxAttendees = new MaxAttendees(20);

        var event1 = new Event(organizer.Id, eventType.Id, title, description, date, location, maxAttendees);
        Console.WriteLine($" Создано мероприятие: {event1.Title.Value} (мест: {event1.MaxAttendees.Value})");

        // 4. Создаём участников
        var attendee1 = new Attendee(new Username("anna_smirnova"));
        var attendee2 = new Attendee(new Username("oleg_ivanov"));
        Console.WriteLine($" Созданы участники: {attendee1.Username.Value}, {attendee2.Username.Value}");

        // 5. Регистрируем участников на мероприятие
        Console.WriteLine($"\n Регистрация на мероприятие ");
        Console.WriteLine($"Свободных мест до регистрации: {event1.GetAvailableSeats()}");

        event1.RegisterAttendee();
        Console.WriteLine($" Зарегистрирован {attendee1.Username.Value}");
        Console.WriteLine($"Свободных мест после: {event1.GetAvailableSeats()}");

        event1.RegisterAttendee();
        Console.WriteLine($" Зарегистрирован {attendee2.Username.Value}");
        Console.WriteLine($"Свободных мест после: {event1.GetAvailableSeats()}");

        // 6. Проверяем работу репозиториев (временное хранилище в памяти)
        Console.WriteLine($"\n Работа с репозиториями");

        var attendeeRepo = new AttendeeRepository();
        await attendeeRepo.AddAsync(attendee1);
        await attendeeRepo.AddAsync(attendee2);

        var allAttendees = await attendeeRepo.GetAllAsync();
        Console.WriteLine($"Всего участников в репозитории: {allAttendees.Count}");

        var foundAttendee = await attendeeRepo.GetByUsernameAsync(new Username("anna_smirnova"));
        Console.WriteLine($"Найден участник по имени: {foundAttendee?.Username.Value ?? "не найден"}");

        // 7. Проверка регистрации через репозиторий
        var registrationRepo = new RegistrationRepository();
        var registration = new Registration(event1.Id, attendee1.Id);
        await registrationRepo.AddAsync(registration);

        var isRegistered = await registrationRepo.IsAttendeeRegisteredAsync(event1.Id, attendee1.Id);
        Console.WriteLine($"\n Участник {attendee1.Username.Value} зарегистрирован на мероприятие: {isRegistered}");

        Console.WriteLine("\nВсе тесты пройдены успешно!");
    }
}