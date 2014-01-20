using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net.Mail;

public partial class ReadOnlyUserInformation : System.Web.UI.Page
{
   
   

    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        
        if (!(Page.IsPostBack))
        {
            Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) + "; URL=ErrorMessage.aspx?Expired=1"); 
            this.Master.TitleOne.Text = "PDs";
            this.Master.TitleTwo.Text = "User Information Update Successful";
            if (String.IsNullOrEmpty(Session["UserNameVar"].ToString()))
            {
                
                this.Master.DisplayUserName.Text = "Welcome! " + "\r\n";
            }
            else
            {
                this.Master.DisplayUserName.Text = "Welcome! " + "\r\n" + Session["UserNameVar"].ToString();
            }
            
            
            PopulateWithNewUserDetail();
           
           }

        }
    
    protected void PopulateWithNewUserDetail()
    {

        string connectionString = ConfigurationManager.AppSettings["EMM_Connection"];
        SqlConnection myConnection = new SqlConnection(connectionString);

        using (SqlCommand cmd = myConnection.CreateCommand())
        {
        
            myConnection.Open();
            string sel = "GetUserInformationReadOnly";
            cmd.Parameters.AddWithValue("User_Information_ID", Convert.ToInt16(Session["UserVar"]));
            cmd.CommandText = sel;
            cmd.CommandType = CommandType.StoredProcedure;

            using (SqlDataReader reader1 = cmd.ExecuteReader())
            {
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        try
                        {
                            this.txtFirstName.Text = reader1["First_Name"].ToString().Trim();
                            this.txtLastName.Text = reader1["Last_Name"].ToString().Trim();
                            this.txtAddress1.Text = reader1["Address1"].ToString().Trim();
                            this.txtAddress2.Text = reader1["Address2"].ToString().Trim();
                            this.txtAddress3.Text = reader1["Address3"].ToString().Trim();
                            this.txtCity.Text = reader1["City"].ToString().Trim();
                            this.cmbState.Text = reader1["State_Name"].ToString().Trim();
                            this.txtCompanyName.Text = reader1["Company_Name"].ToString().Trim();
                            this.txtemail.Text = reader1["email"].ToString().Trim();
                            this.txtPostal_Code.Text = reader1["Postal_Zip_Code"].ToString().Trim();
                            this.txtTelephone.Text = reader1["Telephone"].ToString().Trim();
                            this.txtFAX.Text = reader1["Fax"].ToString().Trim();
                            this.cmbNewUserRegion.Text = reader1["Region"].ToString().Trim();
                            this.cmbNewUserSalutation.Text = reader1["Salutation_Text"].ToString().Trim();
                            this.cmbCountries.Text = reader1["Country"].ToString().Trim();

                        }
                        catch (Exception e)
                        {

                            reader1.Close();
                            myConnection.Close();
                            Session["ErrorMessage"] = "Error in loading User Read Only Information: " + e.Message;
                            Response.Redirect("ErrorMessage.aspx", false);
                            return;
                        }
                    }
                }
                reader1.Close();
            }
            
            myConnection.Close();
        }
    }



    
    
      protected void cmdSubmitNewUser_Click(object sender, EventArgs e)
    {

         
         if (this.cmdSubmitNewUser.Text == "Begin Using Application")
         {
             UserInformationDB udb = new UserInformationDB();
             try
             {
                 Session["UserNameVar"] = udb.GetUserProperName(Convert.ToInt16(Session["UserVar"]));
             }
             catch
             {
                 udb = null; 
                 Session["ErrorMessage"] = "GetUserProperName Not building";
                 Response.Redirect("ErrorMessage.aspx", false);
                 return;


             }

             udb = null;
             try
             {
                 Response.Redirect("ProductSearchCriteria.aspx", false);
                 return;
             }
             catch (Exception ex)
             {
                 Session["ErrorMessage"]  = "Error in Response.Redirect to ProductSearchCriteria.aspx: " + ex.Message ;
                 Response.Redirect("ErrorMessage.aspx", false);
                 return;
             }
             
         }
          
       
        }
    
    
}
        


