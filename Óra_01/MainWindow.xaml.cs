using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Óra_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists("jegyzetek.json"))
            {
                var jegyzetek = JsonConvert.DeserializeObject<Jegyzet[]>(File.ReadAllText("jegyzetek.json"));
                jegyzetek.ToList().ForEach(x => bejegyzesek.Items.Add(x));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = new Jegyzet(tbox.Text, "");
            bejegyzesek.Items.Add(item);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (bejegyzesek.SelectedItem != null)
            {
                (bejegyzesek.SelectedItem as Jegyzet).Tartalom = editor.Text;
            }
            
        }

        private void bejegyzesek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bejegyzesek.SelectedItem != null)
            {
                editor.Text = (bejegyzesek.SelectedItem as Jegyzet).Tartalom;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            List<Jegyzet> jegyzetek = new List<Jegyzet>();
            foreach (var item in bejegyzesek.Items)
            {
                jegyzetek.Add(item as Jegyzet);
            }

            string JsonData = JsonConvert.SerializeObject(jegyzetek);
            File.WriteAllText("jegyzetek.json", JsonData);
        }

        private void bejegyzesek_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            bejegyzesek.Items.Remove(bejegyzesek.SelectedItem);
        }
    }

    internal class Jegyzet
    {
        private string cim;
        private string tartalom;

        public Jegyzet(string cim, string tartalom)
        {
            this.Cim = cim;
            this.Tartalom = tartalom;
        }

        public string Cim { get => cim; set => cim = value; }
        public string Tartalom { get => tartalom; set => tartalom = value; }

        public override string ToString()
        {
            return Cim;
        }
    }
}
