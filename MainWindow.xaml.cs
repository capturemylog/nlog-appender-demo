using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Windows;
using NLog;

namespace CaptureMyLog.NLog.FunctionalTests
{
    public partial class MainWindow : Window
    {
        public static readonly Logger _LOGGER = LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void buttonGenerateSqlException_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=ThisDbDoesNotExist;Integrated Security=True"))
                {
                    connection.Open();
                }
            }
            catch (Exception exception)
            {
                _LOGGER.Fatal(exception.Message);
            }
        }

        private void buttonGenerateUnauthorizedAccessException_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string contentOfFile = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            }
            catch (UnauthorizedAccessException exception)
            {
                _LOGGER.Error(exception.Message, exception);
            }
        }

        private void buttonGenerateNullReferenceException_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string value = null;
                if (value.Length == 0) { }
            }
            catch (NullReferenceException exception)
            {
                _LOGGER.Error(exception.Message);

            }
        }

        private void buttonLogOnInfoLevel_Click(object sender, RoutedEventArgs e)
        {
            _LOGGER.Warn("fyi: all your base are belong to us!");
        }

        private void buttonLogOnDebugLevel_Click(object sender, RoutedEventArgs e)
        {
            _LOGGER.Debug("cannot debug, just took an arrow 2 da knee!");
        }

        private void buttonLogInfo_Click(object sender, RoutedEventArgs e)
        {
            _LOGGER.Info(this.textBoxCustomMessage.Text);
            this.textBoxCustomMessage.Text = string.Empty;
        }
    }
}
