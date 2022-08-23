# About
An educational algorithm for mass balance calculation of charges for a blast furnace in an ironmaking unit.

This algorithm was developed as part of a group work for one assignment in Ironmaking subject (CEFET-ES, Brazil, 2006). The team members were (in alphabetical order):

1. Daniel Figueiredo Leao Oliveira
1. Dickson Alves de Souza
1. Eduardo Junca
1. Erasmo Schultz
1. Gustavo Coqui Barbosa
1. Maria Maria Cordeiro Moreira Cunillo Zacchei
1. Victor Bridi Telles

and professor was Marcelo Lucas Pereira Machado


# A Refresh on Pascal syntax

```Pascal
(* My beautiful function returns an interesting result *)  
Function Beautiful : Integer;
```

The use of (* and *) as comment delimiters dates from the very first days of the Pascal language. It has been replaced mostly by the use of { and } as comment delimiters, as in the following example:
 
```Pascal
{ My beautiful function returns an interesting result }  
Function Beautiful : Integer;  
```

The comment can also span multiple lines:

```Pascal
{  
   My beautiful function returns an interesting result,  
   but only if the argument A is less than B.  
}  
Function Beautiful (A,B : Integer): Integer;
```

Single line comments can also be made with the // delimiter: 

The excerpts above were extracted from the documentation of Free Pascal, link below
https://www.freepascal.org/docs-html/ref/refse2.html

