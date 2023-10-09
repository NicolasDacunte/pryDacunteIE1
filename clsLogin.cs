using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace pryDacunteIE1
{
    
    
        
        internal class clsLogin
        {
            OleDbConnection connectionBD;
            public void abrirBd()
            {

                connectionBD = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source= C:\Users\Alumno\Source\Repos\pryDacunteIE1\Resources");
                connectionBD.Open();
                
            }
           
        }

    
}
