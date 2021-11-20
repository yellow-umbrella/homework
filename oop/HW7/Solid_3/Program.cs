using System;

class Rectangle
{
    public virtual int Width { get; set; }
    public virtual int Height { get; set; }
    public int GetRectangleArea()
    {
        return Width * Height;
    }
}

//квадрат наслідується від прямокутника!!! <Принцип підстановки Лісков>
class Square
{
    public virtual int Side { get; set; }
    public int GetSquareArea()
    {
        return Side * Side;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Rectangle rect = new Rectangle();
        rect.Width = 5;
        rect.Height = 10;

        Square square = new Square();
        square.Side = 10;

        Console.WriteLine(rect.GetRectangleArea());
        Console.WriteLine(square.GetSquareArea());
        //Відповідь 100? Що не так???
        Console.ReadKey();
    }
}
