using System.Collections.Generic;
using System.Windows;

namespace ARKcommands
{
    /// <summary>
    /// AddWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow(List<ARKCommand> ls)
        {
            InitializeComponent();
            lsCommands = ls;
        }

        public AddWindow(List<ARKCommand> ls, ARKCommand cur)
        {
            InitializeComponent();
            ls.Remove(cur);
            lsCommands = ls;
            txtName.Text = cur.Name;
            txtCommand.Text = cur.Command;
            cmbType.Text = cur.Type;
            cmbSP.Text = cur.Special;
        }

        private List<ARKCommand> lsCommands;

        private string Transmit(string name)
        {
            switch(name)
            {
                case "通用": return "0";
                case "孤岛": return "1";
                case "中心岛": return "2";
                case "焦土": return "3";
                case "畸变": return "4";
                case "仙境": return "5";
                case "灭绝": return "6";
                case "瓦尔盖罗": return "7";
            }
            return "0";
        }

        private void CLear()
        {
            txtName.Text = "";
            txtCommand.Text = "";
            //cmbType.SelectedItem = null;
            //cmbMap.SelectedItem = null;
            //cmbSP.SelectedItem = null;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
                return;
            ARKCommand command = new ARKCommand()
            {
                Name = txtName.Text.Trim(),
                Command = txtCommand.Text.Trim() + weizhui.Text,
                Type = cmbType.Text,
                Map = Transmit(cmbMap.Text),
                Special = cmbSP.Text
            };
            lsCommands.Add(command);
            CLear();
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
                Add_Click(null, null);
            function.XmlS(lsCommands);
            Close();
        }
    }
}
