using System;
using System.Collections.Generic;
using System.IO;

namespace CalculatorApp
{
    public class FileUtils
    {
        public Stack<byte> ReadFromFile(string fileName, out char sign)
        {
            Stack<byte> result = new Stack<byte>();
            char currentChar;
            byte currentByte;
            sign = '+';

            using (StreamReader reader = new StreamReader(fileName))
            {
                while (reader.Peek() >= 0)
                {
                    currentChar = (char)reader.Read();

                    if (!char.IsDigit(currentChar) && 
                        !currentChar.Equals('+') && 
                        !currentChar.Equals('-'))
                    {
                        throw new Exception(string.Format("File {0} does not contain a valid number.", 
                                                          fileName));
                    }

                    if (currentChar.Equals('+') || currentChar.Equals('-'))
                    {
                        sign = currentChar;
                        continue;
                    }

                    if (!byte.TryParse(currentChar.ToString(), out currentByte))
                    {
                        throw new Exception(string.Format("Error when reading from file {0}.",
                                                          fileName));
                    }

                    result.Push(currentByte);
                }
            }

            return result;
        }
    }
}
