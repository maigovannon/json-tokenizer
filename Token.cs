using System;
using System.Collections.Generic;

namespace JSONTokenizer {

   public class Token {
      public enum E {
         Punctuation,
         String,
         Number,
         Keyword,
         Invalid,
      }
      public Token(E kind, object value) { Kind = kind;  Value = value; }

      public override string ToString () {
         return $"{Kind.ToString ().PadRight (StrLen)}: {Value}";
      }

      public static readonly int StrLen = E.Punctuation.ToString ().Length;

      public E Kind;
      public object Value;
   }

   public static class Extensions {
      public static void ForEach<T>(this IEnumerable<T> coll, Action<T> act) {
         foreach (var e in coll) act (e);
      }
   }
}
