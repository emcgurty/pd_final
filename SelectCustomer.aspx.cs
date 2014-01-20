using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Text.RegularExpressions;

public partial class SelectCustomer : System.Web.UI.Page
{

    bool DontDoAnthing = true;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(Page.IsPostBack))
        {

            Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) + "; URL=ErrorMessage.aspx?Expired=1");
            this.lblNewUserSubTitle.Visible = true;
            this.lblNewUserSubTitle.Text = "Here the Technical Service Representative identifies him/herself and provides required details of that valued CG customer";
            this.divRepName.Style["display"] = "none";
            this.Master.TitleOne.Text = "Product Compliance";
            this.divFirstLast.Attributes.Add("display", "inline");
            this.divFirstAddress.Attributes.Add("display", "inline");
            this.divCompanyName.Attributes.Add("display", "inline");
            this.divCity.Attributes.Add("display", "inline");
            this.divNAUSCanada.Attributes.Add("display", "inline");
            this.divRegion.Attributes.Add("display", "inline");
            this.divZip.Attributes.Add("display", "inline");
            this.DivShowCountry.Attributes.Add("display", "inline");
            

            this.divFirstLast.Style["display"] = "none";
            this.divFirstAddress.Style["display"] = "none";
            this.divCompanyName.Style["display"] = "none";
            this.divCity.Style["display"] = "none";
            this.divNAUSCanada.Style["display"] = "none";
            this.divRegion.Style["display"] = "none";
            this.divZip.Style["display"] = "none";
            this.DivShowCountry.Style["display"] = "inline";
           
            PopulateNewRegisterUser();
           
        }
    }

    protected void PopulateWithUserDetail()
    {

        string connectionString = ConfigurationManager.AppSettings["EMM_Connection"];
        SqlConnection myConnection = new SqlConnection(connectionString);

        using (SqlCommand cmd = myConnection.CreateCommand())
        {
            
            int tmpID = Convert.ToInt16(this.cmbCustomerName.SelectedValue);
            myConnection.Open();

            string sel = "GetCustomerInformation";
		    cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("Customer_ID", tmpID);
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
                            this.txtemail.Text = reader1["email"].ToString().Trim();
                            this.txtFirstName.Text = reader1["Contact_FirstName"].ToString().Trim();
                            this.txtLastName.Text = reader1["Contact_LastName"].ToString().Trim();
                            this.txtAddress1.Text = reader1["Address1"].ToString().Trim();
                            this.txtAddress2.Text = reader1["Address2"].ToString().Trim();
                            this.txtAddress3.Text = reader1["Address3"].ToString().Trim();
                            this.txtCity.Text = reader1["City"].ToString().Trim();
                            this.cmbState.SelectedValue = reader1["State_ID"].ToString();
                            this.txtPostal_Code.Text = reader1["Postal_Code"].ToString().Trim();
                            this.txtTelephone.Text = reader1["Telephone"].ToString().Trim();
                            this.txtFAX.Text = reader1["Fax"].ToString().Trim();
                            this.cmbRegion.SelectedValue = reader1["Region_ID"].ToString();
                            this.cmbSalutation.SelectedValue = reader1["Salutation_ID"].ToString().Trim();
                            this.cmbCountry.SelectedValue = reader1["Country_ID"].ToString().Trim();
                           

                            

                        }
                        catch (Exception e)
                        {
                            reader1.Close();
                            myConnection.Close(); 
                            Session["ErrorMessage"] = "An error occurred in outputting User Information: " + e.Message;
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
            Session["ErrorMessage"] = "Error in PopulateNewRegisterUser " + se.Message;
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
                cmbCountry.DataSource = reader;
                cmbCountry.DataBind();

            } 
            sql_Cmd.CommandText = "GetAllRegions";
            sql_Cmd.CommandType = CommandType.StoredProcedure;

            using (SqlDataReader reader = sql_Cmd.ExecuteReader())
            {
                cmbRegion.DataSource = reader;
                cmbRegion.DataBind();

            }
            sql_Cmd.CommandText = "GetAllSalutations";
            sql_Cmd.CommandType = CommandType.StoredProcedure;

            using (SqlDataReader reader = sql_Cmd.ExecuteReader())
            {
                cmbSalutation.DataSource = reader;
                cmbSalutation.DataBind();
                reader.Close();
                
            }
        }

        myConnection.Close();
    }


    protected void cmdSubmitNewUser_Click(object sender, EventArgs e)
    {

        int retVal;
        this.divResponse.Style["display"] = "none";
        this.lblReponse.Text = String.Empty;
        string retStr = String.Empty;

        if (!(PerformValidation()))
        {

            int MaintainTechnicalServiceSelected = -1;
            int MaintainCompanySelected = Convert.ToInt16(this.cmbCustomerName.SelectedValue);
            try
            {

                UserInformationDB UiDb = new UserInformationDB();
                if (this.divRepName.Style["display"] == "inline")
                {
                    int cmbValue = Convert.ToInt16(this.cmbReps.SelectedValue);
                    MaintainTechnicalServiceSelected = UiDb.UpdateTechnicalReps(cmbValue, this.txtRepFirstName.Text, this.txtRepLastName.Text, this.txtRepNewEmail.Text);

                }
                else
                {
                    MaintainTechnicalServiceSelected = Convert.ToInt16(this.cmbReps.SelectedValue);
                }

                retVal = UiDb.UpdateCustomerInformation(Convert.ToInt16(this.cmbCustomerName.SelectedValue),
                this.txtCompanyName.Text,
                this.txtAddress1.Text,
                this.txtAddress2.Text,
                this.txtAddress3.Text,
                txtCity.Text,
                Convert.ToInt32(this.cmbState.SelectedValue),
                this.txtPostal_Code.Text,
                this.txtTelephone.Text,
                this.txtFAX.Text,
                this.txtemail.Text,
                Convert.ToInt32(this.cmbCountry.SelectedValue),
                Convert.ToInt32(this.cmbRegion.SelectedValue),
                Convert.ToInt32(this.cmbSalutation.SelectedValue),
                this.txtFirstName.Text,
                this.txtLastName.Text);

                this.divResponse.Style["display"] = "inline";
                this.cmbReps.DataBind();
                this.cmbCustomerName.DataBind();
                this.cmbCustomerName.SelectedValue = Convert.ToString(MaintainCompanySelected);
                this.lblReponse.Text = "Update of customer data was successful.";
                this.cmbReps.SelectedValue = Convert.ToString(MaintainTechnicalServiceSelected);
                this.divRepName.Style["display"] = "none";
                this.txtRepEmail.Text = UiDb.GetTechnicalRepEmail(Convert.ToInt16(this.cmbReps.SelectedValue));
                UiDb = null;
                this.lnkAddNewCustomer.Text = "Add New Customer";
                this.txtNewCustomerName.Text = String.Empty;

            }
            catch (Exception ex)
            {
                Session["ErrorMessage"]  = "Error in update of customer information with error: " + ex.Message ;
                Response.Redirect("ErrorMessage.aspx", false);
                return;

            }
        }
        else
        {

            this.divResponse.Style["display"] = "inline";
            this.lblReponse.Text = "Due to missing data, your update was not completed.";
        }
     
        }
      
   protected bool PerformValidation()
    {
        bool boolFoundMissing = false;

        this.divFirstLast.Style["display"] = "none";
        this.divFirstAddress.Style["display"] = "none";
        this.divCompanyName.Style["display"] = "none";
        this.divCity.Style["display"] = "none";
        this.divNAUSCanada.Style["display"] = "none";
        this.divRegion.Style["display"] = "none";
        this.divZip.Style["display"] = "none";

       
        if (String.IsNullOrEmpty(this.txtPostal_Code.Text))
        {
            this.divZip.Style["display"] = "inline";
            this.lblZip.Text = "Please provide a contact zip code.";
            boolFoundMissing = true;
        }


        if (String.IsNullOrEmpty(this.txtCompanyName.Text))
        {
            this.divCompanyName.Style["display"] = "inline";
            this.lblCompanyName.Text = "Please provide a company name.";
            boolFoundMissing = true;
        }
        
       
       
        if (String.IsNullOrEmpty(this.txtFirstName.Text) || (String.IsNullOrEmpty(this.txtLastName.Text)))
        {
           this.divFirstLast.Style["display"] = "inline";
           this.lblContactName.Text = "Please provide a contact first and last name.";
           boolFoundMissing = true;
        }

         if (String.IsNullOrEmpty(this.txtAddress1.Text))
        {
            this.divFirstAddress.Style["display"] = "inline";
            this.lblContactAddress.Text = "Please provide at least one contact address line.";
            boolFoundMissing = true;
        }


        if (String.IsNullOrEmpty(this.txtCity.Text))
        {
            this.divCity.Style["display"] = "inline";
            this.lblDivCity.Text = "Please provide a contact City.";
            boolFoundMissing = true;
        }


        if (String.IsNullOrEmpty(this.txtPostal_Code.Text))
        {
            this.divZip.Style["display"] = "inline";
            boolFoundMissing = true;
        }


        if (Convert.ToInt16(this.cmbState.SelectedValue) > 0)
        {

            RegionDB rdb = new RegionDB();
            bool boolretVal = rdb.IsNorthAmerican(Convert.ToInt32(this.cmbCountry.SelectedValue));
            
            if ((boolretVal) && (!(this.cmbState.SelectedIndex > 0)))
            {
                this.divNAUSCanada.Style["display"] = "inline";
                this.lblAmerica.Text = "Please select from 'USA/Canada'.";
                boolFoundMissing = true;
            }

            if (!(boolretVal))
            {
                this.cmbState.SelectedIndex = -1;
                this.divNAUSCanada.Style["display"] = "inline";
                this.lblAmerica.Text = "Your 'USA/Canada' selection was removed as it is not within your selected 'Region'.";
            }


            rdb = null;
        }


        if ((!(Convert.ToInt32(this.cmbRegion.SelectedValue)> 0)) || (!(Convert.ToInt32(this.cmbCountry.SelectedValue) > 0)))
        {
            this.divRegion.Style["display"] = "inline";
            this.lblRegion.Text = "Please provide a valid Region.";
            boolFoundMissing = true;
        }



        return boolFoundMissing;

    }

   private void UpdateCountry()
    {
       
                string connectionString = ConfigurationManager.AppSettings["EMM_Connection"];
                SqlConnection myConnection = new SqlConnection(connectionString);

                using (SqlCommand cmd = myConnection.CreateCommand())
                {
                    myConnection.Open();
                    string sel = "GetCountryID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@region_id", Convert.ToInt32(cmbRegion.SelectedValue)));
                    cmd.CommandText = sel;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        this.cmbCountry.DataSource = dr;
                        this.cmbCountry.DataBind();
                        dr.Close();
                    }
                    myConnection.Close();
                }
     }

   



    protected void cmbNewUserRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateCountry();
        this.txtSelectedCountry.Value = this.cmbCountry.SelectedValue.ToString();
    }
    protected void cmbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DontDoAnthing)
        {   
            PopulateWithUserDetail();
            this.divCustomerDetail.Style["display"] = "inline";
            this.txtCompanyName.Text = this.cmbCustomerName.SelectedItem.ToString();
            this.txtNewCustomerName.Text = String.Empty;
        }
        DontDoAnthing = true;
    }



   
    protected void lnkAddNewCustomer_Click(object sender, EventArgs e)
    {

        UserInformationDB udb = new UserInformationDB();
        
        if (this.lnkAddNewCustomer.Text == "Skip Adding New Customer")
        {

            
            int retVal = udb.DeleteNewCustomer(Convert.ToInt16(Session["IsCustomerRep"]));
            this.lnkAddNewCustomer.Text = "Add New Customer";
            this.cmbSalutation.SelectedIndex = -1;
            this.cmbCountry.DataSource = null;
            this.cmbRegion.SelectedIndex = -1;
            this.txtAddress1.Text = String.Empty;
            this.txtAddress2.Text = String.Empty;
            this.txtAddress3.Text = String.Empty;
            this.txtCity.Text = String.Empty;
            this.txtFAX.Text = String.Empty;
            this.txtemail.Text = String.Empty;
            this.cmbState.SelectedIndex = -1;
            this.txtTelephone.Text = String.Empty;
            this.txtPostal_Code.Text = String.Empty;
            this.txtFirstName.Text = String.Empty;
            this.txtLastName.Text = String.Empty;
            this.DivShowCountry.Style["display"] = "inline";
            this.txtCompanyName.Text = String.Empty;
            this.txtNewCustomerName.Text = String.Empty;
            this.txtNewCustomerName.Focus();
            this.cmbCustomerName.DataBind();
            udb = null;
            return;
        }

        this.divResponse.Style["display"] = "none";

        if (String.IsNullOrEmpty(this.txtNewCustomerName.Text))
        {
            this.lblReponse.Text = ("Please provide a new Company Name.").ToUpper();
            this.divResponse.Style["display"] = "inline";
            udb = null;
            return;
        }

        if ((Convert.ToInt16(this.cmbReps.SelectedValue) == -1) && (this.divRepName.Style["display"] == "none"))
        {
            this.lblReponse.Text = ("Before proceeding, please select a Technical Rep.").ToUpper();
            this.divResponse.Style["display"] = "inline";
            udb = null;
            return;
        }
        
        
        DontDoAnthing = false;
        
        int MaintainTechnicalServiceSelected = Convert.ToInt16(this.cmbReps.SelectedValue);
        this.divCustomerDetail.Style["display"] = "inline";
    
        Session["IsCustomerRep"] = udb.InsertNewCustomer(this.txtNewCustomerName.Text.ToString().Trim());
        this.cmbCustomerName.DataBind();
        this.cmbCustomerName.SelectedValue = Session["IsCustomerRep"].ToString();
        this.cmbReps.SelectedValue = Convert.ToString(MaintainTechnicalServiceSelected);
        this.txtCompanyName.Text = this.cmbCustomerName.SelectedItem.ToString();
        this.cmbSalutation.SelectedIndex = -1;
        this.cmbCountry.DataSource = null;
        this.cmbRegion.SelectedIndex = -1;
        this.txtAddress1.Text = String.Empty;
        this.txtAddress2.Text = String.Empty;
        this.txtAddress3.Text = String.Empty;
        this.txtCity.Text = String.Empty;
        this.txtFAX.Text = String.Empty;
        this.txtemail.Text = String.Empty;
        this.cmbState.SelectedIndex = -1;
        this.txtTelephone.Text = String.Empty;
        this.txtPostal_Code.Text = String.Empty;
        this.txtFirstName.Text = String.Empty;
        this.txtLastName.Text = String.Empty;
        this.DivShowCountry.Style["display"] = "inline";
        this.txtCompanyName.Focus();
        this.lnkAddNewCustomer.Text = "Skip Adding New Customer";
        udb = null;
    }
    
    protected void cmdEnterApplication_Click(object sender, EventArgs e)
    {
        
        
        if (!(PerformValidation()))
        {
            UserInformationDB udb = new UserInformationDB();
            string TechnicalServiceRepsName = udb.GetTechnicalProperName(Convert.ToInt16(this.cmbReps.SelectedValue));
            Session["UserNameVar"] = TechnicalServiceRepsName;
            Session["IsCustomerRep"] = Convert.ToInt16(this.cmbReps.SelectedValue);
            Session["CurrentCompany"] = Convert.ToInt16(this.cmbCustomerName.SelectedValue);
            udb = null;
            Response.Redirect("ProductSearchCriteria.aspx", false);
            return;
        }
        else
        {
            this.lblReponse.Text = ("The current customer record is not complete").ToUpper();
            return;
        }

               
    }
    protected void lnkAddNewTechnicalRep_Click(object sender, EventArgs e)
    {
        this.divRepName.Style["display"] = "inline";
        if (this.cmbReps.SelectedValue == "-1")
        {
            this.txtRepNewEmail.Text = String.Empty;
            this.txtRepFirstName.Text = String.Empty;
            this.txtRepLastName.Text = String.Empty;
        }
        else
        {
            string[] arrayID;
            string ParseName = this.cmbReps.SelectedItem.ToString();
            char[] comma = { ',' };
            arrayID = ParseName.Split(comma);
            this.txtRepNewEmail.Text = this.txtRepEmail.Text;
            this.txtRepFirstName.Text = arrayID[1].Trim();
            this.txtRepLastName.Text = arrayID[0].Trim();
            arrayID = null;
 
        }
    }
    protected void cmbReps_SelectedIndexChanged(object sender, EventArgs e)
    {
        UserInformationDB udb = new UserInformationDB();
        this.txtRepNewEmail.Text = String.Empty;
        this.txtRepFirstName.Text = String.Empty;
        this.txtRepLastName.Text = String.Empty;

        if (!(this.cmbReps.SelectedValue == "-1"))
        {
            this.txtRepEmail.Text = udb.GetTechnicalRepEmail(Convert.ToInt16(this.cmbReps.SelectedValue));

        }
        else
        {
            this.txtRepEmail.Text = String.Empty;
        }

        
        udb = null;
    }


    protected void cmbRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.DivShowCountry.Style["display"] = "inline";
        UpdateCountry();
    }
   
}
        


