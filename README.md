
# Teste técnico 

Este projeto faz parte do teste técnico da empresa X para desenvolvedor .NET e conta com Back-end e Front-end.

## Funcionalidades

- Informações do desenvolvedor
- Maiores desafios do desenvolvedor
- Processamento de .CSVs a partir de uma pasta informada pelo usuário

### Sobre o processamento e calculos de csv:
**Por departamento**: MesVigencia, AnoVigencia, Calculos de TotalPagar, TotalDescontos e TotalExtras.
**Por Funcionarios do departamento**: Nome, TotalReceber,Horas extras,HorasDebito, DiasFalta,Dias Extras e DiasTrabalhados.

### Atenção: Departamentos possíveis para leitura de CSV: 
- RH
- TI
- Compras
- Departamento de Operações Especiais

Seguindo o Padrão: {NOME_DO_DEPARTAMENTO}-{MES_VIGENCIA}-{ANO_VIGENCIA}.csv || Ex: Compras-Fevereiro-2022.csv


## Stack utilizada

**Front-end:** Razor, JavaScript, JQuery, Ajax

**Back-end:** .NET 6, ASP.NET CORE MVC

Demais informações sobre o Back-end:
- Biblioteca para leitura de CSVs: Csv Helper
- Arquitetura: Arquitetura Limpa com foco em DDD
- Optei por não utilizar CQRS pois não tem operação de leitura/escrita em BD
- Paralelismo para processar e ler os CSVs
  
## Demonstração
https://github.com/garodriguesfs10/desafio-auvo/assets/44370579/213eac97-051c-4fc4-aa07-3dcff149ed21

Resposta JSON:
![image](https://github.com/garodriguesfs10/desafio-auvo/assets/44370579/5b5e77d3-deb9-4985-a59a-a1db05d24b3b)


## Rodando localmente

Clone o projeto

```bash
  git clone https://github.com/garodriguesfs10/desafio-auvo.git
```

Entre no diretório do projeto

```bash
  cd .\desafio-auvo\DesafioAuvo.Web\
```

Inicie o servidor

```bash
  dotnet run 
```




