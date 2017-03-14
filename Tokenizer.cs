using System;
using System.Collections.Generic;
using System.Linq;
using E = JSONTokenizer.Token.E;

namespace JSONTokenizer {

   public class Tokenizer {
      public Tokenizer (string text) {
         mText = " " + text;
         mIdx = 0;

         mDict = new Dictionary<E, int> ();
         (Enum.GetValues (typeof (E)) as E[]).ForEach (e => mDict.Add (e, 0));
      }
      
      /// <summary>Get the next token</summary>
      public Token Next () {
         if (++mIdx >= mText.Length) return null;
         var token = GetToken (); mDict[token.Kind]++;
         return token.Kind == E.Invalid ? Next () : token;
      }

      /// <summary>Helper method to get the next token</summary>
      Token GetToken () {
         while (Skip.Any (s => s == mText[mIdx])) {
            if (++mIdx >= mText.Length) return null;
         }

         // Punc
         if (Punctuations.Any (p => p == mText[mIdx])) return new Token (E.Punctuation, $"'{mText[mIdx]}'");

         // String
         var str = GetString (); if (str != null) return new Token (E.String, $"\"{str}\"");

         // Number 
         var num = GetNumberString (); if (num != null) return new Token (E.Number, num);

         // Keyword
         int end = mText.IndexOfAny (Delims, mIdx); if (end == -1) return null;
         str = mText.Substring (mIdx, end - mIdx); mIdx = end - 1;
         if (Keywords.Any (k => k == str)) return new Token (E.Keyword, str);

         // This token does not satisfy any of the above criteria
         InvalidToken (str); return new Token (E.Invalid, str);
      }

      void InvalidToken (string err) {
         Console.ForegroundColor = ConsoleColor.Red;
         Console.WriteLine ($"Invalid token found:{err}");
         Console.ForegroundColor = ConsoleColor.Gray;
      }

      string GetString () {
         if (mText[mIdx] != '"' || mText[mIdx - 1] == '\\') return null;

         int c = mIdx;
         int st = ++mIdx;
         // Find the closing quote
         while (true) {
            while (mIdx < mText.Length && mText[mIdx] != '"') ++mIdx;
            if (mText.Length <= mIdx) break;

            bool escaped = false;
            for (int j = mIdx - 1; j > c && mText[j] == '\\'; j++) escaped = !escaped;

            if (!escaped) break; // This is not an escaped '"'. End the loop
            c = mIdx++;
         }
         return mText.Substring (st, mIdx - st);
      }

      string GetNumberString () {
         var i = mText.IndexOfAny (Delims, mIdx); if (i == -1) return null;
         var s = mText.Substring (mIdx, i - mIdx);
         double v;
         if (double.TryParse (s, out v)) { mIdx = i - 1; return s; }
         return null;
      }

      public Dictionary<E, int> TokenCount => mDict;
      Dictionary<E, int> mDict;

      static readonly string[] Keywords = { "true", "false", "null" };
      static readonly char[] Punctuations = { '{', '}', '[', ']', ':', ',' };
      static readonly char[] Skip = { '\r', '\n', ' ', '\t' };
      static readonly char[] Delims = Punctuations.Concat (Skip).ToArray ();

      string mText;
      int mIdx;
   }
}
