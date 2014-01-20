<%@ Application Language="C#" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.Web.SessionState" %>
<%@ Import Namespace="System.IO" %>

<script runat=server >

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        string CPath = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath + @"docs\";

        for (int counter = 1; counter < Convert.ToInt16(Session["FileOutputCount"]); counter++)
        {
            try
            {
                File.Delete(@CPath + Session["PDF_name"].ToString() + counter + Session["GetRandomNumber"].ToString() + ".pdf");
            }
            catch
            { }

        }

        try
        {
            File.Delete(@CPath + Session["PDF_name"].ToString() + "Gb" + Session["GetRandomNumber"].ToString() + ".pdf");
        }
        catch
        { }


    }
        
    void Application_Error(object sender, EventArgs e) 
    {
        ////get reference to the source of the exception chain
        Exception ex = Server.GetLastError().GetBaseException();

        try
        {

            Session["ErrorMessage"]  = "An unhandled exception has occurred: " +
                                   "MESSAGE: " + ex.Message + "\r\n"
                                + "SOURCE: " + ex.Source + "\r\n"
                                + "Stack: " + ex.StackTrace + "\r\n"
                                + "FORM: " + Request.Form.ToString() + "\r\n"
                                + "QUERYSTRING: " + Request.QueryString.ToString() + "\r\n"
                                + "TARGETSITE: " + ex.TargetSite;
            Response.Redirect("ErrorMessage.aspx", false);
            
        }
        catch
        {  
                        
        }
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        string CPath = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath + @"docs\";
        
        for (int counter = 1; counter < Convert.ToInt16(Session["FileOutputCount"]); counter++)
        {
            try
            {
                File.Delete(@CPath + Session["PDF_name"].ToString() + counter.ToString() + "*.pdf");
            }
            catch
            { }

        }

        try
        {
            File.Delete(@CPath + Session["PDF_name"].ToString() + "Global" + Session["GetRandomNumber"].ToString() + ".pdf");
        }
        catch
        { }


        try
        {
            Session.Contents.RemoveAll();
        }
        catch
        { }
            
        

    }
       

</script>