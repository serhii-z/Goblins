using System;
using System.Collections.Generic;
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

namespace GoblinProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PriorityQueue queue = new PriorityQueue();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EnqueueButton_Click(object sender, RoutedEventArgs e)
        {
            int priority = Convert.ToInt32(textBox2.Text);
            if(priority == 0)
            {
                queue.Enqueue(new Goblin(textBox1.Text, priority));
            }
            else if(priority == 1)
            {
                int ind = queue.Count / 2;
                queue.Insert(ind, new Goblin(textBox1.Text, priority));
            }
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
            foreach (Goblin item in queue)
            {
                listBox.Items.Add(item.Name);
            }
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
            Goblin temp = (Goblin)queue.Dequeue();
        }
    }
}
