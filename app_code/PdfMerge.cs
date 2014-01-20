using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

public class PdfMerge 

{
    public static bool MergeFiles(string destinationFile, string[] sourceFiles)
        {

        int i = 0;
        int n;
       
   
        try
            {
                int f = 0;
                // we create a reader for a certain document
                PdfReader reader = new PdfReader(sourceFiles[f]);
                n = reader.NumberOfPages;
                Document document = new Document(reader.GetPageSizeWithRotation(1));
                PdfWriter writer;
            
                try
                {
                    writer = PdfWriter.GetInstance(document, new FileStream(destinationFile, FileMode.CreateNew));
                }
                catch (PdfException pex)
                {
                   System.Web.HttpContext.Current.Session["ErrorMessage"] = "There was an error in PDfMerge.MergeFiles to establish a PdfWriter.GetInstance with FileMode.CreateNew.  With error message:" + pex.Message + ". Inner exception: " + pex.InnerException + ". Stack trace: " + pex.StackTrace;
                   return false;

                }
                
                document.Open();
               
                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;
                int rotation;

                while (f < sourceFiles.Length)
                {
                    i = 0;
                    while (i < n)
                    {
                        i++;
                        document.SetPageSize(reader.GetPageSizeWithRotation(i));
                        document.NewPage();

                        page = writer.GetImportedPage(reader, i);
                                             rotation = reader.GetPageRotation(i);

                        if (rotation == 90 || rotation == 270)
                        {
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        }
                        else
                        {
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        }

                    }
                    f++;
                    if (f < sourceFiles.Length)
                    {
                        try
                        {
                            reader = new PdfReader(sourceFiles[f]);
                            n = reader.NumberOfPages;
                        }
                        catch (PdfException pex)
                        {

                            System.Web.HttpContext.Current.Session["ErrorMessage"] = "There was an error in PDfMerge.MergeFiles to read the number of pages from PDF source files.  With error message:" + pex.Message + ". Inner exception: " + pex.InnerException + ". Stack trace: " + pex.StackTrace; 
                            document.Close();
                            return false;

                        }

                    }
                }

               
            
            document.Close();
            
            }
            catch 
            {
              return false;
            }
            return true;

        }

        
    }
