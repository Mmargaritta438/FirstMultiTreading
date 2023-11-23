namespace MyFirstMutiTreading
{
    class Program
    {
        static void Main(string[] args) 
        {
            // Threading = An execution path of program
            //             We can use multiple threads to perform,
            //             Different tasks of our program at the same time.
            //             Current thead runnning is "main" thead
            //             using System.Theading;

            Thread mainThread = Thread.CurrentThread;
            mainThread.Name = "Main Thread";
            //Console.WriteLine(mainThread.Name);

            Thread threadone = new Thread(() => CountDown("Timer #one"));
            Thread threadtwo = new Thread(() => CountUp("Timer #two"));

            threadone.Start();
            threadtwo.Start();

            //CountDown();
            //CountUp();

            Console.WriteLine(mainThread.Name + " is complete!");

            Console.ReadKey();
        }

        private static void CountUp()
        {
            return;
        }

        private static void CountDown()
        {
            return;
        }

        public static void CountDown(String name)
        {
            for (int i = 10000; i >= 0; i--)
            {
                Console.WriteLine("Timer #1 : " + i + " seconds");
                Thread.Sleep(1);
            }
            Console.WriteLine("Timer #1 is complete!");
        }
        public static void CountUp(String name)
        {
            for (int i = 0; i <= 10000; i++)
            {
                Console.WriteLine("Timer #2 : " + i + " seconds");
                Thread.Sleep(1);
            }
            Console.WriteLine("Timer #2 is complete!");
        }
    }
}