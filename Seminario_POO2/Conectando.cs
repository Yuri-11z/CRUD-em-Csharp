using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Globalization;

namespace Seminario_POO2
{
    public class Conectando 
    {

        private static SQLiteConnection conexao;

        public static SQLiteConnection conexaoBanco()
        {
            conexao = new SQLiteConnection("Data Source= C:\\Users\\yuria\\OneDrive\\Área de Trabalho\\Seminario POO2\\Seminario_POO2\\Seminario_POO2\\bin\\Debug\\net8.0\\Banco_SeminarioPOO2 ");
            conexao.Open();
            return conexao;
        }
       
        






    }
}
