grammar LabCalculator;

compileUnit : expression EOF;

expression : 
	operatorToken=(INC|DEC) LPAREN expression RPAREN #IncrementalExpr
	| LPAREN expression RPAREN #ParenthesizedExpr
	| <assoc=right> expression EXPONENT expression #ExponentialExpr
	| expression operatorToken=(MULTIPLY|DIVIDE) expression #MultiplicativeExpr
	| operatorToken=(ADD|SUBTRACT) expression #UnaryAdditiveExpr
	| expression operatorToken=(ADD|SUBTRACT) expression #BinaryAdditiveExpr
	| NUMBER #NumberExpr
	| IDENTIFIER #IdentifierExpr
	;

NUMBER : INT (',' INT)?;
IDENTIFIER : ('A'..'Z')+ ('0'..'9')+;

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

WS : [\t\r\n ]->channel(HIDDEN);