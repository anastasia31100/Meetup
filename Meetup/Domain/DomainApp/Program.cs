using Meetup.Domain;
using Meetup.Domain.Exceptions;
using Meetup.ValueObjects;

namespace Meetup.DomainApp;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Платформа мероприятий - Демонстрация доменной логики\n");

        try
        {
            // Демонстрация работы с Value Objects
            DemonstrateValueObjects();

            // Демонстрация создания пользователей
            var organizer = CreateOrganizer();
            var attendee = CreateAttendee();

            // Демонстрация создания типов мероприятий
            var eventTypes = CreateEventTypes();

            // Демонстрация создания мероприятий
            var events = CreateEvents(organizer, eventTypes);

            // Демонстрация регистрации на мероприятия
            DemonstrateRegistration(attendee, events);

            // Демонстрация редактирования мероприятия
            DemonstrateEditEvent(organizer, events[0]);

            // Демонстрация отмены регистрации
            DemonstrateCancelRegistration(attendee, events[1]);

            // Демонстрация отмены мероприятия
            DemonstrateCancelEvent(organizer, events[2]);

            // Демонстрация обработки ошибок
            DemonstrateErrorHandling(organizer, attendee, events);

            Console.WriteLine("\nВсе демонстрации завершены успешно");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n ошибка: {ex.Message}");
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    static void DemonstrateValueObjects()
    {
        Console.WriteLine("Демонстрация Value Objects");

        try
        {
            var validUsername = new Username("john_doe");
            Console.WriteLine($"Создан Username: {validUsername.Value}");

            var validTitle = new Title("Tech Conference 2024");
            Console.WriteLine($" Создан Title: {validTitle.Value}");

            var validLocation = new Location("Moscow, Expocentre");
            Console.WriteLine($" Создан Location: {validLocation.Value}");

            try
            {
                var invalidUsername = new Username("a");
                Console.WriteLine($" Должно было выбросить исключение: {invalidUsername.Value}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Исключение при создании короткого имени: {ex.Message}");
            }

            try
            {
                var invalidTitle = new Title(new string('A', 201));
                Console.WriteLine($" Должно было выбросить исключение");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Исключение при создании длинного заголовка: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Ошибка: {ex.Message}");
        }

        Console.WriteLine();
    }

    static Organizer CreateOrganizer()
    {
        var username = new Username("event_organizer");
        var organizer = new Organizer(username, EntityType.Individual); // ← без Guid
        Console.WriteLine($"Создан организатор: {organizer.Username.Value}");
        return organizer;
    }

    static Attendee CreateAttendee()
    {
        var username = new Username("john_attendee");
        var attendee = new Attendee(Guid.NewGuid(), username);
        Console.WriteLine($" Создан участник: {attendee.Username.Value}");
        Console.WriteLine();
        return attendee;
    }

    static List<EventType> CreateEventTypes()
    {
        var eventTypes = new List<EventType>();

        // Используем публичный конструктор без Guid
        var conference = new EventType(
            new EventTypeName("Conference"),
            new EventDescription("Professional conferences and summits"));
        eventTypes.Add(conference);
        Console.WriteLine($"Создан тип мероприятия: {conference.Name.Value}");

        var workshop = new EventType(
            new EventTypeName("Workshop"),
            new EventDescription("Hands-on workshops and training"));
        eventTypes.Add(workshop);
        Console.WriteLine($"Создан тип мероприятия: {workshop.Name.Value}");
        var meetup = new EventType(
       new EventTypeName("Meetup"),
       new EventDescription("Casual meetups and networking"));
        eventTypes.Add(meetup);
        Console.WriteLine($"Создан тип мероприятия: {meetup.Name.Value}");

        Console.WriteLine();
        return eventTypes;
    }

    static List<Event> CreateEvents(Organizer organizer, List<EventType> eventTypes)
    {
        var events = new List<Event>();

        var event1 = organizer.CreateEvent(
            new Title("Annual Tech Conference 2024"),
            new EventDescription("The biggest tech event of the year"),
            DateTime.UtcNow.AddDays(30),
            new Location("Moscow, Crocus Expo"),
            100,
            eventTypes[0]
        );
        events.Add(event1);
        Console.WriteLine($" Создано мероприятие: \"{event1.Title.Value}\" (мест: {event1.MaxAttendees}, дата: {event1.EventDate:yyyy-MM-dd})");

        var event2 = organizer.CreateEvent(
            new Title("Advanced .NET Workshop"),
            new EventDescription("Deep dive into modern .NET development"),
            DateTime.UtcNow.AddDays(14),
            new Location("St. Petersburg, Tech Hub"),
            30,
            eventTypes[1]
        );
        events.Add(event2);
        Console.WriteLine($"✓ Создано мероприятие: \"{event2.Title.Value}\" (мест: {event2.MaxAttendees})");

        var event3 = organizer.CreateEvent(
            new Title("DevOps Meetup"),
            new EventDescription("Monthly DevOps community meetup"),
            DateTime.UtcNow.AddDays(7),
            new Location("Online"),
            50,
            eventTypes[2]
        );
        events.Add(event3);
        Console.WriteLine($" Создано мероприятие: \"{event3.Title.Value}\" (мест: {event3.MaxAttendees})");

        Console.WriteLine();
        return events;
    }

    static void DemonstrateRegistration(Attendee attendee, List<Event> events)
    {
        Console.WriteLine("Демонстрация регистрации на мероприятия");

        var registration1 = attendee.RegisterForEvent(events[0]);
        Console.WriteLine($" {attendee.Username.Value} зарегистрировался на \"{events[0].Title.Value}\"");
        Console.WriteLine($"  Свободных мест: {events[0].AvailableSeats()}/{events[0].MaxAttendees}");

        var registration2 = attendee.RegisterForEvent(events[1]);
        Console.WriteLine($" {attendee.Username.Value} зарегистрировался на \"{events[1].Title.Value}\"");
        Console.WriteLine($"  Свободных мест: {events[1].AvailableSeats()}/{events[1].MaxAttendees}");

        Console.WriteLine($"\nАктивных регистраций у {attendee.Username.Value}: {attendee.ActiveRegistrations.Count}");

        try
        {
            attendee.RegisterForEvent(events[0]);
            Console.WriteLine(" Должно было выбросить исключение о повторной регистрации");
        }
        catch (AlreadyRegisteredException ex)
        {
            Console.WriteLine($" Исключение: {ex.Message}");
        }

        Console.WriteLine();
    }

    static void DemonstrateEditEvent(Organizer organizer, Event eventObj)
    {
        Console.WriteLine(" Демонстрация редактирования мероприятия");

        var newTitle = new Title("Annual Tech Conference 2024 - Updated");
        var newMaxAttendees = 150;

        var updated = organizer.EditEvent(eventObj, newTitle, eventObj.Description,
            eventObj.EventDate, eventObj.Location, newMaxAttendees);

        if (updated)
        {
            Console.WriteLine($" Мероприятие обновлено:");
            Console.WriteLine($"  Новый заголовок: {eventObj.Title.Value}");
            Console.WriteLine($"  Новое количество мест: {eventObj.MaxAttendees}");
        }

        try
        {
            var anotherOrganizer = new Organizer( new Username("fake_organizer"), EntityType.Individual);
            anotherOrganizer.EditEvent(eventObj, newTitle, eventObj.Description,
                eventObj.EventDate, eventObj.Location, newMaxAttendees);
            Console.WriteLine(" Должно было выбросить исключение");
        }
        catch (AnotherOrganizerEditEventException ex)
        {
            Console.WriteLine($" Исключение: {ex.Message}");
        }

        Console.WriteLine();
    }

    static void DemonstrateCancelRegistration(Attendee attendee, Event eventObj)
    {
        Console.WriteLine("Демонстрация отмены регистрации ");

        var cancelled = attendee.CancelRegistration(eventObj);
        if (cancelled)
        {
            Console.WriteLine($" {attendee.Username.Value} отменил регистрацию на \"{eventObj.Title.Value}\"");
            Console.WriteLine($"  Свободных мест: {eventObj.AvailableSeats()}/{eventObj.MaxAttendees}");
        }

        try
        {
            attendee.CancelRegistration(eventObj);
            Console.WriteLine(" Должно было выбросить исключение");
        }
        catch (NoRegistrationFoundException ex)
        {
            Console.WriteLine($"Исключение: {ex.Message}");
        }

        Console.WriteLine();
    }

    static void DemonstrateCancelEvent(Organizer organizer, Event eventObj)
    {
        Console.WriteLine("Демонстрация отмены мероприятия ");

        organizer.CancelEvent(eventObj);
        Console.WriteLine($" Мероприятие \"{eventObj.Title.Value}\" отменено");
        Console.WriteLine($"  Активно: {eventObj.IsActive}, Отменено: {eventObj.IsCancelled}");

        try
        {
            organizer.CancelEvent(eventObj);
            Console.WriteLine(" Должно было выбросить исключение");
        }
        catch (EventNotActiveException ex)
        {
            Console.WriteLine($" Исключение: {ex.Message}");
        }

        Console.WriteLine();
    }

    static void DemonstrateErrorHandling(Organizer organizer, Attendee attendee, List<Event> events)
    {
        Console.WriteLine("Демонстрация обработки ошибок");

        try
        {
            var pastEvent = organizer.CreateEvent(
                new Title("Past Event"),
                new EventDescription("This event is in the past"),
                DateTime.UtcNow.AddDays(-1),
                new Location("Somewhere"),
                10,
               new EventType(new EventTypeName("Test"))
            );

            attendee.RegisterForEvent(pastEvent);
            Console.WriteLine(" Должно было выбросить исключение");
        }
        catch (EventAlreadyStartedException ex)
        {
            Console.WriteLine($" Исключение: {ex.Message}");
        }

        try
        {
            var fullEvent = organizer.CreateEvent(
                new Title("Full Event"),
                new EventDescription("This event is full"),
                DateTime.UtcNow.AddDays(10),
                new Location("Somewhere"),
                1,
                new EventType(new EventTypeName("Test"))
            );

            var attendee1 = new Attendee(Guid.NewGuid(), new Username("user1"));
            var attendee2 = new Attendee(Guid.NewGuid(), new Username("user2"));

            attendee1.RegisterForEvent(fullEvent);
            attendee2.RegisterForEvent(fullEvent);
            Console.WriteLine(" Должно было выбросить исключение");
        }
        catch (EventFullException ex)
        {
            Console.WriteLine($" Исключение: {ex.Message}");
        }

        Console.WriteLine();

        // Показываем итоговую информацию
        Console.WriteLine("Итоговая информация");
        Console.WriteLine($"Организатор: {organizer.Username.Value}");
        Console.WriteLine($"Количество мероприятий: {organizer.Events.Count}");
        Console.WriteLine($"Участник: {attendee.Username.Value}");
        Console.WriteLine($"Активных регистраций: {attendee.ActiveRegistrations.Count}");
        Console.WriteLine($"В истории: {attendee.History.Count}");
    }
}