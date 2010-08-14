using System;

namespace ProgramingGameXNA
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ProgramingGame game = new ProgramingGame())
            {
                game.Run();
            }
        }
    }
}

