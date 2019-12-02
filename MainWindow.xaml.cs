using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ARKcommands
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        ObservableCollection<ARKCommand> lsCommands = new ObservableCollection<ARKCommand>();
        List<ARKCommand> ALLCommands = new List<ARKCommand>();
        string filterType = "全部";
        string filterMap = "通用";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ALLCommands = function.XmlUnS();
            lsCommandsUI.ItemsSource = lsCommands;
        }

        private void notified(IEnumerable<ARKCommand> ls = null)
        {
            if (ls == null)
                ls = filterType == "全部" && filterMap == "通用" ? ALLCommands :
                    ALLCommands.Where(x => (filterType == "全部" || x.Type == filterType) && (filterMap == "通用" || x.Map == filterMap) && string.IsNullOrEmpty(x.Special));
            lsCommands.Clear();
            foreach(var ark in ls)
            {
                lsCommands.Add(ark);
            }
        }

        private void rbType_Click(object sender, RoutedEventArgs e)
        {
            filterType = (e.OriginalSource as RadioButton).Content.ToString();
            //rbMapAll.IsChecked = true;
            //filterMap = "通用";
            notified();
        }

        private void rbMap_Click(object sender, RoutedEventArgs e)
        {
            filterMap = (e.OriginalSource as RadioButton).Tag.ToString();
            //rbTypeAll.IsChecked = true;
            //filterType = "全部";
            notified();
        }

        private void rbSpecial_Click(object sender, RoutedEventArgs e)
        {
            notified(ALLCommands.Where(x => x.Special == (e.OriginalSource as RadioButton).Content.ToString()));
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow(ALLCommands);
            addWindow.Owner = Application.Current.MainWindow;
            addWindow.ShowDialog();
            ALLCommands = function.XmlUnS();
            notified();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
                notified(ALLCommands.Where(x => x.Name.Contains(txtSearch.Text)));
        }

        private void lsCommandsUI_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsCommandsUI.SelectedItem == null)
            {
                txtResult.Text = "";
                return;
            }
            string command = (lsCommandsUI.SelectedItem as ARKCommand).Command;
            try
            {
                command = string.Format(command, txtNum.Text, txtQuality.Text, (bool)IsBlue.IsChecked ? "1" : "0");
            }
            catch(Exception ex)
            {

            }
            txtResult.Text = command;
            Clipboard.SetText(command);
        }
    }
}
