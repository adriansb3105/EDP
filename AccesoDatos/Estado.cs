using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;

namespace AccesoDatos
{
    /// <summary>
    /// Adrián Serrano
    /// 26/09/2021
    /// Clase para administrar la escrita en la bitácora y los códigos de error
    /// </summary>
    public class Estado
    {
        public static string path = @"\\gaia\AppFiles\EDP\pruebas\";
        //public static string path = @"C:\Users\adria\Documents\Projects\EDP\files\";
        public static string fotos_path = path + "fotos";
        public static string logs_path = path + "logs";
        public static string no_foto = fotos_path + "\\" + "no_foto.jpg";
        public static int ERROR_INESPERADO = -1;
        public static int CREADO = 201;
        public static int ACTUALIZADO = 202;
        public static int INDICE_DUPLICADO = 2601;
        public static int CLAVE_DUPLICADA = 2627;

        public static void ErrorBitacora(string e, string m)
        {
            Directory.SetCurrentDirectory(logs_path);
            FileStream archivo = new FileStream("Error.log", FileMode.Open, FileAccess.Write);
            archivo.Seek(0, SeekOrigin.End);
            StreamWriter sw = new StreamWriter(archivo);
            sw.WriteLine("");
            sw.WriteLine("**************************");
            sw.WriteLine(System.DateTime.Now.ToString());
            sw.WriteLine("**************************");
            sw.WriteLine("(######)   " + m + "   (######)");
            sw.WriteLine(e);
            sw.Close();
            archivo.Close();
        }
    }
}
