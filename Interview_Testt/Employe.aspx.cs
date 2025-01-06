using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Interview_Testt
{
    public partial class Employe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropdown();
            }

        }

        private void BindDropdown()
        {
            try
            {
                List<Employee> emp = GetEmployees();

                emp.Sort((x , y) => string.Compare(x.Name, y.Name));    

                DropDownList1.DataSource = emp;
                DropDownList1.DataTextField = "Name";
                DropDownList1.DataValueField = "ID";
                DropDownList1.DataBind();

                DropDownList1.Items.Insert(0, new ListItem("Select Employee", ""));
                
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('"+ ex.Message + "')</script>");
            }
        }
        public List<Employee> GetEmployees()
        {
            string data = @"
        <Employees> 
           <Employee> 
            <Name>David</Name> 
            <ID>101</ID> 
            <IsActive>true</IsActive> 
            </Employee> 
            <Employee> 
            <Name>Tom</Name> 
            <ID>102</ID> 
            <IsActive>true</IsActive> 
            </Employee> 
            <Employee> 
            <Name>Rick</Name> 
            <ID>103</ID> 
            <IsActive>false</IsActive> 
            </Employee> 
            <Employee> 
            <Name>Mark</Name> 
            <ID>104</ID> 
            <IsActive>true</IsActive> 
          </Employee> 
         </Employees>
    ";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            List<Employee> employees = new List<Employee>();
            XmlNodeList employeeNodes = doc.GetElementsByTagName("Employee");

            foreach (XmlNode item in employeeNodes)
            {
                string name = item["Name"].InnerText;
                string id = item["ID"].InnerText;
                bool isActive = bool.Parse(item["IsActive"].InnerText);

                if (isActive)
                {
                    employees.Add(new Employee
                    {
                        Name = name,
                        ID = id,
                        IsActive = isActive
                    });
                }
            }

            return employees; // This should be outside the foreach loop
        }

        

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedId = DropDownList1.SelectedValue;
            string selectedName = DropDownList1.SelectedItem.Text;

            SelectedEmployeeLabel.Text = $"Selected Employee : {selectedName} , ID : {selectedId}";
        }
    }
}