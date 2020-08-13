using DidonCmsAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using Npgsql;
using L2T.Core.BusinessEntities;



namespace DidonCmsAPI.Controllers
{


    public class APIDidonController : ApiController
    {

        private string CONNECTION_STRING = Convert.ToString(ConfigurationSettings.AppSettings["CONNECTION_STRING"]);
        private string chaineConnexion;
        private NpgsqlConnection npgsqlConnectionAPI;
        private string msisdn;
        public string sms { get; private set; }


        //  PGDbContext _context;

        /*   public APIDidonController()
           {
               _context = new PGDbContext();

           }*/


        #region api didonCMS


        [System.Web.Http.HttpGet]

        [System.Web.Http.Route("api/id_user_service/{id_user_service:int}/nbrSMS/{nbrSMS:int}")]
        public List<Data> GetList(int id_user_service, int nbrSMS) {
        var result = new List<Data>();
         string sql3 = @"select * from fct_service_elensan_tv_flux(:id_user_service, :nbrSMS)";
        NpgsqlConnection pgcon = new NpgsqlConnection(Convert.ToString(ConfigurationSettings.AppSettings["CONNECTION_STRING"]));
        pgcon.Open();
        NpgsqlCommand pgcom = new NpgsqlCommand(sql3, pgcon);
        pgcom.CommandType = System.Data.CommandType.Text;
        pgcom.Parameters.AddWithValue("id_user_service",id_user_service );
        pgcom.Parameters.AddWithValue("nbrSMS", nbrSMS);
        NpgsqlDataReader pgreader = pgcom.ExecuteReader();

        while (pgreader.Read()) {

                Data tmpRecord = new Data()
                
                
                {
                    msisdn = pgreader[1].ToString(),
                    sms = pgreader[2].ToString()

                };
                result.Add(tmpRecord);
            }
            return result;
        }




        /*
            {
                string sql3 = @"select * from fct_service_elensan_tv_flux(:id_user_servicel, :nbrSMS)";
                try
                {

                   //chaineConnexion = CONNECTION_STRING;
                  // npgsqlConnectionAPI = new NpgsqlConnection(chaineConnexion);

                  // npgsqlConnectionAPI.Open();

                    //  if (isAuthentificate(key) ==1 )
                    // {
                    var result = _context.Database.SqlQuery<Data>("select * from fct_service_elensan_tv_flux("+ id_user_service+","+ nbrSMS+") ").AsEnumerable();
                  //  Console.WriteLine(result);
                   // npgsqlConnectionAPI.Dispose();
                   // npgsqlConnectionAPI.Close();
                    return result;
                    // }


                }


                catch (Exception ex)
                {
                    return null;
                }
            }*/

        #endregion

        /*   public int isAuthentificate(string key)
               {
                   try
                   {
                       int userCount = _context.Database.SqlQuery<int>("select count(*) from cm_user where md5(login || pwd)='" + key + "'").SingleOrDefault();
                       return userCount;

                   }
                   catch (Exception ex)
                   {
                       return 0;
                   }
               }*/
    }
}