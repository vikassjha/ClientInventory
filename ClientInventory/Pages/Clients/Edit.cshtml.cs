using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ClientInventory.Pages.Clients
{

   
    public class EditModel : PageModel
    {
        public Client clientinfo = new Client();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
                string connectionString = "Data Source=USHYDVIKASSJHA7;Initial Catalog=SampleSSIS;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlcmd = " Select * from clients where id=@id";
                    using (SqlCommand cmd = new SqlCommand(sqlcmd, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                
                                clientinfo.id = "" + reader.GetInt32(0);
                                clientinfo.name = reader.GetString(1);

                                clientinfo.email = reader.GetString(2);

                                clientinfo.phone = reader.GetString(3);
                                clientinfo.address = reader.GetString(4);

                                



                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }
        public void onPost() {

            clientinfo.name = Request.Form["name"];
            clientinfo.email = Request.Form["email"];
            clientinfo.address = Request.Form["address"];
            clientinfo.phone = Request.Form["phone"];
            clientinfo.id= Request.Form["id"];

            if (clientinfo.name.Length == 0 || clientinfo.phone.Length == 0 || clientinfo.email.Length == 0 || clientinfo.address.Length == 0 || clientinfo.id.Length==0)
            {

                errorMessage = "All fields are required";
                return;

            }
            try
            {
                string connectionString = "Data Source=USHYDVIKASSJHA7;Initial Catalog=SampleSSIS;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlcmd = "UPDATE  clients SET name = @name , email = @email, phone = @phone , address=@address" +" WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(sqlcmd, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", clientinfo.name);
                        cmd.Parameters.AddWithValue("@email", clientinfo.email);
                        cmd.Parameters.AddWithValue("@phone", clientinfo.phone);
                        cmd.Parameters.AddWithValue("@address", clientinfo.address);
                        cmd.Parameters.AddWithValue("@id", clientinfo.id);

                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Clients/Index");

        }
    }
}
