using CRUD_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Data.SqlClient;

namespace CRUD_WebAPI.Controllers
{
    public class HomeController : Controller
    {
        string dbConnectionString = @"Data Source=DESKTOP-CKOPJQ7\SQLEXPRESS;Initial Catalog=CRUD;User ID=sa;Password=1234";
        SqlDataReader reader;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            UserInfo UI = new UserInfo();

            SqlConnection mainConn = new SqlConnection(dbConnectionString);
            mainConn.Open();
            SqlCommand selectCmd = new SqlCommand("select * from UserInfoCRUDADO", mainConn);
            reader = selectCmd.ExecuteReader();

            while (reader.Read())
            {
                UI.UserName = reader.GetValue(1).ToString();
            }

            mainConn.Close();
            return View(UI);
        }

        [HttpPost]
        public IActionResult Index(UserInfo UI)
        {
            SqlConnection mainConn = new SqlConnection(dbConnectionString);
            mainConn.Open();
            SqlCommand insertCmd = new SqlCommand("insert into UserInfoCRUDADO (UserName) values ('" + UI.UserName + "')",mainConn);
            insertCmd.ExecuteNonQuery();
            mainConn.Close();
            return View(UI);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}