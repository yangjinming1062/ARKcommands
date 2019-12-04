using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
        string fType = "全部";
        string fMap = "0";
        string fSP = "全部";
        string fSearch = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ALLCommands = function.XmlUnS();
            ALLCommands.Sort(new ARKCompare());
            lsCommands = new ObservableCollection<ARKCommand>(ALLCommands);
            lsCommandsUI.ItemsSource = lsCommands;
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow(ALLCommands);
            addWindow.Owner = Application.Current.MainWindow;
            addWindow.ShowDialog();
            ALLCommands = function.XmlUnS();
            Notified();
        }

        private void Notified()
        {
            List<ARKCommand> ls = ALLCommands.Where(x => (fType == "全部" || x.Type == fType) &&
                    (fMap == "0" || x.Map == fMap) && (fSP == "全部" || x.Special == fSP) && (fSearch == "" || x.Name.Contains(fSearch))).ToList();
            ls.Sort(new ARKCompare());
            lsCommands = new ObservableCollection<ARKCommand>(ls);
            lsCommandsUI.ItemsSource = lsCommands;
        }

        private void rbType_Click(object sender, RoutedEventArgs e)
        {
            lbNum.Content = rbDragon.IsChecked.Value ? "等级" : "数量";
            lbQuality.Content = rbDragon.IsChecked.Value ? "距离" : "品质";
            fType = (e.OriginalSource as RadioButton).Content.ToString();
            Notified();
        }

        private void rbMap_Click(object sender, RoutedEventArgs e)
        {
            fMap = (e.OriginalSource as RadioButton).Tag.ToString();
            Notified();
        }

        private void rbSpecial_Click(object sender, RoutedEventArgs e)
        {
            fSP = (e.OriginalSource as RadioButton).Content.ToString();
            Notified();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            fSearch = txtSearch.Text;
            Notified();
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
                if (command.Contains("{3}"))//SpawnDino [蓝图位置] [距离] [Y轴坐标] [Z坐标] [等级]
                    command = string.Format(command, txtQuality.Text, "0", "0", txtNum.Text);
                else if (command.Contains("{2}"))
                    command = string.Format(command, txtNum.Text, txtQuality.Text, (bool)IsBlue.IsChecked ? "1" : "0");
            }
            catch { }
            txtResult.Text = command;
            Clipboard.SetText(command);
        }

        private void lsCommandsUI_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lsCommandsUI.SelectedItem == null)
                return;
            AddWindow addWindow = new AddWindow(ALLCommands, lsCommandsUI.SelectedItem as ARKCommand);
            addWindow.Owner = Application.Current.MainWindow;
            addWindow.ShowDialog();
            ALLCommands = function.XmlUnS();
            Notified();
        }
    }

    class ARKCompare : IComparer<ARKCommand>
    {
        public int Compare(ARKCommand x, ARKCommand y)
        {
            return string.Compare(x.Name, y.Name);
        }
    }
}
