using Serilog;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SentryMissingBreadcrumbPoc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // See App.xaml.cs for setup
        private readonly ILogger logger = Log.ForContext<MainWindow>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Add_Debug(object sender, RoutedEventArgs e)
        {
            logger.Debug("This is a debug message at {Time}", DateTimeOffset.UtcNow);
        }

        private void Add_Info(object sender, RoutedEventArgs e)
        {
            logger.Information("This is a info level message at {Time}", DateTimeOffset.UtcNow);
        }

        private void Add_Error(object sender, RoutedEventArgs e)
        {
            logger.Error("This is a error level message at {Time}", DateTimeOffset.UtcNow);
        }

        private void Throw_Task(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                logger.Information("about to throw exception in task");
                throw new TestException("throw in task");
            });
        }

        private void Throw(object sender, RoutedEventArgs e)
        {
            logger.Information("about to throw exception directly");
            throw new TestException("throw directly");
        }
    }
}
