using System;

/*Даний інтерфейс поганий тим, що він включає занадто багато методів.
 А що, якщо наш клас товарів не може мати знижок або промокодом, або для нього немає сенсу встановлювати матеріал з 
 якого зроблений (наприклад, для книг). Таким чином, щоб не реалізовувати в кожному класі невикористовувані в ньому методи, краще 
розбити інтерфейс на кілька дрібних і кожним конкретним класом реалізовувати потрібні інтерфейси.
Перепишіть, розбивши інтерфейс на декілька інтерфейсів, керуючись принципом розділення інтерфейсу. 
Опишіть класи книжки (розмір та колір не потрібні, але потрібна ціна та знижки) та верхній одяг (колір, розмір, ціна знижка),
які реалізують притаманні їм інтерфейси.
<Принцип розділення інтерфейсу>*/

interface IItem
{
    void SetPrice(double price);
}

interface IDiscountable
{
    void ApplyDiscount(String discount);
    void ApplyPromocode(String promocode);
}

interface ISizable
{
    void SetSize(byte size);
}

interface IColorable
{
    void SetColor(byte color);
}

class Book : IItem, IDiscountable
{
    public void SetPrice(double price) { }
    public void ApplyDiscount(String discount) { }
    public void ApplyPromocode(String promocode) { }
}

class Cloth : IItem, IDiscountable, ISizable, IColorable
{
    public void SetPrice(double price) { }
    public void ApplyDiscount(String discount) { }
    public void ApplyPromocode(String promocode) { }
    public void SetSize(byte size) { }
    public void SetColor(byte color) { }
}

class Program
{
    static void Main(string[] args)
    {
       
        Console.ReadKey();
    }
}