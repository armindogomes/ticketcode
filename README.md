# TicketCode

TicketCode is a very small .NET library for generating ticket codes from pattern-based expressions. Build codes for tickets, coupons, or IDs using a concise and developer-friendly DSL. Supports uppercase and lowercase letters, digits, and date tokens. Built on ANTLR.

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

TicketCode supports quantifiers, allowing you to specify how many times a given token should repeat. Quantifiers are expressed with curly braces: `{n}`.

**How quantifiers work:**

* Place `{n}` immediately after any token or token + modifier combination.
* The token will be repeated exactly `n` times in the generated code.
* Example: `$a{6}` generates 6 random alphanumeric characters.
* You can use quantifiers with any supported token:

  * `$l(u){4}` → 4 uppercase letters
  * `$n{3}` → 3 digits
  * `$a(l){8}` → 8 lowercase alphanumerics
* If a quantifier is omitted, the token is used only once (default is 1).

**Examples:**

* `$l(u){5}` → `QKMTN` (five uppercase letters)
* `$n{4}` → `2941` (four digits)
* `$a{8}` → `mY72tFaW` (eight alphanumeric characters)

You can freely combine quantifiers with other tokens, modifiers, and static text to form complex patterns.

## Requirements

* .NET Standard 2.0+
* [Antlr4.Runtime.Standard](https://www.nuget.org/packages/Antlr4.Runtime.Standard)

## License

MIT
