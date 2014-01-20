using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.IO;


public class DB_Values
{

     
    private static List<DB_Row_Values> search_row = new List<DB_Row_Values>();
    private static List<DB_Row_Hold> search_row_criteria = new List<DB_Row_Hold>();
    private static List<DB_Row_Selected> search_row_selected = new List<DB_Row_Selected>();


    public DB_Values()
    {}

    
    public List<DB_Row_Values> DB_List
    {

        get { return search_row; }

        set { search_row = value; }

    }

    public List<DB_Row_Values> GetRowValues()      
    {
        return search_row;
    }

    public List<DB_Row_Hold> GetRowValuesCriteria(int Product_ID, int Agency_ID)      
    {
        int PID = 0;
        int AID = 0;
        if (Agency_ID == -1)
            return search_row_criteria;

        if (!(Product_ID > 0))
        {
            //PID = Convert.ToInt16(Session["SelectedProductID"]);
        }
        else
        {
            PID = Product_ID;
        }

        if (!(Agency_ID > 0))
        {
        //    AID = Convert.ToInt16(Session["SelectedAgencyID"]);
        }
        else
        {
            AID = Agency_ID;
        }



         search_row_criteria.Clear();
         //int counter = 0;

         foreach(DB_Row_Values drw in DB_List)
         {
             if ((drw.Product_ID == PID ) && (drw.Agency_ID == AID))
             {
                 search_row_criteria.Add(new DB_Row_Hold(drw.ColrowID, drw.Product_ID, drw.Agency_ID, drw.Issue_ID, drw.Product_Name, drw.Agency, drw.Concern_Issue, drw.Is_Selected));
             }
         }
         return search_row_criteria;
    }
 
    // Show all user selections in the UserSelections
    public List<DB_Row_Selected> GetRowValuesSelected()
    {
        search_row_selected.Clear();
        
        foreach (DB_Row_Values drw in DB_List)
        {
            if (drw.Is_Selected == 1)
            {
                search_row_selected.Add(new DB_Row_Selected(drw.ColrowID, drw.Product_ID, drw.Agency_ID, drw.Issue_ID, drw.Product_Name, drw.Agency, drw.Concern_Issue, drw.Is_Selected));
            }
        }
        return search_row_selected;
    }


    public int GetSelectRowCount()
    {
        return search_row_selected.Count;

    }
    
    
    public static int GetRowCount()
    {
        return search_row.Count;
        
    }
    
 
    public bool InsertRows(int intRowCounter, int PID, int AID, int IID, 
                           string Product, string Issue, string Agency, int Is_Selected)
    {

        try
        {
            search_row.Add(new DB_Row_Values(intRowCounter,PID, AID, IID, Product, Agency, Issue, Is_Selected ));
            
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

    public void UpdateCurrentRow(int ColrowID, int iIs_Selected) 
    {
        int cr = ColrowID;
        search_row[cr].Is_Selected= iIs_Selected;
       

    }


  



    
}








    
    
    
    
    