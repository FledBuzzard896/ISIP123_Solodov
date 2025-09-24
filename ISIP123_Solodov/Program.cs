//Необходимо реализовать класс с перегрузкой методов Area для расчёта площади и Perimeter для расчёта периметра. 
//Расчёты должны проводится для следующих фигур:
//Квадрат
//Прямоугольник
//Треугольник (формула Герона)
//Круг
//Многоугольник по вершинам (площадь по «формуле Гаусса», периметр — сумма рёбер)
double s1 = 6;
double s2 = 7;
double s3 = 8;
double r = 4;

Figure figure = new Figure();
Console.WriteLine(figure.Area(s1));
Console.WriteLine(figure.Perimeter(s1));
Console.WriteLine(figure.Area(s1, s2));
Console.WriteLine(figure.Perimeter(s1, s2));
class Figure
{
    public double side1;
    public double side2;
    public double side3;
    public double radius;

    public Figure(double side1 = 0, double side2 = 0, double side3 = 0, double radius = 0)
    {
        this.side1 = side1;
        this.side2 = side2;
        this.side3 = side3;
        this.radius = radius;
    }

    // Square
    public double Area(double side1) { return side1 * side1; }
    public double Perimeter(double side1) { return side1 * 4; }

    // Rectangle
    public double Area(double side1, double side2) { return side1 * side2; }
    public double Perimeter(double side1, double side2) { return (side1 + side2) * 2; }

    // Triangle
    public double Area(double side1, double side2, double side3)
    {

        double p = Perimeter(side1, side2, side3) / 2;
        return Math.Sqrt(p * (p - side1) * (p - side2) * (p - side3));
    }
    public double Perimeter(double side1, double side2, double side3) { return side1 + side2 + side3; }

    // Round
    public double Area(double radius, bool isRound)
    {

        if (isRound)
        {
            return Math.PI * radius * radius;
        }
        return Area(radius);
    }
    public double Perimeter(double radius, bool isRound)
    {

        if (isRound)
        {
            return Math.PI * radius * 2;
        }
        return Perimeter(radius);
    }

    // Что-то
    public double Area(params string[] coordinate)
    {

        double[] many_X = new double[coordinate.Length / 2];
        double[] many_Y = new double[coordinate.Length / 2];
        double fParam = 0, sParam = 0;


        foreach (string str in coordinate)
        {

            string[] tempMassive = str.Split(' ');
            many_X.Append(Convert.ToDouble(tempMassive[0]));
            many_Y.Append(Convert.ToDouble(tempMassive[1]));
        }

        for (int i = 0; i < many_X.Length; i++)
        {
            if (i == many_X.Length - 1)
            {
                fParam += many_X[i] * many_Y[0];
                sParam += many_Y[i] * many_X[0];
            }
            else
            {
                fParam += many_X[i] * many_Y[i + 1];
                sParam += many_Y[i] * many_X[i + 1];
            }
        }
        return 0.5 * Math.Abs(fParam - sParam);
    }
}
