using TicketCode;

Console.WriteLine("\nLetters");
Console.WriteLine(TicketCodeFactory.Generate("$l{50}"));
Console.WriteLine(TicketCodeFactory.Generate("$l(u){50}"));
Console.WriteLine(TicketCodeFactory.Generate("$l(l){50}"));

Console.WriteLine("\nAlphanumeric");
Console.WriteLine(TicketCodeFactory.Generate("$a{50}"));
Console.WriteLine(TicketCodeFactory.Generate("$a(u){50}"));
Console.WriteLine(TicketCodeFactory.Generate("$a(l){50}"));

Console.WriteLine("\nNumbers");
Console.WriteLine(TicketCodeFactory.Generate("$n{50}"));

Console.WriteLine("\nRealistic example");
Console.WriteLine(TicketCodeFactory.Generate("TICKET-$yyyy-$mm-$l(u){10}"));
Console.WriteLine(TicketCodeFactory.Generate("CUPOM-$yyyy-$a(u){10}"));
Console.WriteLine(TicketCodeFactory.Generate("$yyyy$mm-$a(u){7}-SALES"));
Console.WriteLine(TicketCodeFactory.Generate("PROMO-$yyyy-$a(u){7}"));
Console.WriteLine(TicketCodeFactory.Generate("PROMO-$mm$yy-$a(u){7}"));

Console.WriteLine("\n\nCalculate");
Console.WriteLine(TicketCodeFactory.Calculate("$l{1}").ToString("N0"));
Console.WriteLine(TicketCodeFactory.Calculate("$a{1}").ToString("N0"));
Console.WriteLine(TicketCodeFactory.Calculate("$n{1}").ToString("N0"));

Console.WriteLine(TicketCodeFactory.Calculate("$l{10}").ToString("N0"));
Console.WriteLine(TicketCodeFactory.Calculate("$a{10}").ToString("N0"));
Console.WriteLine(TicketCodeFactory.Calculate("$n{10}").ToString("N0"));

Console.WriteLine(TicketCodeFactory.Calculate("TICKET").ToString("N0"));
Console.WriteLine(TicketCodeFactory.Calculate("TICKET-$n").ToString("N0"));
Console.WriteLine(TicketCodeFactory.Calculate("TICKET-$n{1}").ToString("N0"));
Console.WriteLine(TicketCodeFactory.Calculate("TICKET-$n{2}").ToString("N0"));
Console.WriteLine(TicketCodeFactory.Calculate("TICKET-$yyyy-$dd-$l(u){5}").ToString("N0"));
Console.WriteLine(TicketCodeFactory.Calculate("TICKET-$yyyy-$dd-$l(l){5}").ToString("N0"));
Console.WriteLine(TicketCodeFactory.Calculate("TICKET-$yyyy-$dd-$l{5}").ToString("N0"));
Console.WriteLine(TicketCodeFactory.Calculate("TICKET-$yyyy-$dd-$l(u){5}-$n{3}").ToString("N0"));