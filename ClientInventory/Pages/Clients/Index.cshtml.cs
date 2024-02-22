using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ClientInventory.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<Client> clientList = new List<Client>();
        public void OnGet()
        {
            string connectionString = "Data Source=USHYDVIKASSJHA7;Initial Catalog=SampleSSIS;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqlcmd = " Select * from clients";
                using(SqlCommand cmd = new SqlCommand(sqlcmd,conn)) { 
                
                 using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Client clientInfo=new Client();
                            clientInfo.id = "" + reader.GetInt32(0);
                            clientInfo.name = reader.GetString(1);

                            clientInfo.email =  reader.GetString(2);

                            clientInfo.phone =  reader.GetString(3);
                            clientInfo.address = reader.GetString(4);

                            clientList.Add(clientInfo);



                        }
                    }
                }
            }
        }
    }
    public class Client {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;

    
    }

}
