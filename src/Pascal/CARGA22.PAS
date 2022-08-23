program calculo_de_carga;

{Este programa realiza um cálculo de carga simplificado para um alto forno, 
feito com base nos exercícios feitos em sala de aula e, também, com base nos conhecimentos do grupo, tanto na montagem da linha de pensamento como na montagem do programa usando a linguagem Pascal.

O programa considera o carregamento de até 3 tipos de minérios 
(estes podem ser os minérios, pelotas e sínteres).

A partir de uma proporção em peso fixa, a carga de minérios é calculada. 
Calcula-se também o teor médio de ferro contido nela.

A carga de coque é feita de forma similar: estipula-se a proporção dos 
diferentes tipos de coque( no máximo 3) e calcula-se a massa de cada um deles 
para atender ao processo.

Pode-se calcular a quantidade de PCI máxima a se injetar, e a partir dela, estipular um valor a ser usado.

O sopro pode ser calculado, considerando-se a necessidade de oxigênio e 
o seu teor no sopro.

Os cálculos são feitos com base nas considerações de que todo o ferro 
carregado é reduzido e se incorpora ao gusa líquido e a reação de redução da
wustita a ferro se processam integralmente a 900 °C, de forma que a reação de redução seja:

FeO +3,12CO = Fe + 2,12 CO + CO2.

Após o uso do programa, pode-se armazenar os dados em um arquivo texto(Calculo_de_carga.doc);

}

{Definição da biblioteca a ser utilizada}
uses crt;

{Definição das variáveis utilizadas}
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
 textmode( 1 ); {Modo de apresentação}
 TextColor( 15 ); {Cor do texto}
 gotoxy(13,10);
 writeln('CALCULO DE CARGA');
 gotoxy(1,24);
 write('Pressione qualquer tecla para continuar.');
 readkey;
 cont:=1;
 while cont <=40 do {Loop para funções gráficas de apresentação do programa}
  begin
   write(#220);
   cont:=cont+1;
   delay(40);
  end;
  Textmode(3);
  TextColor(15);
  resp:= 's';
  while resp ='s' do {Loop principal do programa. Através dele pode-se executar o programa diversas vezes}
   begin
    {Inicialização das variáveis, evitando-se erros nas equações}
    xt:=0; {Massa de gusa (em toneladas)} 
    xk:=0; {Massa de gusa (em kilogramas)} 
    g:=0; {Teor de ferro no gusa(%)}
    h:=0; {Teor de carbono no gusa(%)}
    MT:=0; {Massa total de minérios carregados} 
    po2:=0; {Percentual de oxigênio no ar soprado}
    NCcarr:=0; {Número de kmols de carbono necessário para ser carregado}
    PCcarr:=0; {Massa de carbono a ser carregado}
    CRmax:=0; {Coke rate máximo(em kg/ton de gusa)}
    CRmin:=0; {Coke rate mínimo(em kg/ton de gusa)}
    Pcoq:=0; {Massa de coque (sem o uso de PCI)-para o cálculo do Coke rate máximo}
    PCqu:=0; {Massa de carbono queimado}
    PCI:=0; {Massa máxima de PCI que pode ser adicionada}
    TeorCM:=0; {Teor médio de carbono fixo na carga de coques}
    CF:=0; {Teor de carbono no PCI}
    PCIesc:=0; {Massa de PCI, inferior à máxima massa permitida}
    PCIrat:=0; {PCI rate (em kg/ton de gusa)}
    PCIratmx:=0; {PCI rate máximo (em kg/ton de gusa}
    FR:=0; {Fuel rate (em kg/ton de gusa)} 
    Vo2:=0; {Volume de oxigênio consumido(em Nm3/dia)}
    Vsopro:=0; {Volume de sopro {em Nm3/dia}}
    Vazao:=0; {Vazão do sopro(em Nm3/min)}
    TeorMM:=0; {Teor médio de ferro na carga dos minérios}
    MTC:=0; {Massa total de coque carregado}
    CRate:=0; {Coque rate (em kg/ton de gusa)}
    cont:=1;
    while cont <=3 do { Loop para inicialização dos vetores criados}
     begin
      PropM[cont]:=0;{Proporção do minério (1,2 ou 3) na carga de minérios}
      TeorM[cont]:=0; {Teor de ferro no minério (1,2 ou 3)}
      MasM[cont]:=0; {Massa do minério (1,2 ou 3) carregado}
      PropC[cont]:=0; {Proporção do coque (1,2 ou 3)} 
      TeorC[cont]:=0; {Teor de carbono fixo no coque (1,2 ou 3)}
      MasC[cont]:=0; {Massa do coque (1,2 ou 3) carregado} 
      cont:=cont+1;
     end;
    clrscr;
    writeln;
    textcolor(12);
    writeln('                   QUANTIDADE E COMPOSIÇAO DO GUSA'); {Início da entrada de dados}
    textcolor(15);
    writeln;
    writeln;
    Write('Informe a quantidade de gusa que deve ser produzido (em toneladas):');
    readln(xt);{ Leitura da massa de gusa em toneladas}
    writeln;
    repeat {Loop de detecção de erros na execução}
     writeln('Informe o teor de ferro e carbono no gusa que ser',#160,' produzido.');
     write('Teor de ferro:');
     readln(g); {Leitura do teor de ferro no gusa} 
     write('Teor de carbono:');
     readln(h);
     writeln;
     If g+h>100 then 
      begin
       textcolor(11);
       writeln('Dados incorretos!'); {Aviso de erro na digitação dos teores de ferro e carbono}
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
     Writeln('Quantos tipos de minérios serao utilizados(m',#160,'ximo de 3)?');
     readln(t); {Leitura do número de minérios (ou similares) a serem utilizados}
     writeln;
     if t > 3 then  
      begin
       textcolor(11);
       writeln('Dado incorreto!'); {Aviso de erro na digitação do número de minérios}
       textcolor(15);
      end;
    until t<=3;
    writeln;
    writeln;
    textcolor(12);
    writeln;
    writeln('                 PROPORÇÃO DE MINERIO E TEOR DE FERRO');
    textcolor(15);
    writeln;
    writeln;
    repeat   {Loop de leitura da proporção do minério na carga e do teor de ferro nele}
     if t=1 then
      begin
       writeln('Informe o teor de ferro no minerio.');
       readln(TeorM[1]);{Leitura do teor de ferro, no caso de um só minério}
       PropM[1]:=100;
      end
     else
      begin
       cont:=1;
       while cont<=t do {Leitura da proporção e do teor de ferro para cada minério, para o caso de mais de um minério}
        begin
         writeln('Informe a proporcao do minerio ',cont,' e o teor de ferro nele.');
         write('Proporcao:');
         readln(PropM[cont]); {Leitura da proporção para cada minério}
         write('Teor de ferro:');
         readln(TeorM[cont]); {Leitura do teor de ferro para cada minério}
         cont:=cont+1;
         writeln;
        end;
      end;
     if PropM[1]+PropM[2]+PropM[3] <> 100 then 
      begin
       textcolor(11);
       writeln('Dados incorretos!'); {Aviso de erro na digitação das proporções}
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
     readln(y); {Leitura do número de coques a serem utilizados}
     writeln;
     if y > 3 then
      begin
       textcolor(11);
       writeln('Dado incorreto!'); {Aviso de erro na digitação do número de coques}
       textcolor(15);
      end;
    until y<=3;
    writeln;
    writeln;
    textcolor(12);
    writeln('                          PROPORÇÃO E TEOR DE CARBONO');
    textcolor(15);
    writeln;
    repeat
     if y=1 then
      begin
       writeln('Informe o teor de carbono no coque.');
       readln(TeorC[1]); {Leitura do teor de  carbono fixo, no caso de um só coque}
       PropC[1]:=100;
      end
     else
      begin
       cont:=1;
       while cont <=y do {Leitura da proporção e do teor de carbono para cada coque, para o caso de mais de um coque}
        begin
         writeln('Informe a propor',#135,'ao do coque ',cont,' na mistura e teor de carbono fixo nele.');
         write('Propor',#135,'ao:');
         readln(PropC[cont]); {Leitura da proporção para cada coque}
         write('Teor de carbono:');
         readln(TeorC[cont]); {Leitura do teor de carbono para cada coque}
         cont:=cont+1;
         writeln;
        end;
      end;
    if PropC[1]+PropC[2]+PropC[3] <> 100 then
     begin
      textcolor(11);
      writeln('Dados incorretos!');  {Aviso de erro na digitação das proporções}
      textcolor(15);
     end;
    until PropC[1]+PropC[2]+PropC[3]=100;
    clrscr;
    xk:= xt*1000;{Conversão de unidades para a massa de gusa: de toneladas para kilogramas}
    feg:= (xk*g)/(100);{Cálculo da massa de ferro no gusa (feg),em kilogramas) }
    MT:= (feg*10000)/(TeorM[1]*PropM[1]+TeorM[2]*PropM[2]+TeorM[3]*PropM[3]); {Cálculo da massa total
     de minérios carregados (em kilogramas)}
    cont:=1;
    while cont<= 3 do
     begin
      MasM[cont]:= (PropM[cont]*MT/100);{Cálculo da massa individual de cada minério ( em kilogramas)}
      MasM[cont]:=MasM[cont]/1000;{Conversão da massa individual de cada minério de kilogramas para toneladas} 
      cont:= cont+1;
     end;
    cont:=1;
    NCcarr:=(2.12*feg)/(56)+(xk*h)/(1200); {Cálculo do número de kmols de carbono a serem carregados}
    PCcarr:=NCcarr*12; {Cálculo da massa de carbono a ser carregada (em kilogramas)}
    TeorCM:=(TeorC[1]*PropC[1]+TeorC[2]*PropC[2]+TeorC[3]*PropC[3])/100; {Cálculo do teor
    médio de carbono fixo na mistura de coques}
    Pcoq:= PCcarr/(TeorCM/100);{peso de coque sem uso de PCI }
    cont:=1;
    CRmax:=Pcoq/xt; {Cálculo do coke rate máximo (em kilogramas/ ton de gusa)}
    PCqu:=(0.02*feg)*(12); {Cálculo da massa de carbono queimado (em kilogramas)}
    writeln;
    textcolor(12);
    Writeln('                             QUANTIDADE DE PCI');
    textcolor(15);
    writeln;
    writeln;
    writeln('Informe o teor de carbono no PCI:');
    readln(CF); {Leitura do teor de carbono fixo no PCI}
    PCI:=(PCqu)/(CF/100); {Cálculo da massa máxima de PCI permitida (em kilogramas)}
    PCIratmx:= PCI/xt; {Cálculo do PCI rate máximo (em kilogramas/ton de gusa)}
    PCI:=PCI/1000; {Conversão da massa máxima de PCI de kilogramas para toneladas}
    writeln;
    repeat
     writeln('Você poderá usar no máximo ',PCI:10:3,' toneladas de PCI.');
     write('Informe um valor em toneladas:');
     readln(PCIesc); {Leitura da massa de PCI adicionada}
     writeln;
     if PCIesc > PCI then
     begin
      textcolor(11);
      writeln('Dado incorreto!'); {Aviso de digitação de valor incorreto}
      textcolor(15);
     end;
    until PCIesc <= PCI;
    PCIesc:=PCIesc*1000; {Conversão da massa de PCI adicionada de toneladas para kilogramas}
    MTC:=(24*feg-PCIesc*CF+feg*150/7+xk*h)/(TeorCM); {Massa total da mistura de coque a ser carregada (em kilogramas)}
    CRate:=MTC/xt; {Coke rate (em kilogramas/ton de gusa)}
    CRmin:=(((feg/56+(xk*h)/1200)*12)*100)/(xt*TeorCM); {Cálculo do coke rate mínimo (em kilogrmas/ton de gusa)}
    cont:=1;
    while cont<= 3 do
     begin
      MasC[cont]:= (PropC[cont]*(MTC/1000))/(100); {Cálculo da massa ndividual de cada coque carregado (em toneladas)}
      cont:= cont+1;
     end;
    PCIrat:=PCIesc/xt; {Cálculo do PCI rate (em kilogramas/ton de gusa)}
    FR:= PCIrat + CRate; {Cálculo do Fuel rate (em kilogramas/ton de gusa)}
    PCIesc:=PCIesc/1000; {Conversão da massa de PCI adicionada de kilogramas para toneladas}
    clrscr;
    textcolor(12);
    writeln('                            TEOR DE OXIGENIO NO SOPRO');
    textcolor(15);
    writeln;
    writeln;
    writeln('Informe o teor de oxigenio no sopro.');
    write('Teor de oxigenio:'); 
    readln(po2); {Leitura do teor de oxigênio no sopro}
    Vo2:= 0.224*feg; {Cálculo do volume}{?????????????????????????????????????????????????????
    ?????????????????????????????????????????????????????????}
    Vsopro:= Vo2/(po2/100); {Cálculo do volume de ar a ser soprado (em Nm3)}
    Vazao:= Vsopro/1440; {Cálculo da vazão de sopro (em Nm3/min)}
    clrscr;
    {Apresentação dos dados de entrada e dos resultados}
      {Apresentação dos dados de entrada}
    cont:=1;
    while cont<=80 do {Loop para funções gráficas de apresentação dos dados de entrada}
     begin
      write(#205);
      cont:=cont+1;
     end;
    cont:=1;
    while cont<=80 do {Loop para funções gráficas de apresentação dos dados de entrada}
     begin
      gotoxy(cont,3);
      write(#205);
      cont:=cont+1;
     end;
    cont:=1;
    while cont<=79 do {Loop para funções gráficas de apresentação dos dados de entrada}
    begin
     gotoxy(cont,25);
     write(#205);
     cont:=cont+1;
    end;
    gotoxy(35,2);
    textcolor(12);
    writeln('DADOS DE ENTRADA');
    gotoxy(1,4);
    write('------------------------------------GUSA:---------------------------------------');
    textcolor(15);
    writeln('Gusa:',xt:7:2,' ton (',g:5:2,' % de Fe,',h:5:2,' % de C.).'); {Apresentação da massa de gusa produzida 
    e dos teores presentes de ferro e carbono} 
    textcolor(12);
    writeln;
    writeln('----------------------------------MINERIOS:------------------------------------');
    textcolor(15);
    writeln('Minerio           Proporcao(%)           Teor(%)');
    cont:=1;
    while cont <=t do
     begin
      writeln('Minerio ',cont,'         ',PropM[cont]:7:2,'              ',TeorM[cont]:7:2);  {Apresentação da proporção de
      cada minério na carga e do respectivo teor de ferro}
      cont:=cont+1;
    end;
    writeln('                     TEOR MEDIO DE FERRO: ',TeorMM:3:2,'%.'); {Apresentação do teor médio de ferro na carga}
    textcolor(12);
    writeln;
    writeln('-----------------------------------COQUE:--------------------------------------');
    textcolor(15);
    writeln('Coque             Proporcao(%)           Teor(%)');
    cont:=1;
    while cont <=y do
     begin
      writeln('Coque ',cont,'           ',PropC[cont]:7:2,'              ',TeorC[cont]:7:2); {Apresentação da proporção de
       cada coque na carga e do respectivo teor de carbono fixo}
      cont:=cont+1;
     end;
    writeln('                   TEOR MEDIO DE CARBONO: ',TeorCM:3:2,'%.'); {Apresentação do
    teor médio de carbono fixo na carga}
    writeln;
    writeln('TEOR DE CARBONO NO PCI: ',CF:3:2,'%.'); {Apresentação do teor de carbono fixo do PCI}
    writeln('TEOR DE OXIGENIO NO SOPRO: ',po2:3:2,'%.'); {Apresentação do teor de oxigênio no sopro}
    writeln('QUANTIDADE DE PCI ADICIONADA: ',PCIesc:7:2,' ton.'); {Apresentação da quantidade de PCI adicionada}
    gotoxy(80,25);
    readkey;
    clrscr;
     {Apresentação dos resultados}
    cont:=1;
    while cont<=80 do {Loop para funções gráficas de apresentação dos resultados}
     begin
      write(#205);
      cont:=cont+1;
     end;
    cont:=1;
    while cont<=80 do {Loop para funções gráficas de apresentação dos resultados}
     begin
      gotoxy(cont,3);
      write(#205);
      cont:=cont+1;
     end;
    cont:=1;
    while cont<=80 do {Loop para funções gráficas de apresentação dos resultados}
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
        writeln('Massa de minerio ',cont,': ',MasM[cont]:7:2,' ton.'); {Apresentação da massa de cada minério carregado}
        cont:=cont+1;
     end;
    MT:=MT/1000; {Conversão da massa total de minério carregado de kilogramas para toneladas}
    textcolor(12);
    writeln('TOTAL CARREGADO: ',MT:7:2,' toneladas por dia.'); {Massa total de minério carregado diariamente}
    textcolor(15);
    cont:=1;
    while cont<= y do
     begin
       writeln('Massa do coque ',cont,': ',MasC[cont]:7:2,' ton.'); {Apresentação da massa de cada coque carregado}
       cont:=cont+1;
     end;
{      end;}{??????????????????????????????????????????????????????????????
????????????????????????????????????????????????????}
     MTC:=MTC/1000;
     textcolor(12);
    writeln('TOTAL CARREGADO: ',MTC:7:2,' toneladas por dia.'); {Massa total de coque carregado diariamente}
    textcolor(15);
    textcolor(12);
    textcolor(12);
    writeln('Coke Rate: ', CRate:7:2,' kg/ton de gusa'); {Apresentação do Coke rate obtido}
    textcolor(15);
    writeln('Coke Rate maximo: ',CRmax:7:2,' kg/ton de gusa'); {Apresentação do Coke rate máximo (em uma operação ALL COKE)}
    writeln('Coke Rate minimo: ',CRmin:7:2,' kg/ton de gusa'); {Apresentação do Coke rate teórico mínimo 
    (através da utilização da quantidade máxima de PCI)}
    writeln;
    textcolor(12);
    writeln('Massa do PCI: ',PCIesc:5:2,' ton'); {Apresentação da massa de PCI adicionada}
    writeln('PCI Rate: ',PCIrat :7:2,' kg/ton de gusa'); {Apresentação do PCI rate obtido}
    textcolor(15);
    writeln('PCI Rate maximo: ',PCIratmx:5:2,' Kg/ton de gusa'); {Apresentação do PCI rate máximo permitido}
    textcolor(12);
    writeln;
    writeln('Fuel Rate: ',FR:7:2,' kg/ton de gusa'); {Apresentação do Fuel rate obtido}
    writeln;
    textcolor(15);
    writeln('Volume de oxigenio consumido: ',Vo2:5:2,'Nm3/dia.'); {Apresentação do volume de oxigênio consumido}
    textcolor(12);
    writeln('Volume de sopro: ',Vazao:5:2,'Nm3/min'); {Apresentação do vazão de sopro}
    textcolor(15);
    gotoxy(1,25);
    write('Pressione [Enter] para ir a tela de gravacao de resultados.'); {Finalização da
    apresentação dos dados de entrada e dos resultados}
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
     begin                                                               
      assign(Texto,'Calculo.doc');
      opcao:='i';
      writeln('Sera criado um arquivo chamado: Calculo.doc.');
      writeln;
      textcolor(12);
      writeln('ATENCAO! Se esse arquivo já havia sido criado anteriormente e voce escolher a');
      Writeln('opcao (c)criar,na proxima pergunta, ele será sobrescrito e TODOS os dados serao');
      writeln('perdidos.');
      writeln;
      textcolor(15);
      writeln('Deseja criar um arquivo ou continuar inserindo dados no arquivo atual?');
      writeln('Opçoes:(c)criar (i)inserir):');
      readln(opcao);
      if opcao = 'c' then
       begin
        rewrite(Texto);
       end
      else
       begin
        append(Texto);
       end;
       {Formatação da apresentação dos dados de entrada e dos resultados no arquivo texto}
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
       begin
        writeln(Texto,'               Minerio ',cont,'         ',PropM[cont]:7:2,'               ',TeorM[cont]:7:2);
        cont:=cont+1;
       end;
      writeln(Texto,'                                    TEOR MEDIO DE FERRO: ',TeorMM:3:2,'%.');
      writeln(Texto);
      writeln(Texto,'-----------------------------------COQUE:---------------------------------------');
      writeln(Texto);
      writeln(Texto,'               Coque             Proporcao(%)           Teor(%)');
      cont:=1;
      while cont <=y do
       begin
        writeln(Texto,'               Coque ',cont,'           ',PropC[cont]:7:2,'                ',TeorC[cont]:7:2);
        cont:=cont+1;
       end;
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
       begin
        writeln(Texto,'                        Massa de minerio ',cont,': ',MasM[cont]:7:2,' ton.');
        cont:=cont+1;
       end;
      writeln(Texto,'                        TOTAL CARREGADO: ',MT:7:2,' toneladas por dia.');
      writeln(Texto);
      writeln(Texto,'---------------------------------COQUE------------------------------------------');
      cont:=1;
      while cont<= y do
       begin
        writeln(Texto,'                        Massa do coque ',cont,': ',MasC[cont]:7:2,' ton.');
        cont:=cont+1;
       end;
     {      end;}
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
       begin
        writeln(Texto);
        cont:=cont+1;
       end;

      if opcao='c'then
       begin
        writeln('O arquivo foi criado com sucesso!');
       end
      else
       begin
        writeln('A insercao de dados foi feita com sucesso!');
       end;
      close(Texto);
     end;
    writeln;
    writeln;
    writeln('Deseja executar o programa novamente?');
    write('Op�oes: (s)sim (n)nao?');
    readln(resp);
   end;{end do while inicial}
   clrscr;
   writeln('    Este programa foi elaborado por alunos do quarto período, do curso ');
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
