:- dynamic dposit/1, dnegat/1, ob_is/2, cond/2.

do_consulting:- 
    ob_is(X,L),check(L), 
    bagof(A, dposit(A), Posits),
    length(Posits, N1), length(L, N2), N1 = N2,
    !,nl,
    write('Об’єкт ідентифікується як '), write(X), write('.'), nl, 
    clear_answers.
do_consulting:- 
    nl, write('Вибачте, ми не можемо Вам допомогти!'), nl, 
    clear_answers.

check([]).
check([H|T]):-
    dposit(H),!,check(T).
check([H|_]):-
    dnegat(H),!,fail.
check([H|T]):-
    cond(H,Txt),ask(H,Txt),check(T).

ask(X,TXT):-
    nl, write(' Чи має ідентифіковуваний об’єкт '),
    write(TXT), write(' (y/n)?- '),
    readln(Repl), remember(X,Repl).

remember(X,[y]):-
    assertz(dposit(X)).
remember(X,[n]):-
    assertz(dnegat(X)), fail.

clear_answers:-
    retract(dposit(_)), fail.
clear_answers:-
    retract(dnegat(_)), fail.
clear_answers.

goal:-
    clear_answers, 
    consult('facts_es.dat'),
    do_consulting.