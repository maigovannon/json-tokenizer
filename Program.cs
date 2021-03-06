﻿using System;
using System.IO;

namespace JSONTokenizer {
   class Program {
      static void Main (string[] args) {
         if (args.Length < 1 || !File.Exists (args[0])) {
            Console.WriteLine ("File does not exist/not provided");
            return;
         }
         var text = File.ReadAllText (args[0]);//.Replace ("\r\n", "\n");
         var tokenizer = new Tokenizer (text);
         for (;;) {
            var token = tokenizer.Next ();
            if (token == null) break;
            Console.WriteLine (token);
         }

         // Output summary
         Console.WriteLine ();
         for (int i = 0; i < 72; i++) Console.Write ('-');
         Console.WriteLine (); Console.WriteLine ();

         foreach (var kvp in tokenizer.TokenCount) Console.WriteLine ($" {kvp.Key.ToString ().PadLeft (Token.StrMaxLen)} | {kvp.Value}");
         Console.WriteLine ();
      }
   }
}
