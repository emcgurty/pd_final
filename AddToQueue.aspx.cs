using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


public partial class AddToQueue : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {

        if (Convert.ToInt16(Session["UserVar"]) != 0) // Not a guest
        {
            
            if (Convert.ToInt16(Session["IsCustomerRep"]) == 0)
            {
                this.Master.MasterLeftColumn(true, true, false, true, true, false, false); 
                
            }
            else
            {
                this.Master.MasterLeftColumn(false, true, false, true, true, false, false); 
                
            }
        }
        else if (!(Convert.ToInt16(Session["UserVar"]) > 0))
        {
            
            Session["ErrorMessage"]  = "A user that has not completed the registration process has entered the application.";
            Response.Redirect("ErrorMessage.aspx", false);
            return;
        }
 
        this.Master.TitleOne.Text = "PD";
        this.Master.TitleTwo.Text = "Declarations Listing";
        this.Master.DisplayUserName.Text = "Welcome! " + Session["UserNameVar"];
        this.divButtons.Attributes.Add("display", "inline");
        this.divNoRecords.Attributes.Add("display", "inline");
        Session["PDF_name"] = "EMMOP" + Session["UserVar"];
        


    }
    
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
             
        if (!(Page.IsPostBack))
        {

                Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) + "; URL=ErrorMessage.aspx?Expired=1"); 
                int retVal = 0;
                int User_Information_ID = Convert.ToInt16(Session["UserVar"]);

                if (!(User_Information_ID > 0))
                 {
                    
                    Session["ErrorMessage"]  = "A user that has not completed the registration process has entered the application.";
                    Response.Redirect("ErrorMessage.aspx", false);
                    return;
                }


                string sel = "GetProductCompliance";
                SqlCommand cmd = new SqlCommand(sel, new SqlConnection(GlobalClass.GetConnectionString()));
                cmd.Parameters.Add(new SqlParameter("@user_id", User_Information_ID));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                QueueView.DataSource = dr;
                QueueView.DataBind();
                dr.Close();
                cmd.Connection.Close();
            
            if (QueueView.Rows.Count > 0)

            {
                this.divButtons.Style["display"] = "inline";
                this.divNoRecords.Style["display"] = "none";
                CheckBox cb;
                UserInformationDB udb = new UserInformationDB();
                retVal = 0;
                string strCPID = string.Empty;

                foreach (GridViewRow row in QueueView.Rows)
                {
                    // Access the CheckBox
                    cb = (CheckBox)row.FindControl("RetainSelector");
                    if (cb != null)
                    {
                        if (cb.Checked)
                        {
                            row.BackColor = System.Drawing.Color.LemonChiffon;
                            strCPID = row.Cells[0].Text.ToString();
                            retVal = udb.RetainToQueue(DateTime.Now, Convert.ToInt16(strCPID), Convert.ToInt16(Session["UserVar"]));
                        }
                    }

                }
                udb = null;
            }
            else
            {
                this.divButtons.Style["display"] = "none";
                this.divNoRecords.Style["display"] = "inline";
            }
            //pdb = null;
            
       }
        
    }
        
        
    protected void QueueView_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void SubmitNewQueueStatus(object sender, EventArgs e)
    {
        int retVal = 0;
        int intRetain = 0;
        CheckBox cb = new CheckBox();
        UserInformationDB udb = new UserInformationDB();
        TableCell tCell;
        
        foreach (GridViewRow row in QueueView.Rows)
        {
            // Access the CheckBox
            cb = (CheckBox)row.FindControl("RetainSelector");
            if (cb != null)
            {
                if (!(cb.Checked))
                {
                    tCell = row.Cells[0];
                    intRetain = Convert.ToInt16(tCell.Text);
                    retVal = udb.DeleteFromQueueSelection(Convert.ToInt16(Session["UserVar"]), intRetain);
                    row.BackColor = System.Drawing.Color.White;



                 
                }
                else
                {
                    tCell = row.Cells[0];
                    intRetain = Convert.ToInt16(tCell.Text);
                    retVal = udb.RetainToQueue(DateTime.Now, intRetain, Convert.ToInt16(Session["UserVar"]));
                    row.BackColor = System.Drawing.Color.LemonChiffon;

                }




            }
        }

       
        udb = null;
    }
    
    protected void ViewSelected_Click(object sender, EventArgs e)
    {

        string BuildChoices = "(";
        int SelectionCount = 0;
        int intRetain = 0;
        CheckBox cb;
        TableCell tCell;
        UserInformationDB udb = new UserInformationDB();
        this.lblLimitSelection.ForeColor = System.Drawing.Color.Black;
        
        foreach (GridViewRow row in QueueView.Rows)
        {
            // Access the CheckBox
            cb = (CheckBox)row.FindControl("ProductSelector");
            if (cb != null)
            {
                if (cb.Checked)
                {
                    tCell = row.Cells[0];
                    BuildChoices += tCell.Text + ",";
                    SelectionCount++;
                }
            }


        }
       
        
        if ((SelectionCount < 6) && (SelectionCount > 0))
        {


            int retVal = 0;
            foreach (GridViewRow row in QueueView.Rows)
            {
                // Access the CheckBox
                cb = (CheckBox)row.FindControl("RetainSelector");
                if (cb != null)
                {
                    if (!(cb.Checked))
                    {
                        tCell = row.Cells[0];
                        intRetain = Convert.ToInt16(tCell.Text);
                        retVal = udb.DeleteFromQueueSelection(Convert.ToInt16(Session["UserVar"]), intRetain);
                        bool boo_retVal = udb.InsertUserActivity(Session["UserVar"].ToString(), "Delete From Queue", intRetain);
                       
                    }
                    else
                    {
                        tCell = row.Cells[0];
                        intRetain = Convert.ToInt16(tCell.Text);
                        retVal = udb.RetainToQueue(DateTime.Now, intRetain, Convert.ToInt16(Session["UserVar"]));
                        

                    }


                }
            }
            
            udb = null;
            BuildChoices = BuildChoices.Substring(0, BuildChoices.Length - 1);
            BuildChoices = BuildChoices + ")";
            Session["Query_String_CG"] = BuildChoices;
            Response.Redirect("Suspend.aspx?build=1", false);
            return;
            
        }
        else
        {

            this.lblLimitSelection.ForeColor = System.Drawing.Color.Red;
            
        }

    }

}