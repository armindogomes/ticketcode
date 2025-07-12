# TicketCode

**Still in development**

TicketCode is a simple .NET library meant to help generate codes for tickets, coupons, or IDs based on pattern expressions. This project is not meant to be revolutionary or ambitious — just a small tool to make life a bit easier if you need to generate codes in a flexible way using an easy DSL. The project is evolving and may change a lot over time.

## Features

* Simple pattern-based syntax (DSL)
* Supports: uppercase letters (`$l(u)`), lowercase letters (`$l(l)`), mixed-case letters (`$l`), digits (`$n`), alphanumerics (`$a`), day (`$dd`), month (`$mm`), year (`$yy`, `$yyyy`)
* Modifiers: `(u)` for uppercase, `(l)` for lowercase (e.g., `$l(u){4}`)
* Quantifiers: `{n}` for repeating n times (e.g., `$a{8}`)
* Extensible: add new tokens easily
* .NET Standard 2.0 compatible
* 100% open source

## Pattern Syntax Examples

* `$l(u){3}$n{4}-$yy` → `XJQ4821-24`
* `$l(l){2}$l(u){2}$n{2}` → `abCD71`
* `$yyyy$mm$dd-$l(u){5}` → `20250709-WVUKR`
* `TCK-$l(u){2}$n{3}` → `TCK-GM572`
* `$a{8}` → `B2t7y3Qk`
* `$a(u){6}` → `JSNQKD`
* `$a(l){6}` → `mnxwqe`

## Usage

```csharp
using TicketCode;

Console.WriteLine(TicketCodeFactory.Generate("TICKET-$yyyy-$mm-$l(u){10}"));
Console.WriteLine(TicketCodeFactory.Generate("CUPOM-$yyyy-$a(u){10}"));
Console.WriteLine(TicketCodeFactory.Generate("$yyyy$mm-$a(u){7}-SALES"));
Console.WriteLine(TicketCodeFactory.Generate("PROMO-$yyyy-$a(u){7}"));
Console.WriteLine(TicketCodeFactory.Generate("PROMO-$mm$yy-$a(u){7}"));

//TICKET-2025-07-GXXDKWXAZZ
//CUPOM-2025-JS6B7A4984
//202507-6O22IP9-SALES
//PROMO-2025-W1XGC1K
//PROMO-0725-6715595
```

## Supported Tokens

| Token   | Description                          |
| ------- | ------------------------------------ |
| `$l`    | Letter (A–Z, a–z)                    |
| `$l(u)` | Uppercase letter (A–Z)               |
| `$l(l)` | Lowercase letter (a–z)               |
| `$a`    | Letter or digit (A–Z, a–z, 0–9)      |
| `$a(u)` | Uppercase letter or digit (A–Z, 0–9) |
| `$a(l)` | Lowercase letter or digit (a–z, 0–9) |
| `$n`    | Digit (0–9)                          |
| `$dd`   | Day (two digits)                     |
| `$mm`   | Month (two digits)                   |
| `$yy`   | Year (last two digits)               |
| `$yyyy` | Year (four digits)                   |

## Quantifiers

TicketCode lets you specify how many times each token is repeated, using `{n}` after the token.

**Examples:**

* `$l(u){5}` → `QKMTN` (five uppercase letters)
* `$n{4}` → `2941` (four digits)
* `$a{8}` → `mY72tFaW` (eight alphanumeric characters)

You can freely combine quantifiers with other tokens, modifiers, and static text to create any patterns you need.

## Requirements

* .NET Standard 2.0+
* [Antlr4.Runtime.Standard](https://www.nuget.org/packages/Antlr4.Runtime.Standard)

## License

MIT
