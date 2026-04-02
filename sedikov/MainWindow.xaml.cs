using System;
using System.Windows;

namespace sedikov
{
    /// <summary>
    /// Главное окно приложения для расчёта электрических величин по закону Ома.
    /// Позволяет вычислить силу тока, напряжение или сопротивление.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Инициализирует новый экземпляр главного окна.
        /// Выполняет начальную настройку подписей полей ввода.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            UpdateLabelsAndResultText();
        }

        /// <summary>
        /// Обработчик события изменения выбранной радиокнопки (Сила тока / Напряжение / Сопротивление).
        /// При смене режима обновляет подписи полей ввода, очищает поля и результат.
        /// </summary>
        /// <param name="sender">Источник события (радиокнопка).</param>
        /// <param name="e">Аргументы события.</param>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateLabelsAndResultText();
            lblResult.Text = "";
            txtValue1.Text = "";
            txtValue2.Text = "";
        }

        /// <summary>
        /// Обновляет текстовые подписи полей ввода и название вычисляемой величины
        /// в соответствии с выбранным режимом (ток, напряжение, сопротивление).
        /// </summary>
        private void UpdateLabelsAndResultText()
        {
            if (rbCurrent.IsChecked == true)
            {
                lblValue1.Text = "Напряжение(Вольт):";
                lblValue2.Text = "Сопротивление(Ом):";
                lblSelectedValue.Text = "Сила тока";
            }
            else if (rbVoltage.IsChecked == true)
            {
                lblValue1.Text = "Сила тока(Ампер):";
                lblValue2.Text = "Сопротивление(Ом):";
                lblSelectedValue.Text = "Напряжение";
            }
            else if (rbResistance.IsChecked == true)
            {
                lblValue1.Text = "Напряжение(Вольт):";
                lblValue2.Text = "Сила тока(Ампер):";
                lblSelectedValue.Text = "Сопротивление";
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Вычислить".
        /// Считывает значения из полей ввода, выполняет расчёт по закону Ома
        /// в зависимости от выбранного режима и выводит результат.
        /// </summary>
        /// <param name="sender">Источник события (кнопка).</param>
        /// <param name="e">Аргументы события.</param>
        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double value1 = GetDoubleValue(txtValue1.Text, GetFieldName1());
                double value2 = GetDoubleValue(txtValue2.Text, GetFieldName2());

                if (rbCurrent.IsChecked == true)
                {
                    // Расчёт силы тока: I = U / R
                    double voltage = value1;
                    double resistance = value2;

                    if (resistance == 0)
                    {
                        ShowError("Сопротивление не может быть равно нулю!");
                        return;
                    }

                    double current = voltage / resistance;
                    lblResult.Text = $"{current:F3} А";
                }
                else if (rbVoltage.IsChecked == true)
                {
                    // Расчёт напряжения: U = I * R
                    double current = value1;
                    double resistance = value2;

                    double voltage = current * resistance;
                    lblResult.Text = $"{voltage:F3} В";
                }
                else if (rbResistance.IsChecked == true)
                {
                    // Расчёт сопротивления: R = U / I
                    double voltage = value1;
                    double current = value2;

                    if (current == 0)
                    {
                        ShowError("Сила тока не может быть равна нулю!");
                        return;
                    }

                    double resistance = voltage / current;
                    lblResult.Text = $"{resistance:F3} Ом";
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Возвращает название первого поля ввода для отображения в сообщениях об ошибках.
        /// </summary>
        /// <returns>Строка с названием величины ("напряжение", "силу тока" и т.д.).</returns>
        private string GetFieldName1()
        {
            if (rbCurrent.IsChecked == true) return "напряжение";
            if (rbVoltage.IsChecked == true) return "силу тока";
            return "напряжение";
        }

        /// <summary>
        /// Возвращает название второго поля ввода для отображения в сообщениях об ошибках.
        /// </summary>
        /// <returns>Строка с названием величины ("сопротивление", "силу тока" и т.д.).</returns>
        private string GetFieldName2()
        {
            if (rbCurrent.IsChecked == true) return "сопротивление";
            if (rbVoltage.IsChecked == true) return "сопротивление";
            return "силу тока";
        }

        /// <summary>
        /// Преобразует строку в число с плавающей точкой.
        /// </summary>
        /// <param name="input">Входная строка от пользователя.</param>
        /// <param name="fieldName">Имя поля для сообщения об ошибке.</param>
        /// <returns>Числовое значение.</returns>
        /// <exception cref="Exception">Выбрасывает исключение, если строка пуста или не является числом.</exception>
        private double GetDoubleValue(string input, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception($"Введите {fieldName}!");
            }
            if (!double.TryParse(input, out double result))
            {
                throw new Exception($"Некорректное значение для {fieldName}! Введите число.");
            }
            return result;
        }

        /// <summary>
        /// Отображает сообщение об ошибке в диалоговом окне и очищает поле результата.
        /// </summary>
        /// <param name="message">Текст сообщения об ошибке.</param>
        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            lblResult.Text = "";
        }
    }
}