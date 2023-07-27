using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinhaBiblioteca
{
    public class ProcessosDoWindows
    {

        public Process[] ListarTodos()
        {
            Process[] Todos = Process.GetProcesses();
            return Todos;
        }

        public Process[] ListarEspecificos(string Aplicacao)
        {

            Process[] localByName = Process.GetProcessesByName(Aplicacao);  // Get all instances of Notepad running on the local computer.  // This will return an empty array if notepad isn't running.
            return localByName;
        }

        public static bool ProcessoAberto(string Aplicacao)
        {
            try
            {
                Process[] localByName = Process.GetProcessesByName(Aplicacao);  // Get all instances of Notepad running on the local computer.  // This will return an empty array if notepad isn't running.
                return localByName.Length > 0;
            }
            catch (Exception)
            {
            }
            return false;
        }

        public static bool ProcessoCorrente(string Aplicacao)
        {
            try
            {
                Process[] localByName = Process.GetProcessesByName(Aplicacao);  // Get all instances of Notepad running on the local computer.  // This will return an empty array if notepad isn't running.
                if (localByName.Length > 1) return true;
            }
            catch (Exception) { }
            return false;
        }

        public static void TerminarProcesso(string NomeProcesso)
        {
            try
            {
                var processes = Process.GetProcessesByName(NomeProcesso);
                foreach (var p in processes)
                    p.Kill();
            }
            catch (Exception) { }
        }

        public void TerminarEsteProcesso()
        {
            try
            {
                var processes = Process.GetCurrentProcess();
                processes.Kill();
            }
            catch (Exception) { }
        }

        public bool Windows_Iniciar(string AppName)
        {
            string runKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            Microsoft.Win32.RegistryKey startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey);

            if (startupKey.GetValue(Application.ProductName) == null)
            {
                return false;
            }

            return true;
        }

        public void Inicia_com_Windows(string AppName)
        {
            try
            {
                string runKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

                Microsoft.Win32.RegistryKey startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey);

                if (startupKey.GetValue(AppName) == null)
                {
                    startupKey.Close();
                    startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey, true);
                    // Add startup reg key
                    startupKey.SetValue(AppName, Application.ExecutablePath.ToString());
                    startupKey.Close();
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }

        }
        public void Não_Inicia_com_Windows(string AppName)
        {
            try
            {
                string runKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

                Microsoft.Win32.RegistryKey startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey);

                // remove startup
                startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey, true);
                startupKey.DeleteValue(AppName, false);
                startupKey.Close();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }



        public void SetStartup(string AppName, bool enable)
        {

            string runKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

            Microsoft.Win32.RegistryKey startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey);

            if (enable)
            {
                if (startupKey.GetValue(AppName) == null)
                {
                    startupKey.Close();
                    startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey, true);
                    // Add startup reg key
                    startupKey.SetValue(AppName, Application.ExecutablePath.ToString());
                    startupKey.Close();
                }
            }
            else
            {
                // remove startup
                startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey, true);
                startupKey.DeleteValue(AppName, false);
                startupKey.Close();
            }
        }
    }
}
