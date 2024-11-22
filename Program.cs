using System;
using System.Collections.Generic;
using System.Linq;

namespace GENERIC1
{
    public class choices
    {
        public int choose;
        crud cd = new crud();
        crud2 cd2 = new crud2();
        public void testing()
        {
            Program.test();
            Console.Write("Choose to Test:");
            choose = int.Parse(Console.ReadLine());

            switch (choose)
            {
                case 1:
                    Console.WriteLine("\n=STUDENT REPOSITORY MANAGEMENT=");
                    cd.Introduce();
                    break;
                case 2:
                    Console.WriteLine("\n=PRODUCT REPOSITORY MANAGEMENT=");
                    cd2.SecondIntroduce();
                    break;
                default:
                    Console.WriteLine("Invalid");
                    break;
            }
        }

    }
}
﻿

namespace GENERIC1
{
    public class crud
    {
        Repository<Student> repository = new Repository<Student>();
        public int id, Choice;
        public string name;
        public void Introduce()
        {
            while (true)
            {
                Program.Option();
                Console.Write("Enter your Choice in Option: ");
                Choice = int.Parse(Console.ReadLine());

                switch (Choice)
                {
                    case 1:
                        ADD();
                        break;
                    case 2:
                        READ();
                        break;
                    case 3:
                        UPDATE();
                        break;
                    case 4:
                        DELETE();
                        break;
                    case 5:
                        EXIT();
                        return;
                    default:
                        break;
                }
            }
        }
        public void ADD()
        {
            Console.WriteLine("\n=========ADD STUDENT========");
            Console.Write("Enter ID: ");
            id = int.Parse(Console.ReadLine());
            Console.Write("Enter Name: ");
            name = Console.ReadLine();
            if (repository.Read(s => s.Id == id) == null)
            {
                repository.Create(new Student { Id = id, Name = name });
                Console.WriteLine("Student Added Successfully!!!");
            }
            else
            {
                Console.WriteLine("ID already Exist!!!");
            }
        }


        public void READ()
        {
            Console.WriteLine("\n=======DISPLAY STUDENT BY ID=======");
            Console.Write("Enter ID to Display: ");
            id = int.Parse(Console.ReadLine());
            var student = repository.Read(s => s.Id == id);
            if (student != null)
            {
                Console.WriteLine("\n\t ID \tNAME");
                Console.WriteLine($"Student: {student.Id} \t{student.Name}");
            }
            else
            {
                Console.WriteLine("Student ID not Found!!!");
            }
        }
        public void UPDATE()
        {
            Console.WriteLine("\n=======UPDATE STUDENT BY ID=======");
            Console.Write("Enter Student ID to Update: ");
            id = int.Parse(Console.ReadLine());
            var student = repository.Read(s => s.Id == id);
            if (student != null)
            {
                Console.Write("Enter new Name: ");
                name = Console.ReadLine();
                repository.Update(s => s.Id == id, new Student { Id = id, Name = name });
                Console.WriteLine("Student Updated Successfully!!!");
            }
            else
            {
                Console.WriteLine("Student ID not Found!!!");
            }

        }
        public void DELETE()
        {
            Console.WriteLine("\n=====DELETE STUDENT BY ID=====");
            Console.Write("Enter Student ID to Delete: ");
            id = int.Parse(Console.ReadLine());
            var student = repository.Read(s => s.Id == id);
            if (student != null)
            {
                repository.Delete(s => s.Id == id);
                Console.WriteLine("Student Successfully Deleted!!!");
            }
            else
            {
                Console.WriteLine("Student ID not Found!!!");
            }
        }
        public void EXIT()
        {
            Console.WriteLine("=======EXIT=======");

        }
    }

    internal class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}


namespace GENERIC1
{
    public class crud2
    {

        DictionaryRepository<int, Product> dictionaryRepository = new DictionaryRepository<int, Product>();
        public int productid, Choice2;
        public string productname;

        public void SecondIntroduce()
        {
            while (true)
            {
                Program.Option2();
                Console.Write("Enter your Choice in Option: ");
                Choice2 = int.Parse(Console.ReadLine());

                switch (Choice2)
                {
                    case 1:
                        ADDPRODUCT();
                        break;
                    case 2:
                        READPRODUCT();
                        break;
                    case 3:
                        UPDATEPRODUCT();
                        break;
                    case 4:
                        DELETEPRODUCT();
                        break;
                    case 5:
                        EXIT2();
                        return;
                    default:
                        break;
                }

            }

        }
        public void ADDPRODUCT()
        {
            Console.WriteLine("\n========ADD PRODUCT========");
            Console.Write("Enter Product ID: ");
            productid = int.Parse(Console.ReadLine());
            Console.Write("Enter Product Name: ");
            productname = Console.ReadLine();
            if (!dictionaryRepository.ContainsKey(productid))
            {
                dictionaryRepository.Add(productid, new Product { ProductName = productname });
                Console.WriteLine("Product Successfully Added");
            }
            else
            {
                Console.WriteLine("ID already Exist!!!");
            }
        }
        public void READPRODUCT()
        {
            Console.WriteLine("\n=====RETRIEVE PRODUCT=====");
            Console.Write("Enter Product ID to Retrieve: ");
            productid = int.Parse(Console.ReadLine());


            if (dictionaryRepository.ContainsKey(productid))
            {
                var product = dictionaryRepository.Get(productid);
                Console.WriteLine("\tProductID \tProductName");
                Console.WriteLine($"Product: {productid} \t\t{product.ProductName}");
            }
            else
            {
                Console.WriteLine("Product ID not Found!!!");

            }

        }
        public void UPDATEPRODUCT()
        {
            Console.WriteLine("\n=====UPDATE PRODUCT======");
            Console.Write("Enter ID to Update: ");
            productid = int.Parse(Console.ReadLine());
            if (dictionaryRepository.ContainsKey(productid))
            {
                var product = dictionaryRepository.Get(productid);
                Console.Write("Enter new Product Name: ");
                productname = Console.ReadLine();
                if (!string.IsNullOrEmpty(productname)) product.ProductName = productname;
                dictionaryRepository.Update(productid, product);
            }
            else
            {
                Console.WriteLine("Product ID not Found!!!");
            }

        }
        public void DELETEPRODUCT()
        {
            Console.WriteLine("\n========DELETE PRODUCT========");
            Console.Write("Enter ID to DELETE: ");
            productid = int.Parse(Console.ReadLine());


            if (dictionaryRepository.ContainsKey(productid))
            {
                var product = dictionaryRepository.Get(productid);
                dictionaryRepository.Delete(productid);
                Console.WriteLine("Product Deleted Successfully!!!");
            }
            else
            {
                Console.WriteLine("Product ID not Found!!!");
            }

        }
        public void EXIT2()
        {
            Console.WriteLine("========EXIT=========");
        }

    }
}
﻿

namespace GENERIC1
{
    public class DictionaryRepository<TKey, TValue>
    {
        private Dictionary<TKey, TValue> data = new Dictionary<TKey, TValue>();

        public void Create(TKey key, TValue value) => data[key] = value;

        public TValue Read(TKey key) => data.ContainsKey(key) ? data[key] : default;

        public Dictionary<TKey, TValue> ReadAll() => new Dictionary<TKey, TValue>(data);

        public void Update(TKey key, TValue newValue)
        {
            if (data.ContainsKey(key)) data[key] = newValue;
        }

        public void Delete(TKey key) => data.Remove(key);
        public bool ContainsKey(TKey key)
        {
            return data.ContainsKey(key);
        }
        public void Add(TKey key, TValue value)
        {
            data[key] = value;
        }
        public TValue Get(TKey key)
        {
            if (!data.ContainsKey(key))
                throw new KeyNotFoundException($"No item found in the key {key}");
            return data[key];
        }
    }
}
namespace GENERIC1
{
    public class Product
    {

        public string ProductName { get; set; }
    }
}

namespace GENERIC1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            choices cdtest = new choices();
            cdtest.testing();

        }
        public static void Option()
        {
            Console.WriteLine("\n============OPTION=========");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Read Student");
            Console.WriteLine("3. Update Student");
            Console.WriteLine("4. Delete Student");
            Console.WriteLine("5. Exit");
        }
        public static void test()
        {
            Console.WriteLine("==========CHOOSE 1 TO TEST=======");
            Console.WriteLine("1. Repository<Student> ");
            Console.WriteLine("2. DictionaryRepository<int, Product> ");
        }
        public static void Option2()
        {
            Console.WriteLine("\n============OPTION=========");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Read Product");
            Console.WriteLine("3. Update Product");
            Console.WriteLine("4. Delete Product");
            Console.WriteLine("5. Exit");
        }
    }
}

namespace GENERIC1
{
    public class Repository<T>
    {
        private List<T> _data = new List<T>();

        public void Create(T item) => _data.Add(item);

        public T Read(Func<T, bool> predicate) => _data.FirstOrDefault(predicate);

        public IEnumerable<T> ReadAll() => _data;

        public void Update(Func<T, bool> predicate, T newItem)
        {
            var index = _data.FindIndex(item => predicate(item));
            if (index != -1) _data[index] = newItem;
        }

        public void Delete(Func<T, bool> predicate)
        {
            var item = _data.FirstOrDefault(predicate);
            if (item != null) _data.Remove(item);
        }
    }
}
