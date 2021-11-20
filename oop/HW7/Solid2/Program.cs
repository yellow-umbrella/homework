using System;

//Який принцип S.O.L.I.D. порушено? Виправте! <Принцип відкритості/закритості>

/*Зверніть увагу на клас EmailSender. Крім того, що за допомогою методу Send, він відправляє повідомлення, 
він ще і вирішує як буде вестися лог. В даному прикладі лог ведеться через консоль.
Якщо трапиться так, що нам доведеться міняти спосіб логування, то ми будемо змушені внести правки в клас EmailSender.
Хоча, здавалося б, ці правки не стосуються відправки повідомлень. Очевидно, EmailSender виконує кілька обов'язків і, 
щоб клас ні прив'язаний тільки до одного способу вести лог, потрібно винести вибір балки з цього класу.*/
class Email
{
    public String Theme { get; set; }
    public String From { get; set; }
    public String To { get; set; }
}

class EmailSender
{
    public void Send(Email email, ILog log)
    {
        // ... sending...
        log.Write(email);
    }
}

interface ILog
{
    void Write(Email email);
}

class ConsoleLog : ILog
{
    public void Write(Email email)
    {
        Console.WriteLine("Email from '" + email.From + "' to '" + email.To + "' was send");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Email e1 = new Email() { From = "Me", To = "Vasya", Theme = "Who are you?" };
        Email e2 = new Email() { From = "Vasya", To = "Me", Theme = "vacuum cleaners!" };
        Email e3 = new Email() { From = "Kolya", To = "Vasya", Theme = "No! Thanks!" };
        Email e4 = new Email() { From = "Vasya", To = "Me", Theme = "washing machines!" };
        Email e5 = new Email() { From = "Me", To = "Vasya", Theme = "Yes" };
        Email e6 = new Email() { From = "Vasya", To = "Petya", Theme = "+2" };

        EmailSender es = new EmailSender();
        ILog log = new ConsoleLog();
        es.Send(e1, log);
        es.Send(e2, log);
        es.Send(e3, log);
        es.Send(e4, log);
        es.Send(e5, log);
        es.Send(e6, log);

        Console.ReadKey();
    }
}