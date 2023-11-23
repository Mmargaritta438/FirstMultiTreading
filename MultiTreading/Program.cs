using FirstMultiThreading;
using System.Diagnostics;

var timer = new Stopwatch();

var numbers = LargeArray.Initialize();

Console.WriteLine("Start calculating with 1 thread");
timer.Start();
var sum = numbers.Sum();
timer.Start();
Console.WriteLine($"Result = {sum}\nTime = {timer.ElapsedMilliseconds} ms");

timer.Reset();
Console.WriteLine("Start calcilating with 4 threads");

timer.Start();
var asyncSum = await numbers.SumAsync(2);
timer.Stop();

Console.WriteLine($"Sums equals ?  = {sum == asyncSum}");
Console.WriteLine($"Time = {timer.ElapsedMilliseconds} ms");

var predicate = new Predicate<int>(IsPerfectSquare);

await numbers.FindAsync(4, predicate);

static bool IsPrime(int number)
{
    if (number < 2)
        return false;

    for (int i = 2; i <= Math.Sqrt(number); i++)
    {
        if (number % i == 0)
            return false;
    }

    return true;
}

static bool IsPerfectSquare(int number) => Math.Sqrt(number) % 1 == 0;