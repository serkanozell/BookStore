using BookStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class DatabaseLogger : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("[Database Logger] = " + message);
        }
    }
}
