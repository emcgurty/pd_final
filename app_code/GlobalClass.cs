using System;
using System.Configuration;


public class GlobalClass
{
    private static string m_Title;
    private static string m_GetImagePath;
    
    public static string Email_Disclaimer()
    {
      return ConfigurationManager.AppSettings["emailDisclaimer1"].ToString() + ConfigurationManager.AppSettings["emailDisclaimer2"].ToString();
    }

    public static string GetImagePath
    {
        get { return m_GetImagePath; }
        set { m_GetImagePath = value; }
    }
    
      
    public static string TitleVar
    {
        get { return m_Title; }
        set { m_Title = value; }
    }
       
    
   public static bool Validate_Password(string PW, string confirm)

    { 
    bool NoError = true;
        
    if (string.IsNullOrEmpty(PW) || string.IsNullOrEmpty(confirm))
    { NoError = false;}
    
    if (!(PW.Length >= 6)&& (PW.Length <= 12))
    { NoError = false;}
           
    if (!( confirm.ToString() ==  PW.ToString() ))
    { NoError = false;}

    return NoError;
   }

    public static bool IsAlphaNumeric(string strToCheck)
    {

        int PasswordLength = strToCheck.Length;
        int i = 0;
        int j = 0;
        bool FoundCap = false;
        bool FoundNumeric = false;
        string[] AlphabetCaps = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        string[] arrPassword;
        arrPassword = new string[PasswordLength];


        for (i = 0; i < PasswordLength; i++)
        {
            arrPassword[i] = strToCheck.Substring(i, 1);
        }


        for (i = 0; i < AlphabetCaps.Length; i++)
        {
            for (j = 0; j < arrPassword.Length; j++)
            {
                if (String.Compare(AlphabetCaps[i], arrPassword[j], false) == 0)
                {
                    FoundCap = true;
                    break;
                }
               
            }
            if (FoundCap)
            {
                break;
            }
        }

        for (j = 0; j < arrPassword.Length; j++)
        {
            try
            {
                Convert.ToInt16(arrPassword[j]);
                FoundNumeric = true;
                break;
            }
            catch
            { }

        }

        return ((FoundNumeric) && (FoundCap));

    }

    public static string GetConnectionString()
    {
        return ConfigurationManager.ConnectionStrings
            ["EMM_Connection"].ConnectionString;
    }

    }