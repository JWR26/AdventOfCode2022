using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExtensions // must have "using MyExtensions" to grant access to the extension methods.
{
    static class MyExtensions // Note to self: Extension methods must be in a non-generic static class!
    {
        // Extention method for the String class - returns the reverse of a string.
        public static string ReverseString(this string word) // Note to self: Methods must be static and append the type with "this "
        {
            char[] letters = word.ToCharArray();
            Array.Reverse(letters);
            return new string(letters);
        }
        // performs a mathematical operation for a given string
        public static ulong Operate(this string operation, ulong oldValue)
        {
            // operation takes the form "oldValue" $ "value", where $ is the operator, returning "value"
            // split the string first
            string[] parts = operation.Split(' ');
            ulong value = (parts[2] == "old") ? oldValue : ulong.Parse(parts[2]);

            switch (parts[1])
            {
                case "+":
                    return oldValue + value;
                case "-":
                    return oldValue - value;
                case "*":
                    return oldValue * value;
                case "/":
                    return oldValue / oldValue;
                default:
                    return oldValue;
            }
        }
    }
}