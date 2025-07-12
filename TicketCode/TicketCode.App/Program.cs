using TicketCode;

Console.WriteLine("Letters");
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