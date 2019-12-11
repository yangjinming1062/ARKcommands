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

        List<ARKCommand> ALLCommands = new List<ARKCommand>();
        string fType = "全部";
        string fMap = "0";
        string fSP = "全部";
        string fSearch = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ALLCommands = function.DatUnS();
            ALLCommands.Sort(new ARKCompare());
            lsCommandsUI.ItemsSource = new ObservableCollection<ARKCommand>(ALLCommands);
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.Owner = Application.Current.MainWindow;
            addWindow.ShowDialog();
            ALLCommands = function.DatUnS();
            Notified();
        }

        private void Notified(List<ARKCommand> ls = null)
        {
            ls = ALLCommands.Where(x => (fType == "全部" || x.Type == fType) && (fMap == "0" || x.Map == fMap)
                && (fSP == "全部" || x.Special == fSP)).ToList();
            lsCommandsUI.ItemsSource = new ObservableCollection<ARKCommand>(ls);
        }

        private void rbType_Click(object sender, RoutedEventArgs e)
        {
            if (rbSP_Func.IsChecked.HasValue && rbSP_Func.IsChecked.Value)
            {
                rbSP_All.IsChecked = true;
                fSP = "全部";
            }
            lbNum.Content = rbDragon.IsChecked.Value ? "等级" : "数量";
            lbQuality.Content = rbDragon.IsChecked.Value ? "距离" : "品质";
            IsBlue.Visibility = rbDragon.IsChecked.Value ? Visibility.Visible : Visibility.Hidden;
            fType = (e.OriginalSource as RadioButton).Content.ToString();
            if (string.IsNullOrEmpty(txtSearch.Text))
                Notified();
            else
                txtSearch.Text = "";
        }

        private void rbMap_Click(object sender, RoutedEventArgs e)
        {
            fMap = (e.OriginalSource as RadioButton).Tag.ToString();
            if (string.IsNullOrEmpty(txtSearch.Text))
                Notified();
            else
                txtSearch.Text = "";
        }

        private void rbSpecial_Click(object sender, RoutedEventArgs e)
        {
            fSP = (e.OriginalSource as RadioButton).Content.ToString();
            if(rbSP_Func.IsChecked.Value)
            {
                rbType_All.IsChecked = true;
                rbMap_All.IsChecked = true;
                fType = "全部";
                fMap = "0";
            }
            if (string.IsNullOrEmpty(txtSearch.Text))
                Notified();
            else
                txtSearch.Text = "";
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            fSearch = txtSearch.Text.ToLower();
            List<ARKCommand> ls = ALLCommands.Where(x => (x.Name.ToLower().Contains(fSearch) && IsSearchName.IsChecked.Value)
                || (x.Command.ToLower().Contains(fSearch) && IsSearchCommand.IsChecked.Value)).ToList();
            if (IsSearchAll.IsChecked.Value)
            {
                lsCommandsUI.ItemsSource = new ObservableCollection<ARKCommand>(ls);
            }
            else
            {
                Notified(ls);
            }
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
                else if (command.Contains("{0}"))
                    command = string.Format(command, txtNum.Text);
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
            ALLCommands = function.DatUnS();
            Notified();
        }

        private void SearchCB_Click(object sender, RoutedEventArgs e) => txtSearch_TextChanged(null, null);
    }
}
