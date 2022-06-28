using Newtonsoft.Json;
using System.Globalization;

namespace ToDoList
{
    internal class Program
    {
        private static readonly CultureInfo cultureInfo = new CultureInfo("en-EN");
        private static readonly string dateFormat = "dd.MM.yyyy";

        static void Main(string[] args)
        {
            bool exists = File.Exists(@"YourToDo.json");
            if (exists)
            {
                StartApplication();
            }
            else
            {
                var toDo = new ToDo();
                var toDoList = toDo.ToDoList;

                Console.WriteLine("The task list is empty. Add a task!\n");

                var toDoItem = new ToDoModel();
                toDoItem.Id = toDoList.Count + 1;

                Console.WriteLine("Enter the start date for the task: ");
                try
                {
                    toDoItem.StartDate = DateTime.ParseExact(Console.ReadLine(), dateFormat, cultureInfo);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error! Enter the start date for the task: ");
                    toDoItem.StartDate = DateTime.ParseExact(Console.ReadLine(), dateFormat, cultureInfo);
                }

                Console.WriteLine("Enter the end date for the task: ");
                try
                {
                    toDoItem.EndDate = DateTime.ParseExact(Console.ReadLine(), dateFormat, cultureInfo);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error! Enter the start date for the task: ");
                    toDoItem.EndDate = DateTime.ParseExact(Console.ReadLine(), dateFormat, cultureInfo);
                }

                Console.WriteLine("Enter task content: ");
                toDoItem.Task = Console.ReadLine();

                toDo.Add(toDoItem);
                SerializeToJson(toDo);

                Console.Clear();

                StartApplication();
            }
        }

        public static void StartApplication()
        {
            while (true)
            {
                var toDoJson = File.ReadAllText(@"ToDo.json");
                var toDo = JsonConvert.DeserializeObject<ToDo>(toDoJson);
                var toDoList = toDo.ToDoList;

                Console.WriteLine($"ToDo Count: {toDo.Count}");

                ShowTasks(toDo);

                Console.WriteLine();

                ShowCommands();

                Console.WriteLine();

                var command = Console.ReadLine();
                Console.WriteLine();
                var readCommand = ReadCommand(command, toDo, toDoList);
                if (readCommand == false)
                {
                    Console.Clear();
                    continue;
                }

                Console.Clear();
            }
        }
        public static void ShowTasks(ToDo toDo)
        {
            Console.WriteLine("ToDO List:");
            Console.WriteLine();

            foreach (var toDoItem in toDo)
                Console.WriteLine(toDoItem);
        }
        public static void SerializeToJson(ToDo toDo)
        {
            using (var streamWriter = new StreamWriter(@"ToDo.json", false))
            {
                var serializedToDoList = JsonConvert.SerializeObject(toDo, Formatting.Indented);
                streamWriter.WriteLine(serializedToDoList);
            }
        }
        public static void ShowCommands()
        {
            Console.WriteLine
                (
                    "Add new ToDo: /post \n" +
                    "Remove the ToDo: /delete \n" +
                    "Change ToDo Task: /change_task \n" +
                    "Change Start Date: /change_start_date \n" +
                    "Изменить дату начала задачи: /change_end_date \n" +
                    "Close the program: /close"
                );
        }
        public static bool ReadCommand(string command, ToDo toDo, List<ToDoModel> toDoList)
        {
            if (command == "/post")
            {
                var postToDoItem = new ToDoModel();

                postToDoItem.Id = 1 + toDoList.Count;

                Console.WriteLine("Enter the start date for the task: ");
                try
                {
                    postToDoItem.StartDate = DateTime.ParseExact(Console.ReadLine(), dateFormat, cultureInfo);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error! Enter the start date for the task: ");
                    postToDoItem.StartDate = DateTime.ParseExact(Console.ReadLine(), dateFormat, cultureInfo);
                }

                Console.WriteLine("Enter the end date for the task: ");
                try
                {
                    postToDoItem.EndDate = DateTime.ParseExact(Console.ReadLine(), dateFormat, cultureInfo);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error! Enter the end date for the task: ");
                    postToDoItem.EndDate = DateTime.ParseExact(Console.ReadLine(), dateFormat, cultureInfo);
                }

                Console.WriteLine("Enter task content: ");
                postToDoItem.Task = Console.ReadLine();

                toDo.Post(postToDoItem);
                SerializeToJson(toDo);

                return true;
            }
            else if (command == "/delete")
            {
                Console.WriteLine("Enter the number of the task you want to delete: ");

                var id = int.Parse(Console.ReadLine());

                toDo.Delete(id);

                SerializeToJson(toDo);

                return true;
            }
            else if (command == "/change_task")
            {
                Console.WriteLine("Enter the number of the task you want to change: ");
                var id = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter new task: ");
                var newTask = Console.ReadLine();

                toDo.ChangeTask(id, newTask);

                SerializeToJson(toDo);

                return true;
            }
            else if (command == "/change_start_date")
            {
                Console.WriteLine("Enter the number of the task whose start date you want to change: ");
                var id = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the new start date in the format dd.mm.yyyy. For example : 01.01.2000");
                var newStart = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", cultureInfo);

                toDo.ChangeStartDate(id, newStart);

                SerializeToJson(toDo);

                return true;
            }
            else if (command == "/change_end_date")
            {
                Console.WriteLine("Enter the number of the task whose end date you want to change: ");
                var id = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the new end date in the format dd.mm.yyyy. For example: 01.01.2000");
                var newEnd = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", cultureInfo);

                toDo.ChangeEndDate(id, newEnd);

                SerializeToJson(toDo);

                return true;
            }
            else if (command == "/close")
            {
                Environment.Exit(0);
            }

            return false;
        }
    }
}