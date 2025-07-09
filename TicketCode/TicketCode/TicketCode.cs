using System;
using System.Linq;
using System.Text;
using TicketCode.Grammar;

namespace TicketCode {

	public class TicketCode : TicketCodeBaseVisitor<string> {

		private readonly Random _rnd = new Random();

		public override string VisitPattern(TicketCodeParser.PatternContext context) {
			var sb = new StringBuilder();
			foreach (var part in context.part()) {
				sb.Append(Visit(part));
			}
			return sb.ToString();
		}

		public override string VisitPart(TicketCodeParser.PartContext context) {
			if (context.TOKEN() != null) {
				var token = context.TOKEN().GetText();

				// ----------- Parsing -----------
				// Example: $l(u){10}
				var type = string.Empty;     // "l", "a", "n"
				var modifier = string.Empty; // "", "u", "l"
				var count = 1;

				// Remove leading '$'
				if (token.StartsWith("$")) {
					token = token.Substring(1);
				}

				// Extract quantifier (e.g., {10})
				int quantOpen = token.IndexOf('{');
				int quantClose = token.IndexOf('}');
				if (quantOpen > -1 && quantClose > quantOpen) {
					count = int.Parse(token.Substring(quantOpen + 1, quantClose - quantOpen - 1));
					token = token.Substring(0, quantOpen);
				}

				// Extract modifier (e.g., (u) or (l))
				int modOpen = token.IndexOf('(');
				int modClose = token.IndexOf(')');
				if (modOpen > -1 && modClose > modOpen) {
					modifier = token.Substring(modOpen + 1, modClose - modOpen - 1);
					type = token.Substring(0, modOpen);
				}
				else {
					type = token;
				}

				// Only "", "u", "l" are supported as modifiers
				if (!string.IsNullOrEmpty(modifier) && modifier != "u" && modifier != "l") {
					Console.WriteLine($"Unsupported modifier: \"{modifier}\". Use only (u) for uppercase or (l) for lowercase.");
					return "";
				}

				// ---------- Dates ----------
				if (type == "dd") {
					return DateTime.Now.Day.ToString("D2");
				}
				if (type == "mm") {
					return DateTime.Now.Month.ToString("D2");
				}
				if (type == "yy") {
					return (DateTime.Now.Year % 100).ToString("D2");
				}
				if (type == "yyyy") {
					return DateTime.Now.Year.ToString("D4");
				}

				// ---------- Numbers ----------
				if (type == "n") {
					return new string(Enumerable.Range(0, count)
						.Select(_ => (char)(_rnd.Next(0, 10) + '0')).ToArray());
				}

				// ---------- Letters ----------
				if (type == "l") {
					// Lowercase only
					if (modifier == "l") {
						return new string(Enumerable.Range(0, count)
							.Select(_ => (char)_rnd.Next('a', 'z' + 1)).ToArray());
					}
					// Uppercase only
					if (modifier == "u") {
						return new string(Enumerable.Range(0, count)
							.Select(_ => (char)_rnd.Next('A', 'Z' + 1)).ToArray());
					}
					// Mixed (no modifier)
					return new string(Enumerable.Range(0, count)
						.Select(_ => _rnd.Next(2) == 0
							? (char)_rnd.Next('A', 'Z' + 1)
							: (char)_rnd.Next('a', 'z' + 1)
						).ToArray());
				}

				// ---------- Alphanumeric ----------
				if (type == "a") {
					// Uppercase + numbers
					if (modifier == "u") {
						return new string(Enumerable.Range(0, count)
							.Select(_ => {
								int pick = _rnd.Next(2);
								if (pick == 0) {
									return (char)_rnd.Next('A', 'Z' + 1);
								}
								else {
									return (char)_rnd.Next('0', '9' + 1);
								}
							}).ToArray());
					}
					// Lowercase + numbers
					if (modifier == "l") {
						return new string(Enumerable.Range(0, count)
							.Select(_ => {
								int pick = _rnd.Next(2);
								if (pick == 0) {
									return (char)_rnd.Next('a', 'z' + 1);
								}
								else {
									return (char)_rnd.Next('0', '9' + 1);
								}
							}).ToArray());
					}
					// Mixed (no modifier): uppercase, lowercase, numbers
					return new string(Enumerable.Range(0, count)
						.Select(_ => {
							int pick = _rnd.Next(3);
							if (pick == 0) {
								return (char)_rnd.Next('a', 'z' + 1);
							}
							if (pick == 1) {
								return (char)_rnd.Next('A', 'Z' + 1);
							}
							return (char)_rnd.Next('0', '9' + 1);
						}).ToArray());
				}

				throw new NotSupportedException($"Unsupported token: ${type}{(modifier != "" ? "(" + modifier + ")" : "")}");
			}

			if (context.LITERAL() != null) {
				return context.LITERAL().GetText();
			}

			throw new NotSupportedException($"Unknown part in pattern: \"{context.GetText()}\". Ensure the pattern contains only supported tokens and literals.");
		}
	}
}