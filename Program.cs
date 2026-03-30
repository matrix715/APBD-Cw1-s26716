using Cwiczenia2.Models;
using Cwiczenia2.Models.Equipments;
using Cwiczenia2.Models.Users;
using Cwiczenia2.Services;

var userService = new UserService();
var equipmentService = new EquipmentService();
var rentalService = new RentalService(userService, equipmentService);
var reportService = new ReportService(equipmentService, userService, rentalService);

LoadSampleData();

bool exit = false;

while (!exit)
{
    ShowMenu();
    Console.Write("Choose an option: ");
    string? choice = Console.ReadLine();

    Console.WriteLine();

    try
    {
        switch (choice)
        {
            case "1":
                AddUser();
                break;

            case "2":
                AddEquipment();
                break;

            case "3":
                ShowAllEquipment();
                break;

            case "4":
                ShowAvailableEquipment();
                break;

            case "5":
                RentEquipment();
                break;

            case "6":
                ReturnEquipment();
                break;

            case "7":
                MarkEquipmentUnavailable();
                break;

            case "8":
                ShowActiveRentalsForUser();
                break;

            case "9":
                ShowOverdueRentals();
                break;

            case "10":
                ShowReport();
                break;

            case "11":
                ShowAllUsers();
                break;

            case "12":
                ShowAllRentals();
                break;

            case "0":
                exit = true;
                Console.WriteLine("Closing program...");
                break;

            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    if (!exit)
    {
        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
        Console.Clear();
    }
}

void ShowMenu()
{
    Console.WriteLine("===== UNIVERSITY EQUIPMENT RENTAL SYSTEM =====");
    Console.WriteLine("1. Add new user");
    Console.WriteLine("2. Add new equipment");
    Console.WriteLine("3. Show all equipment");
    Console.WriteLine("4. Show available equipment");
    Console.WriteLine("5. Rent equipment to user");
    Console.WriteLine("6. Return equipment");
    Console.WriteLine("7. Mark equipment as unavailable");
    Console.WriteLine("8. Show active rentals for user");
    Console.WriteLine("9. Show overdue rentals");
    Console.WriteLine("10. Generate report");
    Console.WriteLine("11. Show all users");
    Console.WriteLine("12. Show all rentals");
    Console.WriteLine("0. Exit");
    Console.WriteLine();
}

void LoadSampleData()
{
    var student1 = new Student("Jan", "Kowalski", "s12345");
    var student2 = new Student("Anna", "Nowak", "s54321");
    var teacher1 = new Teacher("Adam", "Wisniewski", "Computer Science");
    var teacher2 = new Teacher("Maria", "Zielinska", "Mathematics");

    userService.AddUser(student1);
    userService.AddUser(student2);
    userService.AddUser(teacher1);
    userService.AddUser(teacher2);

    var laptop1 = new Laptop("Dell Latitude", EquipmentStatus.Available, "Intel i5", "16GB");
    var laptop2 = new Laptop("Lenovo ThinkPad", EquipmentStatus.Available, "Intel i7", "32GB");
    var camera1 = new Camera("Canon EOS", EquipmentStatus.Available, 24, "DSLR");
    var camera2 = new Camera("Sony Alpha", EquipmentStatus.Available, 32, "Mirrorless");
    var projector1 = new Projector("Epson X500", EquipmentStatus.Available, "1920x1080", "3000");
    var projector2 = new Projector("BenQ MX550", EquipmentStatus.Available, "1280x800", "3600");

    equipmentService.AddEquipment(laptop1);
    equipmentService.AddEquipment(laptop2);
    equipmentService.AddEquipment(camera1);
    equipmentService.AddEquipment(camera2);
    equipmentService.AddEquipment(projector1);
    equipmentService.AddEquipment(projector2);

    rentalService.RentEquipment(student1.Id, laptop1.Id, 7);
    rentalService.RentEquipment(teacher1.Id, camera1.Id, 3);

    var overdueRental = rentalService.GetAllRentals()
        .FirstOrDefault(r => r.Equipment.Id == camera1.Id && !r.IsReturned);

    if (overdueRental != null)
    {
        overdueRental.DueDate = DateTime.Now.AddDays(-2);
    }
}

void AddUser()
{
    Console.WriteLine("=== Add User ===");
    Console.WriteLine("1. Student");
    Console.WriteLine("2. Teacher");
    Console.Write("Choose user type: ");
    string? type = Console.ReadLine();

    Console.Write("First name: ");
    string firstName = Console.ReadLine() ?? "";

    Console.Write("Last name: ");
    string lastName = Console.ReadLine() ?? "";

    if (type == "1")
    {
        Console.Write("Student number: ");
        string studentNumber = Console.ReadLine() ?? "";

        var student = new Student(firstName, lastName, studentNumber);
        userService.AddUser(student);

        Console.WriteLine("Student added.");
    }
    else if (type == "2")
    {
        Console.Write("Department: ");
        string department = Console.ReadLine() ?? "";

        var teacher = new Teacher(firstName, lastName, department);
        userService.AddUser(teacher);

        Console.WriteLine("Teacher added.");
    }
    else
    {
        Console.WriteLine("Invalid user type.");
    }
}

void AddEquipment()
{
    Console.WriteLine("=== Add Equipment ===");
    Console.WriteLine("1. Laptop");
    Console.WriteLine("2. Camera");
    Console.WriteLine("3. Projector");
    Console.Write("Choose equipment type: ");
    string? type = Console.ReadLine();

    Console.Write("Equipment name: ");
    string name = Console.ReadLine() ?? "";

    if (type == "1")
    {
        Console.Write("Processor: ");
        string processor = Console.ReadLine() ?? "";

        Console.Write("RAM: ");
        string ram = Console.ReadLine() ?? "";

        var laptop = new Laptop(name, EquipmentStatus.Available, processor, ram);
        equipmentService.AddEquipment(laptop);

        Console.WriteLine("Laptop added.");
    }
    else if (type == "2")
    {
        Console.Write("Megapixels: ");
        int megapixels = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Camera type: ");
        string cameraType = Console.ReadLine() ?? "";

        var camera = new Camera(name, EquipmentStatus.Available, megapixels, cameraType);
        equipmentService.AddEquipment(camera);

        Console.WriteLine("Camera added.");
    }
    else if (type == "3")
    {
        Console.Write("Resolution: ");
        string resolution = Console.ReadLine() ?? "";

        Console.Write("Brightness (lumens): ");
        string lumens = Console.ReadLine() ?? "";

        var projector = new Projector(name, EquipmentStatus.Available, resolution, lumens);
        equipmentService.AddEquipment(projector);

        Console.WriteLine("Projector added.");
    }
    else
    {
        Console.WriteLine("Invalid equipment type.");
    }
}

void ShowAllEquipment()
{
    Console.WriteLine("=== All Equipment ===");
    var equipmentList = equipmentService.GetAllEquipment();

    if (equipmentList.Count == 0)
    {
        Console.WriteLine("No equipment found.");
        return;
    }

    foreach (var equipment in equipmentList)
    {
        Console.WriteLine($"ID: {equipment.Id}, Name: {equipment.Name}, Status: {equipment.Status}");
    }
}

void ShowAvailableEquipment()
{
    Console.WriteLine("=== Available Equipment ===");
    var equipmentList = equipmentService.GetAvailableEquipment();

    if (equipmentList.Count == 0)
    {
        Console.WriteLine("No available equipment.");
        return;
    }

    foreach (var equipment in equipmentList)
    {
        Console.WriteLine($"ID: {equipment.Id}, Name: {equipment.Name}, Status: {equipment.Status}");
    }
}

void RentEquipment()
{
    Console.WriteLine("=== Rent Equipment ===");
    ShowAllUsers();
    Console.Write("\nEnter user ID: ");
    int userId = int.Parse(Console.ReadLine() ?? "0");

    ShowAvailableEquipment();
    Console.Write("\nEnter equipment ID: ");
    int equipmentId = int.Parse(Console.ReadLine() ?? "0");

    Console.Write("Enter number of rental days: ");
    int days = int.Parse(Console.ReadLine() ?? "0");

    rentalService.RentEquipment(userId, equipmentId, days);
    Console.WriteLine("Equipment rented successfully.");
}

void ReturnEquipment()
{
    Console.WriteLine("=== Return Equipment ===");

    var activeRentals = rentalService.GetAllRentals()
        .Where(r => !r.IsReturned)
        .ToList();

    if (activeRentals.Count == 0)
    {
        Console.WriteLine("No active rentals.");
        return;
    }

    foreach (var rental in activeRentals)
    {
        Console.WriteLine(
            $"Rental ID: {rental.Id}, User: {rental.User.FirstName} {rental.User.LastName}, " +
            $"Equipment: {rental.Equipment.Name}, Due Date: {rental.DueDate}");
    }

    Console.Write("\nEnter rental ID to return: ");
    int rentalId = int.Parse(Console.ReadLine() ?? "0");

    var rentalToReturn = rentalService.GetAllRentals().FirstOrDefault(r => r.Id == rentalId);
    if (rentalToReturn == null)
    {
        Console.WriteLine("Rental not found.");
        return;
    }

    rentalService.ReturnEquipment(rentalId);

    Console.WriteLine($"Equipment returned. Penalty: {rentalToReturn.PenaltyAmount}");
}

void MarkEquipmentUnavailable()
{
    Console.WriteLine("=== Mark Equipment as Unavailable ===");
    ShowAllEquipment();
    Console.Write("\nEnter equipment ID: ");
    int equipmentId = int.Parse(Console.ReadLine() ?? "0");

    equipmentService.MarkAsUnavailable(equipmentId);
    Console.WriteLine("Equipment status changed to Unavailable.");
}

void ShowActiveRentalsForUser()
{
    Console.WriteLine("=== Active Rentals for User ===");
    ShowAllUsers();

    Console.Write("\nEnter user ID: ");
    int userId = int.Parse(Console.ReadLine() ?? "0");

    var rentals = reportService.GetActiveRentalsForUser(userId);

    if (rentals.Count == 0)
    {
        Console.WriteLine("No active rentals for this user.");
        return;
    }

    foreach (var rental in rentals)
    {
        Console.WriteLine(
            $"Rental ID: {rental.Id}, Equipment: {rental.Equipment.Name}, " +
            $"Rental Date: {rental.Rentdate}, Due Date: {rental.DueDate}");
    }
}

void ShowOverdueRentals()
{
    Console.WriteLine("=== Overdue Rentals ===");
    var overdueRentals = reportService.GetOverdueRentals();

    if (overdueRentals.Count == 0)
    {
        Console.WriteLine("No overdue rentals.");
        return;
    }

    foreach (var rental in overdueRentals)
    {
        Console.WriteLine(
            $"Rental ID: {rental.Id}, User: {rental.User.FirstName} {rental.User.LastName}, " +
            $"Equipment: {rental.Equipment.Name}, Due Date: {rental.DueDate}");
    }
}

void ShowReport()
{
    Console.WriteLine("=== Report ===");
    Console.WriteLine(reportService.GenerateSystemReport());
}

void ShowAllUsers()
{
    Console.WriteLine("=== Users ===");
    var users = userService.GetAllUsers();

    if (users.Count == 0)
    {
        Console.WriteLine("No users found.");
        return;
    }

    foreach (var user in users)
    {
        Console.WriteLine(
            $"ID: {user.Id}, {user.FirstName} {user.LastName}, Type: {user.UserTyp}, Limit: {user.MaxRentals}");
    }
}

void ShowAllRentals()
{
    Console.WriteLine("=== All Rentals ===");
    var rentals = rentalService.GetAllRentals();

    if (rentals.Count == 0)
    {
        Console.WriteLine("No rentals found.");
        return;
    }

    foreach (var rental in rentals)
    {
        string status = rental.IsReturned ? "Returned" : "Active";
        Console.WriteLine(
            $"Rental ID: {rental.Id}, User: {rental.User.FirstName} {rental.User.LastName}, " +
            $"Equipment: {rental.Equipment.Name}, Rent Date: {rental.Rentdate}, Due Date: {rental.DueDate}, " +
            $"Return Date: {rental.ReturnDate}, Status: {status}, Penalty: {rental.PenaltyAmount}"
        );
    }
}