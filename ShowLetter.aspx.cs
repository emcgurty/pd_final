using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;




public partial class ShowLetter : System.Web.UI.Page
{

    private string CPath= String.Empty;
    private int this_FileOutPutCount = 0;
    private bool boolretVal = true;

    protected void Page_Init(object sender, EventArgs e)
    {

       
        this.Master.TitleOne.Text = "PDs";
        this.Master.TitleTwo.Text = "Declaration Letter";
           
        Session["FileOutputCount"] = "0";
        this_FileOutPutCount = Convert.ToInt16(Session["FileOutputCount"]);

       
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!(Page.IsPostBack))
        {

            if (String.IsNullOrEmpty(Session["UserNameVar"].ToString()) || (Convert.ToInt16(Session["UserVar"]) == 0))
        {
            Session["ErrorMessage"]  = "Either you have not logged onto this application or not completed its registration process.";
            Response.Redirect("ErrorMessage.aspx?Expired=2", false);
            return;

        }
        else
        {
            this.Master.DisplayUserName.Text = "Welcome! " + Session["UserNameVar"].ToString();
            if (Convert.ToInt16(Session["IsCustomerRep"]) == 0)
            {
                this.Master.MasterLeftColumn(true, false, true, true, true, false, false);
            }
            else
            {
                this.Master.MasterLeftColumn(false, false, true, true, true, false, false);
            }
        }
        
        Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) + "; URL=ErrorMessage.aspx?Expired=1");
        CPath = Server.MapPath("docs");
        Session["WhichDoc"] = String.Empty;
        boolretVal = true;
        this_FileOutPutCount = Convert.ToInt16(Session["FileOutputCount"]);
        bool retval = true;

        if (!(IsPostBack))
        {

            string temp;
            string[] arrayID;
            string[] arrayTemp1;
            string[] arrayTemp2;
            string[] arrayTemp;

            try 
            {
                
                File.Delete(Server.MapPath(Session["WhichDoc"].ToString()));
            }
            catch{}
            
            Session["GetRandomNumber"] = GetRandomNumber();

            // Currently there are 5 regions!!!!   
            // This is the only hard-coded aspect of this application!!!!
            int regID = 1;
            int j = 0;
            int k = 0;
            int i = 0;
            string QueryStringValue = String.Empty;


            if (!(String.IsNullOrEmpty(Request.QueryString["Product_Compliance_ID"])))
            {

                if (Request.QueryString["Product_Compliance_ID"].ToString() == "1")
                {


                    temp = Session["Query_String_CG"].ToString();
                    temp = temp.Trim();
                    temp = temp.Substring(1, temp.Length - 1);  // remove left (
                    temp = temp.Substring(0, temp.Length - 1);    // remove right )

                    char[] comma = { ',' };
                    arrayID = temp.Split(comma);
                    arrayTemp = temp.Split(comma);
                    arrayTemp1 = temp.Split(comma);
                    arrayTemp2 = temp.Split(comma);

                    for (i = 0; i < arrayID.Length; i++)
                    {
                        arrayTemp[i] = "0";
                        arrayTemp1[i] = "0";
                        arrayTemp2[i] = "0";
                    }


                    ProductsDB pdb = new ProductsDB();
                    j = 0;
                    k = 0;

                    while (regID < 6)
                    {

                        for (i = 0; i < arrayID.Length; i++)
                        {

                            if (regID == pdb.GetRegionID(arrayID[i]))
                            {
                                if (regID == 4) /// The Global request.  Build one at a time
                                {
                                    arrayTemp[0] = arrayID[i];
                                    retval = PopulatePDF(arrayTemp, regID, 0);
                                }
                                else if (regID == 2) /// The European requests, group together ()
                                {
                                    arrayTemp1[j] = arrayID[i];
                                    j++;
                                }
                                else if ((regID == 1) || (regID == 3) || (regID == 5)) /// The non-Eurpoean, Global Institutions and Other Claims, group together ()
                                {
                                    arrayTemp2[k] = arrayID[i];
                                    k++;
                                }


                            }
                        }

                        regID++;
                    }

                    if (k > 0)
                    {
                        retval = PopulatePDF(arrayTemp2, 1, ++this_FileOutPutCount);
                    }

                    if (j > 0)
                    {
                        retval = PopulatePDF(arrayTemp1, 2, ++this_FileOutPutCount);
                    }


                    pdb = null;
                }
                else
                {
                    Session["ErrorMessage"]  = "An incorrect value has been set to the application query string.";
                    Response.Redirect("ErrorMessage.aspx", false);
                    return;
                }
            }
        }

        else
        {
            Session["ErrorMessage"]  = "NO PID From AddToQueue";
            Response.Redirect("ErrorMessage.aspx", false);
            return;
        }

        // Now merge all the the output PDFs.
        string GlobalDestinationFile = Server.MapPath(@"docs\" + Session["PDF_name"].ToString() + (++this_FileOutPutCount) + ".pdf");
        try
        {
            File.Delete(GlobalDestinationFile);
        }
        catch
        { }

        String[] arrFiles;
        arrFiles = new String[this_FileOutPutCount - 1];
        for (int counter = 1; counter < this_FileOutPutCount; counter++)
        {
            arrFiles[counter - 1] = Server.MapPath(@"docs\" + Session["PDF_name"].ToString() + counter + Session["GetRandomNumber"].ToString() + ".pdf");
        }

        boolretVal = PdfMerge.MergeFiles(GlobalDestinationFile, arrFiles);
        
        if (!(boolretVal))
        {
            Session["ErrorMessage"] =  "There was an error in PDfMerge.MergeFiles.";
            Response.Redirect("ErrorMessage.aspx", false);
            return;
        }


        for (int counter = 1; counter < this_FileOutPutCount; counter++)
        {
            try
            {
                File.Delete(Server.MapPath(@"docs\" + Session["PDF_name"].ToString() + counter + Session["GetRandomNumber"].ToString() + ".pdf"));
            }
            catch
            { }

        }

        try
        {
            File.Delete(Server.MapPath(@"docs\" + Session["PDF_name"].ToString() + "Global" + Session["GetRandomNumber"].ToString() + ".pdf"));
        }
        catch
        { }

        if (!(boolretVal))
        {
            Response.Redirect("ErrorMessage.aspx", false);
            return;
        }

        this.embedTemplate1.Attributes.Add("src", null);
        try
        {
            string pathName = Server.MapPath("docs");
            File.Copy(pathName + @"\" + Session["PDF_name"].ToString() + (this_FileOutPutCount) + ".pdf", pathName + @"\" + Session["PDF_name"].ToString() + (this_FileOutPutCount) + Session["GetRandomNumber"].ToString() + ".pdf");
        }
        catch { }

        Session["WhichDoc"] = "Docs/" + Session["PDF_name"].ToString() + (this_FileOutPutCount) + Session["GetRandomNumber"].ToString() + ".pdf";
        this.embedTemplate1.Attributes.Add("src", Session["WhichDoc"].ToString());
        this.embedTemplate1.Attributes.Add("name", "pdfTemplate1" + Convert.ToInt16(Session["GetRandomNumber"]));
        Response.Expires = 0;
        }
    }

    private bool PopulatePDF(string[] arrayCPID, int regID, int PDFCount)
    {

        #region myvariables
        int UserID = 0;
        int LineSpacing = 12;
        int stewID = 0;
        string sel = String.Empty;
        string Build_Content = String.Empty;
        string Build_Body = String.Empty;
        string Input = String.Empty;
        string Output = String.Empty;
        string GlobalOutput = String.Empty;
        string stewClosing = String.Empty;
        string stewSignature = String.Empty;
        string stewSignatureImage = String.Empty;
        string attachmentFile = String.Empty;
        bool DisplayedParagraph = false;
        bool boolretVal = true;
        int left_indent = 72*6 - 36; 
        string strFirstParagraph = String.Empty;
        string fileTemp = String.Empty;
        string sTemp = String.Empty;
        Document document = new Document(PageSize.A4, 36, 36, 72, (72));   
        PdfWriter writer;
        Paragraph declaration_paragraphs;
        Paragraph letter_format;
        DateTime CurrTime = DateTime.Now;
        string myString = string.Empty;
        #endregion

        if (PDFCount != 0)
        {   
            Output = Server.MapPath(@"docs\") + Session["PDF_name"].ToString() + PDFCount + Session["GetRandomNumber"].ToString() + ".pdf";
        }
        else
        {   GlobalOutput = Server.MapPath(@"docs\") + Session["PDF_name"].ToString() + "Global" + Session["GetRandomNumber"].ToString() + ".pdf";
        }

        string connectionString = ConfigurationManager.AppSettings["EMM_Connection"];
        SqlConnection myConnection = new SqlConnection(connectionString);

        using (SqlCommand cmd = myConnection.CreateCommand())
        {

            #region OpenConnection
            try
            {
                myConnection.Open();
            }
            catch (SqlException se)
            {
                Session["ErrorMessage"] = "Error in PopulateNewRegisterUser: " + se.Message;
                Response.Redirect("ErrorMessage.aspx", false);
                return false;
            }
            #endregion

            #region GetStewID
            sel = "GetStewardID";
            cmd.CommandText = sel;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Region_ID", regID));

            using (SqlDataReader STID = cmd.ExecuteReader()) 
            {

                if (STID.HasRows)
                {
                    while (STID.Read())
                    {
                        stewID = Convert.ToInt16(STID["Steward_ID"]);
                    }
                }
                STID.Close();
            }
            #endregion
            
            #region OpenPDFFile
            if (PDFCount != 0)
            {
                writer = PdfWriter.GetInstance(document, new FileStream(Output, FileMode.Create));

            }
            else
            {
                try
                {
                    writer = PdfWriter.GetInstance(document, new FileStream(GlobalOutput, FileMode.Create));
                }
                catch (PdfException pex)
                {
                    myConnection.Close();
                    Session["ErrorMessage"] = "Error in GetInstance:" + pex.Message;
                    Response.Redirect("ErrorMessage.aspx", false);
                    return false;
                }

            }
            #endregion

            document.Open();
            Image logo1;

            try
            {
                //place the logo
                string imagePath = Server.MapPath(@"docs\") + "C_logo.bmp";
                logo1 = Image.GetInstance(imagePath);
                logo1.Alignment = Image.ALIGN_LEFT;
                logo1.SetAbsolutePosition(72*3/8, document.PageSize.Height - logo1.ScaledHeight +72*5/16);
                logo1.ScalePercent(48, 48);
                logo1.HasBorder(0);
                document.Add(logo1);

            }

            catch (Exception e)
            {
                myConnection.Close();
                document.Close();
                Session["ErrorMessage"] = "An error has occurred in closing the current Declaration letter: " + e.Message;
                Response.Redirect("ErrorMessage.aspx", false);
                return false;
            }

try
            {
                //place the logo
                string imagePath = Server.MapPath(@"docs\") + "Millennium_logo.bmp";
                Image logo = Image.GetInstance(imagePath);
                logo.SetAbsolutePosition(72 * 6, 72*11- 72*15/16); 
                logo.ScalePercent(48, 48);
                logo.HasBorder(0);
                document.Add(logo);

            }

            catch (Exception e)
            {
                myConnection.Close();
                document.Close();
                Session["ErrorMessage"] = "An error has occurred in closing the current Declaration letter: " + e.Message;
                Response.Redirect("ErrorMessage.aspx", false);
                return false;
            }

            #region GetStewardInformation
            cmd.Parameters.Clear();
            sel = "GetStewardInformation";
            cmd.Parameters.Add(new SqlParameter("@stew_ID", stewID));
            cmd.CommandText = sel;
            cmd.CommandType = CommandType.StoredProcedure;

            using (SqlDataReader stewReader = cmd.ExecuteReader())
            {
                try
                {
                    if (stewReader.HasRows)
                    {
                        while (stewReader.Read())
                        {
                            stewClosing = "\r\n" + stewReader["Stew_Closing"].ToString().Trim() + ",";
                            stewSignature =  stewReader["Stew_Closing_Name"].ToString().Trim() + "\r\n" +
                                            stewReader["Stew_Title"].ToString().Trim();
                            stewSignatureImage = Server.MapPath(@"docs\" + stewReader["Stew_Signature"].ToString().Trim());
                            Build_Content = "\r\n\r\n\r\n\r\n" + stewReader["Stew_Name"].ToString().Trim() + "\r\n"
                                            + stewReader["Stew_Address1"].ToString().Trim() + ", "
                                            + stewReader["Stew_Address2"].ToString().Trim() + "\r\n";
                            if (!(String.IsNullOrEmpty(stewReader["Stew_Address3"].ToString().Trim())))
                            {
                                Build_Content += stewReader["Stew_Address3"].ToString().Trim() + "\r\n";
                            }
                            Build_Content += stewReader["Stew_City"].ToString().Trim();

                            if (stewReader["Abbrev"].ToString().Trim() != null)
                            {
                                Build_Content += ", " + stewReader["Abbrev"].ToString().Trim() + " ";
                            }
                            else
                            {
                                Build_Content += "\r\n";
                            }
                            Build_Content += stewReader["Stew_Postal_Code"].ToString().Trim() + "\r\n"
                            + stewReader["Stew_Phone"].ToString().Trim() + " (phone)\r\n"
                            + stewReader["Stew_Fax"].ToString().Trim() + " (facsimile)\r\n"
                            + stewReader["Stew_Email"].ToString().Trim() ; 
                        }
                    }
                }
                catch (SqlException sep)
                {

                    document.Close();
                    Session["ErrorMessage"] = "An error has occurred in generating Steward information:" + sep.Message;
                    Response.Redirect("ErrorMessage.aspx", false);
                    return false;
                }

            }

            try  // output Build_Content and Date, Build Body comes later
            {
                letter_format = new Paragraph(LineSpacing, Build_Content, new Font(Font.TIMES_ROMAN, 9));
                letter_format.IndentationLeft = left_indent;

                document.Add(letter_format);
                Build_Content = string.Empty;
                Build_Content = "\r\n" + string.Format("{0:dd MMMM yyyy}", CurrTime) + "\r\n";
                letter_format = new Paragraph(LineSpacing, Build_Content, new Font(Font.TIMES_ROMAN, 9));
                letter_format.IndentationLeft = left_indent;
                document.Add(letter_format);

            }
            catch (Exception e)
            {

                myConnection.Close(); 
                document.Close();
                Session["ErrorMessage"] = "An error has occurred outputting Steward Information:" + e.Message;
                Response.Redirect("ErrorMessage.aspx", false);
                return false;

            }
            
            #endregion

            #region GetCustomerLetterInformation
            
            if (Convert.ToInt16(Session["IsCustomerRep"]) == 0)
            {
                UserID = Convert.ToInt16(Session["UserVar"]);
                sel = "GetUserMailingInformation";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@User_Information_ID", UserID));
                cmd.CommandText = sel;
                cmd.CommandType = CommandType.StoredProcedure;

            }
            else
            {
                UserID = Convert.ToInt16(Session["CurrentCompany"]);
                sel = "GetCustomerLetterInformation";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@Customer_ID", UserID));
                cmd.CommandText = sel;
                cmd.CommandType = CommandType.StoredProcedure;

            }

            using (SqlDataReader userReader = cmd.ExecuteReader())
            {

                //  Output the user address information
                try
                {

                    while (userReader.Read())
                    {
                        if (!(Convert.ToInt16(Session["IsCustomerRep"]) == 0))
                        {
                            Build_Body = "\r\n" + "Technical Service Rep, " + Session["UserNameVar"].ToString() + ", on behalf of:" + "\r\n";
                            Build_Body = Build_Body + userReader["Full_Name"].ToString().Trim() + "\r\n";

                        }
                        else
                        {
                            Build_Body = userReader["Full_Name"].ToString().Trim() + "\r\n";
                        }

                        if (!(String.IsNullOrEmpty(userReader["Company_Name"].ToString().Trim())))
                        {
                            Build_Body += userReader["Company_Name"].ToString().Trim() + "\r\n";
                            Build_Body += userReader["Address1"].ToString().Trim() + "\r\n";

                        }
                        else
                        {
                            Build_Body += userReader["Address1"].ToString().Trim() + "\r\n";
                        }



                        if (!(String.IsNullOrEmpty(userReader["Address2"].ToString().Trim())))
                        {
                            Build_Body += userReader["Address2"].ToString().Trim() + "\r\n";
                        }

                        if (!(String.IsNullOrEmpty(userReader["Address3"].ToString().Trim())))
                        {
                            Build_Body += userReader["Address3"].ToString().Trim() + "\r\n";
                        }

                        if (!(String.IsNullOrEmpty(userReader["State_Name"].ToString().Trim())))
                        {
                            Build_Body += userReader["City"].ToString().Trim() + ", "
                                       + userReader["State_Name"].ToString().Trim() + "\r\n"
                                       + userReader["Country"].ToString().Trim() + "\r\n";
                        }
                        else
                        {
                            Build_Body += userReader["City"].ToString().Trim() + ", "
                                      + userReader["Country"].ToString().Trim() + "\r\n";
                        }

                        Build_Body += userReader["Postal_Zip_Code"].ToString().Trim() + "\r\n\r\n";

                        Build_Body += "Dear " + userReader["Full_Name"].ToString().Trim() + ":\r\n";
                    }
                }
                catch (Exception e)
                {
                    myConnection.Close();
                    document.Close();
                    Session["ErrorMessage"] = "An error has occurred in generating the Declarations User Information:" + e.Message;
                    Response.Redirect("ErrorMessage.aspx", false);
                    return false;
                }
                userReader.Close();
            }

            try  
            {
                letter_format = new Paragraph(LineSpacing, Build_Body, new Font(Font.TIMES_ROMAN, 9));
                letter_format.IndentationLeft = 15;
                document.Add(letter_format);
                Build_Body = string.Empty;
                              

            }
            catch (Exception e)
            {

                myConnection.Close(); 
                document.Close();
                Session["ErrorMessage"] = "An error has occurred outputting Customer Information:" + e.Message;
                Response.Redirect("ErrorMessage.aspx", false);
                return false;

            }
            
#endregion

            #region GetFirstParagraph
            sel = "GetLetterContent";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@Region_ID", regID));
            cmd.CommandText = sel;
            cmd.CommandType = CommandType.StoredProcedure;
            Build_Content = string.Empty;

            using (SqlDataReader letterReader = cmd.ExecuteReader())
            {
                try
                {

                    if (letterReader.HasRows)
                    {
                        while (letterReader.Read())
                        {
                            strFirstParagraph = letterReader["Paragraph_1"].ToString().Trim();
                        }
                    }
                    letterReader.Close();

                }
                catch { }
            }

       
            #endregion

            #region IF_ELSE
            int myIN1 = 0;
            int myIN2 = 0;
            int myIN3 = 0;
            int myIN4 = 0;
            int myIN5 = 0;
            cmd.Parameters.Clear();
                        
            if (arrayCPID.Length == 1)
            {
                myIN1 = Convert.ToInt16(arrayCPID[0]);
                sel = "GetProductIssueDeclaration";
                cmd.CommandText = sel;
                cmd.Parameters.Add(new SqlParameter("@intPCID1", myIN1));
                cmd.CommandType = CommandType.StoredProcedure;

            }
            else if (arrayCPID.Length == 2)
            {
                myIN1 = Convert.ToInt16(arrayCPID[0]);
                myIN2 = Convert.ToInt16(arrayCPID[1]);
                sel = "GetProductIssueDeclaration2";
                cmd.CommandText = sel;
                cmd.Parameters.Add(new SqlParameter("@intPCID1", myIN1));
                cmd.Parameters.Add(new SqlParameter("@intPCID2", myIN2));
                cmd.CommandType = CommandType.StoredProcedure;

            }
            else if (arrayCPID.Length == 3)
            {
                myIN1 = Convert.ToInt16(arrayCPID[0]);
                myIN2 = Convert.ToInt16(arrayCPID[1]);
                myIN3 = Convert.ToInt16(arrayCPID[2]);
                sel = "GetProductIssueDeclaration3";
                cmd.CommandText = sel;
                cmd.Parameters.Add(new SqlParameter("@intPCID1", myIN1));
                cmd.Parameters.Add(new SqlParameter("@intPCID2", myIN2));
                cmd.Parameters.Add(new SqlParameter("@intPCID3", myIN3));
                cmd.CommandType = CommandType.StoredProcedure;

            }
            else if (arrayCPID.Length == 4)
            {
                myIN1 = Convert.ToInt16(arrayCPID[0]);
                myIN2 = Convert.ToInt16(arrayCPID[1]);
                myIN3 = Convert.ToInt16(arrayCPID[2]);
                myIN4 = Convert.ToInt16(arrayCPID[3]);
                sel = "GetProductIssueDeclaration4";
                cmd.CommandText = sel;
                cmd.Parameters.Add(new SqlParameter("@intPCID1", myIN1));
                cmd.Parameters.Add(new SqlParameter("@intPCID2", myIN2));
                cmd.Parameters.Add(new SqlParameter("@intPCID3", myIN3));
                cmd.Parameters.Add(new SqlParameter("@intPCID4", myIN4));
                cmd.CommandType = CommandType.StoredProcedure;

            }
            else if (arrayCPID.Length == 5)
            {
                myIN1 = Convert.ToInt16(arrayCPID[0]);
                myIN2 = Convert.ToInt16(arrayCPID[1]);
                myIN3 = Convert.ToInt16(arrayCPID[2]);
                myIN4 = Convert.ToInt16(arrayCPID[3]);
                myIN5 = Convert.ToInt16(arrayCPID[4]);
                sel = "GetProductIssueDeclaration5";
                cmd.CommandText = sel;
                cmd.Parameters.Add(new SqlParameter("@intPCID1", myIN1));
                cmd.Parameters.Add(new SqlParameter("@intPCID2", myIN2));
                cmd.Parameters.Add(new SqlParameter("@intPCID3", myIN3));
                cmd.Parameters.Add(new SqlParameter("@intPCID4", myIN4));
                cmd.Parameters.Add(new SqlParameter("@intPCID5", myIN5));
                cmd.CommandType = CommandType.StoredProcedure;
            }
            #endregion

            #region buildDeclaration
            using (SqlDataReader contentReader = cmd.ExecuteReader())
            {

                if (contentReader.HasRows)
                {
                    while (contentReader.Read())
                    {
                        if (!(DisplayedParagraph))
                        {
                            if (regID == 4)
                            {
                                strFirstParagraph = "\r\n" + strFirstParagraph.Replace("*", contentReader["Product_Name"].ToString().Trim()) ;
                                letter_format = new Paragraph(LineSpacing, strFirstParagraph, new Font(Font.TIMES_ROMAN, 9));
                                letter_format.IndentationLeft = 15;
                                document.Add(letter_format);
                                DisplayedParagraph = true;
                            }
                            else
                            {
                                strFirstParagraph = "\r\n" + strFirstParagraph ;
                                letter_format = new Paragraph(LineSpacing, strFirstParagraph, new Font(Font.TIMES_ROMAN, 9));
                                letter_format.IndentationLeft = 15;
                                document.Add(letter_format);
                                DisplayedParagraph = true;
                            }
                        }

                        Build_Content = "\r\n";
                        declaration_paragraphs = new Paragraph(15, Build_Content, new Font(Font.TIMES_ROMAN, 9));
                        document.Add(declaration_paragraphs);
                        Build_Content = String.Empty;

                        if (regID != 4)
                        {
                            
                                                 

                            sTemp = contentReader["Product_Name"].ToString().Trim();
                            Build_Body = contentReader["Declaration_Text"].ToString().Trim();
                            Build_Content = Build_Content + Build_Body.Replace("*", sTemp) + "\r\n" ;
                            declaration_paragraphs = new Paragraph(LineSpacing, Build_Content, new Font(Font.TIMES_ROMAN, 9));
                            declaration_paragraphs.FirstLineIndent = 10;
                            declaration_paragraphs.IndentationLeft = 15;
                            document.Add(declaration_paragraphs);

                        }
                    }  // close while
                }  // close if hasRows
                contentReader.Close();
            }
            #endregion

            #region GetSecondParagraph
            sel = "GetLetterContent";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@Region_ID", regID));
            cmd.CommandText = sel;
            cmd.CommandType = CommandType.StoredProcedure;
            //Build_Content = string.Empty;

            using (SqlDataReader letterReader = cmd.ExecuteReader())
            {
                if (letterReader.HasRows)
                {
                    while (letterReader.Read())
                    {
                        if (regID != 4)
                        {
                            myString = "\r\n" + letterReader["Paragraph_2"].ToString().Trim() + "\r\n";
                        }
                        else
                        {

                            myString =  letterReader["Paragraph_2"].ToString().Trim() + "\r\n";
                        }
                    }
                }
                letterReader.Close();
            }
            #endregion

            
            letter_format = new Paragraph();
            letter_format.KeepTogether = true;
            letter_format = new Paragraph(LineSpacing, myString + stewClosing, new Font(Font.TIMES_ROMAN, 9));
            letter_format.IndentationLeft = 15;
            document.Add(letter_format);

            try
            {
                //place the logo
                string imagePath = stewSignatureImage;
                Image logo = Image.GetInstance(imagePath);
                logo.ScalePercent(50, 50);
                logo.HasBorder(0);
                document.Add(logo);

            }

            catch (Exception e)
            {
                myConnection.Close(); 
                document.Close();
                Session["ErrorMessage"] = "An error has occurred in closing the current Declaration letter: " + e.Message;
                Response.Redirect("ErrorMessage.aspx", false);
                return false;
            }

            letter_format = new Paragraph(LineSpacing, stewSignature, new Font(Font.TIMES_ROMAN, 9));
            letter_format.IndentationLeft = 15;
            document.Add(letter_format);


            try
            { 
                document.Close();
            }
            catch { }


            sel = "LearnIfGlobal";
            cmd.Parameters.Clear();
            bool boolretValue = false;
            cmd.Parameters.Add(new SqlParameter("@Region_ID", regID));
            cmd.CommandText = sel;
            cmd.CommandType = CommandType.StoredProcedure;

            using (SqlDataReader Globalreader = cmd.ExecuteReader())
            {
                String[] arrFiles;

                if (Globalreader.HasRows)
                {
                    while (Globalreader.Read())
                    {
                        boolretValue = Convert.ToBoolean(Globalreader["Is_Global"]);
                    }
                   
                }
                Globalreader.Close();

                if (boolretValue)
                {
                    string GlobalDestinationFile = Server.MapPath(@"docs\" + Session["PDF_name"].ToString() + ++this_FileOutPutCount + Session["GetRandomNumber"].ToString() + ".pdf");
                    arrFiles = new String[2];
                                       
                    DateTime local_DT = DateTime.Now;
                    sel = "GetMaxDateWHERE";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@CPID", arrayCPID[0]));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sel;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                local_DT = (DateTime)dr["M_Date"];
                            }
                        }
                        dr.Close();
                    }
                    
                   
                    sel = "GetGlobalFoodDoc";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@Product_Compliance_ID", Convert.ToInt32(arrayCPID[0])));
                    cmd.Parameters.Add(new SqlParameter("@m_date", local_DT));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = sel;
                    
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                attachmentFile = dr["GFD"].ToString();
                            }
                        }
                        else
                        { 
                            attachmentFile = String.Empty; 
                        }
                        
                        dr.Close();                    
                    }
                    

                    if (!(String.IsNullOrEmpty(attachmentFile)))
                    {
                        attachmentFile = Server.MapPath(@"docs\" + attachmentFile + ".pdf");
                    }
                    else if (String.IsNullOrEmpty(attachmentFile))
                    {
                        myConnection.Close(); 
                        Session["ErrorMessage"] = "The Global Status attachment file does not exists";
                        return false;
                    }

                    arrFiles[0] = GlobalOutput;
                    arrFiles[1] = attachmentFile;
                    boolretVal = PdfMerge.MergeFiles(GlobalDestinationFile, arrFiles);
                    
                    if (!(boolretVal))
                    {
                        myConnection.Close(); 
                        Session["ErrorMessage"] = "There was an error in PDfMerge.MergeFiles.";
                        Response.Redirect("ErrorMessage.aspx", false);
                        return false;
                    }
                }
                
            }  //using (SqlCommand cmd = myConnection.CreateCommand())
        }
         myConnection.Close();
         return true;
    }  // function

       
    


    protected string GetRandomNumber()
    {
        Random rm = new Random();
        return Convert.ToString(rm.Next(1000, 9999));
    }

   
   


  


    
}








                 