using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Shell;

namespace RestartSingleInstanceApplication
{
    public class App : Application, ISingleInstanceApp
    {
        private static Mutex _singleInstanceMutex;
        private static bool _restartApp = false;
        private static string _applicationId = "62EF6B63-36EB-4B9B-A132-762A9FDA3B2E";

        [STAThread]
        public static void Main(string[] args)
        {
            // while (true)
            // {
            //     if (!SingleInstance<App>.InitializeAsFirstInstance(_applicationId))
            //     {
            //         Thread.Sleep(1000);
            //         continue;
            //     }
            //
            //     break;
            // }
            //
            // var app = new App();
            // app.Run();
            //
            // //释放 mutex && ipc channel
            // SingleInstance<App>.Cleanup();

            while (true)
            {
                _singleInstanceMutex = new Mutex(false, _applicationId, out var firstInstance);
                if (firstInstance)
                {
                    var app = new App();
                    app.Run();
                }
                else if(!_restartApp)
                {
                    return;
                }
            
                Thread.Sleep(2000);
            }
        }

        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            throw new NotImplementedException();
        }

        public App()
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }
    }
}
