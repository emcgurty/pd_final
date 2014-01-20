using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel;
using System.Web.SessionState;

[DataObject(true)]
public class UserInformationDB
{

    //RetainToQueue
    [DataObjectMethod(DataObjectMethodType.Insert)]
    public int RetainToQueue(DateTime Selection_Date, int CPID, int User_Information_ID)
    {

        int retVal = 0;
        string ins = "RetainToQueue";
        SqlCommand cmd = new SqlCommand(ins, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.AddWithValue("@User_Information_ID", User_Information_ID);
        cmd.Parameters.AddWithValue("@Selection_Date", Selection_Date);
        cmd.Parameters.AddWithValue("@CPID", CPID);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        retVal = cmd.ExecuteNonQuery();
        cmd.Connection.Close();
        return retVal;

    }
    
    
    
    //[GetTechnicalRepEmail]
    [DataObjectMethod(DataObjectMethodType.Select)]
    public String GetTechnicalRepEmail(int Rep_ID)
    {
        string sel = "GetTechnicalRepEmail";
        SqlCommand cmd = new SqlCommand(sel, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.Add(new SqlParameter("@Rep_ID", Rep_ID));
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        sel = "email Not Found";

        if (dr.HasRows)
        {
            while (dr.Read())
            {
                sel = dr["email"].ToString();
            }
        }
        dr.Close();
        cmd.Connection.Close();
        return sel;
    }


    //[GetTechnicalProperName]
    [DataObjectMethod(DataObjectMethodType.Select)]
    public String GetTechnicalProperName(int Rep_ID)
    {
        string sel = "GetTechnicalProperName";
        SqlCommand cmd = new SqlCommand(sel, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.Add(new SqlParameter("@Rep_ID", Rep_ID));
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        sel = "Name Not Found";
        
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                sel = dr["Full_Name"].ToString();
            }
        }
        dr.Close();
        cmd.Connection.Close();
        return sel;
    }


    //[DeleteNewCustomer]
    [DataObjectMethod(DataObjectMethodType.Delete)]
    public int DeleteNewCustomer(int CUID)
    {

        int retVal = 0;
        string ins = "DeleteNewCustomer";
        SqlCommand cmd = new SqlCommand(ins, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.AddWithValue("@Customer_ID", CUID);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        retVal = cmd.ExecuteNonQuery();
        cmd.Connection.Close();
        return retVal;
    }
    
    //[InsertNewCustomer]
    [DataObjectMethod(DataObjectMethodType.Insert)]
    public int InsertNewCustomer(string Customer_Name)
    {

        int retVal = 0;
        string ins = "InsertNewCustomer";
        SqlCommand cmd = new SqlCommand(ins, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.AddWithValue("@Customer_Name", Customer_Name);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        retVal = (int) cmd.ExecuteScalar();
        cmd.Connection.Close();
        return retVal;

    }


  
    
    [DataObjectMethod(DataObjectMethodType.Update)]
    public int DoUnBlock(string UName, string UPassword)
    {
        string sel = "DoUnBlock";
        SqlCommand cmd = new SqlCommand(sel, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.Add(new SqlParameter("@User_Name", UName));
        cmd.Parameters.Add(new SqlParameter("@User_Password", UPassword));
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        int i = cmd.ExecuteNonQuery();
        cmd.Connection.Close();
        return i;
    }



    //FindCustomerRep
    [DataObjectMethod(DataObjectMethodType.Select)]
    public bool FindCustomerRep(string UName, string UPassword)
    {

        string sel = "FindCustomerRep";
        bool boolretVal = false;

        try
        {


            SqlCommand cmd = new SqlCommand(sel, new SqlConnection(GlobalClass.GetConnectionString()));
            cmd.Parameters.Add(new SqlParameter("@User_Name", UName));
            cmd.Parameters.Add(new SqlParameter("@User_Password", UPassword));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.Read())
            {
               boolretVal = Convert.ToBoolean(dr["Is_Customer_Rep"].ToString().Trim());
            }
            dr.Close();
            cmd.Connection.Close();
           
        }
        catch //(SqlException sqex)
        {
            
            boolretVal = false;
           
        }
    
        
        return boolretVal;
    }
    
    
    [DataObjectMethod(DataObjectMethodType.Select)]
    public bool FindBlock(string UName, string UPassword)
    {

        string sel = "FindBlock";
        bool boolretVal = false; 
       
           
            SqlCommand cmd = new SqlCommand(sel, new SqlConnection(GlobalClass.GetConnectionString()));
            cmd.Parameters.Add(new SqlParameter("@User_Name", UName));
            cmd.Parameters.Add(new SqlParameter("@User_Password", UPassword));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
           
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.Read())
            {
                boolretVal = Convert.ToBoolean(dr["Is_Blocked"].ToString().Trim());
            }
            dr.Close();
            cmd.Connection.Close();
            return boolretVal;
    }

    [DataObjectMethod(DataObjectMethodType.Update)]
    public int UpdateTechnicalReps(int RID, string First_Name, string Last_Name, string email)
    {

        int RetValue = 0;
        string ins = "UpdateTechnicalReps";
        SqlCommand cmd = new SqlCommand(ins, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.AddWithValue("@Rep_ID", RID);
        cmd.Parameters.AddWithValue("@First_Name", First_Name);
        cmd.Parameters.AddWithValue("@Last_Name", Last_Name);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                RetValue =  Convert.ToInt16(dr["Rep_ID"]);
            }
        }
        dr.Close();
        cmd.Connection.Close();
        return RetValue;

    }
    
    
    [DataObjectMethod(DataObjectMethodType.Update)]
    public int UpdateCustomerInformation(int CUID, string Company_Name, string Address1, string Address2, string Address3,
                      string City, int State_ID, string Postal_Code, string Telephone,string Fax, string email, int Country_ID, int Region_ID,
                      int Salutation_ID, string First_Name, string Last_Name)	
      {
                
        string ins = "UpdateCustomerInformation";
        SqlCommand cmd = new SqlCommand(ins, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.AddWithValue("@Customer_ID", CUID);
        cmd.Parameters.AddWithValue("@Company_Name", Company_Name);
        cmd.Parameters.AddWithValue("@Address1", Address1);
        cmd.Parameters.AddWithValue("@Address2", Address2);
        cmd.Parameters.AddWithValue("@Address3", Address3);
        cmd.Parameters.AddWithValue("@City", City);
        cmd.Parameters.AddWithValue("@State_ID", State_ID);
        cmd.Parameters.AddWithValue("@Postal_Code", Postal_Code);
        cmd.Parameters.AddWithValue("@phone", Telephone);
        cmd.Parameters.AddWithValue("@Fax", Fax);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@Country_ID", Country_ID);
        cmd.Parameters.AddWithValue("@Region_ID", Region_ID);
        cmd.Parameters.AddWithValue("@Salutation_ID", Salutation_ID);
        cmd.Parameters.AddWithValue("@First_Name", First_Name);
        cmd.Parameters.AddWithValue("@Last_Name", Last_Name);
        
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        int i = cmd.ExecuteNonQuery();
        cmd.Connection.Close();
        return i;
    }

       
    [DataObjectMethod(DataObjectMethodType.Insert)]
    public int InsertUserInformation(int User_Information_ID, string Company_Name, string Address1, string Address2, string Address3,
                      string City, int State_ID, string Postal_Zip_Code, string Telephone,string Fax, string email, int Country_ID, int Region_ID,
                      int Salutation_ID, string First_Name, string Last_Name, int Blocked)	
      {

        // retVal = udb.DoUnBlock(this.txtUserName.Text.ToString().Trim(), this.txtCurrentPassword.Text.ToString().Trim()); 
        string ins = "NewUserInformation";
        SqlCommand cmd = new SqlCommand(ins, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.AddWithValue("@User_Information_ID", User_Information_ID);
        cmd.Parameters.AddWithValue("@Company_Name", Company_Name);
        cmd.Parameters.AddWithValue("@Address1", Address1);
        cmd.Parameters.AddWithValue("@Address2", Address2);
        cmd.Parameters.AddWithValue("@Address3", Address3);
        cmd.Parameters.AddWithValue("@City", City);
        cmd.Parameters.AddWithValue("@State_ID", State_ID);
        cmd.Parameters.AddWithValue("@Postal_Zip_Code", Postal_Zip_Code);
        cmd.Parameters.AddWithValue("@Telephone", Telephone);
        cmd.Parameters.AddWithValue("@Fax", Fax);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@Country_ID", Country_ID);
        cmd.Parameters.AddWithValue("@Region_ID", Region_ID);
        cmd.Parameters.AddWithValue("@Salutation_ID", Salutation_ID);
        cmd.Parameters.AddWithValue("@First_Name", First_Name);
        cmd.Parameters.AddWithValue("@Last_Name", Last_Name);
        cmd.Parameters.AddWithValue("@Is_Blocked", Blocked);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        int i = cmd.ExecuteNonQuery();
        cmd.Connection.Close();
        return i;
    }

    [DataObjectMethod(DataObjectMethodType.Delete)]
    public int DeleteUserInformation(int User_Information_ID)
    {
        string del = "DeleteUserInformation";
        SqlCommand cmd = new SqlCommand(del, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.AddWithValue("@User_Information_ID", User_Information_ID);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        int i = cmd.ExecuteNonQuery();
        cmd.Connection.Close();
        return i;
    }
    
    
    public int FindUserIDFromUserName(string UN)
    {
        string sel = "FindUserIDFromUserName";
        int retValue = 0;
        SqlCommand cmd = new SqlCommand(sel, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.AddWithValue("@UN", UN);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        if (!(cmd.ExecuteScalar() == null))
        {
            try
            {
                retValue = ((int)(cmd.ExecuteScalar()));
            }
            catch //(SqlException sexc)
            {

                retValue = - 1;
            }
        }
        cmd.Connection.Close(); 
        return retValue;
    }


    
    
    public string FindUserName(int UID)
    {
        string sel = "FindUserName";
        SqlCommand cmd = new SqlCommand(sel, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.AddWithValue("@User_Information_ID", UID);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        if (dr.Read())
        {
            sel = dr["User_Name"].ToString().Trim();
        }
        else
        {
            sel = String.Empty;
        }
        dr.Close();
        cmd.Connection.Close();
        return sel;

    }
    
    [DataObjectMethod(DataObjectMethodType.Update)]
    public int UpdateUserPassword(int User_Information_ID, string User_Password)
    {
        string up = "UpdateUserPassword";

        SqlCommand cmd = new SqlCommand(up, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.AddWithValue("User_Information_ID", User_Information_ID);
        cmd.Parameters.AddWithValue("User_Password", User_Password);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        int i = cmd.ExecuteNonQuery();
        cmd.Connection.Close();
        return i;

    }
    

    public String GetUser(string User_Name, string User_Password)
    {
        string sel = "GetUser";
        string retVal = String.Empty;
        SqlCommand cmd = new SqlCommand(sel, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.AddWithValue("User_Name", User_Name);
        cmd.Parameters.AddWithValue("User_Password", User_Password);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        if (dr.HasRows)
        {
            while (dr.Read())
                {
                retVal =  dr["User_Information_ID"].ToString().Trim();
                }
        }
        else
        {
            retVal =  "0";
        }
        
        dr.Close();
        cmd.Connection.Close();
        return retVal;
    }

    [DataObjectMethod(DataObjectMethodType.Delete)]
    public int DeleteFromQueueSelection(int User_Information_ID, int CPID)
    {
        string del = "DeleteFromQueueSelection";
        SqlCommand cmd = new SqlCommand(del, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.AddWithValue("@User_Information_ID", User_Information_ID);
        cmd.Parameters.AddWithValue("@CPID", CPID);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        int i = cmd.ExecuteNonQuery();
        return i;
    }
    
    [DataObjectMethod(DataObjectMethodType.Insert)]
    public bool InsertIntoQueue(DateTime Selection_Date, int CPID)
    {

        
        string ins = "InsertIntoQueue";
        SqlCommand cmd = new SqlCommand(ins, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.AddWithValue("@User_Information_ID", System.Web.HttpContext.Current.Session["UserVar"]);
        cmd.Parameters.AddWithValue("@Selection_Date", @Selection_Date);
        cmd.Parameters.AddWithValue("@CPID", CPID);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        cmd.ExecuteNonQuery();
        cmd.Connection.Close();
        return true;

    } 
    
    [DataObjectMethod(DataObjectMethodType.Select)]
    public string GetUserProperName(int UID)
    {
        string sel = "GetUserProperName";
        SqlDataReader dr;
        SqlCommand cmd = new SqlCommand(sel, new SqlConnection(GlobalClass.GetConnectionString()));;
        try
        {
            
            cmd.Parameters.AddWithValue("@UID", UID);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
        }
        catch //(SqlException seq)
        {
            cmd.Connection.Close(); 
            return String.Empty;
        }
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.Read())
            {
                sel =  dr["Full_Name"].ToString().Trim();
            }
            else
            {
                sel =  String.Empty;
            }

            dr.Close();
            cmd.Connection.Close();
            return sel;

    }

    //DeleteTechnicalRepQueue
    [DataObjectMethod(DataObjectMethodType.Insert)]
    public int DeleteTechnicalRepQueue(int User_Information_ID)
    {

        DateTime Activity_Date = new DateTime();
        Activity_Date = DateTime.Now;
        string ins = "DeleteTechnicalRepQueue";

        SqlCommand cmd = new SqlCommand(ins, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.AddWithValue("@User_Information_ID", User_Information_ID);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        int i = cmd.ExecuteNonQuery();
        cmd.Connection.Close();
        return i;


    }


    [DataObjectMethod(DataObjectMethodType.Insert)]
    public bool InsertUserActivity(string User_Information_ID, string Activity, int PID)
    {

        DateTime Activity_Date = new DateTime();
        Activity_Date = DateTime.Now;
        string ins = "InsertUserActivity";
        
            SqlCommand cmd = new SqlCommand(ins, new SqlConnection(GlobalClass.GetConnectionString()));
            cmd.Parameters.AddWithValue("@User_Information_ID", Convert.ToInt16(User_Information_ID));
            cmd.Parameters.AddWithValue("@Activity", Activity);
            cmd.Parameters.AddWithValue("@Activity_Date", Activity_Date);
            cmd.Parameters.AddWithValue("@Product_Declaration_ID", PID);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return true;
        
        
    }


    
    [DataObjectMethod(DataObjectMethodType.Select)]
    public int GetUserInfoFromFirstLastEmail(string First, string Last, string email)
    {
        int UID = 0 ;
        string strSQL = "GetUserInfoFromFirstLastEmail";
        SqlCommand sql_Cmd = new SqlCommand(strSQL, new SqlConnection(GlobalClass.GetConnectionString()));
        sql_Cmd.Parameters.AddWithValue("@First_Name", First);
        sql_Cmd.Parameters.AddWithValue("@Last_Name", Last);
        sql_Cmd.Parameters.AddWithValue("@email", email);
        sql_Cmd.CommandType = CommandType.StoredProcedure;
        sql_Cmd.Connection.Open();
        SqlDataReader dr = sql_Cmd.ExecuteReader(CommandBehavior.CloseConnection);
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                UID = Convert.ToInt32(dr["User_Information_ID"]);
            }
        }
        dr.Close();
        sql_Cmd.Connection.Close(); 
        return UID;

    }

    [DataObjectMethod(DataObjectMethodType.Insert)]
    public int InsertNewUser( string User_Name, string User_Password, string email, int Blocked)
    {
        string ins = "InsertNewUser";
        SqlCommand cmd = new SqlCommand(ins, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.AddWithValue("@User_Name", User_Name);
        cmd.Parameters.AddWithValue("@User_Password", User_Password);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@Is_Blocked", Blocked);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        int i = cmd.ExecuteNonQuery();
        cmd.Connection.Close();
        return i;
    }





}


