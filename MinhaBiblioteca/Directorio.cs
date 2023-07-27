using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinhaBiblioteca
{
    public class Directorio
    {
        public static void txt_GravarArquivo(string Nome, String Conteudo)
        {
            try
            {
                if (File.Exists(Nome)) File.Delete(Nome);
                using (StreamWriter writer = new StreamWriter(Nome, true))
                {
                    writer.Write(Conteudo);
                }
            }
            catch (Exception) { }

        }

        public static bool GravarArquivo(string Nome, byte[] Conteudo)
        {
            try
            {
                File.WriteAllBytes(Nome, Conteudo);
                return true;
            }
            catch (Exception) { }
            return false;
        }

        public static bool GravarArquivo(string Nome, String Conteudo)
        {
            try
            {
                if (File.Exists(Nome)) File.Delete(Nome);
                using (StreamWriter writer = new StreamWriter(Nome, true))
                {
                    writer.Write(Conteudo);
                }
                return true;
            }
            catch (Exception) { }
            return false;
        }


        public static bool Copiar(string origem, string destino, bool sobrepor)
        {
            try
            {
                File.Copy(origem, destino, sobrepor);
                return true;
            }
            catch (Exception) { }
            return false;
        }

        public static bool CriarDirectorio(string directorio)
        {
            try
            {
                if (!Directory.Exists(directorio))
                {
                    //Criamos um com o nome folder
                    Directory.CreateDirectory(directorio);
                    return true;
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Falha ao criar directorio.\nMotivo: " + ex.Message);
            }
            return false;
        }

        public static bool DeleteFicheiros(string directorio)
        {
            try
            {

                System.IO.DirectoryInfo d = new System.IO.DirectoryInfo(directorio);
                foreach (var item in d.GetFiles())
                {
                    item.Delete();
                }
                return true;
            }
            catch (Exception) { }
            return false;
        }

        public static FileInfo[] GetFicheiros(String directorio)
        {
            try
            {
                System.IO.DirectoryInfo d = new System.IO.DirectoryInfo(directorio);
                return d.GetFiles();
            }
            catch (Exception)
            {
                return new FileInfo[0];
            }
        }

        public static IEnumerable<String> EnumerateFiles(String directorio)
        {
            try
            {
                return Directory.EnumerateFiles(directorio);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static FileInfo[] GetFicheiros()
        {

            try
            {
                System.IO.DirectoryInfo d = new System.IO.DirectoryInfo(GetDirectorioActual());
                return d.GetFiles();
            }
            catch (Exception)
            {
                return new FileInfo[0];
            }
        }

        public static String GetDirectorioActual()
        {

            try
            {
                return System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString();
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static String GetDirectorioLocal()
        {

            try
            {
                return System.IO.Path.GetDirectoryName("C:").ToString();
            }
            catch (Exception)
            {
            }
            return null;
        }


        public static void txt_AdicionaTexto(string Nome, String Conteudo)
        {
            try
            {
                StreamWriter writer = File.AppendText(Nome);
                writer.WriteLine(Conteudo);
                writer.Close();
            }
            catch (Exception) { }
        }

        public static bool Existe(string Nome)
        {
            try
            {
                return File.Exists(Nome);
            }
            catch (Exception) { }
            return false;
        }

        public static bool Delete(string Nome)
        {
            try
            {
                File.Delete(Nome);
                return true;
            }
            catch (Exception) { }
            return false;
        }

        public static string txt_LerArquivo(string Nome)
        {
            string res = "";
            try
            {
                if (File.Exists(Nome))
                {
                    using (StreamReader Reader = new StreamReader(Nome))
                    {
                        res = Reader.ReadLine();
                    }
                }
                return res;
            }
            catch (Exception) { }
            return null;
        }

        public static string txt_LerTodoArquivo(string Nome)
        {
            string res = "";
            try
            {
                if (File.Exists(Nome))
                {
                    byte[] fl = File.ReadAllBytes(Nome);
                    res = Encoding.UTF8.GetString(fl);
                }
                return res;
            }
            catch (Exception) { }
            return null;
        }

    }
}
