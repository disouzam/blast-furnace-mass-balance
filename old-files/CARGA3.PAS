program calculo_de_carga;
uses crt; 

var  xt,xk,g,h,MT,po2,NCcarr,feg,PCcarr,CRmax,CRmin,Pcoq,PCqu,PCI,
     TeorCM,CF,PCIesc,PCIrat,PCIratmx,FR,Vo2,Vsopro,Vazao,TeorMM,MTC,CRate:real;
       t,cont,y,z:integer;
       s,resp,c,i,opcao:char;
       Texto:text;
     PropM: array[1..3] of real;
     TeorM: array[1..3] of real;
     PropC: array[1..3] of real;
     TeorC: array[1..3] of real;
     MasM: array[1..3] of real;
     MasC: array[1..3] of real;
     
begin
 clrscr;
 TextMode(1);
 TextColor(15);
 gotoxy(13,10);
 writeln('CALCULO DE CARGA');
 gotoxy(1,24);
 write('Pressione qualquer tecla para continuar.');
 readkey;
 cont:=1;
 while cont <=40 do
   begin
     write(#220);
     cont:=cont+1;
     delay(20);
   end;
 Textmode(3);
 TextColor(15);
 resp:= 's';
 while resp ='s' do
  begin
    xt:=0;
    xk:=0;
    g:=0;
    h:=0;
    MT:=0;
    po2:=0;
    NCcarr:=0;
    PCcarr:=0;
    CRmax:=0;
    CRmin:=0;
    Pcoq:=0;
    PCqu:=0;
    PCI:=0;
    TeorCM:=0;
    CF:=0;
    PCIesc:=0;
    PCIrat:=0;
    PCIratmx:=0;
    FR:=0;
    Vo2:=0;
    Vsopro:=0;
    Vazao:=0;
    TeorMM:=0;
    MTC:=0;
    CRate:=0;
    cont:=1;
    while cont <=3 do
        begin
          PropM[cont]:=0;
          TeorM[cont]:=0;
          MasM[cont]:=0;
          PropC[cont]:=0;
          TeorC[cont]:=0;
          MasC[cont]:=0;
          cont:=cont+1;
        end;
  clrscr;
  writeln;
  textcolor(12);
  writeln('                   QUANTIDADE E COMPOSI€AO DO GUSA');
  textcolor(15);
  writeln;
  writeln;
  Write('Informe a quantidade de gusa que deve ser produzido (em toneladas):');
  readln(xt);
  writeln;
  repeat
  writeln('Informe o teor de ferro e carbono no gusa que ser',#160,' produzido.');
  write('Teor de ferro:');
  readln(g);
  write('Teor de carbono:');
  readln(h);
  writeln;
  If g+h>100 then
    begin
      textcolor(11);
      writeln('Dados incorretos!');
      textcolor(15);
    end;
  until g+h<=100;
  clrscr;
  textcolor(12);
  writeln('                           TIPOS DE MINERIO');
  textcolor(15);
  writeln;
  writeln;
  repeat
  Writeln('Quantos tipos de min‚rios serao utilizados(m',#160,'ximo de 3)?');
  readln(t);
  writeln;
  if t > 3 then
   begin
    textcolor(11);
    writeln('Dado incorreto!');
    textcolor(15);
   end;
  until t<=3;
  writeln;
  writeln;
  textcolor(12);
  writeln;
  writeln('                 PROPOR€AO DE MINERIO E TEOR DE FERRO');
  textcolor(15);
  writeln;
  writeln;
  repeat
  if t=1 then
   begin
    writeln('Informe o teor de ferro no minerio.');
    readln(TeorM[1]);
    PropM[1]:=100;
   end
  else
   begin
   cont:=1;
  while cont<=t do
    begin
      writeln('Informe a proporcao do minerio ',cont,' e o teor de ferro nele.');
      write('Proporcao:');
      readln(PropM[cont]);
      write('Teor de ferro:');
      readln(TeorM[cont]);
      cont:=cont+1;
      writeln;
    end;
  end;
  if PropM[1]+PropM[2]+PropM[3] <> 100 then
   begin
     textcolor(11);
     writeln('Dados incorretos!');
     textcolor(15);
   end;
  until PropM[1]+PropM[2]+PropM[3]=100;
  TeorMM:=(TeorM[1]*PropM[1]+TeorM[2]*PropM[2]+TeorM[3]*PropM[3])/100;
  clrscr;
  textcolor(12);
  writeln('                              TIPOS DE COQUE');
  textcolor(15);
  writeln;
  writeln;
  repeat
  writeln('Quantos tipos de coque serao utilizados(m',#160,'ximo de 3)?');
  readln(y);
  writeln;
  if y > 3 then
    begin
     textcolor(11);
     writeln('Dado incorreto!');
     textcolor(15);
    end;
  until y<=3;
  writeln;
  writeln;
  textcolor(12);
  writeln('                          PROPOR€AO E TEOR DE CARBONO');
  textcolor(15);
  writeln;
  repeat
  if y=1 then
   begin
    writeln('Informe o teor de carbono no coque.');
    readln(TeorC[1]);
    PropC[1]:=100;
   end
   else
    begin
    cont:=1;
  while cont <=y do
    begin
      writeln('Informe a propor',#135,'ao do coque ',cont,' na mistura e teor de carbono fixo nele.');
      write('Propor',#135,'ao:');
      readln(PropC[cont]);
      write('Teor de carbono:');
      readln(TeorC[cont]);
      cont:=cont+1;
      writeln;
    end;
    end;
  if PropC[1]+PropC[2]+PropC[3] <> 100 then
    begin
    textcolor(11);
    writeln('Dados incorretos!');
    textcolor(15);
    end;
  until PropC[1]+PropC[2]+PropC[3]=100;
  clrscr;
  xk:= xt*1000;
  feg:= (xk*g)/(100);
  MT:= (feg*10000)/(TeorM[1]*PropM[1]+TeorM[2]*PropM[2]+TeorM[3]*PropM[3]);
  cont:=1;
  while cont<= 3 do
   begin
    MasM[cont]:= (PropM[cont]*MT/100);
    MasM[cont]:=MasM[cont]/1000;
    cont:= cont+1;
   end;
  cont:=1;
  NCcarr:=(2.12*feg)/(56)+(xk*h)/(1200);
  PCcarr:=NCcarr*12;
  TeorCM:=(TeorC[1]*PropC[1]+TeorC[2]*PropC[2]+TeorC[3]*PropC[3])/100;
  Pcoq:= PCcarr/(TeorCM/100);{peso de coque sem uso de PCI}
  cont:=1;
  CRmax:=Pcoq/xt;
  PCqu:=(0.02*feg)*(12);
  writeln;
  textcolor(12);
  Writeln('                             QUANTIDADE DE FINOS');
  textcolor(15);
  writeln;
  writeln;
  writeln('Informe o teor de carbono nos finos:');
  readln(CF);
  PCI:=(PCqu)/(CF/100);
  PCIratmx:= PCI/xt;
  PCI:=PCI/1000;
  writeln;
  repeat
  writeln('Vocˆ poder  usar no m ximo ',PCI:10:3,' toneladas de finos.');
  write('Informe um valor em toneladas:');
  readln(PCIesc);
  writeln;
  if PCIesc > PCI then
   Begin
     textcolor(11);
     writeln('Dado incorreto!');
     textcolor(15);
   end;
  until PCIesc <= PCI;
  PCIesc:=PCIesc*1000;
  MTC:=(24*feg-PCIesc*CF+feg*150/7+xk*h)/(TeorCM);
  CRate:=MTC/xt;
  CRmin:=(((feg/56+(xk*h)/1200)*12)*100)/(xt*TeorCM); 
  cont:=1;
  while cont<= 3 do
   begin
    MasC[cont]:= (PropC[cont]*MTC)/(100);
    cont:= cont+1;
   end;
  PCIrat:=PCIesc/xt;
  FR:= PCIrat + CRate;
  PCIesc:=PCIesc/1000;
  clrscr;
  textcolor(12);
  writeln('                            TEOR DE OXIGENIO NO SOPRO');
  textcolor(15);
  writeln;
  writeln;
  writeln('Informe o teor de oxigenio no sopro.');
  write('Teor de oxigenio:');
  readln(po2);
  Vo2:= 0.224*feg;
  Vsopro:= Vo2/(po2/100);
  Vazao:= Vsopro/1440;
  clrscr;
  cont:=1;
  while cont<=80 do
   begin
     write(#205);
     cont:=cont+1;
   end;
  cont:=1;
  while cont<=80 do
   begin
     gotoxy(cont,3);
     write(#205);
     cont:=cont+1;
   end;
  cont:=1;
  while cont<=79 do
  begin
    gotoxy(cont,25);
    write(#205);
    cont:=cont+1;
  end;
  gotoxy(35,2);
  textcolor(12);
  writeln('DADOS DE ENTRADA');
  gotoxy(1,4);
  write('------------------------------------GUSA----------------------------------------');
  textcolor(15);
  writeln('Gusa:',xt:7:2,' ton (',g:5:2,' % de Fe,',h:5:2,' % de C.).');
  textcolor(12);
  writeln;
  writeln('--------------------------------- MINERIOS: -----------------------------------');
  textcolor(15);
  writeln('Minerio           Proporcao(%)           Teor(%)');
   cont:=1;
  while cont <=t do
   begin
  writeln('Minerio ',cont,'         ',PropM[cont]:7:2,'              ',TeorM[cont]:7:2);
    cont:=cont+1;
   end;
  writeln(t,' minerios com teor medio de ferro na carga de ',TeorMM:3:2,'%.');
  textcolor(12);
  writeln;
  writeln('---------------------------------- COQUE: -------------------------------------');
  textcolor(15);
  writeln('Coque             Proporcao(%)           Teor(%)');
   cont:=1;
  while cont <=y do
   begin
  writeln('Coque ',cont,'           ',PropC[cont]:7:2,'              ',TeorC[cont]:7:2);
    cont:=cont+1;
   end;

  writeln(y,' coque(s) com teor medio de carbono  na carga de ',TeorCM:3:2,'%.');
  writeln;
  writeln('Teor de carbono no PCI: ',CF:3:2,'%.');
  writeln('Teor de oxigenio no sopro: ',po2:3:2,'%.');
  writeln('Quantidade de PCI adicionada: ',PCIesc:7:2,' ton.');
  gotoxy(80,25);
  readkey;
  clrscr;

  cont:=1;
  while cont<=80 do
   begin
     write(#205);
     cont:=cont+1;
   end;
  cont:=1;
  while cont<=80 do
   begin
     gotoxy(cont,3);
     write(#205);
     cont:=cont+1;
   end;
  cont:=1;
  while cont<=80 do
  begin
    gotoxy(cont,24);
    write(#205);
    cont:=cont+1;
  end;
  gotoxy(35,2);
  textcolor(12);
  write('RESULTADOS');
  textcolor(15);
  gotoxy(1,4);
  cont:=1;
  while cont<= t do
   begin
    writeln('Massa de minerio ',cont,':',MasM[cont]:7:2,' ton.');
    cont:=cont+1;
    end;
  writeln;
  cont:=1;
  while cont<= y do
   begin
    writeln('Massa do coque ',cont,':',MasC[cont]:7:2,' ton.');
    cont:=cont+1;
    end;
  writeln('Massa do PCI: ',PCIesc:5:2,' ton');
  writeln;
  writeln('Coke Rate: ', CRate:7:2,' kg/ton de gusa');
  writeln('Coke Rate maximo: ',CRmax:7:2,' kg/ton de gusa');
  writeln('Coke Rate minimo: ',CRmin:7:2,' kg/ton de gusa');
  writeln;
  writeln('PCI Rate: ',PCIrat :7:2,' kg/ton de gusa');
  writeln('PCI Rate maximo: ',PCIratmx:5:2,' Kg/ton de gusa');
  writeln;
  writeln('Fuel Rate: ',FR:7:2,' kg/ton de gusa');
  writeln;
  writeln('Volume de oxigenio consumido: ',Vo2:5:2,'Nm3/dia.');
  writeln('Volume de sopro: ',Vazao:5:2,'Nm3/min');
  gotoxy(1,25);
  write('Pressione [Enter] para ir … tela de grava‡ao de resultados.');
  readln;
  clrscr;
  
  
  
  
  
  
  
  
  Writeln('                               GRAVA€AO DOS RESULTADOS');
  writeln;
  writeln;
  writeln('Deseja criar um arquivo texto com os dados obtidos?');
  write('Op‡oes:(s)sim (n)nao?');
  readln(resp);
  writeln;
  writeln;
  if resp = 's' then
  begin
   assign(Texto,'Dados.doc');
   opcao:='i';
   writeln('Ser  criado um arquivo chamado: Dados.doc.');
   writeln;
   textcolor(12);
   writeln('ATEN€AO! Se esse arquivo ja havia sido criado anteriormente e voce escolher a');
   Writeln('op‡ao (c)criar,na proxima pergunta, ele ser  sobrescrito e TODOS os dados serao');
   writeln('perdidos.');
   writeln;
   textcolor(15);
   writeln('Deseja criar um arquivo ou continuar inserindo dados no arquivo atual?');
   write('Op‡oes:(c)criar (i)inserir):');
   readln(opcao);
   if opcao = 'c' then
    begin
     rewrite(Texto);
    end
    else
     begin
      append(Texto);
     end;
  writeln(Texto,'                             DADOS DE ENTRADA');
  writeln(Texto,'  ');
  writeln(Texto,'------------------------------------GUSA----------------------------------------');
  writeln(Texto,'Gusa:',xt:7:2,' ton (',g:5:2,' % de Fe,',h:5:2,' % de C.).');
  writeln(Texto,'  ');
  writeln(Texto,'--------------------------------- MINERIOS: ------------------------------------');
  writeln(Texto,'Minerio           Proporcao(%)           Teor(%)');
   cont:=1;
  while cont <=t do
   begin
  writeln(Texto,'Minerio ',cont,'         ',PropM[cont]:7:2,'              ',TeorM[cont]:7:2);
    cont:=cont+1;
   end;
  writeln(Texto,'  ');
  writeln(Texto,t,' minerios com teor medio de ferro na carga de ',TeorMM:3:2,'%.');
  writeln(Texto,'  ');
  writeln(Texto,'---------------------------------- COQUE: -------------------------------------');
  writeln(Texto,'Coque             Proporcao(%)           Teor(%)');
   cont:=1;
  while cont <=y do
   begin
  writeln(Texto,'Coque ',cont,'           ',PropC[cont]:7:2,'              ',TeorC[cont]:7:2);
    cont:=cont+1;
   end;
  writeln(Texto,'  ');
  writeln(Texto,y,' coque(s) com teor medio de carbono  na carga de ',TeorCM:3:2,'%.');
  writeln(Texto,'  ');
  writeln(Texto,'Teor de carbono no PCI: ',CF:3:2,'%.');
  writeln(Texto,'Teor de oxigenio no sopro: ',po2:3:2,'%.');
  writeln(Texto,'Quantidade de PCI adicionada: ',PCIesc:7:2,' ton.');
  writeln(Texto,'  ');
  writeln(Texto,'  ');
  writeln(Texto,'  '); 
  write(Texto,'RESULTADOS');
  writeln(Texto,'  '); 
  writeln(Texto,'  ');   
  cont:=1;
  while cont<= t do
   begin
    writeln(Texto,'Massa de minerio ',cont,':',MasM[cont]:7:2,' ton.');
    cont:=cont+1;
    end;
  writeln(Texto,'  ');
  cont:=1;
  while cont<= y do
   begin
    writeln(Texto,'Massa do coque ',cont,':',MasC[cont]:7:2,' ton.');
    cont:=cont+1;
    end;
  writeln(Texto,'Massa do PCI: ',PCIesc:5:2,' ton');
  writeln(Texto,'  ');
  writeln(Texto,'Coke Rate: ', CRate:7:2,' kg/ton de gusa');
  writeln(Texto,'Coke Rate maximo: ',CRmax:7:2,' kg/ton de gusa');
  writeln(Texto,'Coke Rate minimo: ',CRmin:7:2,' kg/ton de gusa');
  writeln(Texto,'  ');
  writeln(Texto,'PCI Rate: ',PCIrat :7:2,' kg/ton de gusa');
  writeln(Texto,'PCI Rate maximo: ',PCIratmx:5:2,' Kg/ton de gusa');
  writeln(Texto,'  ');
  writeln(Texto,'Fuel Rate: ',FR:7:2,' kg/ton de gusa');
  writeln(Texto,'  ');
  writeln(Texto,'Volume de oxigenio consumido: ',Vo2:5:2,'Nm3/dia.');
  writeln(Texto,'Volume de sopro: ',Vazao:5:2,'Nm3/min');

  
  
  if opcao='c'then
    begin
      writeln('O arquivo foi criado com sucesso!');
    end else
     begin
      writeln('A inser‡ao de dados foi feita com sucesso!');
     end;
   close(Texto);
  end;
  writeln;
  writeln;
  writeln('Deseja executar o programa novamente?');
  write('Op‡oes: (s)sim (n)nao?');
  readln(resp);
 end;
  clrscr;
  writeln('    Este programa foi elaborado por alunos do quarto per¡odo, do curso ');
  writeln('    de Tecnologia em Metalurgia e Materiais - CEFET/ES, Abril de 2006.');
  gotoxy(1,5);
  textcolor(11);
  writeln('                               ALUNOS:');
  writeln;
  writeln('                   Daniel Figueiredo Leao Oliveira');
  writeln;
  writeln('                        Dickson Alves de Souza');
  writeln;
  writeln('                           Eduardo Junca');
  writeln;
  writeln('                           Erasmo Schultz');
  writeln;
  writeln('                        Gustavo Coqui Barbosa');
  writeln;
  writeln('              Maria Maria Cordeiro Moreira Cunillo Zacchei');
  writeln;
  writeln('                         Victor Bridi Telles');
  writeln;
  writeln;
  writeln('PROFESSOR: Marcelo Lucas Pereira Machado');
  gotoxy(1,25);
  textcolor(7);
  textbackground(4);
  write('Pressione qualquer tecla para sair do programa.');
  readkey;
end.