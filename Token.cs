﻿using System;
using System.Collections.Generic;

namespace JSONTokenizer {
   public class Token {
      public Token (E kind, object value) { Kind = kind; Value = value; }

      public enum E {
         Punctuation,
         String,
         Number,
         Keyword,
         Invalid,
      }

      public override string ToString () => $"{Kind.ToString ().PadRight (StrLen)}: {Value}";

      public static readonly int StrLen = E.Punctuation.ToString ().Length;

      public readonly E Kind;
      public readonly object Value;
   }

   public static class Extensions {
      public static void ForEach<T>(this IEnumerable<T> coll, Action<T> act) {
         foreach (var e in coll) act (e);
      }
   }
}
