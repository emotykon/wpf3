using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf_lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DoubleAnimation rotationAnim = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(3),
                RepeatBehavior = RepeatBehavior.Forever // Obrót w pętli
            };

            // Uruchamiamy animację na właściwości Angle naszej rotacji
            MyRotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, rotationAnim);
            this.DataContext = new MyViewModel();
        }
        public class RelayCommand : ICommand
        {
            private readonly Action _execute;
            public RelayCommand(Action execute) => _execute = execute;
            public bool CanExecute(object parameter) => true;
            public void Execute(object parameter) => _execute();
            public event EventHandler CanExecuteChanged;
        }

        // ViewModel - tutaj trzymasz dane i logikę
        public class MyViewModel : INotifyPropertyChanged
        {
            private string _title = "Cześć! Kliknij przycisk.";
            public string AppTitle
            {
                get => _title;
                set { _title = value; OnPropertyChanged(); }
            }

            // Definicja komendy
            public ICommand ChangeTextCommand => new RelayCommand(() => {
                AppTitle = "Komenda MVVM zadziałała!";
            });

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string name = null)
                => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void AnimatedButton_Click(object sender, RoutedEventArgs e)
        {
            // 1. Tworzymy animację: od obecnej szerokości do 300 pikseli
            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.To = 300;
            widthAnimation.Duration = TimeSpan.FromSeconds(0.5); // Czas trwania: pół sekundy

            // Opcjonalnie: efekt odbicia na końcu
            widthAnimation.EasingFunction = new ElasticEase { Oscillations = 2, Springiness = 5 };

            // 2. Uruchamiamy animację na konkretnej właściwości obiektu
            AnimatedButton.BeginAnimation(Button.WidthProperty, widthAnimation);
        }
    }
}