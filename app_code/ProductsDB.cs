using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel;

[DataObject(true)]
public class ProductsDB
{


    public bool FindQueueRecord(int CPID)
    {
        string sel = String.Empty;
        bool retValue = true; 
        string connectionString = ConfigurationManager.AppSettings["EMM_Connection"];
        SqlConnection myConnection = new SqlConnection(connectionString);
        

        try
        {
            bool recordReturned = false;

            using (SqlCommand cmd  = myConnection.CreateCommand())
            {

               DateTime retDateTime = DateTime.Now; 
               #region OpenConnection
                try
                {
                    myConnection.Open();
                }
                catch (SqlException se)
                {
                    System.Web.HttpContext.Current.Session["ErrorMessage"] = "Error in creating connection in FindQueueRecord: " + se.Message;
                    return false; //error
                }
                #endregion

                sel = "FindQueueRecord";
                cmd.Parameters.Add(new SqlParameter("@User_Information_ID", Convert.ToInt16(System.Web.HttpContext.Current.Session["UserVar"])));
                cmd.Parameters.Add(new SqlParameter("@CPID", CPID));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sel;

                using (SqlDataReader dr1 = cmd.ExecuteReader())
                {

                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            retDateTime = Convert.ToDateTime(dr1["Selection_Date"]);
                            recordReturned = true;
                        }

                    }
                    dr1.Close();
                }

                if ((recordReturned))
                {

                    sel = "InsertIntoQueueArchive";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@User_Information_ID", Convert.ToInt16(System.Web.HttpContext.Current.Session["UserVar"]));
                    cmd.Parameters.AddWithValue("@Selection_Date", retDateTime);
                    cmd.Parameters.AddWithValue("@CPID", CPID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sel;

                    using (SqlDataReader dr2 = cmd.ExecuteReader())
                    {
                        dr2.Close();
                    }

                    sel = "DeleteFromQueueSelection";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@User_Information_ID", Convert.ToInt16(System.Web.HttpContext.Current.Session["UserVar"]));
                    cmd.Parameters.AddWithValue("@CPID", CPID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sel;

                    using (SqlDataReader dr3 = cmd.ExecuteReader())
                    {
                        dr3.Close();
                       
                    }
                    retValue =  true;
                }
                else
                {
                    retValue =  true;
                    
                }

                myConnection.Close();  
            } 
             
            }    
        catch (SqlException se)
        {
            System.Web.HttpContext.Current.Session["ErrorMessage"] = "Error in FindQueueRecord: " + se.Message;
            retValue = false;   // Error handled
        }
        
       return retValue;
}
        

     
    public int GetRegionID(string PCID)
    {
        int intPCID;
        intPCID = Convert.ToInt32(PCID);
        string sel = "GetRegionID";
        SqlCommand cmd = new SqlCommand(sel, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.Add(new SqlParameter("@Product_Compliance_ID", intPCID));
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        if (dr.HasRows)
        {
            while (dr.Read())
            {
                intPCID = Convert.ToInt32(dr["Region_ID"].ToString());
            }
        }
        else
        {
            intPCID =  0;
        }

        dr.Close();
        cmd.Connection.Close();
        return intPCID;

    }


    
    public string GetProductName(string PID)
    {
        string returnVal = "";
        int intPID = Convert.ToInt32(PID);

        string sel = "GetProductName";
        SqlCommand cmd = new SqlCommand(sel, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.Add(new SqlParameter("@product_id", intPID));
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        if (dr.HasRows)
        {
            while (dr.Read()) 
            { 
                returnVal = dr["Product_Name"].ToString(); 
            }
        }
        dr.Close();
        cmd.Connection.Close();
        return returnVal;

    }
        
    
    public int GetProductComplianceID(int PID, int AID, int IID)
    {
        int returnVal = 0;

        string sel = "GetProductComplianceID";
        SqlCommand cmd = new SqlCommand(sel, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.Add(new SqlParameter("@agency_id", AID));
        cmd.Parameters.Add(new SqlParameter("@issue_id", IID));
        cmd.Parameters.Add(new SqlParameter("@product_id", PID));
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        if (dr.HasRows)
        {
            while (dr.Read())
            { 
                returnVal = Convert.ToInt32(dr["Product_Compliance_ID"]); 
            }
        }
        dr.Close();
        cmd.Connection.Close();
        return returnVal;

    }

}




