using yod.Phonology;

namespace yodTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var b = true;
            if (args.Length > 0 && args[0] == "--all") b = false;
            var tests = new Tests(b);
            tests.Run();
        }
    }
}
