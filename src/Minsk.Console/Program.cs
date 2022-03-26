namespace Minsk.Console
{
    // https://www.youtube.com/watch?v=psTZi6xpTlM&feature=youtu.be&list=PLRAdsfhKI4OWNOSfS7EUu5GRAVmze1t2y&t=1822
    internal static class Program
    {
        private static void Main()
        {
            var repl = new MinskRepl();
            repl.Run();
        }
    }
}
