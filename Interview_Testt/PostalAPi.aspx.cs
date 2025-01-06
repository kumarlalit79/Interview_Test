using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;



namespace Interview_Testt
{
    public partial class PostalAPi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnSubmit_Click(object sender, EventArgs e)
        {
            string pinCode = txtPinCode.Text.Trim();

            if (!string.IsNullOrEmpty(pinCode))
            {
                
                await GetCityByPinCode(pinCode);
            }
        }
        private async Task GetCityByPinCode(string pinCode)
        {
            try
            {
                
                string apiUrl = $"http://www.postalpincode.in/api/pincode/{pinCode}";

                
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        
                        JObject jsonResponse = JObject.Parse(responseData);

                       
                        string status = jsonResponse["Status"].ToString();

                        if (status == "Success")
                        {
                           
                            string cityName = jsonResponse["PostOffice"][0]["District"].ToString();
                            lblResult.Text = $"City Name: {cityName}";
                            lblError.Text = string.Empty;  
                        }
                        else
                        {
                            lblError.Text = "Invalid PIN code or data not found.";
                            lblResult.Text = string.Empty;
                        }
                    }
                    else
                    {
                        lblError.Text = "Failed to fetch data from the API.";
                        lblResult.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = $"Error: {ex.Message}";
                lblResult.Text = string.Empty;
            }
        }
    }
}