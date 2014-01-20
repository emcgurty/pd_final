using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel;

[DataObject(true)]
public class RegionDB
{
  
    // IsNorthAmerican
    [DataObjectMethod(DataObjectMethodType.Select)]
    public bool IsNorthAmerican(int Country_ID)
    {


            string sel = "IsNorthAmerican";
            bool boolretVal = false;
            SqlCommand cmd = new SqlCommand(sel, new SqlConnection(GlobalClass.GetConnectionString()));
            cmd.Parameters.Add(new SqlParameter("@Country_ID", Country_ID));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            if (!(cmd.ExecuteScalar() == null))
            {
                boolretVal = ((bool)(cmd.ExecuteScalar()));
            }

            cmd.Connection.Close(); 
            return boolretVal;
      
      
            
    }
    
  
  

   
}




