using System;
using Antlr4.Runtime;
using TicketCode.Grammar;

namespace TicketCode {

	public static class TicketCodeFactory {

		public static string Generate(string pattern) => VisitPattern(pattern, new TicketCode());

		public static bool TryGenerate(string pattern, out string result) {
			result = null;
			try {
				result = Generate(pattern);
				return true;
			}
			catch {
				result = null;
				return false;
			}
		}

		public static long Calculate(string pattern) => VisitPattern(pattern, new TicketCodeCalculator());

		public static bool TryCalculate(string pattern, out long result) {
			result = 0;
			try {
				result = Calculate(pattern);
				return true;
			}
			catch {
				result = 0;
				return false;
			}
		}

		private static T VisitPattern<T>(string pattern, TicketCodeBaseVisitor<T> visitor) {
			if (pattern == null) {
				throw new ArgumentNullException(nameof(pattern), "Pattern cannot be null");
			}

			if (string.IsNullOrWhiteSpace(pattern)) {
				throw new ArgumentException("Pattern cannot be empty.", nameof(pattern));
			}

			var inputStream = new AntlrInputStream(pattern);
			var lexer = new TicketCodeLexer(inputStream);
			var tokens = new CommonTokenStream(lexer);
			var parser = new TicketCodeParser(tokens);

			var tree = parser.pattern();
			return visitor.Visit(tree);
		}
	}
}