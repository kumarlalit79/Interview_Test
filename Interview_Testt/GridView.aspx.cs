using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Interview_Testt
{
    public partial class GridView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
           

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"
                    SELECT u.Id, u.Name, u.Address, u.City, u.Mobile, u.Email, u.WhatsAppNumber, 
                           u.Aadhaar_Card_Number, 
                           u.Aadhaar_Card_Pic,
                            u.Passport_Size_Pic,
                           (SELECT STRING_AGG(Q.Qualification, ', ') 
                            FROM Qualification Q 
                            WHERE Q.user_id = u.Id) AS Qualifications 
                    FROM user_tbl u";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);
                gvUsers.DataSource = dt;
                gvUsers.DataBind();
            }
        }

        protected void Btn_Edit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string userid = btn.CommandName.ToString();
            Session["id"] = userid;
            Response.Redirect("Crud.aspx?Action=True");
        }

        protected void Btn_delete_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            Button btn = (Button)sender;
            string userid = btn.CommandName.ToString();
            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM user_tbl WHERE id = " + userid + "", con);
                    
                    deleteCmd.ExecuteNonQuery();

                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();

                        BindGrid();
                    }
                }
            }
        }

        protected void btnserach_Click(object sender, EventArgs e)
        {
            string param = "";

            if (ddlsearchtype.SelectedIndex > 0)
            {
                if (ddlsearchtype.SelectedValue == "AadharNo")
                {
                    param = " and Aadhaar_Card_Number=" + "'"+txtsearchnumber.Text+"'";
                }
                else
                {
                    param = " and Mobile=" + "'" + txtsearchnumber.Text + "'";
                }
                BindGridbysearch(param);
            }
            else
            {
                BindGrid();
            }
        }
        private void BindGridbysearch(string param)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;


            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"
                    SELECT u.Id, u.Name, u.Address, u.City, u.Mobile, u.Email, u.WhatsAppNumber, 
                           u.Aadhaar_Card_Number, 
                           u.Aadhaar_Card_Pic,u.Passport_Size_Pic,
                           (SELECT STRING_AGG(Q.Qualification, ', ') 
                            FROM Qualification Q 
                            WHERE Q.user_id = u.Id) AS Qualifications 
                    FROM user_tbl u where 1=1 " + param+"";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);
                gvUsers.DataSource = dt;
                gvUsers.DataBind();
            }
        }

        protected void btnsearchall_Click(object sender, EventArgs e)
        {
            ddlsearchtype.ClearSelection();
            txtsearchnumber.Text =string.Empty;

            BindGrid();
        }
    }
}