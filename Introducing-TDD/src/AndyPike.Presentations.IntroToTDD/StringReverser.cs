using System;
using System.Text;

namespace AndyPike.Presentations.IntroToTDD
{
    public class StringReverser : IReverser<string>
    {
        private readonly ILogger logger;

        public StringReverser(ILogger logger)
        {
            this.logger = logger;
        }

        public string Reverse(string input)
        {
            if (input == null) throw new ArgumentNullException("input");

            var output = new StringBuilder();

            for(int x = input.Length - 1; x >= 0; x--)
            {
                output.Append(input[x]);
            }

            logger.Info("'" + input + "' was reversed.");

            return output.ToString();
        }
    }
}