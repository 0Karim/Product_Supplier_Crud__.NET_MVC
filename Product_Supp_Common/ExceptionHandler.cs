using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace Product_Supp_Common
{
    public static class ExceptionHandler
    {
        private const string coreExcep = "-------------[Core Exception]-----------------";
        private const string bubbledExcep = "-------------[Bubbled Exception]-----------------";


        public static void LogException(Exception ex , string layerName)
        {
            if (layerName.Contains("Data_Access_Layer")) //Exception from data access layer
            {
                WriteExceptionToFile("~/Errors/Product_Supp_DataAccessLayer_Exceptions/", layerName + ".txt", GetExceptionMessageFormatted(ex));
            }
            else if (layerName.Contains("Bussiness_Logic_Layer")) //Exception from business logic layer
            {
                WriteExceptionToFile("~/Errors/Product_Supp_BusiniessLogicLayer_Exceptions/", layerName + ".txt", GetExceptionMessageFormatted(ex));
            }
            else if(layerName.Contains("Presentation_Layer")) //Exception from Presentation Layer
            {
                WriteExceptionToFile("~/Errors/ProductSupp_MVC_PresentationLayer_Exceptions/", layerName + ".txt", GetExceptionMessageFormatted(ex));
            }
        }

        private static void WriteExceptionToFile(string folderpath , string filename , string filetext)
        {
            if(!string.IsNullOrEmpty(folderpath) && !string.IsNullOrEmpty(filename) && !string.IsNullOrEmpty(filetext))
            {
                //create folder first if not created
                folderpath = CreateFolder(folderpath);

                //get file name
                filename = Path.GetFileName(filename);

                string file_path_name = Path.Combine(folderpath, filename);

                using(var fs = new FileStream(file_path_name , FileMode.Append))
                {
                    using (var sw = new StreamWriter(fs , Encoding.UTF8))
                    {
                        sw.Write(filetext);
                        sw.Flush();
                        sw.Close();
                    }
                }
            }
        }

        private static string CreateFolder(string folderpath)
        {
            if(!string.IsNullOrEmpty(folderpath))
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(folderpath)))
                {
                     
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderpath));
                }
            }
            return HttpContext.Current.Server.MapPath(folderpath);
        }


        private static string GetExceptionMessageFormatted(Exception ex)
        {
            var sb = new StringBuilder();

            do
            {
                if(ex.InnerException == null)
                {
                    sb.AppendLine(coreExcep);
                    sb.AppendLine("Source\t\t: " + ex.Source.Trim());
                    sb.AppendLine("Method\t\t: " + ex.TargetSite.Name);
                    sb.AppendLine("Date\t\t: " + DateTime.Now.ToString("dd/MM/yyyy"));
                    sb.AppendLine("Time\t\t: " + DateTime.Now.ToString("HH:mm:ss"));
                    sb.AppendLine("Error\t\t: " + ex.Message.Trim());
                    sb.AppendLine("Stack Trace\t: " + ex.StackTrace.Trim());
                }
                else
                {
                    sb.AppendLine(bubbledExcep);
                    sb.AppendLine("Source\t\t: " + ex.Source.Trim());
                    sb.AppendLine("Method\t\t: " + ex.TargetSite.Name);
                    sb.AppendLine("Date\t\t: " + DateTime.Now.ToString("dd/MM/yyyy"));
                    sb.AppendLine("Time\t\t: " + DateTime.Now.ToString("HH:mm:ss"));
                    sb.AppendLine("Error\t\t: " + ex.Message.Trim());
                    sb.AppendLine("Stack Trace\t: " + ex.StackTrace.Trim());

                    ex = ex.InnerException;
                }
            } while (ex.InnerException != null);


            return sb.ToString();
        }
    }
}
