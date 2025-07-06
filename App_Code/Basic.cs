using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Collections;

/// <summary>
/// Summary description for Basic
/// </summary>
public class Basic
{
    public string checkSafeMode(string userid)
    {
               
        SqlConnection cn;
        SqlCommand cmd;
        cn = new SqlConnection(ConfigurationManager.AppSettings["LIS"]);
        cmd = new SqlCommand();
        cn.Open();
        cmd.CommandText = "select status from SafeMode where username=@username";
        cmd.Parameters.AddWithValue("@username",userid);
        cmd.Connection = cn;
        Object obj = cmd.ExecuteScalar();

        cn.Close();

        return obj.ToString();
    }

}