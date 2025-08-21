using Lesson4_DataLayer.DataLayer;
using Lesson4_DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// DataLayer DataAccessLayer,  DL, DAL
namespace Lesson4_DataLayer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nВывод клиетов по ID\n");
            CustomerModel cust1 = DL.Customer.ByID(1);
            CustomerModel cust2 = DL.Customer.ByID(2);
            Console.WriteLine("Клиент 1: " + cust1);
            Console.WriteLine("Клиент 2: " + cust2);

            Console.WriteLine("\n+++\n");

            Console.WriteLine("\nДобавление клиента\n");
            CustomerModel newCustomer = new CustomerModel(0, "FN_new", "SomeLastName", DateTime.Now); // При добавлении решил что пользователь
                                                                                                      // должен быть добавлен в текущий момент времени
            int id = DL.Customer.Insert(newCustomer);
            Console.WriteLine($"Новый клиент добавлен с ID = {id}");

            Console.WriteLine("\n+++\n");

            Console.WriteLine("\nОтображение списка всех клиентов\n");
            List<CustomerModel> allcustomers = DL.Customer.All();

            foreach (var cust in allcustomers)
            {
                Console.WriteLine(cust);
            }

            //Delete
            Console.WriteLine("\n+++\n");

            Console.WriteLine("\nУдаление клиента под ID 3\n");
            int deletedCount = DL.Customer.Delete(3);
            if (deletedCount > 0)
                Console.WriteLine($"Удалено {deletedCount} записей.");
            else
                Console.WriteLine("Удаление не выполнено.");

            //Update
            Console.WriteLine("\n+++\n");

            Console.WriteLine("\nОбновление информации о клиенте\n");
            CustomerModel customerToUpdate = DL.Customer.ByID(1);
            customerToUpdate.FirstName = "UpdatedFirst";
            customerToUpdate.LastName = "UpdatedLast";
            customerToUpdate.BirthDate = DateTime.Now.AddYears(-30);

            int updated = DL.Customer.Update(customerToUpdate);
            Console.WriteLine("Данные клиента успешно обновлены.");

            Console.WriteLine("\nВсе процедуры выполнены успешно. Нажмите Enter для выхода...");
            Console.ReadLine();
        }
    }
}
