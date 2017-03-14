using System;
using System.IO;

namespace JSONTokenizer {
   class Program {
      static void Main (string[] args) {
         args[0] = @"C:\mm\CS17\002\customerbenddeductions.deductions";
         var text = File.ReadAllText (args[0]);//.Replace ("\r\n", "\n");
         var tokenizer = new Tokenizer (text);
         for (;;) {
            var token = tokenizer.Next ();
            if (token == null) break;
            Console.WriteLine (token);
         }

         Console.WriteLine ();
         for (int i = 0; i < 72; i++) Console.Write ('-');
         Console.WriteLine (); Console.WriteLine ();

         foreach (var kvp in tokenizer.TokenCount) Console.WriteLine ($" {kvp.Key.ToString ().PadLeft (Token.StrLen)} | {kvp.Value}");
         Console.WriteLine ();
      }
   }
}
