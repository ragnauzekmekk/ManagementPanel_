using ManagementPanel_.Bll.Services;
using ManagementPanel_.Data.EntityFramework;
using ManagementPanel_.Model;
using ManagementPanel_.Model.ComplexType;
using ManagementPanel_.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementPanel_.Bll.ServiceManagers
{
    public class AccountServiceManager : IAccountService
    {
        ManagementPanelContext db;

        public AccountServiceManager(ManagementPanelContext _db)
        {
            db = _db;
        }
        public Result<User> Login(LoginModel model)
        {
            var result = new Result<User>();

            var cnn = db.Database.Connection;
            cnn.Open();
            var cmd = cnn.CreateCommand();
            try
            {
                var psword= Extension.Encrypt(model.Password, model.Username);

                var sql = String.Format(@"SELECT * FROM ManagementPanel.dbo.[User](nolock) where Username='{0}' and Password='{1}'", model.Username, psword);

                result.Status = false;
                using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        var obj = new User();
                        obj.ID = Convert.ToInt32(dr["ID"]);
                        obj.Name = dr["Name"].ToString();
                        obj.Surname = dr["Surname"].ToString();
                        obj.Username = dr["Username"].ToString();
                        obj.Password = Extension.Decrypt(dr["Password"].ToString(), obj.Username);
                        obj.Email = dr["Email"].ToString();
                        obj.Phone = dr["Phone"].ToString();
                        obj.Date = Convert.ToDateTime(dr["Date"]);
                        obj.Admin = Convert.ToBoolean(dr["Admin"]);

                        result.Object = obj;
                        result.Status = true;
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "Kaydetme işleminde hata meydana gelmiştir.";
                result.Status = false;
            }

            return result;
        }
    }
}
