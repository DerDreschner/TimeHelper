using System;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using TimeHelper.Converter;
using TimeHelper.Repositories;

namespace TimeHelper {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.Name);

            InitializeComponent();

            pickedDate.SelectedDate = DateTime.Now;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void PickedDate_Changed(object sender, RoutedEventArgs e) {
            UpdateForm();
        }

        private void UpdateForm() {
            var eventEntries = EventEntryRepository.GetUptimeEntries(pickedDate.SelectedDate);

            var detailedUptimeEntries = EventEntryListToUptimeEntryListConverter.Converter(eventEntries);
            var dailyUptimeEntries = UptimeEntryListToDailyUptimeListConverter.Convert(detailedUptimeEntries);

            detailedUptime.ItemsSource = detailedUptimeEntries;
            dailyUptime.ItemsSource = dailyUptimeEntries;
        }

    }
}
