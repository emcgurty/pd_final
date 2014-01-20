using System;
using System.Collections.Generic;
using System.Text;


public class Grid_Row_Values
{
    private int gTable_ID = 0;
    private int gProduct_Compliance_ID = 0;
    private int gProduct_ID = 0;
    private int gAgency_ID = 0;
    private int gIssue_ID = 0;
    private string gProduct_Name = String.Empty;
    private string gAgency = String.Empty;
    private string gConcern_Issue = String.Empty;
    private int gIs_Selected = 0;
    


    public Grid_Row_Values(int intTID, int intPCID, int intPID, int intAID, int intIID, 
                          string strP, string strA, string strI, int intIS)
    {
          this.gTable_ID = intTID;
          this.gProduct_Compliance_ID = intPCID;
          this.gProduct_ID = intPID;
          this.gAgency_ID = intAID;
          this.gIssue_ID = intIID;
          this.gProduct_Name = strP;
          this.gAgency = strA;
          this.gConcern_Issue = strI;
          this.gIs_Selected = intIS;
    }


    public int ColrowID
    {
        get
        {
            return gTable_ID;
        }
        set
        {
            gTable_ID = value;
        }
    }

    public int Product_Compliance_ID
    {
        get
        {
            return gProduct_Compliance_ID;
        }
        set
        {
            gProduct_Compliance_ID = value;
        }
    }
    
    
    public string Product_Name
    {
        get
        {
            return gProduct_Name;
        }
        set
        {
            gProduct_Name = value;
        }
    }
    
    
    public string Agency
    {
        get
        {
            return gAgency;
        }
        set
        {
            gAgency = value;
        }
    }


    public string Concern_Issue
    {
        get
        {
            return gConcern_Issue;
        }
        set
        {
            gConcern_Issue = value;
        }
    }

    public int Product_ID
    {
        get
        {
            return gProduct_ID;
        }
        set
        {
            gProduct_ID = value;
        }
    }

    public int Agency_ID
    {
        get
        {
            return gAgency_ID;
        }
        set
        {
            gAgency_ID = value;
        }
    }

    public int Issue_ID
    {
        get
        {
            return gIssue_ID;
        }
        set
        {
            gIssue_ID = value;
        }
    }

    public int Is_Selected
    {
        get
        {
            return gIs_Selected;
        }
        set
        {
            gIs_Selected = value;
        }
    } 
    

}





