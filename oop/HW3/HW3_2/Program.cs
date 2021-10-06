using System;
using System.Collections.Generic;

namespace HW3_2
{
    class Program
    {
        static void Main(string[] args)
        {
            const string PROMPT = "Pick one:\n" +
                "1) Create group\n" +
                "2) Add a good student to a group\n" +
                "3) Add a bad student to a group\n" +
                "4) Get group info\n" +
                "5) Get full group info\n" +
                "6) Study for all students\n" +
                "7) Quit";

            Group group = null;
            while (true)
            {
                Console.WriteLine(PROMPT);
                string option = Console.ReadLine();
                if (option == "1")
                {
                    Console.Write("Group name: ");
                    string name = Console.ReadLine();
                    group = new Group(name);
                    Console.WriteLine("Group created!");
                }
                else if (option == "7")
                {
                    Console.WriteLine("Bye!");
                    break;
                }
                else
                {
                    if (group == null)
                    {
                        Console.WriteLine("Create a group first!");
                        continue;
                    }
                    if (option == "2" || option == "3")
                    {
                        Console.Write("Student name: ");
                        string name = Console.ReadLine();
                        Student student;
                        if (option == "2")
                        {
                            student = new GoodStudent(name);
                        }
                        else
                        {
                            student = new BadStudent(name);
                        }
                        group.AddStudent(student);
                    }
                    else if (option == "4")
                    {
                        group.GetInfo();
                    }
                    else if (option == "5")
                    {
                        group.GetFullInfo();
                    }
                    else if (option == "6")
                    {
                        group.StudyAll();
                    }
                    else
                    {
                        Console.WriteLine("Wrong option!");
                    }
                }
            }
        }
    }

    class Group
    {
        private string name;
        private List<Student> students;

        public Group(string name)
        {
            this.name = name;
            students = new List<Student>();
        }

        public void AddStudent(Student st)
        {
            students.Add(st);
        }

        public void GetInfo()
        {
            Console.WriteLine(name);
            foreach (Student st in students)
            {
                Console.WriteLine(st.getName());
            }
        }

        public void GetFullInfo()
        {
            Console.WriteLine(name);
            foreach (Student st in students)
            {
                Console.WriteLine(st.getName() + " " + st.getState());
            }
        }

        public void StudyAll()
        {
            foreach (Student st in students)
            {
                st.Study();
            }
        }
    }

    abstract class Student
    {
        private string name;
        protected string state;

        public Student(string name)
        {
            this.name = name;
            state = "";
        }

        public string getName()
        {
            return name;
        }

        public string getState()
        {
            return state;
        }

        public abstract void Study();

        public void Read()
        {
            state += " Read";
        }

        public void Write()
        {
            state += " Write";
        }

        public void Relax()
        {
            state += " Relax";
        }
    }

    class GoodStudent : Student
    {
        public GoodStudent(string name) : base(name)
        {
            state += "good";
        }

        public override void Study()
        {
            Read();
            Write();
            Read();
            Write();
            Relax();
        }

    }

    class BadStudent : Student
    {
        public BadStudent(string name) : base(name)
        {
            state += "bad";
        }

        public override void Study()
        {
            Relax();
            Relax();
            Relax();
            Relax();
            Relax();
        }

    }
}
