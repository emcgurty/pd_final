using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Text.RegularExpressions;

public partial class RegisterNewUser : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
       
        if (!(Page.IsPostBack))
        {

            Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) + "; URL=ErrorMessage.aspx?Expired=1"); 
            this.Master.TitleOne.Text = "Product Compliance";
            //this.cmbNewUserRegion.Attributes.Add("onChange", "PicklistChanged();");  Post region back
            this.cmbCountries.Attributes.Add("onChange", "LearnSelectedValue(this.value);");


            if (!(String.IsNullOrEmpty(Request.QueryString["Status"])))
            {
                if (Request.QueryString["Status"] == "New")
                {


                    string strName = string.Empty;

                    if (String.IsNullOrEmpty(Session["UserNameVar"].ToString()))
                    {
                        Session["UserNameVar"] = "New User";
                        
                    }
                    else
                    {
                        strName = Session["UserNameVar"].ToString();
                    }

                    this.Master.DisplayUserName.Text = "Welcome! " + "\r\n" + Session["UserNameVar"].ToString();
                    this.Master.MasterLeftColumn(false, false, false, false, false, false, false);
                    this.Master.TitleTwo.Text = "New User Registration";
                    this.lblNewUserSubTitle.Visible = true;
                    this.lblNewUserSubTitle.Text = ConfigurationManager.AppSettings["NewUserTitle"];
                    this.Master.DisplayUserName.Text = "Registering Guest";
                    this.cmdSubmitNewUser.Text = "Complete Registration";
                }
                else if (Request.QueryString["Status"] == "Existing")
                {
                    this.Master.DisplayUserName.Text = "Welcome! " + "\r\n" + Session["UserNameVar"].ToString();
                    this.Master.MasterLeftColumn(false, true, true, true, true, false, false);
                    this.Master.TitleTwo.Text = "Registrated User Detail Update";
                    this.lblNewUserSubTitle.Visible = true;
                    this.lblNewUserSubTitle.Text = "Here you may update your user details";
                    this.Master.DisplayUserName.Text = Session["UserNameVar"].ToString();
                    this.cmdSubmitNewUser.Text = "Update Information";


                }
                else
                {
                    Session["ErrorMessage"]  = "Incorrect Query String sent to RegisterNewUser.aspx";
                    Response.Redirect("ErrorMessage.aspx?Expired=2", false);
                    return;
                }
            }

            else if ((String.IsNullOrEmpty(Request.QueryString["Status"])))
            {
                Session["ErrorMessage"]  = "Either you have not logged onto this application or not completed its registration process.";
                Response.Redirect("ErrorMessage.aspx?Expired=2", false);
                return;
            }

            PopulateNewRegisterUser();
            PopulateWithUserDetail();
            
            
            this.divFirstLast.Attributes.Add("display", "inline");
            this.divFirstAddress.Attributes.Add("display", "inline");
            this.divCity.Attributes.Add("display", "inline");
            this.divNAUSCanada.Attributes.Add("display", "inline");
            this.divRegion.Attributes.Add("display", "inline");
            this.divZip.Attributes.Add("display", "inline");

            this.divFirstLast.Style["display"] = "none";
            this.divFirstAddress.Style["display"] = "none";
            this.divCity.Style["display"] = "none";
            this.divNAUSCanada.Style["display"] = "none";
            this.divRegion.Style["display"] = "none";
            this.divZip.Style["display"] = "none";
        }
    }

    protected void PopulateWithUserDetail()
    {

        this.lblNewUserSubTitle.Visible = true;
        int tmpID = Convert.ToInt16(Session["UserVar"]);

        string connectionString = ConfigurationManager.AppSettings["EMM_Connection"];
        SqlConnection myConnection = new SqlConnection(connectionString);

        using (SqlCommand cmd = myConnection.CreateCommand())
        {
            myConnection.Open();
            string sel = "GetUserTextInformation";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@User_Information_ID", tmpID));
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


                            this.txtUserName.Text = reader1["User_Name"].ToString().Trim();
                            this.txtemail.Text = reader1["email"].ToString().Trim();
                            this.txtFirstName.Text = reader1["First_Name"].ToString().Trim();
                            this.txtLastName.Text = reader1["Last_Name"].ToString().Trim();
                            this.txtAddress1.Text = reader1["Address1"].ToString().Trim();
                            this.txtAddress2.Text = reader1["Address2"].ToString().Trim();
                            this.txtAddress3.Text = reader1["Address3"].ToString().Trim();
                            this.txtCity.Text = reader1["City"].ToString().Trim();
                            this.cmbState.SelectedValue = reader1["State_ID"].ToString();
                            this.txtCompanyName.Text = reader1["Company_Name"].ToString().Trim();
                            this.txtPostal_Code.Text = reader1["Postal_Zip_Code"].ToString().Trim();
                            this.txtTelephone.Text = reader1["Telephone"].ToString().Trim();
                            this.txtFAX.Text = reader1["Fax"].ToString().Trim();
                            this.cmbNewUserRegion.SelectedValue = reader1["Region_ID"].ToString().Trim();
                            this.cmbNewUserSalutation.SelectedValue = reader1["Salutation_ID"].ToString().Trim();
                            UpdateCountry();
                            this.cmbCountries.SelectedValue = reader1["Country_ID"].ToString().Trim();
                            if (!(String.IsNullOrEmpty(reader1["Country_ID"].ToString().Trim())))
                            {
                                this.txtSelectedCountry.Value = reader1["Country_ID"].ToString().Trim();
                            }

                        }
                        catch (Exception e)
                        {
                            reader1.Close();
                            myConnection.Close();
                            Session["ErrorMessage"] = "An error has occurred in outputting User Information: " + e.Message;
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


    protected void PopulateNewRegisterUser()
    {
        string connectionString = ConfigurationManager.AppSettings["EMM_Connection"];
        SqlConnection myConnection = new SqlConnection(connectionString);

        try
        {
            myConnection.Open();
        }
        catch (SqlException se)
        {
            Session["ErrorMessage"]  = "Error in PopulateNewRegisterUser: " + se.Message;
            Response.Redirect("ErrorMessage.aspx", false);
            return;

        }
        using (SqlCommand sql_Cmd = myConnection.CreateCommand())
        {

            
            sql_Cmd.CommandText = "GetAllStates";
            sql_Cmd.CommandType = CommandType.StoredProcedure;

            using (SqlDataReader reader = sql_Cmd.ExecuteReader())
            {
                cmbState.DataSource = reader;
                cmbState.DataBind();

            }

            sql_Cmd.CommandText = "GetAllCountries";
            sql_Cmd.CommandType = CommandType.StoredProcedure;

            using (SqlDataReader reader = sql_Cmd.ExecuteReader())
            {
                cmbCountries.DataSource = reader;
                cmbCountries.DataBind();

            } 
            sql_Cmd.CommandText = "GetAllRegions";
            sql_Cmd.CommandType = CommandType.StoredProcedure;

            using (SqlDataReader reader = sql_Cmd.ExecuteReader())
            {
                cmbNewUserRegion.DataSource = reader;
                cmbNewUserRegion.DataBind();

            }
            sql_Cmd.CommandText = "GetAllSalutations";
            sql_Cmd.CommandType = CommandType.StoredProcedure;

            using (SqlDataReader reader = sql_Cmd.ExecuteReader())
            {
                cmbNewUserSalutation.DataSource = reader;
                cmbNewUserSalutation.DataBind();
                reader.Close();
            }
        }

        myConnection.Close();
    }


    protected void cmdSubmitNewUser_Click(object sender, EventArgs e)
    {

        int retVal;
        UserInformationDB UiDb = new UserInformationDB();
        string retStr = String.Empty;

        if (!(PerformValidation()))
        {
            // Insert new user into tbl_User
            //UserInformationDB UiDb = new UserInformationDB();
            retVal = UiDb.InsertUserInformation(Convert.ToInt16(Session["UserVar"]), this.txtCompanyName.Text, this.txtAddress1.Text,
                this.txtAddress2.Text, this.txtAddress3.Text, txtCity.Text,
                Convert.ToInt32(this.cmbState.SelectedValue),
                this.txtPostal_Code.Text, this.txtTelephone.Text,
                this.txtFAX.Text, this.txtemail.Text, Convert.ToInt32(this.txtSelectedCountry.Value),
                Convert.ToInt32(this.cmbNewUserRegion.SelectedValue),
                Convert.ToInt32(this.cmbNewUserSalutation.SelectedValue),
                this.txtFirstName.Text, this.txtLastName.Text, 0);


            try
            {
                Response.Redirect("ReadOnlyUserInformation.aspx", false);
                return;
            }
            catch (Exception ex)
            {
                UiDb = null;
                Session["ErrorMessage"] = "Error in Response.Redirect to ReadOnlyUserInformation.aspx: " + ex.Message ;
                Response.Redirect("ErrorMessage.aspx", false);
                return;
            }
        }
        else
        {

            if (!(this.txtSelectedCountry.Value.ToString() == "0"))
            {

                this.cmbCountries.SelectedValue = this.txtSelectedCountry.Value.ToString();
            }
        }
        UiDb = null;
        }
      
   protected bool PerformValidation()
    {
        bool boolFoundMissing = false;

        this.divFirstLast.Style["display"] = "none";
        this.divFirstAddress.Style["display"] = "none";
        this.divCity.Style["display"] = "none";
        this.divNAUSCanada.Style["display"] = "none";
        this.divRegion.Style["display"] = "none";
        this.divZip.Style["display"] = "none";
        
        // This validation only checks to see if a value is there.
        if (String.IsNullOrEmpty(this.txtFirstName.Text) || (String.IsNullOrEmpty(this.txtLastName.Text)))
        {
           this.divFirstLast.Style["display"] = "inline";
           boolFoundMissing = true;
        }

         if (String.IsNullOrEmpty(this.txtAddress1.Text))
        {
            this.divFirstAddress.Style["display"] = "inline";
            boolFoundMissing = true;
        }


        if (String.IsNullOrEmpty(this.txtCity.Text))
        {
            this.divCity.Style["display"] = "inline";
            boolFoundMissing = true;
        }


        if (String.IsNullOrEmpty(this.txtPostal_Code.Text))
        {
            this.divZip.Style["display"] = "inline";
            boolFoundMissing = true;
        }


        if (Convert.ToInt32(this.txtSelectedCountry.Value.ToString().Trim()) > 0)
        {

            RegionDB rdb = new RegionDB();
            bool boolretVal = rdb.IsNorthAmerican(Convert.ToInt32(this.txtSelectedCountry.Value.ToString().Trim()));
            
            if ((boolretVal) && (!(this.cmbState.SelectedIndex > 0)))
            {
                this.divNAUSCanada.Style["display"] = "inline";
                boolFoundMissing = true;
            }

            if (!(boolretVal))
            {
                this.cmbState.SelectedIndex = -1;
            }


            rdb = null;
        }


        if ((!(Convert.ToInt32(this.cmbNewUserRegion.SelectedValue)> 0)) || (!(Convert.ToInt32(this.txtSelectedCountry.Value.ToString().Trim()) > 0)))
        {
            this.divRegion.Style["display"] = "inline";
            boolFoundMissing = true;
        }


        return boolFoundMissing;

    }

   private void UpdateCountry()
    {
        string sel = "GetCountryID";
        SqlCommand cmd = new SqlCommand(sel, new SqlConnection(GlobalClass.GetConnectionString()));
        cmd.Parameters.Add(new SqlParameter("@region_id", Convert.ToInt32(cmbNewUserRegion.SelectedValue)));
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        this.cmbCountries.DataSource = dr;
        this.cmbCountries.DataBind();
        dr.Close();
        cmd.Connection.Close(); 
       
    }

    protected void BuildIfStatements()
    {

        int[] ar_region;
        string strOutput = String.Empty;
        string sel = String.Empty;
        int i = 0;
        int j = 0;
        string strSQL = String.Empty;
        string defaultval = String.Empty;
        int intRegion_ID = 0;


        string connectionString = ConfigurationManager.AppSettings["EMM_Connection"];
        SqlConnection myConnection = new SqlConnection(connectionString);


        using (SqlCommand cmd = myConnection.CreateCommand())
        {
            myConnection.Open();
            strSQL = "GetAllRegion";
            cmd.CommandText = strSQL;
            cmd.CommandType = CommandType.StoredProcedure;

            using (SqlDataReader regr1 = cmd.ExecuteReader())
            {
                i = 1;
                if (regr1.HasRows)
                {

                    while (regr1.Read())
                    {
                        i++;
                    }
                }
                regr1.Close();
            }

            ar_region = new int[i - 1];
            i = 0;

            using (SqlDataReader regr = cmd.ExecuteReader())
            {
                if (regr.HasRows)
                {
                    while (regr.Read())
                    {
                        ar_region[i] = Convert.ToInt32(regr["Region_ID"]);
                        i++;
                    }
                }
                regr.Close();
            }


            for (i = 0; i < ar_region.Length; i++)
            {
                strOutput = "if (IDval == '" + ar_region[i] + "'){";
                Response.Write(strOutput);
                strOutput = "\r\n\r\n";
                Response.Write(strOutput);
                intRegion_ID = Convert.ToInt32(ar_region[i]);

                sel = "GetCountryID";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@region_id", intRegion_ID));
                cmd.CommandText = sel;
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader ctryr = cmd.ExecuteReader())
                {
                    if (ctryr.HasRows)
                    {
                        j = 0;

                        while (ctryr.Read())
                        {
                            if (j == 0)
                            {
                                defaultval = ctryr["Country_ID"].ToString().Trim();

                            }

                            strOutput = "vCountry.options[" + j + "]=new Option('" + ctryr["Country"].ToString().Trim() + "','" + ctryr["Country_ID"].ToString().Trim() + "');";
                            Response.Write(strOutput);
                            Response.Write("\r\n");
                            j++;
                        }  //closes the WHILE

                        strOutput = "vSelected.value = " + defaultval + ";";
                        Response.Write(strOutput);
                        Response.Write("\r\n");
                        Response.Write("}");
                        Response.Write("\r\n");
                    }
                    else
                    {
                        Response.Write("}");
                        Response.Write("\r\n");
                    }

                    ctryr.Close();
                }

            }
            myConnection.Close();
        }
    }



    protected void cmbNewUserRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateCountry();
        this.txtSelectedCountry.Value = this.cmbCountries.SelectedValue.ToString();
    }
}
        


