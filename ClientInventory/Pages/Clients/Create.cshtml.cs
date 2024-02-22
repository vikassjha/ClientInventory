using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Data.SqlClient;

namespace ClientInventory.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public Client clientinfo = new Client();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() {

            clientinfo.name = Request.Form["name"];
            clientinfo.email = Request.Form["email"];
            clientinfo.address = Request.Form["address"];
            clientinfo.phone = Request.Form["phone"];

            if(clientinfo.name.Length == 0 || clientinfo.phone.Length == 0 || clientinfo.email.Length == 0 || clientinfo.address.Length ==0 ) {

                errorMessage = "All fields are required";
                return;
            
            }
            try
            {
                string connectionString = "Data Source=USHYDVIKASSJHA7;Initial Catalog=SampleSSIS;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlcmd = "Insert INTO clients " + "( name, email, phone, address) VALUES " + "(@name, @email, @phone, @address);";
                    using (SqlCommand cmd = new SqlCommand(sqlcmd, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", clientinfo.name);
                        cmd.Parameters.AddWithValue("@email", clientinfo.email);
                        cmd.Parameters.AddWithValue("@phone", clientinfo.phone);
                        cmd.Parameters.AddWithValue("@address", clientinfo.address);
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            clientinfo.address = "";
            clientinfo.phone = "";
            clientinfo.name = "";
            clientinfo.email = "";
            successMessage = "Client Added Successfully";

            Response.Redirect("/Clients/Index");
        
        }
    }
}
