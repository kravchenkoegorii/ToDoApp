using System.Collections;

namespace ToDoList
{
    public class ToDo : ICollection<ToDoModel>
    {
        public List<ToDoModel> ToDoList { get; set; } = new List<ToDoModel>(0);

        public void Post(ToDoModel toDoItem) => ToDoList.Add(toDoItem);
        public void Delete(int id)
        {
            try
            {
                ToDoList.Remove(ToDoList[--id]);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка. Введите номер задачи, которую вы хотите удалить");
            }

            for (int i = 1; i < ToDoList.Count; i++)
            {
                ToDoList[i].Id = ++i;
            }
        }
        public void ChangeTask(int id, string newTask) => ToDoList[--id].Task = newTask;
        public void ChangeStartDate(int id, DateTime date) => ToDoList[--id].StartDate = date;
        public void ChangeEndDate(int id, DateTime date) => ToDoList[--id].EndDate = date;


        public int Count => ToDoList.Count;
        public bool IsReadOnly => false;
        public void Add(ToDoModel toDoItem) => ToDoList.Add(toDoItem);
        public bool Remove(ToDoModel toDoItem) => ToDoList.Remove(toDoItem);
        public void Clear() => ToDoList.Clear();
        public bool Contains(ToDoModel toDoItem) => ToDoList.Contains(toDoItem);
        public void CopyTo(ToDoModel[] array, int arrayIndex) => ToDoList.CopyTo(array, arrayIndex);
        public IEnumerator<ToDoModel> GetEnumerator() => ToDoList.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ToDoList.GetEnumerator();
    }
}
