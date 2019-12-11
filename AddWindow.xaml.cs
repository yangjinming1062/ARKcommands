using System.Collections.Generic;
using System.Windows;

namespace ARKcommands
{
    /// <summary>
    /// AddWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
            lsCommands = function.DatUnS();
        }

        public AddWindow(List<ARKCommand> ls, ARKCommand cur)
        {
            InitializeComponent();
            lsCommands = ls;
            lsCommands.Remove(cur);
            txtName.Text = cur.Name;
            txtCommand.Text = cur.Command;
            cmbType.Text = cur.Type;
            cmbSP.Text = cur.Special;
            cmbMap.Text = function.Transmit(cur.Map);
        }

        private List<ARKCommand> lsCommands;

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
                Map = function.UnTransmit(cmbMap.Text),
                Special = cmbSP.Text
            };
            lsCommands.Add(command);
            CLear();
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
                Add_Click(null, null);
            function.DatS(lsCommands);
            Close();
        }
    }
}
