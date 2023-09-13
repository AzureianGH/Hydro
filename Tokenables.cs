using System;
using System.Collections.Generic;
using System.Linq;

namespace Hydro.Tokenables
{
    public class TokenManager
    {
        private Dictionary<string, TokenType> tokenDictionary;

        public TokenManager()
        {
            InitializeTokenDictionary();
        }
        public string[] block =
            {
                "{",
                "}"
            };
        public string[] parenthesis =
        {
                "(",
                ")"
            };
        public string[] bracket =
        {
                "[",
                "]"
            };
        // Operators
        public string[] operators =
        {
                "+",
                "-",
                "*",
                "/",
                "%",
                "=",
                "+=",
                "-=",
                "*=",
                "/=",
                "%=",
                "++",
                "--",
                "==",
                "!=",
                ">",
                "<",
                ">=",
                "<=",
                "&&",
                "||",
                "!",
                "&",
                "|",
                "^",
                "~",
                "<<",
                ">>",
                ">>=",
                "<<=",
                "&=",
                "|=",
                ";",
                ":",
                "."
                // Add more operators as needed
            };
        public string[] keywords =
            {
                "ostream",
                "istream",
                "return",
                "true",
                "false"
                // Add more keywords as needed
            };
        public string[] Type =
        {
                "int",
                "float",
                "double",
                "string",
                "char",
                "bool",
                "class"
            };
        public string[] StringLiteral =
        {
                "\"",
                "\'"
            };
        private void InitializeTokenDictionary()
        {
            tokenDictionary = new Dictionary<string, TokenType>();

            // Keywords
            
            foreach (var type in Type)
            {
                tokenDictionary[type] = TokenType.Type;
            }
            foreach (var keyword in keywords)
            {
                tokenDictionary[keyword] = TokenType.Keyword;
            }
            foreach (var str in StringLiteral)
            {
                tokenDictionary[str] = TokenType.StringLiteral;
            }

            foreach (var op in operators)
            {
                tokenDictionary[op] = TokenType.Operator;
            }
            foreach (var bl in block)
            {
                tokenDictionary[bl] = TokenType.Block;
            }
            foreach (var pr in parenthesis)
            {
                tokenDictionary[pr] = TokenType.Parenthesis;
            }
            foreach (var br in bracket)
            {
                tokenDictionary[br] = TokenType.Bracket;
            }

            // Literals (for simplicity, treating all numbers as literals)
            tokenDictionary["0"] = TokenType.Literal;
            tokenDictionary["1"] = TokenType.Literal;
            tokenDictionary["2"] = TokenType.Literal;
            tokenDictionary["3"] = TokenType.Literal;
            tokenDictionary["4"] = TokenType.Literal;
            tokenDictionary["5"] = TokenType.Literal;
            tokenDictionary["6"] = TokenType.Literal;
            tokenDictionary["7"] = TokenType.Literal;
            tokenDictionary["8"] = TokenType.Literal;
            tokenDictionary["9"] = TokenType.Literal;

            // Whitespace
            string[] whitespace = { " ", "\t" };
            foreach (var ws in whitespace)
            {
                tokenDictionary[ws] = TokenType.Whitespace;
            }

            // Newline
            string[] newline = { "\n", "\r" };
            foreach (var nl in newline)
            {
                tokenDictionary[nl] = TokenType.Newline;
            }

            // Comment
            string[] comment = { "//", "/*", "*/" };
            foreach (var cm in comment)
            {
                tokenDictionary[cm] = TokenType.Comment;
            }
        }

        public Token FindLongestMatch(string script, int startIndex)
        {
            foreach (var tokenType in tokenDictionary.Keys.OrderByDescending(k => k.Length))
            {
                if (script.Substring(startIndex).StartsWith(tokenType))
                {
                    foreach (string toktp in Type)
                    {
                        if (tokenType == toktp)
                        {
                            int endIndex = startIndex + tokenType.Length;
                            if (endIndex < script.Length)
                            {
                                if (StringLiteral.Contains(tokenType) && script[endIndex] == tokenType[0])
                                {
                                    // It's a string literal
                                    int nextIndex = endIndex + 1;
                                    while (nextIndex < script.Length && script[nextIndex] != tokenType[0])
                                    {
                                        nextIndex++;
                                    }

                                    if (nextIndex < script.Length && script[nextIndex] == tokenType[0])
                                    {
                                        // Include the closing quote in the string literal
                                        nextIndex++;
                                    }

                                    return new Token(script.Substring(startIndex, nextIndex - startIndex), TokenType.StringLiteral);
                                }
                                else if (script[endIndex] == ' ')
                                {
                                    int nextIndex = endIndex + 1;
                                    // Check if it's followed by an identifier (function name)
                                    while (nextIndex < script.Length && (Char.IsLetterOrDigit(script[nextIndex]) || script[nextIndex] == '_'))
                                    {
                                        nextIndex++;
                                    }
                                    if (nextIndex < script.Length && script[nextIndex] == '(')
                                    {
                                        // It's a function declaration
                                        return new Token(script.Substring(startIndex, nextIndex - startIndex), TokenType.Function);
                                    }
                                }
                            }
                        }
                    }
                    return new Token(tokenType, tokenDictionary[tokenType]);
                }
            }

            return new Token("null", TokenType.Unknown);
        }
    }

    public struct Token
    {
        public string Value;
        public TokenType Type;

        public Token(string value, TokenType type)
        {
            Value = value;
            Type = type;
        }
    }

    public enum TokenType
    {
        Identifier,
        Keyword,
        Operator,
        Literal,
        Comment,
        Whitespace,
        Newline,
        EndOfStream,
        Unknown,
        Block,
        Parenthesis,
        Bracket,
        Type,
        Function,
        Class,
        Object,
        StringLiteral
    }
}
