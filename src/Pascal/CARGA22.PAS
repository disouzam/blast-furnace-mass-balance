program calculo_de_carga;

{Este programa realiza um cálculo de carga simplificado para um alto forno, 
feito com base nos exercícios feitos em sala de aula e, também, com base nos
conhecimentos do grupo, tanto na montagem da linha de pensamento como na
montagem do programa usando a linguagem Pascal.

O programa considera o carregamento de até 3 tipos de minérios 
(estes podem ser os minérios, pelotas e sínteres).

A partir de uma proporção em peso fixa, a carga de minérios é calculada. 
Calcula-se também o teor médio de ferro contido nela.

A carga de coque é feita de forma similar: estipula-se a proporção dos 
diferentes tipos de coque( no máximo 3) e calcula-se a massa de cada um deles 
para atender ao processo.

Pode-se calcular a quantidade de PCI máxima a se injetar, e a partir dela,
estipular um valor a ser usado.

O sopro pode ser calculado, considerando-se a necessidade de oxigênio e 
o seu teor no sopro.

Os cálculos são feitos com base nas considerações de que todo o ferro 
carregado é reduzido e se incorpora ao gusa líquido e a reação de redução da
wustita a ferro se processam integralmente a 900 °C, de forma que a
reação de redução seja:

FeO +3,12CO = Fe + 2,12 CO + CO2.

Após o uso do programa, pode-se armazenar os dados em um arquivo texto
(Calculo_de_carga.doc);

}

{
  enconding: Windows-1252 - https://www.ascii-code.com/

  "ASCII, stands for American Standard Code for Information Interchange. It's a 7-bit character code where every single bit represents a unique character. On this webpage you will find 8 bits, 256 characters, ASCII table according to Windows-1252 (code page 1252) which is a superset of ISO 8859-1 in terms of printable characters. In the range 128 to 159 (hex 80 to 9F), ISO/IEC 8859-1 has invisible control characters, while Windows-1252 has writable characters. Windows-1252 is probably the most-used 8-bit character encoding in the world."

}

{Definição da biblioteca a ser utilizada}
uses crt;

{Definição das variáveis utilizadas}
var xt,xk,g,h,MT,po2,NCcarr,feg,PCcarr,CRmax,CRmin,Pcoq,PCqu,PCI,
    TeorCM,CF,PCIesc,PCIrat,PCIratmx,FR,Vo2,Vsopro,Vazao,TeorMM,MTC,CRate:real;

    t,cont,y:integer;

    resp,opcao:char;

    Texto:text;
    PropM: array[1..3] of real;
    TeorM: array[1..3] of real;
    PropC: array[1..3] of real;
    TeorC: array[1..3] of real;
    MasM: array[1..3] of real;
    MasC: array[1..3] of real;

begin
  clrscr;

  {Modo de apresentação}
  textmode( 1 );

  {Cor do texto}
  TextColor( 15 );
  gotoxy(13,10);
  writeln('CALCULO DE CARGA');
  gotoxy(1,24);
  write('Pressione qualquer tecla para continuar.');
  readkey;
  Textmode(3);
  TextColor(15);
  resp:= 's';

  {Loop principal do programa. Através dele pode-se executar o programa diversas vezes}
  while resp ='s' do 
   begin

    {Inicialização das variáveis, evitando-se erros nas equações}

    {Massa de gusa (em toneladas)} 
    xt:=0;

    {Massa de gusa (em kilogramas)} 
    xk:=0;

    {Teor de ferro no gusa(%)}
    g:=0;

    {Teor de carbono no gusa(%)}
    h:=0;

    {Massa total de minérios carregados} 
    MT:=0;

    {Percentual de oxigênio no ar soprado}
    po2:=0;

    {Número de kmols de carbono necessário para ser carregado}
    NCcarr:=0;

    {Massa de carbono a ser carregado}
    PCcarr:=0;

    {Coke rate máximo(em kg/ton de gusa)}
    CRmax:=0;

    {Coke rate mínimo(em kg/ton de gusa)}
    CRmin:=0;

    {Massa de coque (sem o uso de PCI)-para o cálculo do Coke rate máximo}
    Pcoq:=0;

    {Massa de carbono queimado}
    PCqu:=0;

    {Massa máxima de PCI que pode ser adicionada}
    PCI:=0;

    {Teor médio de carbono fixo na carga de coques}
    TeorCM:=0;

    {Teor de carbono no PCI}
    CF:=0;

    {Massa de PCI, inferior à máxima massa permitida}
    PCIesc:=0;

    {PCI rate (em kg/ton de gusa)}
    PCIrat:=0;

    {PCI rate máximo (em kg/ton de gusa}
    PCIratmx:=0;

    {Fuel rate (em kg/ton de gusa)} 
    FR:=0;

    {Volume de oxigênio consumido(em Nm3/dia)}
    Vo2:=0;

    {Volume de sopro (em Nm3/dia)}
    Vsopro:=0;

    {Vazão do sopro(em Nm3/min)}
    Vazao:=0;

    {Teor médio de ferro na carga dos minérios}
    TeorMM:=0;

    {Massa total de coque carregado}
    MTC:=0;

    {Coque rate (em kg/ton de gusa)}
    CRate:=0;

    cont:=1;

    { Loop para inicialização dos vetores criados}
    while cont <=3 do
    begin
      {Proporção do minério (1,2 ou 3) na carga de minérios}
      PropM[cont]:=0;

      {Teor de ferro no minério (1,2 ou 3)}
      TeorM[cont]:=0;

      {Massa do minério (1,2 ou 3) carregado}
      MasM[cont]:=0;

      {Proporção do coque (1,2 ou 3)} 
      PropC[cont]:=0;

      {Teor de carbono fixo no coque (1,2 ou 3)}
      TeorC[cont]:=0;

      {Massa do coque (1,2 ou 3) carregado} 
      MasC[cont]:=0;

      cont:=cont+1;
    end;

    clrscr;
    writeln;
    textcolor(12);

    {Início da entrada de dados}
    writeln('                   QUANTIDADE E COMPOSI',#199,#195,'O DO GUSA');
    textcolor(15);
    writeln;
    writeln;
    Write('Informe a quantidade de gusa que deve ser produzido (em toneladas):');

    { Leitura da massa de gusa em toneladas}
    readln(xt);
    writeln;

    {Loop de detecção de erros na execução}
    repeat 
      writeln('Informe o teor de ferro e carbono no gusa que ser',#225,' produzido.');
      write('Teor de ferro:');

      {Leitura do teor de ferro no gusa} 
      readln(g);

      write('Teor de carbono:');
      readln(h);
      writeln;
      If g+h>100 then 
      begin
        textcolor(11);

        {Aviso de erro na digitação dos teores de ferro e carbono}
        writeln('Dados incorretos!');

        textcolor(15);
      end;
    until g+h<=100;

    clrscr;
    textcolor(12);
    writeln('                           TIPOS DE MIN',#201,'RIO');
    textcolor(15);
    writeln;
    writeln;
    repeat

      {Leitura do número de minérios (ou similares) a serem utilizados}
      writeln('Quantos tipos de min',#233,'rios ser'#227'o utilizados (m',#225,'ximo de 3)?');
      readln(t);

      writeln;

      if t > 3 then  
      begin
        textcolor(11);

        {Aviso de erro na digitação do número de minérios}
        writeln('Dado incorreto!');

        textcolor(15);
      end;
    until t<=3;
    writeln;
    writeln;
    textcolor(12);
    writeln;
    writeln('                 PROPOR',#199,#195,'O DE MIN',#201,'RIO E TEOR DE FERRO');
    textcolor(15);
    writeln;
    writeln;

    {Loop de leitura da proporção do minério na carga e do teor de ferro nele}
    repeat   
      if t=1 then
      begin

        {Leitura do teor de ferro, no caso de um só minério}
        writeln('Informe o teor de ferro no min',#233,'rio.');
        readln(TeorM[1]);

        PropM[1]:=100;
      end
      else
      begin
        cont:=1;

        {Leitura da proporção e do teor de ferro para cada minério, para o caso de mais de um minério}
        while cont<=t do 
        begin
          writeln('Informe a propor',#231,#227,'o do min',#233,'rio ',cont,' e o teor de ferro nele.');

          {Leitura da proporção para cada minério}
          write('Propor',#231,#227,'o:');
          readln(PropM[cont]);

          {Leitura do teor de ferro para cada minério}
          write('Teor de ferro:');
          readln(TeorM[cont]);

          cont:=cont+1;
          writeln;
        end;
      end;

      if PropM[1]+PropM[2]+PropM[3] <> 100 then 
      begin
        textcolor(11);

        {Aviso de erro na digitação das proporções}
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
      {Leitura do número de coques a serem utilizados}
      writeln('Quantos tipos de coque ser',#227,'o utilizados (m',#225,'ximo de 3)?');
      readln(y);
      writeln;

      if y > 3 then
      begin
        textcolor(11);

        {Aviso de erro na digitação do número de coques}
        writeln('Dado incorreto!');

        textcolor(15);
      end;

    until y<=3;
    writeln;
    writeln;
    textcolor(12);
    writeln('                          PROPOR',#199,#195,'O E TEOR DE CARBONO');
    textcolor(15);
    writeln;
    repeat
      if y=1 then
      begin
        {Leitura do teor de  carbono fixo, no caso de um só coque}
        writeln('Informe o teor de carbono no coque.');
        readln(TeorC[1]);

        PropC[1]:=100;
      end
      else
      begin
        cont:=1;

        {Leitura da proporção e do teor de carbono para cada coque, para o caso de mais de um coque}
        while cont <=y do 
        begin
          writeln('Informe a propor',#231,#227,'o do coque ',cont,' na mistura e teor de carbono fixo nele.');

          {Leitura da proporção para cada coque}
          write('Propor',#231,#227,'o:');
          readln(PropC[cont]);

          {Leitura do teor de carbono para cada coque}
          write('Teor de carbono:');
          readln(TeorC[cont]);

          cont:=cont+1;
          writeln;
        end;
      end;
      if PropC[1]+PropC[2]+PropC[3] <> 100 then
      begin
        textcolor(11);

        {Aviso de erro na digitação das proporções}
        writeln('Dados incorretos!');

        textcolor(15);
      end;
    until PropC[1]+PropC[2]+PropC[3]=100;

    clrscr;

    {Conversão de unidades para a massa de gusa: de toneladas para kilogramas}
    xk:= xt*1000;

    {Cálculo da massa de ferro no gusa (feg),em kilogramas) }
    feg:= (xk*g)/(100);

    {Cálculo da massa total de minérios carregados (em kilogramas)}
    MT:= (feg*10000)/(TeorM[1]*PropM[1]+TeorM[2]*PropM[2]+TeorM[3]*PropM[3]);
    cont:=1;

    while cont<= 3 do
    begin
      {Cálculo da massa individual de cada minério ( em kilogramas)}
      MasM[cont]:= (PropM[cont]*MT/100);

      {Conversão da massa individual de cada minério de kilogramas para toneladas} 
      MasM[cont]:=MasM[cont]/1000;
      cont:= cont+1;
    end;
    cont:=1;

    {Cálculo do número de kmols de carbono a serem carregados}
    NCcarr:=(2.12*feg)/(56)+(xk*h)/(1200);

    {Cálculo da massa de carbono a ser carregada (em kilogramas)}
    PCcarr:=NCcarr*12;

    {Cálculo do teor médio de carbono fixo na mistura de coques}
    TeorCM:=(TeorC[1]*PropC[1]+TeorC[2]*PropC[2]+TeorC[3]*PropC[3])/100;

    {peso de coque sem uso de PCI }
    Pcoq:= PCcarr/(TeorCM/100);

    cont:=1;

    {Cálculo do coke rate máximo (em kilogramas/ ton de gusa)}
    CRmax:=Pcoq/xt;

    {Cálculo da massa de carbono queimado (em kilogramas)}
    PCqu:=(0.02*feg)*(12);

    writeln;
    textcolor(12);
    Writeln('                             QUANTIDADE DE PCI');
    textcolor(15);
    writeln;
    writeln;

    {Leitura do teor de carbono fixo no PCI}
    writeln('Informe o teor de carbono no PCI:');
    readln(CF);

    {Cálculo da massa máxima de PCI permitida (em kilogramas)}
    PCI:=(PCqu)/(CF/100);

    {Cálculo do PCI rate máximo (em kilogramas/ton de gusa)}
    PCIratmx:= PCI/xt;

    {Conversão da massa máxima de PCI de kilogramas para toneladas}
    PCI:=PCI/1000;
    writeln;
    repeat
      writeln('Voc',#234,' poder',#225,' usar no m',#225,'ximo ',PCI:10:3,' toneladas de PCI.');

      {Leitura da massa de PCI adicionada}
      write('Informe um valor em toneladas:');
      readln(PCIesc);

      writeln;

      if PCIesc > PCI then
      begin
        textcolor(11);

        {Aviso de digitação de valor incorreto}
        writeln('Dado incorreto!');

        textcolor(15);
      end;

    until PCIesc <= PCI;

    {Conversão da massa de PCI adicionada de toneladas para kilogramas}
    PCIesc:=PCIesc*1000;

    {Massa total da mistura de coque a ser carregada (em kilogramas)}
    MTC:=(24*feg-PCIesc*CF+feg*150/7+xk*h)/(TeorCM);

    {Coke rate (em kilogramas/ton de gusa)}
    CRate:=MTC/xt;

    {Cálculo do coke rate mínimo (em kilogrmas/ton de gusa)}
    CRmin:=(((feg/56+(xk*h)/1200)*12)*100)/(xt*TeorCM);
    cont:=1;

    while cont<= 3 do
    begin
      {Cálculo da massa ndividual de cada coque carregado (em toneladas)}
      MasC[cont]:= (PropC[cont]*(MTC/1000))/(100);
      cont:= cont+1;
    end;

    {Cálculo do PCI rate (em kilogramas/ton de gusa)}
    PCIrat:=PCIesc/xt;

    {Cálculo do Fuel rate (em kilogramas/ton de gusa)}
    FR:= PCIrat + CRate;

    {Conversão da massa de PCI adicionada de kilogramas para toneladas}
    PCIesc:=PCIesc/1000;

    clrscr;
    textcolor(12);
    writeln('                            TEOR DE OXIG',#202,'NIO NO SOPRO');
    textcolor(15);
    writeln;
    writeln;
    writeln('Informe o teor de oxig',#234,'nio no sopro.');

    {Leitura do teor de oxigênio no sopro}
    write('Teor de oxig',#234,'nio:');
    readln(po2);

    {Cálculo do volume}
    Vo2:= 0.224*feg;

    {Cálculo do volume de ar a ser soprado (em Nm3)}
    Vsopro:= Vo2/(po2/100);

    {Cálculo da vazão de sopro (em Nm3/min)}
    Vazao:= Vsopro/1440;

    clrscr;

    {Apresentação dos dados de entrada e dos resultados}

    {Apresentação dos dados de entrada}
    cont:=1;

    {Loop para funções gráficas de apresentação dos dados de entrada}
    while cont<=80 do
    begin
      write(#205);
      cont:=cont+1;
    end;

    cont:=1;

    {Loop para funções gráficas de apresentação dos dados de entrada}
    while cont<=80 do 
    begin
      gotoxy(cont,3);
      write(#205);
      cont:=cont+1;
    end;

    cont:=1;

    {Loop para funções gráficas de apresentação dos dados de entrada}
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
    write('------------------------------------GUSA:---------------------------------------');
    textcolor(15);
    writeln('Gusa:',xt:7:2,' ton (',g:5:2,' % de Fe,',h:5:2,' % de C.).');

    {Apresentação da massa de gusa produzida 
    e dos teores presentes de ferro e carbono} 
    textcolor(12);
    writeln;

    writeln('----------------------------------MIN',#201,'RIOS:------------------------------------');
    textcolor(15);
    writeln('Min',#233,'rio           Propor',#231,#227,'o(%)           Teor(%)');
    cont:=1;

    while cont <=t do
    begin
      {Apresentação da proporção de
      cada minério na carga e do respectivo teor de ferro}
      writeln('Min',#233,'rio ',cont,'         ',PropM[cont]:7:2,'              ',TeorM[cont]:7:2);
      cont:=cont+1;
    end;

    {Apresentação do teor médio de ferro na carga}
    writeln('                     TEOR M',#201,'DIO DE FERRO: ',TeorMM:3:2,'%.');
    textcolor(12);

    writeln;
    writeln('-----------------------------------COQUE:--------------------------------------');
    textcolor(15);
    writeln('Coque             Propor',#231,#227,'o(%)           Teor(%)');

    cont:=1;

    while cont <=y do
    begin
      {Apresentação da proporção de
      cada coque na carga e do respectivo teor de carbono fixo}
      writeln('Coque ',cont,'           ',PropC[cont]:7:2,'              ',TeorC[cont]:7:2);

      cont:=cont+1;
    end;

    {Apresentação do
    teor médio de carbono fixo na carga}
    writeln('                   TEOR M',#201,'DIO DE CARBONO: ',TeorCM:3:2,'%.');

    writeln;

    {Apresentação do teor de carbono fixo do PCI}
    writeln('TEOR DE CARBONO NO PCI: ',CF:3:2,'%.');

    {Apresentação do teor de oxigênio no sopro}
    writeln('TEOR DE OXIG',#202,'NIO NO SOPRO: ',po2:3:2,'%.');

    {Apresentação da quantidade de PCI adicionada}
    writeln('QUANTIDADE DE PCI ADICIONADA: ',PCIesc:7:2,' ton.');

    gotoxy(80,25);
    readkey;
    clrscr;

    {Apresentação dos resultados}
    cont:=1;

    {Loop para funções gráficas de apresentação dos resultados}
    while cont<=80 do
    begin
      write(#205);
      cont:=cont+1;
    end;

    cont:=1;

    {Loop para funções gráficas de apresentação dos resultados}
    while cont<=80 do
    begin
      gotoxy(cont,3);
      write(#205);
      cont:=cont+1;
    end;

    cont:=1;

    {Loop para funções gráficas de apresentação dos resultados}
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
      {Apresentação da massa de cada minério carregado}
      writeln('Massa de min',#233,'rio ',cont,': ',MasM[cont]:7:2,' ton.');
      cont:=cont+1;
    end;

    {Conversão da massa total de minério carregado de kilogramas para toneladas}
    MT:=MT/1000;

    textcolor(12);

    {Massa total de minério carregado diariamente}
    writeln('TOTAL CARREGADO: ',MT:7:2,' toneladas por dia.');

    textcolor(15);
    cont:=1;

    while cont<= y do
    begin

      {Apresentação da massa de cada coque carregado}
      writeln('Massa do coque ',cont,': ',MasC[cont]:7:2,' ton.');

      cont:=cont+1;
    end;

{      end;}{??}

    MTC:=MTC/1000;

    textcolor(12);

    {Massa total de coque carregado diariamente}
    writeln('TOTAL CARREGADO: ',MTC:7:2,' toneladas por dia.');
    textcolor(15);
    textcolor(12);
    textcolor(12);

    {Apresentação do Coke rate obtido}
    writeln('Coke Rate: ', CRate:7:2,' kg/ton de gusa');

    textcolor(15);

    {Apresentação do Coke rate máximo (em uma operação ALL COKE)}
    writeln('Coke Rate m',#225,'ximo: ',CRmax:7:2,' kg/ton de gusa');

    {Apresentação do Coke rate teórico mínimo 
    (através da utilização da quantidade máxima de PCI)}
    writeln('Coke Rate m',#237,'nimo: ',CRmin:7:2,' kg/ton de gusa');

    writeln;
    textcolor(12);

    {Apresentação da massa de PCI adicionada}
    writeln('Massa do PCI: ',PCIesc:5:2,' ton');

    {Apresentação do PCI rate obtido}
    writeln('PCI Rate: ',PCIrat :7:2,' kg/ton de gusa');

    textcolor(15);

    {Apresentação do PCI rate máximo permitido}
    writeln('PCI Rate m',#225,'ximo: ',PCIratmx:5:2,' Kg/ton de gusa');

    textcolor(12);
    writeln;

    {Apresentação do Fuel rate obtido}
    writeln('Fuel Rate: ',FR:7:2,' kg/ton de gusa');

    writeln;
    textcolor(15);

    {Apresentação do volume de oxigênio consumido}
    writeln('Volume de oxig',#234,'nio consumido: ',Vo2:5:2,'Nm3/dia.');

    textcolor(12);

    {Apresentação do vazão de sopro}
    writeln('Volume de sopro: ',Vazao:5:2,'Nm3/min');

    textcolor(15);
    gotoxy(1,25);

    {Finalização da
    apresentação dos dados de entrada e dos resultados}
    write('Pressione [Enter] para ir a tela de grava',#231,#227,'o de resultados.');

    readln;
    clrscr;

    {Procedimentos para armazenamento dos dados em um arquivo texto}
    Writeln('                               GRAVA',#199,#195,'O DOS RESULTADOS');
    writeln;
    writeln;
    writeln('Deseja criar um arquivo texto com os dados obtidos?');
    write('Op',#231,#245,'es:(s)sim (n)n',#227,'o?');
    readln(resp);
    writeln;
    writeln;

    if resp = 's' then
    begin
      assign(Texto,'Calculo.doc');
      opcao:='i';
      writeln('Ser',#225,' criado um arquivo chamado: Calculo.doc.');
      writeln;
      textcolor(12);
      writeln('ATENCAO! Se esse arquivo j',#225,' havia sido criado anteriormente e voce escolher a');
      Writeln('op',#231,#227,'o (c)criar,na pr',#243,'xima pergunta, ele ser',#225,' sobrescrito e TODOS os dados ser',#227,'o');
      writeln('perdidos.');
      writeln;
      textcolor(15);
      writeln('Deseja criar um arquivo ou continuar inserindo dados no arquivo atual?');
      writeln('Op',#231,#245,'es:(c)criar (i)inserir):');
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
      writeln(Texto,'------------------------------------GUSA:---------------------------------------');
      writeln(Texto,'                             Gusa:',xt:7:2,' ton (',g:5:2,' % de Fe,',h:5:2,' % de C.).');
      writeln(Texto);
      writeln(Texto,'----------------------------------MIN',#201,'RIOS:-------------------------------------');
      writeln(Texto);
      writeln(Texto,'               Min',#233,'rio           Propor',#231,#227,'o(%)           Teor(%)');
      cont:=1;

      while cont <=t do
      begin
        writeln(Texto,'               Min',#233,'rio ',cont,'         ',PropM[cont]:7:2,'               ',TeorM[cont]:7:2);
        cont:=cont+1;
      end;

      writeln(Texto,'                                    TEOR M',#201,'DIO DE FERRO: ',TeorMM:3:2,'%.');
      writeln(Texto);
      writeln(Texto,'-----------------------------------COQUE:---------------------------------------');
      writeln(Texto);
      writeln(Texto,'               Coque             Propor',#231,#227,'o(%)           Teor(%)');

      cont:=1;
      while cont <=y do
      begin
        writeln(Texto,'               Coque ',cont,'           ',PropC[cont]:7:2,'                ',TeorC[cont]:7:2);
        cont:=cont+1;
      end;

      writeln(Texto,'                                  TEOR M',#201,'DIO DE CARBONO: ',TeorCM:3:2,'%.');
      writeln(Texto);
      writeln(Texto,'                           TEOR DE CARBONO NO PCI: ',CF:3:2,'%.');
      writeln(Texto,'                        TEOR DE OXIG',#202,'NIO NO SOPRO: ',po2:3:2,'%.');
      writeln(Texto,'                      Quantidade de PCI adicionada: ',PCIesc:7:2,' ton.');
      readkey;
      writeln(Texto);
      writeln(Texto);
      writeln(Texto);
      writeln(Texto);
      writeln(Texto,'                               RESULTADOS');
      writeln(Texto);
      writeln(Texto,'--------------------------------MIN',#201,'RIOS----------------------------------------');

      cont:=1;
      while cont<= t do
      begin
        writeln(Texto,'                        Massa de min',#233,'rio ',cont,': ',MasM[cont]:7:2,' ton.');
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
      writeln(Texto,'                        Coke Rate m',#225,'ximo: ',CRmax:7:2,' kg/ton de gusa');
      writeln(Texto,'                        Coke Rate m',#237,'nimo: ',CRmin:7:2,' kg/ton de gusa');
      writeln(Texto);

      writeln(Texto,'                        PCI Rate: ',PCIrat :7:2,' kg/ton de gusa');
      writeln(Texto,'                        PCI Rate m',#225,'ximo: ',PCIratmx:5:2,' Kg/ton de gusa');
      writeln(Texto);
      writeln(Texto,'                        Fuel Rate: ',FR:7:2,' kg/ton de gusa');
      writeln(Texto);
      writeln(Texto,'-----------------------------DADOS DE SOPRO-------------------------------------');
          writeln(Texto,'                        Volume de oxig',#234,'nio consumido: ',Vo2:5:2,'Nm3/dia.');
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
    write('Opçoes: (s)sim (n)nao?');
    readln(resp);
  end;{end do while inicial}

  clrscr;
  writeln('    Este programa foi elaborado por alunos do quarto per',#237,'odo, do curso ');
  writeln('    de Tecnologia em Metalurgia e Materiais - CEFET/ES, Abril de 2006.');
  gotoxy(1,5);
  textcolor(11);
  writeln('                               ALUNOS:');
  writeln;
  writeln('                   Daniel Figueiredo Le',#227,'o Oliveira');
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
