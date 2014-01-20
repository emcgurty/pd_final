using System;
using System.Drawing;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


/// <summary>
/// Summary description for MasterPage
/// </summary>
public partial class MasterPage : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
    }
              
      
    protected void Page_Init(object sender, EventArgs e)
    {
          
        
        if (!(IsPostBack))
        {
           
            this.lblCopyright.Text = ConfigurationManager.AppSettings["copyright"].ToString() + " (Last modified on Friday, September 4, 2009 at 7:00 AM (EST))"; 
            this.divClickToEdit.Attributes.Add("Display", "inline");
            this.divContactUs.Attributes.Add("Display", "inline");
            this.divGotoQueue.Attributes.Add("Display", "inline");
            this.divLogout.Attributes.Add("Display", "inline");
            this.divProductSearch.Attributes.Add("Display", "inline");
            this.divRegisterToView.Attributes.Add("Display", "inline");
            this.divRegisteredUser.Attributes.Add("Display", "inline");
            
            
            

        }

    }


     
    
    
    
    
    public Label DisplayUserName
    {
        get
        {
            return lblCurrentUserName;
        }
        set
        {
            lblCurrentUserName = value;
        }
    }


    public HiddenField PixelSize
    {
        get
        {
            return txtPXSize;
        }
        set
        {
            txtPXSize = value;
        }
    }
   

   

    //hyperContact_Us
    public HyperLink Hyper_Contact_Us
    {
        get
        {
            return hyperContact_Us;
        }
        set
        {
            hyperContact_Us = value;
        }
    }

    
    //hyperRegisterToView
    public HyperLink Hyper_Register_To_View
    {
        get
        {
            return hyperRegisterToView;
        }
        set
        {
            hyperRegisterToView = value;
        }
    }

    //hyperProduct Search

    public HyperLink Hyper_Product_Search
    {
        get
        {
            return hyperProductSearch;
        }
        set
        {
            hyperProductSearch = value;
        }
    }


    public HyperLink Hyper_Go_To_Queue
    {
        get
        {
            return hyperGoToQueues;
        }
        set
        {
            hyperGoToQueues = value;
        }


    }

    public HyperLink Hyper_Logout
    {
        get
        {
            return hyperLogout;
        }
        set
        {
            hyperLogout = value;
        }


    }


    public Label TitleOne
    {
        get
        {
            return lblTitleOne;
        }
        set
        {
            lblTitleOne = value;
        }
    }

    public Label TitleTwo
    {
        get
        { return lblTitleTwo; }
        set
        {
            lblTitleTwo = value;
        }
    }

 

    protected void btnExitApplication_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Contact_Us.aspx");
            return;
        }
        catch (Exception ex)
        {
            Session["ErrorMessage"]  = "Error on Response.Redirect to Contact_Us.aspx: "  + ex.Message ;
            Response.Redirect("ErrorMessage.aspx");
            return;
        }

    }

   

    public void MasterLeftColumn(bool ilblClickToEdit, bool ihypProduct,
                                 bool ihyperGoTo, bool ihyperLogout,
                                 bool ihyperContactUs, bool ihyperRegisterToView,
                                 bool ihyperRegisteredUser)
    {


        if (ilblClickToEdit)
        {
            this.divClickToEdit.Style["Display"] = "inline";
            
        }
        else
        {
            this.divClickToEdit.Style["Display"] = "none";
            
        }

        if (!(Convert.ToInt16(Session["IsCustomerRep"]) == 0))
        {
            this.divClickToEdit.Style["Display"] = "none";
        }

        if (ihypProduct)
        {
            this.divProductSearch.Style["Display"] = "inline";

        }
        else 
        {
            this.divProductSearch.Style["Display"] = "none";

        }

  
       if (ihyperGoTo)
        {
            this.divGotoQueue.Style["Display"] = "inline";
            
        }
        else 
        {
            this.divGotoQueue.Style["Display"] = "none";
            
        }
         
       if (ihyperLogout)
        {
            this.divLogout.Style["Display"] = "inline";
           
        }
        else 
        {
            this.divLogout.Style["Display"] = "none";
           
        }


        if (ihyperContactUs)
        {
            this.divContactUs.Style["Display"] = "inline";
           
        }
        else 
        {
            this.divContactUs.Style["Display"] = "none";
           
        }

  

        if (ihyperRegisterToView )
        {
            this.divRegisterToView.Style["Display"] = "inline";
           
        }
        else
        {
            this.divRegisterToView.Style["Display"] = "none";
           
        }

        if (ihyperRegisteredUser)
        {
            this.divRegisteredUser.Style["Display"] = "inline";

        }
        else
        {
            this.divRegisteredUser.Style["Display"] = "none";

        }

  
         
    }
}

