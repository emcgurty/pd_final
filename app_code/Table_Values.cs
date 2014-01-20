using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.IO;


public class Table_Values
{

    //public List<Grid_Row_Values> search_row; 
    private static List<Grid_Row_Values> search_row = new List<Grid_Row_Values>();

    public Table_Values()
    {}

    public List<Grid_Row_Values> GSDList
    {

        get { return search_row; }

        set { search_row = value; }

    }

    public List<Grid_Row_Values> GetRowValues()       /// Should be called add new row
    {
        return search_row;
    }

      

    public static int GetRowCount()
    {
        return search_row.Count;
        
    }
    
 
    public bool InsertRows(int intRowCounter, int PCID, int PID, int AID, int IID, string Product, string Issue, string Agency, int intIS)
    {

        try
        {
            search_row.Add(new Grid_Row_Values(intRowCounter,PCID, PID, AID, IID, Product, Agency, Issue, intIS ));
            
        }
        catch
        {
            return false;
        }

        return true;
    }

   public void DeleteRowValue()
    {
        search_row.Clear();
   }

    public static void UpdateCurrentRow(int ColrowID, int iTID, int iPID, int iAID, int iIID,
                            string sProduct, string sIssue, string sAgency, int iIs_Selected ) 
    {
        int cr = ColrowID -1;
        search_row[cr].ColrowID = iTID;
        search_row[cr].Product_ID = iPID;
        search_row[cr].Agency_ID = iAID;
        search_row[cr].Issue_ID = iIID;
        search_row[cr].Product_Name = sProduct;
        search_row[cr].Concern_Issue = sIssue;
        search_row[cr].Agency = sAgency;
        search_row[cr].Is_Selected = iIs_Selected;

    }




    
}








    
    
    
    
    