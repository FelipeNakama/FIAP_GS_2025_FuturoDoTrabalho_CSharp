using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using BurnoutWatcherMonitor.Data;
using BurnoutWatcherMonitor.Models;
using BurnoutWatcherMonitor.Services;
using BurnoutWatcherMonitor.ViewModels;

namespace BurnoutWatcherMonitor
{
    // Janela principal do aplicativo que mostra o dashboard com a lista de colaboradores, painel de resumo e controles para simulação de leituras.
    public partial class MainWindow : Window
    {
        private ObservableCollection<Employee> _employees = new ObservableCollection<Employee>();
        private IProcessor _processor = new MockAIProcessor();
        private Employee? _selectedEmployee;

        // Inicializa a janela principal, configura valores iniciais dos sliders, registradores de eventos e associa o DataContext padrão (DashboardViewModel).
        public MainWindow()
        {
            InitializeComponent();

            sliderFacial.Value = 30;
            sliderPosture.Value = 20;
            sliderActivity.Value = 40;

            sliderFacial.ValueChanged += (s, e) => lblFacial.Text = sliderFacial.Value.ToString("F0");
            sliderPosture.ValueChanged += (s, e) => lblPosture.Text = sliderPosture.Value.ToString("F0");
            sliderActivity.ValueChanged += (s, e) => lblActivity.Text = sliderActivity.Value.ToString("F0");

            if (DesignerProperties.GetIsInDesignMode(this)) return;

            Loaded += MainWindow_Loaded;
            DataContext = new DashboardViewModel();
        }

        /// <summary>
        /// Carrega a lista de colaboradores do repositório ao iniciar a janela.
        /// Popula a coleção local de funcionários e sincroniza com o ViewModel,
        /// selecionando o primeiro colaborador se houver itens.
        /// </summary>
        /// <param name="sender">Remetente do evento Loaded.</param>
        /// <param name="e">Argumentos do evento RoutedEventArgs.</param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var list = Repository.LoadEmployees();
                _employees = new ObservableCollection<Employee>(list);
                lstEmployees.ItemsSource = _employees;

                if (_employees.Any())
                {
                    lstEmployees.SelectedIndex = 0;
                }

                if (DataContext is DashboardViewModel dvm)
                {
                    dvm.Employees.Clear();
                    foreach (var emp in _employees) dvm.Employees.Add(emp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar employees: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Manipulador de mudança de seleção na lista de colaboradores.
        /// Atualiza o colaborador selecionado e a visualização de resumo.
        /// </summary>
        /// <param name="sender">Remetente do evento SelectionChanged.</param>
        /// <param name="e">Argumentos do evento SelectionChangedEventArgs.</param>
        private void LstEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedEmployee = lstEmployees.SelectedItem as Employee;
            UpdateSelectedEmployeeView();
        }

       
        // Atualiza a exibição do colaborador selecionado no painel de resumo.
        // Mostra nome, histórico de leituras e o último stressScore calculado
        private void UpdateSelectedEmployeeView()
        {
            if (_selectedEmployee == null)
            {
                txtEmployeeName.Text = "(Selecione um colaborador)";
                lstReadings.ItemsSource = null;
                pbStress.Value = 0;
                lblStressValue.Text = "-";
                return;
            }

            txtEmployeeName.Text = _selectedEmployee.Name;
            lstReadings.ItemsSource = _selectedEmployee.Readings;

            if (_selectedEmployee.Readings != null && _selectedEmployee.Readings.Any())
            {
                var last = _selectedEmployee.Readings.Last();
                var lastStress = _processor.CalculateStressScore(last);
                pbStress.Value = lastStress;
                lblStressValue.Text = $"{lastStress:F1} ({_processor.EvaluateLevel(lastStress, _selectedEmployee.Thresholds)})";
            }
            else
            {
                pbStress.Value = 0;
                lblStressValue.Text = "-";
            }
        }

        /// <summary>
        /// Executa a simulação de leitura usando os valores dos sliders:
        /// - cria instâncias de CameraSensor e WearableSensor com os valores atuais;
        /// - obtém SensorReading de cada sensor e compõe uma leitura única para o colaborador;
        /// - adiciona a leitura ao histórico do colaborador, calcula o stressScore via IProcessor;
        /// - aplica regras de alerta (High imediato; Moderate persistente N=3);
        /// - atualiza a UI e persiste alterações na coleção em memória.
        /// </summary>
        /// <param name="sender">Remetente do evento Click.</param>
        /// <param name="e">Argumentos do evento RoutedEventArgs.</param>
        private void BtnSimulate_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedEmployee == null)
            {
                MessageBox.Show("Selecione primeiro um colaborador na lista.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var camera = new CameraSensor("cam1", "desk") { FacialScore = sliderFacial.Value };
            var wearable = new WearableSensor("wear1", "desk") { PostureScore = sliderPosture.Value, ActivityScore = sliderActivity.Value };

            var rCam = camera.GetReading();
            var rWear = wearable.GetReading();

            var reading = new SensorReading
            {
                EmployeeId = _selectedEmployee.Id,
                Timestamp = DateTime.Now,
                FacialScore = rCam.FacialScore,
                PostureScore = rWear.PostureScore,
                ActivityScore = rWear.ActivityScore
            };

            _selectedEmployee.Readings.Add(reading);
            UpdateSelectedEmployeeView();

            var stress = _processor.CalculateStressScore(reading);
            var level = _processor.EvaluateLevel(stress, _selectedEmployee.Thresholds);

            if (level == "High")
            {
                var alert = new Alert { Timestamp = DateTime.Now, StressScore = stress, Level = level };
                _selectedEmployee.Alerts.Add(alert);
                MessageBox.Show($"ALERTA: {_selectedEmployee.Name} - High stress ({stress:F1})", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                _selectedEmployee.ConsecutiveModerateCount = 0;
            }
            else if (level == "Moderate")
            {
                _selectedEmployee.ConsecutiveModerateCount++;
                if (_selectedEmployee.ConsecutiveModerateCount >= 3)
                {
                    var alert = new Alert { Timestamp = DateTime.Now, StressScore = stress, Level = level };
                    _selectedEmployee.Alerts.Add(alert);
                    MessageBox.Show($"Alerta: {_selectedEmployee.Name} - Moderate persistente ({stress:F1})", "Alerta", MessageBoxButton.OK, MessageBoxImage.Information);
                    _selectedEmployee.ConsecutiveModerateCount = 0;
                }
            }
            else
            {
                _selectedEmployee.ConsecutiveModerateCount = 0;
            }

            lstEmployees.Items.Refresh();
            lstReadings.Items.Refresh();
            UpdateSelectedEmployeeView();
        }

        /// <summary>
        /// Abre a janela de detalhe do colaborador selecionado, atribuindo um EmployeeViewModel como DataContext.
        /// </summary>
        /// <param name="sender">Remetente do evento Click.</param>
        /// <param name="e">Argumentos do evento RoutedEventArgs.</param>
        private void BtnOpenDetail_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedEmployee == null)
            {
                MessageBox.Show("Selecione um colaborador primeiro.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var vm = new ViewModels.EmployeeViewModel(_selectedEmployee);
            var detail = new Views.EmployeeDetailWindow
            {
                DataContext = vm
            };
            detail.Show();
        }

        /// <summary>
        /// Salva manualmente a coleção de colaboradores no repositório persistente (employees.json).
        /// </summary>
        /// <param name="sender">Remetente do evento Click.</param>
        /// <param name="e">Argumentos do evento RoutedEventArgs.</param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Repository.SaveEmployees(_employees.ToList());
                MessageBox.Show("Dados salvos.", "Salvar", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
