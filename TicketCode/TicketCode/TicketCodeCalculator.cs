using System;
using TicketCode.Grammar;

namespace TicketCode {

	public class TicketCodeCalculator : TicketCodeBaseVisitor<long> {

		public override long VisitPattern(TicketCodeParser.PatternContext context) {
			long total = 1;
			foreach (var part in context.part()) {
				total *= Visit(part);
			}
			return total;
		}

		public override long VisitPart(TicketCodeParser.PartContext context) {
			if (context.TOKEN() != null) {
				var token = context.TOKEN().GetText();

				// ----------- Parsing -----------
				var type = string.Empty;
				var modifier = string.Empty;
				var count = 1;

				if (token.StartsWith("$")) {
					token = token.Substring(1);
				}

				var quantOpen = token.IndexOf('{');
				var quantClose = token.IndexOf('}');
				if (quantOpen > -1 && quantClose > quantOpen) {
					count = int.Parse(token.Substring(quantOpen + 1, quantClose - quantOpen - 1));
					token = token.Substring(0, quantOpen);
				}

				var modOpen = token.IndexOf('(');
				var modClose = token.IndexOf(')');
				if (modOpen > -1 && modClose > modOpen) {
					modifier = token.Substring(modOpen + 1, modClose - modOpen - 1);
					type = token.Substring(0, modOpen);
				}
				else {
					type = token;
				}

				// ---------- Dates ----------
				if (type == "dd") {
					return 1;
				}

				if (type == "mm") {
					return 1;
				}

				if (type == "yy") {
					return 1;
				}

				if (type == "yyyy") {
					return 1;
				}

				// ---------- Numbers ----------
				if (type == "n") {
					return (long)Math.Pow(10, count);
				}

				// ---------- Letters ----------
				if (type == "l") {
					var alphabetSize =
						modifier == "u" ? 26 :           // Só maiúsculas
						modifier == "l" ? 26 :           // Só minúsculas
						52;                              // Maiúsculas e minúsculas
					return Pow(alphabetSize, count);
				}

				// ---------- Alphanumeric ----------
				if (type == "a") {
					var alphaNumSize =
						modifier == "u" ? 36 :          // Maiúsculas + números
						modifier == "l" ? 36 :          // Minúsculas + números
						62;                             // Maiúsculas + minúsculas + números
					return Pow(alphaNumSize, count);
				}

				throw new NotSupportedException($"Unsupported token: ${type}{(modifier != "" ? "(" + modifier + ")" : "")}");
			}

			if (context.LITERAL() != null) {
				// Literal não afeta as combinações
				return 1;
			}

			throw new NotSupportedException($"Unknown part in pattern: \"{context.GetText()}\". Ensure the pattern contains only supported tokens and literals.");
		}

		private static long Pow(int value, int exponent) {
			long result = 1;
			for (var i = 0; i < exponent; i++) {
				result *= value;
			}
			return result;
		}
	}
}