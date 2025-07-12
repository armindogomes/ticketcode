namespace TicketCode.Test;

public class TicketCodeFactoryTest {

	[Fact]
	public void Letters_MixedCase_Generates50Letters() {
		var code = TicketCodeFactory.Generate("$l{50}");
		Assert.Equal(50, code.Length);
		Assert.Matches("^[A-Za-z]{50}$", code);
	}

	[Fact]
	public void Letters_UpperCase_Generates50UppercaseLetters() {
		var code = TicketCodeFactory.Generate("$l(u){50}");
		Assert.Equal(50, code.Length);
		Assert.Matches("^[A-Z]{50}$", code);
	}

	[Fact]
	public void Letters_LowerCase_Generates50LowercaseLetters() {
		var code = TicketCodeFactory.Generate("$l(l){50}");
		Assert.Equal(50, code.Length);
		Assert.Matches("^[a-z]{50}$", code);
	}

	[Fact]
	public void Alphanumeric_MixedCase_Generates50AlphaNumeric() {
		var code = TicketCodeFactory.Generate("$a{50}");
		Assert.Equal(50, code.Length);
		Assert.Matches("^[A-Za-z0-9]{50}$", code);
	}

	[Fact]
	public void Alphanumeric_UpperCase_Generates50AlphaNumericUpper() {
		var code = TicketCodeFactory.Generate("$a(u){50}");
		Assert.Equal(50, code.Length);
		Assert.Matches("^[A-Z0-9]{50}$", code);
	}

	[Fact]
	public void Alphanumeric_LowerCase_Generates50AlphaNumericLower() {
		var code = TicketCodeFactory.Generate("$a(l){50}");
		Assert.Equal(50, code.Length);
		Assert.Matches("^[a-z0-9]{50}$", code);
	}

	[Fact]
	public void Numbers_Generates50Digits() {
		var code = TicketCodeFactory.Generate("$n{50}");
		Assert.Equal(50, code.Length);
		Assert.Matches("^[0-9]{50}$", code);
	}

	[Fact]
	public void TicketCode_TicketPattern_IsFormattedCorrectly() {
		var now = DateTime.Now;
		var code = TicketCodeFactory.Generate("TICKET-$yyyy-$mm-$l(u){10}");

		Assert.StartsWith($"TICKET-{now:yyyy}-{now:MM}-", code);
		Assert.Equal(25, code.Length);
		Assert.Matches(@"^TICKET-\d{4}-\d{2}-[A-Z]{10}$", code);
	}

	[Fact]
	public void TicketCode_CupomPattern_IsFormattedCorrectly() {
		var now = DateTime.Now;
		var code = TicketCodeFactory.Generate("CUPOM-$yyyy-$a(u){10}");

		Assert.StartsWith($"CUPOM-{now:yyyy}-", code);
		Assert.Equal(21, code.Length);
		Assert.Matches(@"^CUPOM-\d{4}-[A-Z0-9]{10}$", code);
	}

	[Fact]
	public void TicketCode_AlphaSalesPattern_IsFormattedCorrectly() {
		var now = DateTime.Now;
		var code = TicketCodeFactory.Generate("$yyyy$mm-$a(u){7}-SALES");

		Assert.StartsWith($"{now:yyyyMM}-", code);
		Assert.EndsWith("-SALES", code);
		Assert.Equal(20, code.Length);
		Assert.Matches(@"^\d{6}-[A-Z0-9]{7}-SALES$", code);
	}

	[Fact]
	public void TicketCode_PromoMonthYearPattern_IsFormattedCorrectly() {
		var now = DateTime.Now;
		var code = TicketCodeFactory.Generate("PROMO-$mm$yy-$a(u){7}");

		Assert.StartsWith($"PROMO-{now:MM}{now:yy}-", code);
		Assert.Equal(18, code.Length);
		Assert.Matches(@"^PROMO-\d{4}-[A-Z0-9]{7}$", code);
	}

	[Fact]
	public void TicketCode_PromoYearPattern_IsFormattedCorrectly() {
		var now = DateTime.Now;
		var code = TicketCodeFactory.Generate("PROMO-$yyyy-$a(u){7}");

		Assert.StartsWith($"PROMO-{now:yyyy}-", code);
		Assert.Equal(18, code.Length);
		Assert.Matches(@"^PROMO-\d{4}-[A-Z0-9]{7}$", code);
	}
}