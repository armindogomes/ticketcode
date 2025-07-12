namespace TicketCode.Test;

public class TicketCodeCalculatorTest {

	[Theory]
	[InlineData("$l{1}", 52)]
	[InlineData("$a{1}", 62)]
	[InlineData("$n{1}", 10)]
	[InlineData("$l{10}", 144555105949057024L)]
	[InlineData("$a{10}", 839299365868340224L)]
	[InlineData("$n{10}", 10000000000L)]
	[InlineData("TICKET", 1)]
	[InlineData("TICKET-$n", 10)]
	[InlineData("TICKET-$n{1}", 10)]
	[InlineData("TICKET-$n{2}", 100)]
	[InlineData("TICKET-$yyyy-$dd-$l(u){5}", 11881376L)]
	[InlineData("TICKET-$yyyy-$dd-$l(l){5}", 11881376L)]
	[InlineData("TICKET-$yyyy-$dd-$l{5}", 380204032L)]
	[InlineData("TICKET-$yyyy-$dd-$l(u){5}-$n{3}", 11881376000L)]
	public void Calculate_ShouldReturnExpectedResult(string pattern, long expected) {
		var result = TicketCodeFactory.Calculate(pattern);
		Assert.Equal(expected, result);
	}
}
