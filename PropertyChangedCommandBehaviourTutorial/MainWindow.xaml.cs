using System.Windows;

namespace PropertyChangedCommandBehaviourTutorial
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}