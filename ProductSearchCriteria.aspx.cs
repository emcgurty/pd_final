using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;


public partial class ProductSearchCriteria : System.Web.UI.Page
{

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {

            Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) + "; URL=ErrorMessage.aspx?Expired=1");
            this.btnShowQueueResults.Visible = false;

            this.Master.TitleTwo.Text = "PDs";
            this.Master.TitleOne.Text = "User Search Criteria";
            if (String.IsNullOrEmpty(Session["UserNameVar"].ToString()) || (Convert.ToInt16(Session["UserVar"]) == 0))
            {
                Session["ErrorMessage"] = "Either you have not logged onto this application or not completed the registration process.";
                Response.Redirect("ErrorMessage.aspx?Expired=2", false);
                return;

            }
            else
            {
                this.Master.DisplayUserName.Text = "Welcome! " + Session["UserNameVar"];
                this.Master.MasterLeftColumn(true, false, true, true, true, false, false);


            }

            this.divShowGrid.Attributes.Add("display", "inline");
            SqlDataReader dr;
            string sel = "GetProductDetail";
            SqlConnection myConnection = new SqlConnection(GlobalClass.GetConnectionString());

            using (SqlCommand cmd = myConnection.CreateCommand())
            {

                try
                {
                    myConnection.Open();
                }
                catch (SqlException se)
                {
                    Session["ErrorMessage"] = "Error in Page_Init (ProductSeachCriteria.aspx " + se.Message;
                    Response.Redirect("ErrorMessage.aspx", false);
                    return;

                }
                
                cmd.CommandText = sel;
                cmd.CommandType = CommandType.StoredProcedure;


                using (dr = cmd.ExecuteReader())
                {
                    this.cmbProduct.DataSource = dr;
                    this.cmbProduct.DataBind();
                }

                this.cmbProduct.SelectedIndex = 0;
                int iSelectedProduct = Convert.ToInt32(this.cmbProduct.SelectedValue);
                Session["SelectedProductID"] = iSelectedProduct.ToString();

                cmbSearchAgency.Items.Clear();
                cmbSearchAgency.Items.Add("{Select an Agency here}");
                cmbSearchAgency.Items[0].Value = "-1";
                this.cmbSearchAgency.SelectedValue = "-1";
                this.cmbProduct.SelectedValue = "-1";

                // Populate the business object
                sel = "GetUserSearch";
                cmd.CommandText = sel;
                cmd.CommandType = CommandType.StoredProcedure;
                
                using (dr = cmd.ExecuteReader())
                {


                    if (dr.HasRows)
                    {
                        DB_Values dbv = new DB_Values();
                        dbv.DeleteRowValue();
                        int counter = 0;
                        while (dr.Read())
                        {
                            dbv.InsertRows(counter++, Convert.ToInt32(dr["Product_ID"]),
                                        Convert.ToInt32(dr["Agency_ID"]),
                                        Convert.ToInt32(dr["Issue_ID"]),
                                        dr["Product_Name"].ToString(),
                                        dr["Concern_Issue"].ToString(),
                                        dr["Agency"].ToString(), 0);
                        }
                    }
                }
                dr.Close();
                myConnection.Close();
            }
        }

    }
    
    
   
        
    
    protected void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ProductID = 0;
        this.divShowWarning.Style["display"] = "none";

        if ((this.cmbProduct.SelectedValue != null) && (Convert.ToInt16(this.cmbProduct.SelectedValue) != -1))
        {
            UserSelectionView.Visible = true; 
            cmbSearchAgency.Items.Clear(); 
            cmbSearchAgency.Items.Add("{Select an Agency here}"); 
            cmbSearchAgency.Items[0].Value = "-1"; 
                        
            this.divShowGrid.Style["display"] = "none";
            this.divSearchForResult.Style["display"] = "none";
            
            ProductID = Convert.ToInt32(this.cmbProduct.SelectedValue);
            this.cmbSearchAgency.DataSource = null;
            String sp = "GetAgencyByProductID";
            SqlCommand cmd = new SqlCommand(sp, new SqlConnection(GlobalClass.GetConnectionString()));
            cmd.Parameters.Add(new SqlParameter("@product_id", ProductID));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            this.cmbSearchAgency.DataSource = dr;
            this.cmbSearchAgency.DataBind();
            dr.Close();
            cmd.Connection.Close();
            this.cmbSearchAgency.SelectedValue = "-1";

        }
        else
        
        {
            UserSelectionView.Visible = false; 
            cmbSearchAgency.Items.Clear();
            cmbSearchAgency.Items.Add("{Select an Agency here}");
            cmbSearchAgency.Items[0].Value = "-1";
        }
        
        
    }
    
   
    
    //UserSelectionView_RowCommand
    protected void UserSelectionView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName == "IncludeSelected") 
        {
            DB_Values dbv = new DB_Values();
            CheckBox cb;
            int i = 0;
            bool CheckedSomething = false;

            // Learn if the user checked one issue of concern
            foreach (GridViewRow gvr in UserSelectionView.Rows)
            {
                cb = (CheckBox)gvr.FindControl("chkIsSelected");
                if (cb.Checked)
                {
                    CheckedSomething = true;
                }
                
            }

            // They checked one issue of concern
            if (CheckedSomething)
            {
                foreach (GridViewRow gvr in UserSelectionView.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("chkIsSelected");
                    if (cb.Checked)
                    {
                        i = Convert.ToInt32(gvr.Cells[0].Text);
                        dbv.UpdateCurrentRow(i, 1);
                        this.btnShowQueueResults.Visible = true;
                        this.divSearchForResult.Style["display"] = "inline";
                    }
                    else
                    {
                        i = Convert.ToInt32(gvr.Cells[0].Text);
                        dbv.UpdateCurrentRow(i, 0);

                    }
                }
            }
            else
            {
                // They didn't check anything, evidently all 
                foreach (GridViewRow gvr in UserSelectionView.Rows)
                {
                    cb = (CheckBox)gvr.FindControl("chkIsSelected");
                    i = Convert.ToInt32(gvr.Cells[0].Text);
                    dbv.UpdateCurrentRow(i, 1);
                    this.btnShowQueueResults.Visible = true;
                    this.divSearchForResult.Style["display"] = "inline";
                }

            }

            // refresh user selections grid view  0131
            UserSelections.DataSource = dbv.GetRowValuesSelected();
            UserSelections.DataBind();
            i = dbv.GetSelectRowCount();
            if (i < 1)
            {
                this.divSearchForResult.Style["display"] = "none";
            }
        }   

    }

    
    protected void ShowResultsToQueue(object sender, EventArgs e)
    {
        try
        {
            DateTime Selection_Date = DateTime.Now;
            DB_Values dbv = new DB_Values();
            ProductsDB pdb = new ProductsDB();
            UserInformationDB udb = new UserInformationDB();
            int CPID = 0;
            bool retVal = false;
            foreach (DB_Row_Values drv in dbv.DB_List)
            {
                if (drv.Is_Selected == 1)
                {
                    CPID = pdb.GetProductComplianceID(drv.Product_ID, drv.Agency_ID, drv.Issue_ID);
                    retVal = pdb.FindQueueRecord(CPID);
                    if (retVal)  // Only one CPID in User Queues
                    {

                        retVal = udb.InsertIntoQueue(Selection_Date, CPID);
                        retVal = udb.InsertUserActivity(Session["UserVar"].ToString(), "Insert Into Queue", CPID);
                    }
                    else    // if there was an error
                    {
                        Response.Redirect("ErrorMessage.aspx", false);
                        return;
                    }
                    
                }

            }
            
            dbv = null;
            pdb = null;
            
            retVal = udb.InsertUserActivity(Session["UserVar"].ToString(), "Product Search", 0);
            udb = null;
            Response.Redirect("AddToQueue.aspx", false);
            return;
        }
        catch (Exception ex)
        {
            Session["ErrorMessage"]  = "Error in Response.Redirect to AddToQueue.aspx: " +  ex.Message;
            Response.Redirect("ErrorMessage.aspx");
            return;
        }
    }


    protected void cmbSearchAgency_SelectedIndexChanged(object sender, EventArgs e)
    {
        UserSelectionView.DataSource = null;
        UserSelections.DataSource = null;
        ProductsDB pdb = new ProductsDB();
        string strWarning = String.Empty;
        this.divShowWarning.Style["display"] = "none";
        strWarning = String.Empty;
        //pdb.HasWarning(this.cmbProduct.SelectedValue, this.cmbSearchAgency.SelectedValue);

        if (!(String.IsNullOrEmpty(strWarning)))
        {
            this.divShowWarning.Style["display"] = "inline";
            this.divShowGrid.Style["display"] = "none";
            this.divSearchForResult.Style["display"] = "none";
            this.lblWarning.Text = strWarning + ". Please make another selection.";
            pdb = null;
            return;
        }

        pdb = null;

        if (Convert.ToInt16(cmbSearchAgency.SelectedValue) == -1)
        {
            UserSelectionView.Visible = false;
        }
        else
        {
            UserSelectionView.Visible = true; 
            DB_Values dbv = new DB_Values();
            int PID = Convert.ToInt32(this.cmbProduct.SelectedValue);
            int AID = Convert.ToInt32(this.cmbSearchAgency.SelectedValue);
            UserSelectionView.DataSource = dbv.GetRowValuesCriteria(PID, AID);
            UserSelectionView.DataBind();
            UserSelections.DataSource = dbv.GetRowValuesSelected();
            UserSelections.DataBind();
            this.divShowGrid.Style["display"] = "inline";
            this.divSearchForResult.Style["display"] = "inline";
        }
    }
    
}
