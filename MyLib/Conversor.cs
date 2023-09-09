using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Globalization;
using System.IO.Compression;
using System.Reflection.Emit;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Image = System.Drawing.Image;
using Label = System.Windows.Forms.Label;

namespace MyLib
{
    public static class Conversor
    {
        public static String versao = "1.0.0.0";
        public static string formatoData = "dd/MM/yyyy HH:mm:ss.f";
        public static string formatoDataApenas = "yyyy-M-dd";

        public static int StringParaInteiro(Object Valor)
        {
            try
            {
                return Convert.ToInt32(Valor);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static String FormatoData(DateTime valor)
        {
            try
            {
                String res = valor.ToString(GetFormatoData());
                return res;
            }
            catch (Exception)
            {
                String res = DateTime.MinValue.ToString(GetFormatoData());
                return res;
            }
        }

        public static string GetFormatoDataDocumentos(object data)
        {
            try
            {
                DateTime dt = Conversor.GetDateTime(data);
                string result = dt.ToString("dd-MM-yyyy");
                return result;
            }
            catch (Exception)
            {
                return DateTime.MinValue.ToString("dd-MM-yyyy");
            }
        }

        public static String GetFormatoData()
        {
            return "yyyy-MM-dd";
        }
        public static Image GetImageBranca()
        {
            try
            {
                int x = 40, y = 40;
                Bitmap bmp = new Bitmap(x, y);
                using (Graphics graph = Graphics.FromImage(bmp))
                {
                    Rectangle ImageSize = new Rectangle(0, 0, x, y);
                    graph.FillRectangle(Brushes.White, ImageSize);
                }
                return (Image)bmp;
            }
            catch (Exception)
            {
                MessageBox.Show("Erro em getImagemranca");
            }
            return null;
        }

        public static int StringParaInteiro(Object Valor, bool ReturnZero)
        {
            try
            {
                return Convert.ToInt32(Valor);
            }
            catch (Exception)
            {

            }
            if (ReturnZero) return 0;
            return int.MinValue;
        }

        public static string codificaBase64(string str)
        {
            if (str == null) return null;
            byte[] conv = UnicodeEncoding.Unicode.GetBytes(str);
            string result = Convert.ToBase64String(conv);
            return result;
        }

        public static Object[] codificaBase64(object[] Objecto)
        {
            for (int i = 0; i < Objecto.Length; i++)
            {
                if (Objecto[i] != null)
                {
                    if (Objecto[i].GetType().ToString() == "System.String")
                    {
                        Objecto[i] = codificaBase64(Objecto[i].ToString());
                    }
                }
            }

            return Objecto;
        }

        public static Object[] decodificaBase64(object[] Objecto)
        {
            for (int i = 0; i < Objecto.Length; i++)
            {
                if (Objecto[i] != null)
                {
                    if (Objecto[i].GetType().ToString() == "System.String")
                    {
                        Objecto[i] = decodificaBase64(Objecto[i].ToString());
                    }
                }
            }

            return Objecto;
        }


        public static Object decodificaBase64(string str)
        {
            if (str == null) return null;
            byte[] conv = Convert.FromBase64String(str);
            Object result = UnicodeEncoding.Unicode.GetString(conv);
            return result;
        }

        public static Font GetFont(String fonte)
        {
            try
            {
                return (Font)new FontConverter().ConvertFromString(fonte);
            }
            catch (Exception)
            {
            }
            return new Font("Tahoma", 10, FontStyle.Regular);
        }

        public static Color GetColor(String cor)
        {
            try
            {
                return Color.FromName(cor);
            }
            catch (Exception)
            {
            }
            return Color.Black;
        }

        public static byte[] Imagem_Byte(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static byte[] ImagemParaByte(Object imagem)
        {
            try
            {
                Image img = (Image)imagem;
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
            catch (Exception)
            {
            }
            return new byte[0];
        }

        public static Image Byte_Imagem(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static string BytesParaString(byte[] dBytes, Encoding encoding)
        {
            try
            {
                string str = encoding.GetString(dBytes);
                return str;
            }
            catch (Exception)
            {
            }
            return "";
        }

        public static string BytesParaString(byte[] dBytes)
        {
            try
            {
                return Encoding.UTF8.GetString(dBytes);
            }
            catch (Exception)
            {

            }
            return "";
        }

        public static byte[] StringParaBytes(string str)
        {
            return Encoding.UTF8.GetBytes(str);// encoding = new System.Text.ASCIIEncoding();
            //return encoding.GetBytes(str);
        }

        public static byte[] String_Byte(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }

        public static byte[] Objecto_ByteArray(Object obj)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (var ms = new MemoryStream())
                {
                    bf.Serialize(ms, obj);
                    return ms.ToArray();
                }
            }
            catch (Exception) { }
            return new byte[1];
        }

        // Convert a byte array to an Object
        public static Object ByteArray_ToObject(byte[] arrBytes)
        {
            try
            {
                MemoryStream memStream = new MemoryStream();
                BinaryFormatter binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                Object obj = (Object)binForm.Deserialize(memStream);
                return obj;
            }
            catch
            {
                return null;
            }
        }

        public static byte[] Soap_FicheiroSoap_ParaByte(Object obj, string caminhoSoap)
        {

            try
            {
                FileStream ficheiro = new FileStream(caminhoSoap, FileMode.Create);
                SoapFormatter format = new SoapFormatter();
                format.Serialize(ficheiro, obj);
                ficheiro.Close();

                byte[] buffer = File.ReadAllBytes(caminhoSoap);
                ficheiro = new FileStream(caminhoSoap, FileMode.Open);
                ficheiro.Close();
                return buffer;

            }
            catch (Exception er)
            {
                return String_Byte("Erro ao serializar a partir do servidor:\n" + er.Message);
            }
        }

        public static byte[] Soap_FicheiroSoap_Byte(string caminhoSoap)
        {
            try
            {
                FileStream ficheiro = new FileStream(caminhoSoap, FileMode.Open);
                byte[] buffer = File.ReadAllBytes(caminhoSoap);
                ficheiro.Close();
                return buffer;
            }
            catch (Exception er)
            {
                return String_Byte("Erro ao serializar a partir do servidor:\n" + er.Message);
            }
        }

        public static Object Soap_Bytes_ParaObject(byte[] bytes)
        {
            try
            {
                Stream ficheiro = new MemoryStream(bytes);
                SoapFormatter format = new SoapFormatter();
                Object retorno = (object)format.Deserialize(ficheiro);
                ficheiro.Close();
                return retorno;
            }
            catch (Exception es)
            {
                return es.Message;
            }
        }

        public static string ConverteObjectParaJSon<T>(T obj)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(ms, obj);
                string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return jsonString;
            }
            catch
            {
                throw;
            }
        }

        public static T ConverteJSonParaObject<T>(string jsonString)
        {
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                T obj = (T)serializer.ReadObject(ms);
                return obj;
            }
            catch
            {
                throw;
            }
        }

        static String Unidade(Double valor)
        {
            if (valor <= 0) return String.Empty;
            if (valor == 1) return "Um";
            if (valor == 2) return "Dois";
            if (valor == 3) return "Três";
            if (valor == 4) return "Quatro";
            if (valor == 5) return "Cinco";
            if (valor == 6) return "Seis";
            if (valor == 7) return "Sete";
            if (valor == 8) return "Oito";
            if (valor == 9) return "Nove";
            return string.Empty;
        }

        static String Dezena(Double valor)
        {
            try
            {
                String e = "e ";
                if (valor <= 9) return Unidade(valor);
                if (valor < 100)
                {
                    if (valor == 10) return "Dez";
                    if (valor % 10 == 0) e = "";
                    if (valor == 11) return "Onze";
                    if (valor == 12) return "Doze";
                    if (valor == 13) return "Treze";
                    if (valor == 14) return "Catorze";
                    if (valor == 15) return "Quinze";
                    if (valor == 16) return "Dezasseis";
                    if (valor == 17) return "Dezassete";
                    if (valor == 18) return "Dezoito";
                    if (valor == 19) return "Dezanove";
                    char u = valor.ToString()[1];
                    double unidade = Convert.ToDouble(u.ToString());
                    if (valor >= 20 && valor < 30) return "Vinte " + e + Unidade(unidade);
                    if (valor >= 30 && valor < 40) return "Trinta " + e + Unidade(unidade);
                    if (valor >= 40 && valor < 50) return "Quarenta " + e + Unidade(unidade);
                    if (valor >= 50 && valor < 60) return "Cinquenta " + e + Unidade(unidade);
                    if (valor >= 60 && valor < 70) return "Sessenta " + e + Unidade(unidade);
                    if (valor >= 70 && valor < 80) return "Setenta " + e + Unidade(unidade);
                    if (valor >= 80 && valor < 90) return "Oitenta " + e + Unidade(unidade);
                    if (valor >= 90 && valor < 100) return "Noventa " + e + Unidade(unidade);
                }
            }
            catch (Exception)
            {
            }
            return string.Empty;
        }

        static String Centena(Double valor)
        {
            try
            {
                String e = "e";
                if (valor < 100) return Dezena(valor);
                if (valor < 1000)
                {
                    if (valor == 100) return "Cem";
                    if (valor % 100 == 0) e = "";

                    char u = valor.ToString()[0];
                    double centenas = Convert.ToDouble(u.ToString());
                    double resto = Convert.ToDouble(valor.ToString().Substring(1));

                    if (centenas == 1) return "Cento " + e + " " + Dezena(resto);
                    if (centenas == 2) return "Duzentos " + e + " " + Dezena(resto);
                    if (centenas == 3) return "Trezentos " + e + " " + Dezena(resto);
                    if (centenas == 4) return "Quatrocentos " + e + " " + Dezena(resto);
                    if (centenas == 5) return "Quinhentos " + e + " " + Dezena(resto);
                    if (centenas == 6) return "Seiscentos " + e + " " + Dezena(resto);
                    if (centenas == 7) return "Setecentos " + e + " " + Dezena(resto);
                    if (centenas == 8) return "Oitocentos " + e + " " + Dezena(resto);
                    if (centenas == 9) return "Novecentos " + e + " " + Dezena(resto);
                }
            }
            catch (Exception)
            {
            }
            return string.Empty;
        }

        static String Milhares(Double valor)
        {
            try
            {
                String e = "e ";
                if (valor < 1000) return Centena(valor);
                if (valor < 1000000)
                {
                    if (valor % 1000 == 0) e = "";

                    string v = valor.ToString();
                    int indice = v.Length - 3;
                    double parte_inicial = Convert.ToDouble(v.Substring(0, indice));
                    double parte_final = Convert.ToDouble(v.Substring(indice));

                    if (parte_inicial == 1) return "Mil " + e + Centena(parte_final);
                    return Centena(parte_inicial) + " Mil " + e + Centena(parte_final);
                }
            }
            catch (Exception)
            {
            }
            return string.Empty;
        }

        static String MilharesDeMilhares(Double valor)
        {
            try
            {
                String e = "";
                if (valor < 1000000) return Milhares(valor);
                if (valor < 1000000000)
                {
                    if (valor % 1000000 == 0) e = "";

                    string v = valor.ToString();
                    int indice = v.Length - 6;
                    double parte_inicial = Convert.ToDouble(v.Substring(0, indice));
                    double parte_final = Convert.ToDouble(v.Substring(indice));

                    if (parte_inicial == 1) return "Um Milhão " + e + Milhares(parte_final);
                    return Centena(parte_inicial) + " Milhões " + e + Milhares(parte_final);
                }
            }
            catch (Exception)
            {
            }
            return string.Empty;
        }


        static String Bilhoes(Double valor)
        {
            try
            {
                String e = "";
                if (valor < 1000000000) return MilharesDeMilhares(valor);
                if (valor < 1000000000000)
                {
                    if (valor % 1000000000 == 0) e = "";

                    string v = valor.ToString();
                    int indice = v.Length - 9;
                    double parte_inicial = Convert.ToDouble(v.Substring(0, indice));
                    double parte_final = Convert.ToDouble(v.Substring(indice));

                    if (parte_inicial == 1) return "Um Bilhão " + e + MilharesDeMilhares(parte_final);
                    return Centena(parte_inicial) + " Bilhões " + e + MilharesDeMilhares(parte_final);
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Look error: " + ex.Message);
            }
            return string.Empty;
        }

        static String Trilhoes(Double valor)
        {
            try
            {
                String e = "";
                if (valor < 1000000000000) return Bilhoes(valor);
                if (valor < 1000000000000000)
                {
                    if (valor % 1000000000000 == 0) e = "";

                    string v = valor.ToString();
                    int indice = v.Length - 12;
                    double parte_inicial = Convert.ToDouble(v.Substring(0, indice));
                    double parte_final = Convert.ToDouble(v.Substring(indice));

                    if (parte_inicial == 1) return "Um Trilhão " + e + Bilhoes(parte_final);
                    return Centena(parte_inicial) + " Trilhões " + e + Bilhoes(parte_final);
                }
                else int.Parse("erro");
            }
            catch (Exception)
            {
                return "Valor não suportado pelo sistema";
            }
            return string.Empty;
        }

        public static String Extenso(Decimal valor, string moeda)
        {
            try
            {
                String de = " de " + moeda + " ";
                if (valor < 1000000) de = " " + moeda + " ";
                String[] partes = valor.ToString().Split(',', '.');
                Double val = Convert.ToDouble(valor);
                if (partes.Length > 1)
                {
                    String centimos = " Cêntimos";
                    if (Convert.ToDouble(partes[1]) <= 0) centimos = "";
                    return (Trilhoes(Convert.ToDouble(partes[0])) + de + Trilhoes(Convert.ToDouble(partes[1])) + centimos).ToUpper();
                }
                return (Trilhoes(val) + de).ToUpper();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return string.Empty;
        }

        public static String Extenso(Decimal valor)
        {
            try
            {
                //if (valor < 1000000) de = " " + moeda + " ";
                String[] partes = valor.ToString().Split(',', '.');
                Double val = Convert.ToDouble(valor);
                if (partes.Length > 1)
                {
                    //if (Convert.ToDouble(partes[1]) <= 0) centimos = "";
                    return (Trilhoes(Convert.ToDouble(partes[0])) + Trilhoes(Convert.ToDouble(partes[1])));
                }
                return (Trilhoes(val));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return string.Empty;
        }


        public static String CapsLock(String texto, bool maiuscula)
        {
            try
            {
                if (maiuscula)
                {
                    return texto.ToUpper();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return texto.ToLower();
        }



        public static int GetNumero(Object Valor)
        {
            try
            {
                return Convert.ToInt32(Valor);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static String GetString(Object Valor)
        {
            try
            {
                return Convert.ToString(Valor);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static double GetDouble(Object Valor)
        {
            try
            {
                return Convert.ToDouble(Valor);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static double GetDoubleEscolar(Object Valor)
        {
            try
            {
                return Convert.ToDouble(Valor.ToString().Replace(".", ","));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static string GetDoubleAssinatura(Object Valor)
        {
            try
            {
                // Gets a NumberFormatInfo associated with the en-US culture.
                NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

                // Displays a value with the default separator (",").


                // Displays the same value with a blank as the separator.
                nfi.NumberDecimalSeparator = ",";
                nfi.NumberGroupSeparator = ".";
                //string x = texto.ToString("N2", nfi);
                // MessageBox.Show("Veio " + x);

                return Convert.ToDouble(Valor).ToString("N2", nfi).Trim().Replace(".", "").Replace(" ", "").Replace(",", ".");
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static int BinarioDecimal(string numeroBinario)
        {
            int expoente = 0;
            int numero;
            int soma = 0;
            string numeroInvertido = ReverteString(numeroBinario);
            for (int i = 0; i < numeroInvertido.Length; i++)
            {
                numero = Convert.ToInt32(numeroInvertido.Substring(i, 1));
                soma += numero * (int)Math.Pow(2, expoente);
                expoente++;
            }
            return soma;
        }
        public static string ReverteString(string str)
        {
            return new string(str.Reverse().ToArray());
        }

        public static decimal GetDecimal(Object Valor)
        {
            try
            {
                return Convert.ToDecimal(Valor);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static Image ImageFromBytes(Object Valor)
        {
            try
            {
                byte[] _bytes = (byte[])Valor;
                Stream str = new MemoryStream(_bytes);
                return Image.FromStream(str);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int GetMesNumerico(string Extenso)
        {
            int Resultado = 0;
            try
            {
                if (Extenso == "Janeiro")
                {
                    Resultado = 1;
                }
                if (Extenso == "Fevereiro")
                {
                    Resultado = 2;
                }
                if (Extenso == "Março")
                {
                    Resultado = 3;
                }
                if (Extenso == "Abril")
                {
                    Resultado = 4;
                }
                if (Extenso == "Maio")
                {
                    Resultado = 5;
                }
                if (Extenso == "Junho")
                {
                    Resultado = 6;
                }
                if (Extenso == "Julho")
                {
                    Resultado = 7;
                }
                if (Extenso == "Agosto")
                {
                    Resultado = 8;
                }
                if (Extenso == "Setembro")
                {
                    Resultado = 9;
                }
                if (Extenso == "Outubro")
                {
                    Resultado = 10;
                }
                if (Extenso == "Novembro")
                {
                    Resultado = 12;
                }
                if (Extenso == "Dezembro")
                {
                    Resultado = 12;
                }


            }
            catch (Exception)
            {
                return 0;
                throw;
            }
            return Resultado;
        }

        public static string GetMesExtenso(int num, bool abrev)
        {
            try
            {
                switch (num)
                {
                    case 1:
                        if (abrev) return "Jan";
                        return "Janeiro";
                    case 2:
                        if (abrev) return "Fev";
                        return "Fevereiro";
                    case 3:
                        if (abrev) return "Mar";
                        return "Março";
                    case 4:
                        if (abrev) return "Abr";
                        return "Abril";
                    case 5:
                        if (abrev) return "Mai";
                        return "Maio";
                    case 6:
                        if (abrev) return "Jun";
                        return "Junho";
                    case 7:
                        if (abrev) return "Jul";
                        return "Julho";
                    case 8:
                        if (abrev) return "Ago";
                        return "Agosto";
                    case 9:
                        if (abrev) return "Set";
                        return "Setembro";
                    case 10:
                        if (abrev) return "Out";
                        return "Outubro";
                    case 11:
                        if (abrev) return "Nov";
                        return "Novembro";
                    case 12:
                        if (abrev) return "Dez";
                        return "Dezembro";
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static DateTime GetDateTime(Object Valor)
        {
            try
            {
                DateTime Data = Convert.ToDateTime(Valor.ToString());
                string format = Data.ToString(formatoData);
                return Convert.ToDateTime(format);
            }
            catch (Exception)
            {
                return new DateTime();
            }
        }

        public static DateTime GetDate(Object Valor)
        {
            try
            {
                DateTime Data = Convert.ToDateTime(Valor.ToString());
                string format = Data.ToString(formatoDataApenas);
                return Convert.ToDateTime(format);
            }
            catch (Exception)
            {
                return new DateTime();
            }
        }

        public static DateTime GetDateTime()
        {
            try
            {
                DateTime Data = Convert.ToDateTime(DateTime.Now.ToString());
                string format = Data.ToString(formatoData);
                return Convert.ToDateTime(format);
            }
            catch (Exception)
            {
                return new DateTime();
            }
        }

        public static string PreencherNumero(int algarismos, Object numero)
        {
            try
            {
                if (numero.ToString().Length < algarismos)
                {
                    return $"{PreencherNumero(algarismos - 1, $"0{numero}")}";
                }
            }
            catch (Exception)
            {
            }
            return numero.ToString();
        }

        public static String FormatDataDiaMes(DateTime Valor)
        {
            try
            {
                return $"{Valor.Day}/{GetMesExtenso(Valor.Month, true)} {PreencherNumero(2, Valor.Hour)}h{PreencherNumero(2, Valor.Minute)}";
            }
            catch (Exception)
            {
                return Valor.ToShortDateString();
            }
        }

        public static bool GetBooleano(Object Valor)
        {
            try
            {
                return Convert.ToBoolean(Valor);
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static Image ImagemResize(Image imagem)
        {
            return ImagemResize(imagem, 400, 400);
        }

        public static Image ImagemResize(Image imagem, int Largura, int Altura)
        {
            try
            {
                Image img = imagem;
                int w = GetNumero(Largura), h = GetNumero(Altura);
                img = Conversor.Resize(img, w, h);
                return img;
            }
            catch (Exception ex) { MessageBox.Show("Olha o meu erro " + ex); }
            return null;
        }

        static Image Resize(Image image, int w, int h)
        {
            Bitmap bmp = new Bitmap(w, h);

            try
            {
                Graphics graphic = Graphics.FromImage(bmp);
                graphic.DrawImage(image, 0, 0, w, h);
                graphic.Dispose();
                return bmp;

            }
            catch
            {
                return bmp;
            }

        }

        static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        public static byte[] Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    //msi.CopyTo(gs);
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }

        public static String ZipToString(string str)
        {
            try
            {
                return Convert.ToBase64String(Zip(str));
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static String Zip4(string str)
        {
            try
            {
                byte[] res = Zip(str);
                for (int i = 0; i < 10; i++)
                {
                    str = Encoding.UTF8.GetString(res);
                    res = Zip(str);
                }
                return Convert.ToBase64String(res);
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    //gs.CopyTo(mso);
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        public static string UnzipFromString(String txt)
        {
            try
            {
                return Unzip(Convert.FromBase64String(txt));
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static string Unzip4(String txt)
        {
            try
            {
                byte[] res = Convert.FromBase64String(txt);
                for (int i = 0; i < 10; i++)
                {
                    txt = Unzip(res);
                    res = Encoding.UTF8.GetBytes(txt);
                }
                return Convert.ToBase64String(res);
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static string Compress(string s)
        {
            var bytes = Encoding.Unicode.GetBytes(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }
                return Convert.ToBase64String(mso.ToArray());
            }
        }

        public static string Decompress(string s)
        {
            var bytes = Convert.FromBase64String(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                return Encoding.Unicode.GetString(mso.ToArray());
            }
        }

        public static double PixelParaCentimetro(int Pixel)
        {
            try
            {
                return Pixel * 0.0264583333;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int CentimetroParaPixel(double Centimetro)
        {
            try
            {
                return GetNumero(Centimetro / 0.0264583333);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static Control Arrastar(Control control)
        {
            try
            {
                Point LogotipoPontoInicial = new Point();
                control.MouseDown += (ss, ee) =>
                {
                    if (ee.Button == System.Windows.Forms.MouseButtons.Left) LogotipoPontoInicial = Control.MousePosition;
                };

                control.MouseMove += (ss, ee) =>
                {
                    if (ee.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        Point temp = Control.MousePosition;
                        Point res = new Point(LogotipoPontoInicial.X - temp.X, LogotipoPontoInicial.Y - temp.Y);

                        control.Location = new Point(control.Location.X - res.X, control.Location.Y - res.Y);
                        LogotipoPontoInicial = temp;
                    }
                };
            }
            catch (Exception)
            {
            }
            return control;
        }

        public static String FonteParaString(Font fonte)
        {
            try
            {
                return new FontConverter().ConvertToString(fonte);
            }
            catch (Exception)
            {
            }
            return "";
        }

        public static Font StringParaFonte(String fonte)
        {
            try
            {
                return (Font)new FontConverter().ConvertFromString(fonte);
            }
            catch (Exception)
            {
            }
            return new Font(new FontFamily("tahoma"), 10, FontStyle.Regular);
        }

        public static byte[] ControlParaBytes(Control control)
        {
            try
            {
                if (control == null) return null;
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                bf.Serialize(ms, control);
                return ms.ToArray();
            }
            catch (Exception)
            {
                //MessageBox.Show("error: " + ex.Message);
            }
            return null;
        }

        public static Control BytesParaControl(byte[] control)
        {
            try
            {
                MemoryStream memStream = new MemoryStream();
                BinaryFormatter binForm = new BinaryFormatter();
                memStream.Write(control, 0, control.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                return (Control)binForm.Deserialize(memStream);
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static String AddEspacamento(String str, int espacos)
        {
            try
            {
                String retorno = "";
                foreach (var item in str)
                {
                    retorno += item;
                    for (int i = 0; i < espacos; i++)
                    {
                        retorno += " ";
                    }
                }
                return retorno;
            }
            catch (Exception)
            {
            }
            return str;
        }

        public static String PartirString(String str, Font font, PrintPageEventArgs e, Object limit)
        {
            try
            {
                float aa = e.Graphics.MeasureString(str, font).Width;
                int limite = Conversor.GetNumero(limit);

                if (aa > limite)
                {
                    //string x_ = 
                    return str.Substring(0, limite) + "\n" + PartirString(str.Substring(limite), font, e, limite);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return str;
        }

        public static int StringLengthParaPixels(String str)
        {
            try
            {
               Label label = new Label();
                label.Text = str;
                label.AutoSize = true;
                //float aa = pe.Graphics.MeasureString("PROGRAMMING", Font).Width;
                return label.Width;
            }
            catch (Exception)
            {
            }
            return str.Length;
        }

        public static string MarcararEsquerda(Object valor)
        {
            try
            {
                if (valor.ToString().Length >= 5) return valor.ToString();
                valor = "0" + valor;
                return MarcararEsquerda(valor, 5);
            }
            catch (Exception)
            {
                //return "bugólogo: " + ex.Message;
            }
            return valor.ToString();
        }

        public static string MarcararEsquerda(Object valor, int limite)
        {
            try
            {
                if (valor.ToString().Length >= limite) return valor.ToString();
                valor = "0" + valor;
                return MarcararEsquerda(valor, limite);
            }
            catch (Exception)
            {
                //return "bugólogo: " + ex.Message;
            }
            return valor.ToString();
        }

        public static string MarcararEsquerda(Object valor, int limite, char letra)
        {
            try
            {
                if (valor.ToString().Length >= limite) return valor.ToString();
                valor = $"{letra}{valor}";
                return MarcararEsquerda(valor, limite);
            }
            catch (Exception)
            {
                //return "bugólogo: " + ex.Message;
            }
            return valor.ToString();
        }
    }
}