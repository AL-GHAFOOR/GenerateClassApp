using System.Collections.Generic;
using ADO.NET_Store_Procedure;
using GenerateProcedureClass.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace GenerateProcedureClass.Controllers
{
    public class HomeController : Controller
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private string connectionstring;

        public HomeController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            connectionstring = _context.Database.GetDbConnection().ConnectionString;
        }

        public IActionResult Index()
        {
            List<string[]> paraStrings = new List<string[]>();
            paraStrings.Add(new[] { "@id", "1" });
            paraStrings.Add(new[] { "@name", "1" });
            List<sp_donate> list = SpProcedureCallBack.GetStoreProcedureCallBack<sp_donate>(connectionstring, "sp_donate", paraStrings);


            return View();
        }

        public IActionResult GenerateFile()
        {
            string contentRootPath = _hostingEnvironment.ContentRootPath + "\\Models";

            // string generateAllProcedureClass = SpProcedureCallBack.GenerateAllProcedureClass(connectionstring, contentRootPath);
            {
                List<string[]> paraStrings = new List<string[]>();
                paraStrings.Add(new[] { "@id", "1" });
                paraStrings.Add(new[] { "@name", "1" });

                string singleFileWithParamter =
                    SpProcedureCallBack.GenerateSingleProcedureClass(connectionstring, "sp_donate", contentRootPath,
                        paraStrings);


                //string singleFileWithoutParameter = SpProcedureCallBack.GenerateSingleProcedureClass(connectionstring, "sp_customer", contentRootPath, null);


                return Content(singleFileWithParamter);

            }
        }
    }
}

