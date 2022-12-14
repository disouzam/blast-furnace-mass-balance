program calculo_de_carga;
{Este programa realiza um c?lculo de carga simplificado para um alto forno, feito com base nos exerc?cios feitos 
em sala de aula e, tamb?m, com base nos conhecimentos do grupo, tanto na montagem da linha de pensamento com na 
montagem do porgrama usando a linguagem Pascal.
O programa considera o carregamento de at? 3 tipos de min?rios (estes podem ser os min?rios, pelotas e s?nteres).
A partir de uma propor??o em peso fixa, a carga de min?rios ? calculada. Calcula-se tamb?m o teor m?dio de ferro
contido nela.
A carga de coque ? feita de forma similar: estipula-se a propor??o dos diferentes tipos de coque( no m?ximo 3)
e calcula-se a massa de cada um deles para atender ao processo.
Pode-se calcular a quantidade de PCI m?xima a se injetar, e a partir dela, estipular um valor a ser usado.
O sopro pode ser calculado, considerando-se a necessidade de oxig?nio e o seu teor no sopro.
Os c?lculos s?o feitos com base nas considera??es de que todo o ferro carregado ? reduzido 
e se incorpora ao gusa l?quido e a rea??o de redu??o da wustita a ferro se processam integralmente a 900?C, de forma 
que a rea??o de redu??o seja:
FeO +3,12CO = Fe + 2,12 CO + CO2.
Ap?s o uso do programa, pode-se armazenar os dados em um arquivo texto(C?lculo.doc);}

uses crt;{Defini??o da biblioteca a ser utilizada}

{Defini??o das vari?veis utilizadas}
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

begin{1}
 clrscr;
 textmode( 1 ); {Modo de apresenta??o}
 TextColor( 15 ); {Cor do texto}
 gotoxy(13,10);
 writeln('CALCULO DE CARGA');
 gotoxy(1,24);
 write('Pressione qualquer tecla para continuar.');
 readkey;
 cont:=1;
 while cont <=40 do {Loop para fun??es gr?ficas de apresenta??o do programa}
  begin{2}
   write(#220);
   cont:=cont+1;
   delay(30);
  end;{2}
  Textmode(3);
  TextColor(15);
  resp:= 's';
  while resp ='s' do {Loop principal do programa. Atrav?s dele pode-se executar o programa diversas vezes}
   begin{3}
    {Inicializa??o das vari?veis, evitando-se erros nas equa??es}
    xt:=0; {Massa de gusa (em toneladas)} 
    xk:=0; {Massa de gusa (em kilogramas)} 
    g:=0; {Teor de ferro no gusa(%)}
    h:=0; {Teor de carbono no gusa(%)}
    MT:=0; {Massa total de min?rios carregados} 
    po2:=0; {Percentual de oxig?nio no ar soprado}
    NCcarr:=0; {N?mero de kmols de carbono necess?rio para ser carregado}
    PCcarr:=0; {Massa de carbono a ser carregado}
    CRmax:=0; {Coke rate m?ximo(em kg/ton de gusa)}
    CRmin:=0; {Coke rate m?nimo(em kg/ton de gusa)}
    Pcoq:=0; {Massa de coque (sem o uso de PCI)-para o c?lculo do Coke rate m?ximo}
    PCqu:=0; {Massa de carbono queimado}
    PCI:=0; {Massa m?xima de PCI que pode ser adicionada}
    TeorCM:=0; {Teor m?dio de carbono fixo na carga de coques}
    CF:=0; {Teor de carbono no PCI}
    PCIesc:=0; {Massa de PCI, inferior ? m?xima massa permitida}
    PCIrat:=0; {PCI rate (em kg/ton de gusa)}
    PCIratmx:=0; {PCI rate m?ximo (em kg/ton de gusa}
    FR:=0; {Fuel rate (em kg/ton de gusa)} 
    Vo2:=0; {Volume de oxig?nio consumido(em Nm3/dia)}
    Vsopro:=0; {Volume de sopro {em Nm3/dia}
    Vazao:=0; {Vaz?o do sopro(em Nm3/min)}
    TeorMM:=0; {Teor m?dio de ferro na carga dos min?rios}
    MTC:=0; {Massa total de coque carregado}
    CRate:=0; {Coque rate (em kg/ton de gusa)}
    cont:=1;
    while cont <=3 do { Loop?para inicializa??o dos vetores criados}
     begin{4}
      PropM[cont]:=0;{Propor??o do min?rio (1,2 ou 3) na carga de min?rios}
      TeorM[cont]:=0; {Teor de ferro no min?rio (1,2 ou 3)}
      MasM[cont]:=0; {Massa do min?rio (1,2 ou 3) carregado}
      PropC[cont]:=0; {Propor??o do coque (1,2 ou 3)} 
      TeorC[cont]:=0; {Teor de carbono fixo no coque (1,2 ou 3)}
      MasC[cont]:=0; {Massa do coque (1,2 ou 3) carregado} 
      cont:=cont+1;
     end{4};
    clrscr;
    writeln;
    textcolor(12);
    writeln('                   QUANTIDADE E COMPOSI?AO DO GUSA'); {In?cio da entrada de dados}
    textcolor(15);
    writeln;
    writeln;
    Write('Informe a quantidade de gusa que deve ser produzido (em toneladas):');
    readln(xt);{ Leitura da massa de gusa em toneladas}
    writeln;
    repeat {Loop de detec??o de erros na execu??o}
     writeln('Informe o teor de ferro e carbono no gusa que ser',#160,' produzido.');
     write('Teor de ferro:');
     readln(g); {Leitura do teor de ferro no gusa} 
     write('Teor de carbono:');
     readln(h);
     writeln;
     If g+h>100 then 
      begin{5}
       textcolor(11);
       writeln('Dados incorretos!'); {Aviso de erro na digita??o dos teores de ferro e carbono}
       textcolor(15);
      end{5};
    until g+h<=100;
    clrscr;
    textcolor(12);
    writeln('                           TIPOS DE MINERIO');
    textcolor(15);
    writeln;
    writeln;
    repeat
     Writeln('Quantos tipos de min?rios serao utilizados(m',#160,'ximo de 3)?');
     readln(t); {Leitura do n?mero de min?rios (ou similares) a serem utilizados}
     writeln;
     if t > 3 then  
      begin{6}
       textcolor(11);
       writeln('Dado incorreto!'); {Aviso de erro na digita??o do n?mero de min?rios}
       textcolor(15);
      end{6};
    until t<=3;
    writeln;
    writeln;
    textcolor(12);
    writeln;
    writeln('                 PROPOR?AO DE MINERIO E TEOR DE FERRO');
    textcolor(15);
    writeln;
    writeln;
    repeat   {Loop de leitura da propor??o do min?rio na carga e do teor de ferro nele}
     if t=1 then
      begin{7}
       writeln('Informe o teor de ferro no minerio.');
       readln(TeorM[1]);{Leitura do teor de ferro, no caso de um s? min?rio}
       PropM[1]:=100;
      end{7}
     else
      begin{8}
       cont:=1;
       while cont<=t do {Leitura da propor??o e do teor de ferro para cada min?rio, para o caso de mais de um min?rio}
        begin{9}
         writeln('Informe a proporcao do minerio ',cont,' e o teor de ferro nele.');
         write('Proporcao:');
         readln(PropM[cont]); {Leitura da propor??o para cada min?rio}
         write('Teor de ferro:');
         readln(TeorM[cont]); {Leitura do teor de ferro para cada min?rio}
         cont:=cont+1;
         writeln;
        end{9};
      end{8};
     if PropM[1]+PropM[2]+PropM[3] <> 100 then 
      begin{10}
       textcolor(11);
       writeln('Dados incorretos!'); {Aviso de erro na digita??o das propor??es}
       textcolor(15);
      end{10};
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
     readln(y); {Leitura do n?mero de coques a serem utilizados}
     writeln;
     if y > 3 then
      begin{11}
       textcolor(11);
       writeln('Dado incorreto!'); {Aviso de erro na digita??o do n?mero de coques}
       textcolor(15);
      end{11};
    until y<=3;
    writeln;
    writeln;
    textcolor(12);
    writeln('                          PROPOR?AO E TEOR DE CARBONO');
    textcolor(15);
    writeln;
    repeat
     if y=1 then
      begin{12}
       writeln('Informe o teor de carbono no coque.');
       readln(TeorC[1]); {Leitura do teor de  carbono fixo, no caso de um s? coque}
       PropC[1]:=100;
      end{12}
     else
      begin{13}
       cont:=1;
       while cont <=y do {Leitura da propor??o e do teor de carbono para cada coque, para o caso de mais de um coque}
        begin{14}
         writeln('Informe a propor',#135,'ao do coque ',cont,' na mistura e teor de carbono fixo nele.');
         write('Propor',#135,'ao:');
         readln(PropC[cont]); {Leitura da propor??o para cada coque}
         write('Teor de carbono:');
         readln(TeorC[cont]); {Leitura do teor de carbono para cada coque}
         cont:=cont+1;
         writeln;
        end{14};
      end{13};
    if PropC[1]+PropC[2]+PropC[3] <> 100 then
     begin{15}
      textcolor(11);
      writeln('Dados incorretos!');  {Aviso de erro na digita??o das propor??es}
      textcolor(15);
     end{15};
    until PropC[1]+PropC[2]+PropC[3]=100;
    clrscr;
    xk:= xt*1000;{Convers?o de unidades para a massa de gusa: de toneladas para kilogramas}
    feg:= (xk*g)/(100);{C?lculo da massa de ferro no gusa (feg),em kilogramas) }
    MT:= (feg*10000)/(TeorM[1]*PropM[1]+TeorM[2]*PropM[2]+TeorM[3]*PropM[3]); {C?lculo da massa total de min?rios carregados (em kilogramas)}
    cont:=1;
    while cont<= 3 do
     begin{16}
      MasM[cont]:= (PropM[cont]*MT/100);{C?lculo da massa individual de cada min?rio ( em kilogramas)}
      MasM[cont]:=MasM[cont]/1000;{Convers?o da massa individual de cada min?rio de kilogramas para toneladas} 
      cont:= cont+1;
     end{16};
    cont:=1;
    NCcarr:=(2.12*feg)/(56)+(xk*h)/(1200); {C?lculo do n?mero de kmols de carbono a serem carregados}
    PCcarr:=NCcarr*12; {C?lculo da massa de carbono a ser carregada (em kilogramas)}
    TeorCM:=(TeorC[1]*PropC[1]+TeorC[2]*PropC[2]+TeorC[3]*PropC[3])/100; {C?lculo do teor m?dio de carbono fixo na mistura de coques}
    Pcoq:= PCcarr/(TeorCM/100);{peso de coque sem uso de PCI }
    cont:=1;
    CRmax:=Pcoq/xt; {C?lculo do coke rate m?ximo (em kilogramas/ ton de gusa)}
    PCqu:=(0.02*feg)*(12); {C?lculo da massa de carbono queimado (em kilogramas)}
    writeln;
    textcolor(12);
    Writeln('                             QUANTIDADE DE PCI');
    textcolor(15);
    writeln;
    writeln;
    writeln('Informe o teor de carbono no PCI:');
    readln(CF); {Leitura do teor de carbono fixo no PCI}
    PCI:=(PCqu)/(CF/100); {C?culo da massa m?xima de PCI permitida (em kilogramas)}
    PCIratmx:= PCI/xt; {C?culo do PCI rate m?ximo (em kilogramas/ton de gusa)}
    PCI:=PCI/1000; {Convers?o da massa m?xima de PCI de kilogramas para toneladas}
    writeln;
    repeat
     writeln('Voc? poder? usar no m?ximo ',PCI:10:3,' toneladas de PCI.');
     write('Informe um valor em toneladas:');
     readln(PCIesc); {Leitura da massa de PCI adicionada}
     writeln;
     if PCIesc > PCI then
     begin{17}
      textcolor(11);
      writeln('Dado incorreto!'); {Aviso de digita??o de valor incorreto}
      textcolor(15);
     end{17};
    until PCIesc <= PCI;
    PCIesc:=PCIesc*1000; {Convers?o da massa de PCI adicionada de toneladas para kilogramas}
    MTC:=(24*feg-PCIesc*CF+feg*150/7+xk*h)/(TeorCM); {Massa total da mistura de coque a ser carregada (em kilogramas)}
    CRate:=MTC/xt; {Coke rate (em kilogramas/ton de gusa)}
    CRmin:=(((feg/56+(xk*h)/1200)*12)*100)/(xt*TeorCM); {C?lculo do coke rate m?nimo (em kilogrmas/ton de gusa)}
    cont:=1;
    while cont<= 3 do
     begin{18}
      MasC[cont]:= (PropC[cont]*(MTC/1000))/(100); {C?lculo da massa ndividual de cada coque carregado (em toneladas)}
      cont:= cont+1;
     end{18};
    PCIrat:=PCIesc/xt; {C?lculo do PCI rate (em kilogramas/ton de gusa)}
    FR:= PCIrat + CRate; {C?lculo do Fuel rate (em kilogramas/ton de gusa)}
    PCIesc:=PCIesc/1000; {Convers?o da massa de PCI adicionada de kilogramas para toneladas}
    clrscr;
    textcolor(12);
    writeln('                            TEOR DE OXIGENIO NO SOPRO');
    textcolor(15);
    writeln;
    writeln;
    writeln('Informe o teor de oxigenio no sopro.');
    write('Teor de oxigenio:'); 
    readln(po2); {Leitura do teor de oxig?nio no sopro}
    Vo2:= 0.224*feg; {C?lculo do volume}??????????????????????????????????????????????????????????????????????????????????????????????????????????????
    Vsopro:= Vo2/(po2/100); {C?lculo do volume de ar a ser soprado (em Nm3)}
    Vazao:= Vsopro/1440; {C?lculo da vaz?o de sopro (em Nm3/min)}
    clrscr;
    {Apresenta??o dos dados de entrada e dos resultados}
      {Apresenta??o dos dados de entrada}
    cont:=1;
    while cont<=80 do {Loop para fun??es gr?ficas de apresenta??o dos dados de entrada}
     begin{19}
      write(#205);
      cont:=cont+1;
     end{19};
    cont:=1;
    while cont<=80 do {Loop para fun??es gr?ficas de apresenta??o dos dados de entrada}
     begin{20}
      gotoxy(cont,3);
      write(#205);
      cont:=cont+1;
     end{20};
    cont:=1;
    while cont<=79 do {Loop para fun??es gr?ficas de apresenta??o dos dados de entrada}
    begin{21}
     gotoxy(cont,25);
     write(#205);
     cont:=cont+1;
    end{21};
    gotoxy(35,2);
    textcolor(12);
    writeln('DADOS DE ENTRADA');
    gotoxy(1,4);
    write('------------------------------------GUSA:---------------------------------------');
    textcolor(15);
    writeln('Gusa:',xt:7:2,' ton (',g:5:2,' % de Fe,',h:5:2,' % de C.).'); {Apresenta??o da massa de gusa produzida 
    e dos teores presentes de ferro e carbono} 
    textcolor(12);
    writeln;
    writeln('----------------------------------MINERIOS:------------------------------------');
    textcolor(15);
    writeln('Minerio           Proporcao(%)           Teor(%)');
    cont:=1;
    while cont <=t do
     begin{22}
      writeln('Minerio ',cont,'         ',PropM[cont]:7:2,'              ',TeorM[cont]:7:2);  {Apresenta??o da propor??o de cada min?rio na carga e do respectivo teor de ferro}
      cont:=cont+1;
     end{22};
    writeln('                     TEOR MEDIO DE FERRO: ',TeorMM:3:2,'%.'); {Apresenta??o do teor m?dio de ferro na carga}
    textcolor(12);
    writeln;
    writeln('-----------------------------------COQUE:--------------------------------------');
    textcolor(15);
    writeln('Coque             Proporcao(%)           Teor(%)');
    cont:=1;
    while cont <=y do
     begin{23}
      writeln('Coque ',cont,'           ',PropC[cont]:7:2,'              ',TeorC[cont]:7:2); {Apresenta??o da propor??o de cada coque na carga e do respectivo teor de carbono fixo}
      cont:=cont+1;
     end{23};
    writeln('                   TEOR MEDIO DE CARBONO: ',TeorCM:3:2,'%.'); {Apresenta??o do teor m?dio de carbono fixo na carga}
    writeln;
    writeln('TEOR DE CARBONO NO PCI: ',CF:3:2,'%.'); {Apresenta??o do teor de carbono fixo do PCI}
    writeln('TEOR DE OXIGENIO NO SOPRO: ',po2:3:2,'%.'); {Apresenta??o do teor de oxig?nio no sopro}
    writeln('QUANTIDADE DE PCI ADICIONADA: ',PCIesc:7:2,' ton.'); {Apresenta??o da quantidade de PCI adicionada}
    gotoxy(80,25);
    readkey;
    clrscr;
     {Apresenta??o dos resultados}
    cont:=1;
    while cont<=80 do {Loop para fun??es gr?ficas de apresenta??o dos resultados}
     begin{24}
      write(#205);
      cont:=cont+1;
     end{24};
    cont:=1;
    while cont<=80 do {Loop para fun??es gr?ficas de apresenta??o dos resultados}
     begin{25}
      gotoxy(cont,3);
      write(#205);
      cont:=cont+1;
     end{25};
    cont:=1;
    while cont<=80 do {Loop para fun??es gr?ficas de apresenta??o dos resultados}
     begin{26}
      gotoxy(cont,24);
      write(#205);
      cont:=cont+1;
     end{26};
    gotoxy(35,2);
    textcolor(12);
    write('RESULTADOS');
    textcolor(15);
    gotoxy(1,4);
    cont:=1;
    while cont<= t do
     begin{27}
        writeln('Massa de minerio ',cont,': ',MasM[cont]:7:2,' ton.'); {Apresenta??o da massa de cada min?rio carregado}
        cont:=cont+1;
     end{27};
    MT:=MT/1000; {Convers?o da massa total de min?rio carregado de kilogramas para toneladas}
    textcolor(12);
    writeln('TOTAL CARREGADO: ',MT:7:2,' toneladas por dia.'); {Massa total de min?rio carregado diariamente}
    textcolor(15);
    cont:=1;
    while cont<= y do
     begin{28}
       writeln('Massa do coque ',cont,': ',MasC[cont]:7:2,' ton.'); {Apresenta??o da massa de cada coque carregado}
       cont:=cont+1;
     end{ 28};
{      end{ };}???????????????????????????????????????????????????????????????????????????????????????????????????????????????????
     MTC:=MTC/1000;
     textcolor(12);
    writeln('TOTAL CARREGADO: ',MTC:7:2,' toneladas por dia.'); {Massa total de coque carregado diariamente}
    textcolor(15);
    textcolor(12);
    textcolor(12);
    writeln('Coke Rate: ', CRate:7:2,' kg/ton de gusa'); {Apresenta??o do Coke rate obtido}
    textcolor(15);
    writeln('Coke Rate maximo: ',CRmax:7:2,' kg/ton de gusa'); {Apresenta??o do Coke rate m?ximo (em uma opera??o ALL COKE)}
    writeln('Coke Rate minimo: ',CRmin:7:2,' kg/ton de gusa'); {Apresenta??o do Coke rate te?rico m?nimo 
    (atrav?s da utiliza??o da quantidade m?xima de PCI)}
    writeln;
    textcolor(12);
    writeln('Massa do PCI: ',PCIesc:5:2,' ton'); {Apresenta??o da massa de PCI adicionada}
    writeln('PCI Rate: ',PCIrat :7:2,' kg/ton de gusa'); {Apresenta??o do PCI rate obtido}
    textcolor(15);
    writeln('PCI Rate maximo: ',PCIratmx:5:2,' Kg/ton de gusa'); {Apresenta??o do PCI rate m?ximo permitido}
    textcolor(12);
    writeln;
    writeln('Fuel Rate: ',FR:7:2,' kg/ton de gusa'); {Apresenta??o do Fuel rate obtido}
    writeln;
    textcolor(15);
    writeln('Volume de oxigenio consumido: ',Vo2:5:2,'Nm3/dia.'); {Apresenta??o do volume de oxig?nio consumido}
    textcolor(12);
    writeln('Volume de sopro: ',Vazao:5:2,'Nm3/min'); {Apresenta??o do vaz?o de sopro}
    textcolor(15);
    gotoxy(1,25);
    write('Pressione [Enter] para ir a tela de gravacao de resultados.'); {Finaliza??o da apresenta??o dos dados de entrada e dos resultados}
    readln;
    clrscr;

    {Procedimentos para armazenamento dos dados em um arquivo texto}
    Writeln('                               GRAVACAO DOS RESULTADOS');
    writeln;
    writeln;
    writeln('Deseja criar um arquivo texto com os dados obtidos?');
    write('Opcoes:(s)sim (n)nao?'); 
    readln(resp);
    writeln;
    writeln;
    if resp = 's' then
     begin{29}                                                               
      assign(Texto,'Calculo.doc');
      opcao:='i';
      writeln('Sera criado um arquivo chamado: Calculo.doc.');
      writeln;
      textcolor(12);
      writeln('ATENCAO! Se esse arquivo j? havia sido criado anteriormente e voce escolher a');
      Writeln('opcao (c)criar,na proxima pergunta, ele ser? sobrescrito e TODOS os dados serao');
      writeln('perdidos.');
      writeln;
      textcolor(15);
      writeln('Deseja criar um arquivo ou continuar inserindo dados no arquivo atual?');
      writeln('Op?oes:(c)criar (i)inserir):');
      readln(opcao);
      if opcao = 'c' then
       begin{30}
        rewrite(Texto);
       end{30}
      else
       begin{31}
        append(Texto);
       end{31};
       {Formata??o da apresenta??o dos dados de entrada e dos resultados no arquivo texto}
      writeln(Texto,'                                DADOS DE ENTRADA');
      writeln(Texto);
      write(Texto,'------------------------------------GUSA:---------------------------------------');
      writeln(Texto,'                             Gusa:',xt:7:2,' ton (',g:5:2,' % de Fe,',h:5:2,' % de C.).');
      writeln(Texto);
      writeln(Texto,'----------------------------------MINERIOS:-------------------------------------');
      writeln(Texto);
      writeln(Texto,'               Minerio           Proporcao(%)           Teor(%)');
      cont:=1;
      while cont <=t do
       begin{32}
        writeln(Texto,'               Minerio ',cont,'         ',PropM[cont]:7:2,'               ',TeorM[cont]:7:2);
        cont:=cont+1;
       end{32};
      writeln(Texto,'                                    TEOR MEDIO DE FERRO: ',TeorMM:3:2,'%.');
      writeln(Texto);
      writeln(Texto,'-----------------------------------COQUE:---------------------------------------');
      writeln(Texto);
      writeln(Texto,'               Coque             Proporcao(%)           Teor(%)');
      cont:=1;
      while cont <=y do
       begin{33}
        writeln(Texto,'               Coque ',cont,'           ',PropC[cont]:7:2,'                ',TeorC[cont]:7:2);
        cont:=cont+1;
       end{33};
      writeln(Texto,'                                  TEOR MEDIO DE CARBONO: ',TeorCM:3:2,'%.');
      writeln(Texto);
      writeln(Texto,'                           TEOR DE CARBONO NO PCI: ',CF:3:2,'%.');
      writeln(Texto,'                        TEOR DE OXIGENIO NO SOPRO: ',po2:3:2,'%.');
      writeln(Texto,'                      Quantidade de PCI adicionada: ',PCIesc:7:2,' ton.');
      readkey;
      writeln(Texto);
      writeln(Texto);
      writeln(Texto);
      writeln(Texto);
      writeln(Texto,'                               RESULTADOS');
      writeln(Texto);
      writeln(Texto,'--------------------------------MINERIOS----------------------------------------');
      cont:=1;
      while cont<= t do
       begin{34}
        writeln(Texto,'                        Massa de minerio ',cont,': ',MasM[cont]:7:2,' ton.');
        cont:=cont+1;
       end{34};
      writeln(Texto,'                        TOTAL CARREGADO: ',MT:7:2,' toneladas por dia.');
      writeln(Texto);
      writeln(Texto,'---------------------------------COQUE------------------------------------------');
      cont:=1;
      while cont<= y do
       begin{35}
        writeln(Texto,'                        Massa do coque ',cont,': ',MasC[cont]:7:2,' ton.');
        cont:=cont+1;
       end{35};
     {      end{ };}???????????????????????????????????????????????????????????????????????????????????????????????
      writeln(Texto,'                        TOTAL CARREGADO: ',MTC:7:2,' toneladas por dia.');
      writeln(Texto);
      writeln(Texto,'----------------------------------PCI-------------------------------------------');
      writeln(Texto,'                        Massa do PCI: ',PCIesc:5:2,' ton');
      writeln(Texto);
      writeln(Texto,'---------------------------------RATES------------------------------------------');
      writeln(Texto,'                        Coke Rate: ', CRate:7:2,' kg/ton de gusa');
      writeln(Texto,'                        Coke Rate maximo: ',CRmax:7:2,' kg/ton de gusa');
      writeln(Texto,'                        Coke Rate minimo: ',CRmin:7:2,' kg/ton de gusa');
      writeln(Texto);

      writeln(Texto,'                        PCI Rate: ',PCIrat :7:2,' kg/ton de gusa');
      writeln(Texto,'                        PCI Rate maximo: ',PCIratmx:5:2,' Kg/ton de gusa');
      writeln(Texto);
      writeln(Texto,'                        Fuel Rate: ',FR:7:2,' kg/ton de gusa');
      writeln(Texto);
      writeln(Texto,'-----------------------------DADOS DE SOPRO-------------------------------------');
      writeln(Texto,'                        Volume de oxigenio consumido: ',Vo2:5:2,'Nm3/dia.');
      writeln(Texto,'                        Volume de sopro: ',Vazao:5:2,'Nm3/min');
      writeln(Texto);
      writeln(Texto);
      writeln(Texto);
      cont:=2*t+2*y;
      while cont<12 do
       begin{36}
        writeln(Texto);
        cont:=cont+1;
       end{36};

      if opcao='c'then
       begin{37}
        writeln('O arquivo foi criado com sucesso!');
       end{37 }
      else
       begin{38 }
        writeln('A insercao de dados foi feita com sucesso!');
       end{38};
      close(Texto);
     end{29};
    writeln;
    writeln;
    writeln('Deseja executar o programa novamente?');
    write('Opcoes: (s)sim (n)nao?');
    readln(resp);
   end{3};{end do while inicial}
   clrscr;
   writeln('    Este programa foi elaborado por alunos do quarto per?odo, do curso ');
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
end.{1}