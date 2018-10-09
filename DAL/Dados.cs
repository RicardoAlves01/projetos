using System;

using System.Collections.Generic;

using System.Text;


namespace Loja.DAL

{
    class Dados
    {

        public static string StringDeConexao
        {
            get
            {
                return "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Loja;Data Source=DESKTOP-V02ORJ2\\SQLEXPRESS01";
                //return "Data Source = DESKTOP-V02ORJ2\\SQLEXPRESS01; Initial Catalog = Loja; Integrated Security = true"; 
                //return "Data Source = SRVD01001; Initial Catalog = Loja; Integrated Security = true";
                //Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Loja;Data Source=DESKTOP-V02ORJ2\SQLEXPRESS01
            }
        }
    }
}