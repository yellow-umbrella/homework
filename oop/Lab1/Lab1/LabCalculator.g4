grammar LabCalculator;

compileUnit : expression EOF;

expression : 
	operatorToken=(INC|DEC) LPAREN expression RPAREN #IncrementalExpr
	| LPAREN expression RPAREN #ParenthesizedExpr
	| expression EXPONENT expression #ExponentialExpr
	| expression operatorToken=(MULTIPLY|DIVIDE) expression #MultiplicativeExpr
	| operatorToken=(ADD|SUBTRACT) expression #UnAdditiveExpr
	| expression operatorToken=(ADD|SUBTRACT) expression #BinAdditiveExpr
	| NUMBER #NumberExpr
	| IDENTIFIER #IdentifierExpr
	;

NUMBER : INT (',' INT)?;
IDENTIFIER : [a-zA-Z]+[1-9][0-9]+;

INT : ('0'..'9')+;

EXPONENT : '^';
MULTIPLY : '*';
DIVIDE : '/';
SUBTRACT : '-';
ADD : '+';
LPAREN : '(';
RPAREN : ')';
INC : 'inc';
DEC : 'dec';

WS : [\t\r\n' ']->channel(HIDDEN);