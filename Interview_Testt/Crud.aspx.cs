using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Interview_Testt
{
    public partial class Crud : System.Web.UI.Page
    {
        SqlConnection con;
        SqlDataAdapter da;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string Action = "";
                Action = Request.QueryString["Action"];
                if (!string.IsNullOrEmpty(Action) && Action == "True")
                {
                    btnAddUser.Visible = false;
                    btnUpdate.Visible = true;
                    showuser();
                }
                else
                {
                    btnAddUser.Visible = true;
                    btnUpdate.Visible = false;
                }
            }
            
        }

        private void showuser()
        {
            try
            {
                string userid = Session["id"].ToString();

                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
               
                
                    SqlDataAdapter da = new SqlDataAdapter(@"SELECT u.Id, u.Name, u.Address, u.City, u.Mobile, u.Email, u.WhatsAppNumber,
                           u.Aadhaar_Card_Number,
                           u.Aadhaar_Card_Pic,u.Gender,Passport_Size_Pic,
                           (SELECT STRING_AGG(Q.Qualification, ', ')
                            FROM Qualification Q
                            WHERE Q.user_id = u.Id) AS Qualifications
                    FROM user_tbl u  where id=" + userid+" ", con);
                  
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        txtName.Text = dt.Rows[0]["Name"].ToString();
                        txtAddress.Text = dt.Rows[0]["Address"].ToString();
                        txtCity.Text = dt.Rows[0]["City"].ToString();
                        txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                        txtEmail.Text = dt.Rows[0]["Email"].ToString();
                        txtWhatsApp.Text = dt.Rows[0]["WhatsAppNumber"].ToString();
                        txtAadhaar.Text = dt.Rows[0]["Aadhaar_Card_Number"].ToString();
                        rblGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
                        hdnaadhar.Value= dt.Rows[0]["Aadhaar_Card_Pic"].ToString(); 
                        hdnpass.Value = dt.Rows[0]["Passport_Size_Pic"].ToString(); 

                        foreach (ListItem item in chkQualifications.Items)
                        {
                            item.Selected = false; 
                        }

                        string qualification = dt.Rows[0]["Qualifications"].ToString();
                        if (!string.IsNullOrEmpty(qualification))
                        {
                            string[] qualifications = qualification.Split(','); 
                            foreach (string qual in qualifications)
                            {
                                ListItem item = chkQualifications.Items.FindByValue(qual.Trim());
                                if (item != null)
                                {
                                    item.Selected = true; // Select the corresponding checkbox
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnAddUser_Click1(object sender, EventArgs e)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

                string adharFilePath = null;
                string passportFilePath = null;

                if (FileUpldAdhar.HasFile)
                {
                    string adharFileName = Path.GetFileName(FileUpldAdhar.FileName);
                    adharFilePath = Path.Combine(Server.MapPath("~/images/"), adharFileName);
                    FileUpldAdhar.SaveAs(adharFilePath); 
                    adharFilePath = "~/images/" + adharFileName; 
                }

                if (FileUpldPassport.HasFile)
                {
                    string passportFileName = Path.GetFileName(FileUpldPassport.FileName);
                    passportFilePath = Path.Combine(Server.MapPath("~/images/"), passportFileName);
                    FileUpldPassport.SaveAs(passportFilePath); 
                    passportFilePath = "~/images/" + passportFileName; 
                }

                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO user_tbl (Name, Address, City, Mobile, Email, WhatsAppNumber, Aadhaar_Card_Pic, Aadhaar_Card_Number, Passport_Size_Pic, Gender, Active) VALUES (@Name, @Address, @City, @Mobile, @Email, @WhatsAppNumber, @Aadhaar_Card_Pic, @Aadhaar_Card_Number, @Passport_Size_Pic, @Gender, @Active); SELECT SCOPE_IDENTITY();", con);

                    cmd.Parameters.AddWithValue("@Name", txtName.Text.Replace("'","`"));
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Replace("'", "`"));
                    cmd.Parameters.AddWithValue("@City", txtCity.Text.Replace("'", "`"));
                    cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Replace("'", "`"));
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Replace("'", "`"));
                    cmd.Parameters.AddWithValue("@WhatsAppNumber", txtWhatsApp.Text.Replace("'", "`"));
                    cmd.Parameters.AddWithValue("@Aadhaar_Card_Pic", adharFilePath);
                    cmd.Parameters.AddWithValue("@Aadhaar_Card_Number", txtAadhaar.Text.Replace("'", "`"));
                    cmd.Parameters.AddWithValue("@Passport_Size_Pic", passportFilePath);
                    cmd.Parameters.AddWithValue("@Gender", rblGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@Active", 1);

                    con.Open();

                    
                    int newUserId = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (ListItem item in chkQualifications.Items)
                    {
                        if (item.Selected)
                        {
                            SqlCommand qualCmd = new SqlCommand("INSERT INTO Qualification (Qualification, user_id) VALUES (@Qualification, @UserId)", con);
                            qualCmd.Parameters.AddWithValue("@Qualification", item.Value);
                            qualCmd.Parameters.AddWithValue("@UserId", newUserId);
                            qualCmd.ExecuteNonQuery();
                        }
                    }

                    Response.Write("<script>alert('Record Saved Successfully')</script>");
                    Response.Redirect("GridView.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                string userid = Session["id"].ToString();
                string adharFilePath = null;
                string passportFilePath = null;

                if (FileUpldAdhar.HasFile)
                {
                    string adharFileName = Path.GetFileName(FileUpldAdhar.FileName);
                    adharFilePath = Path.Combine(Server.MapPath("~/images/"), adharFileName);
                    FileUpldAdhar.SaveAs(adharFilePath);
                    adharFilePath = "~/images/" + adharFileName;
                }
                else
                {
                    adharFilePath=hdnaadhar.Value;
                }

                if (FileUpldPassport.HasFile)
                {
                    string passportFileName = Path.GetFileName(FileUpldPassport.FileName);
                    passportFilePath = Path.Combine(Server.MapPath("~/images/"), passportFileName);
                    FileUpldPassport.SaveAs(passportFilePath);
                    passportFilePath = "~/images/" + passportFileName;
                }
                else
                {
                    passportFilePath = hdnpass.Value;
                }



                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE user_tbl SET Name = @Name, Address = @Address, City = @City, Mobile = @Mobile, Email = @Email, WhatsAppNumber = @WhatsAppNumber, Aadhaar_Card_Pic = @Aadhaar_Card_Pic, Aadhaar_Card_Number = @Aadhaar_Card_Number, Passport_Size_Pic = @Passport_Size_Pic, Gender = @Gender, Active = @Active WHERE Id = @UserId", con);

                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@City", txtCity.Text);
                    cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@WhatsAppNumber", txtWhatsApp.Text);
                    cmd.Parameters.AddWithValue("@Aadhaar_Card_Pic", string.IsNullOrEmpty(adharFilePath) ? (object)DBNull.Value : adharFilePath);
                    cmd.Parameters.AddWithValue("@Aadhaar_Card_Number", txtAadhaar.Text);
                    cmd.Parameters.AddWithValue("@Passport_Size_Pic", string.IsNullOrEmpty(passportFilePath) ? (object)DBNull.Value : passportFilePath);
                    cmd.Parameters.AddWithValue("@Gender", rblGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@Active", 1);
                    cmd.Parameters.AddWithValue("@UserId", userid);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    // Delete existing qualifications
                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM Qualification WHERE user_id = @UserId", con);
                    deleteCmd.Parameters.AddWithValue("@UserId", userid);
                    deleteCmd.ExecuteNonQuery();

                    // Insert new qualifications
                    foreach (ListItem item in chkQualifications.Items)
                    {
                        if (item.Selected)
                        {
                            SqlCommand qualCmd = new SqlCommand("INSERT INTO Qualification (Qualification, user_id) VALUES (@Qualification, @UserId)", con);
                            qualCmd.Parameters.AddWithValue("@Qualification", item.Value);
                            qualCmd.Parameters.AddWithValue("@UserId", userid);
                            qualCmd.ExecuteNonQuery();
                        }
                    }

                    Response.Write("<script>alert('Record Updated Successfully')</script>");
                    Response.Redirect("GridView.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "')</script>");
            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("GridView.aspx");
        }
    }
}