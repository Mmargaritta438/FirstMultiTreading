using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FirstMultiThreading
{
    public static class LargeArray
    {

        public static async Task<long> SumAsync(this int[] array, int overlapping)
        {
            var tasks = new Task<long>[overlapping];

            var block = (int)Math.Ceiling(array.Length / (double)overlapping);

            for (var i = 0; i < overlapping; i++)
            {
                var index = i;

                tasks[i] = Task.Run(() => {
                    var left = index * block;

                    var right = Math.Min((index + 1) * block, array.Length);

                    var span = array.AsSpan(left..right);

                    return SumInRange(span);
                });
            }

            var sums = await Task.WhenAll(tasks);

            return sums.Sum();
        }

        public static async Task FindAsync(this int[] array, int overlapping, Predicate<int> predicate)
        {
            var tasks = new Task[overlapping];

            var block = (int)Math.Ceiling(array.Length / (double)overlapping);

            for (var i = 0; i < overlapping; i++)
            {
                var index = i;

                tasks[i] = Task.Run(() => {
                    var left = index * block;

                    var right = Math.Min((index + 1) * block, array.Length);

                    var span = array.AsSpan(left..right);

                    FindInRange(span, predicate);
                });
            }

            await Task.WhenAll(tasks);
        }

        public static int[] Initialize()
        {
            var numbers = new int[10_000_000];

            var random = new Random();

            for (var i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(-10_000, 10_000);
            }

            return numbers;
        }

        private static long SumInRange(Span<int> span)
        {
            var sum = 0L;

            for (int i = 0; i < span.Length; i++)
            {
                sum += span[i];
            }

            return sum;
        }

        private static void FindInRange(Span<int> span, Predicate<int> predicate)
        {
            for (var i = 0; i < span.Length; i++)
            {
                if (predicate(span[i]))
                {
                    Console.WriteLine(span[i]);
                }
            }
        }
    }
}