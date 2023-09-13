using Hydro.Tokenables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydro.Lexing
{
    public class Lexer
    {
        public List<Token> Lex(string script)
        {
            TokenManager tm = new TokenManager();
            List<Token> tokens = new List<Token>();

            int currentIndex = 0;

            while (currentIndex < script.Length)
            {
                // Find the longest matching token at the current index.
                Token longestMatch = tm.FindLongestMatch(script, currentIndex);

                if (longestMatch.Type == TokenType.Unknown)
                {
                    // Handle unrecognized token here (report an error or skip).
                    // For now, let's skip it.
                    currentIndex++;
                }
              else if (longestMatch.Type == TokenType.Whitespace)
              {
                //ignore
              }
                else
                {
                    tokens.Add(longestMatch);
                    currentIndex += longestMatch.Value.Length;
                }
            }

            return tokens;
        }
    }
}