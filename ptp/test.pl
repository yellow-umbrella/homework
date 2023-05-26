:- initialization(do_consulting).
:- dynamic(have_found/1).

do_consulting:- 
    find, have_found(X),!,nl,
  write('Object is '),
  write(X),write('.'),nl, clear_facts.
do_consulting:-
    nl,write('Fuck you!'),
  nl, clear_facts.
% goal
do_consulting.

find:- test1(X), test2(X,Y), test3(X,Y,Z),!.
find.

clear_facts:-
    retract(have_found(_)), fail.
clear_facts.

has_it(X):-
    write('Does it has '),
  write(X),write(' (y/n)? -\n'), readln(R), R=[y].

% test1 test2 test3 – див. далі


test1(y):-has_it('stripes'),!.
test1(n).

test2(y,y):-has_it('red fins'),assertz(have_found('perch')),!.
test2(y,n):-assertz(have_found('pike')).

test2(n,y):-has_it('tendrils'),!.
test2(n,n):-asserta(have_found('crucian')).

test3(n,y,y):-has_it('small scale'),assertz(have_found('tench')),!.
test3(n,y,n):-assertz(have_found('carp')).