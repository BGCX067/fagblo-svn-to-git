using System;

namespace Fagblo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Fagblo game = new Fagblo())
            {
                game.Run();
            }
        }
    }
}

