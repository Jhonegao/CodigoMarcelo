using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //using (Serializer<Cliente> ss = new Serializer<Cliente>())
            //{
            //    ss.Insert(new Cliente()
            //    {
            //        CNH = "908098908",
            //        CPF = "897897987",
            //        Nome = "iojfdsjfds"
            //    });
            //}
        }
    }
    class Serializer<T> : IDisposable where T : class, new()
    {
        private BinaryFormatter formatter = new BinaryFormatter();
        private FileStream fs = null;

        public Serializer()
        {
            fs = new FileStream(typeof(T).Name + ".txt", FileMode.OpenOrCreate);
        }

        public void Insert(T item)
        {
            List<T> data = GetData();
            if (data.Count == 0)
            {
                data = new List<T>();
            }
            data.Add(item);
            formatter.Serialize(fs, data);
        }

        public List<T> GetData()
        {
            if (fs.Length == 0)
            {
                return new List<T>();
            }
            List<T> data = formatter.Deserialize(fs) as List<T>;
            return data;
        }

        public void Dispose()
        {
            this.fs.Dispose();
        }
    }

}
