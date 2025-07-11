using System;
using Antlr4.Runtime;
using TicketCode.Grammar;

namespace TicketCode {

	public static class TicketCodeFactory {

		public static string Generate(string pattern) {
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

			var visitor = new TicketCode();
			return visitor.Visit(tree);
		}

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
	}
}