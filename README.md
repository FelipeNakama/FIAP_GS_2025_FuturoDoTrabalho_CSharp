# Integrantes do Grupo:
- Eduardo Mazelli RM: 553236
- Felipe Megumi Nakama RM: 552821
- Micael Santos Azarais RM: 552699


# BurnoutWatcherMonitor
BurnoutWatcherMonitor é um protótipo educacional que imagina como a tecnologia pode ajudar a prevenir burnout e promover bem‑estar no trabalho. Através de sensores simulados e uma interface simples em WPF, o sistema demonstra como dados sobre expressão facial, postura e atividade podem ser combinados para estimar níveis de estresse, gerar alertas e apoiar intervenções preventivas.

Este repositório contém o código-fonte do aplicativo WPF (.NET 8) pronto para abrir no Visual Studio. O objetivo é mostrar conceitos de POO (herança e polimorfismo), interface gráfica, persistência simples e regras de negócio aplicadas a um problema socialmente relevante.

## Visão geral do problema
No trabalho do futuro, tecnologias como IA e sensores podem oferecer sinais precoces de fadiga, estresse e risco de burnout. No entanto, essas tecnologias precisam ser usadas com responsabilidade e com foco no bem‑estar das pessoas. Este projeto prototipa uma solução local e didática para:

- coletar métricas comportamentais (facial, postura, atividade),

- agregar essas métricas em um escore de estresse,

- sinalizar níveis preocupantes para colaborador e equipe de RH,

- registrar histórico e permitir intervenção (acknowledge de alertas).

## O que este protótipo faz
Simula sensores (câmera e wearable) com sliders para gerar leituras.

- Calcula um stressScore a cada leitura usando uma fórmula transparente.

- Classifica o resultado em Low, Moderate ou High, com regras de alerta:

  - Alerta imediato quando nível é High.

  - Alerta quando Moderate ocorre 3 vezes consecutivas.

- Mantém histórico de leituras e alertas por colaborador.

- Salva/recupera os dados em um arquivo JSON local (employees.json).

- Oferece uma segunda tela (detalhe) com histórico completo e opção de “acknowledge” de alertas.

## Principais decisões de projeto
- POO: Sensor é classe abstrata; CameraSensor e WearableSensor herdam e implementam GetReading() (polimorfismo).

- Separação mínima de responsabilidades: Models, Services, Data, ViewModels e Views organizados em pastas.

- Persistência simples com System.Text.Json em AppContext.BaseDirectory.

- Interface didática: sliders para simular entradas e botões para controlar fluxo (simular, abrir detalhe, salvar).

## Fórmula usada (transparente e justificável)
stressScore = 0.5 * facial + 0.3 * posture + 0.2 * activity Valores normalizados para o intervalo 0–100. Thresholds por colaborador: Moderate (padrão 50), High (padrão 75).

Regras:

- Low: score < Moderate

- Moderate: Moderate ≤ score < High

- High: score ≥ High

Alerta:

- High → alerta imediato

- Moderate → conta como “moderate consecutivo”; ao atingir 3 consecutivos, gera alerta persistente

## Estrutura do repositório
- BurnoutWatcherMonitor.csproj — projeto WPF (.NET 8)

- App.xaml / App.xaml.cs — ponto de entrada e salvamento ao sair

- MainWindow.xaml / MainWindow.xaml.cs — dashboard principal (lista, simulação, resumo)

- Views/EmployeeDetailWindow.xaml / .xaml.cs — janela de detalhe com histórico e acknowledge

- Models/ — Employee, Sensor, CameraSensor, WearableSensor, SensorReading, Thresholds, Alert

- Services/ — IProcessor, MockAIProcessor (cálculo do score)

- Data/ — Repository (Load/Save JSON, seed)

- ViewModels/ — DashboardViewModel, EmployeeViewModel (pequeno suporte para bindings)

- Helpers/ — BaseViewModel, RelayCommand

## Como executar (passo a passo)
Requisitos:

- Visual Studio 2022 (ou superior) com suporte a .NET 8, ou .NET SDK 8 instalado.

Passos:

- Clone o repositório: git clone <URL-do-repositório>

- Abra a solução BurnoutWatcherMonitor.sln no Visual Studio.

- Compile (Build → Rebuild Solution).

- Execute o projeto (F5).

- A MainWindow abrirá com uma lista seed de colaboradores (por exemplo: Alice, Bruno, Carla).

## Como usar
- Selecione um colaborador na lista (coluna esquerda).

- Ajuste os sliders na coluna direita para simular valores de Facial, Posture e Activity.

- Clique em “Simular leitura”.

  - A leitura é adicionada ao histórico do colaborador.
  
  - O ProgressBar e o texto exibem o último stressScore e o nível.
  
  - Se as regras de alerta forem acionadas, uma MessageBox anuncia o alerta e o registro é criado.

- Clique em “Abrir detalhe” para ver todas as leituras do colaborador e os alertas.

  - Na tela de detalhe é possível marcar um alerta como tratado (Acknowledge).

- Clique em “Salvar dados” para gravar manualmente o arquivo employees.json.

- Ao fechar o aplicativo, os dados são salvos automaticamente se o Dashboard estiver ativo.

## Onde ficam os dados
O arquivo employees.json é salvo em AppContext.BaseDirectory (pasta de execução do app). É gerado automaticamente com um pequeno conjunto seed na primeira execução. Para restaurar o estado inicial, apague employees.json e reinicie o app.

## Observações importantes e limitações
- Este projeto é um protótipo didático. A “IA” aqui é um Mock (MockAIProcessor) que aplica uma fórmula simples e transparente. Não use estes resultados em decisões reais de saúde.

- Questões de privacidade, consentimento, robustez e validação científica são fundamentais antes de qualquer uso em produção, e não foram abordados aqui.

- O sistema roda localmente e não envia dados para nuvem ou APIs.
