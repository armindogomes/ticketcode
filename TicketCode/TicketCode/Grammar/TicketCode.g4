grammar TicketCode;

// A pattern is made of one or more parts and ends at EOF
pattern     : part+ EOF ;

// Each part is either a special token or a literal
part        : TOKEN | LITERAL ;

// Token supports letters, alphanumeric, numbers, and dates with optional modifiers and quantifiers
TOKEN       : '$' (
                'n'                             // number
              | 'l' (MODIFIER)?                 // letter with optional modifier
              | 'a' (MODIFIER)?                 // alphanumeric with optional modifier
              | 'mm' | 'dd' | 'yy' | 'yyyy'     // dates
            ) (QUANT)? ;

// Modifiers for case: (u) for uppercase, (l) for lowercase
MODIFIER    : '(' ( 'u' | 'l' ) ')' ;

// Literal: any sequence that does not contain $
LITERAL     : ~[$]+ ;

// Quantifier: a number between braces, like {10}
QUANT       : '{' DIGITS '}' ;

// Digits (used in quantifier)
DIGITS      : [0-9]+ ;

// Whitespace is ignored
WS          : [ \t\r\n]+ -> skip ;