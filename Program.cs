using System;
using Hydro.Tokenables;
using Hydro.Lexing;

namespace Hydro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Lexer lexer = new Lexer();
            string inputScript = "int main() { string thing = \"what\"; return 0; }";
            List<Token> tokens = lexer.Lex(inputScript); // Change Token[] to List<Token>

            // Print tokens
            foreach (Token token in tokens)
            {
                Console.WriteLine($"Token Value: {token.Value}, Token Type: {token.Type}");
            }
        }
    }
}
