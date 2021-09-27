using ManagementPanel_.Bll.Services;
using ManagementPanel_.Data.EntityFramework;
using ManagementPanel_.Model.Models;
using ManagementPanel_.Model.ComplexType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementPanel_.Model;
using System.Data.SqlClient;
using System.Data;

namespace ManagementPanel_.Bll.ServiceManagers
{
    public class UsersServiceManager : IUsersService
    {
        ManagementPanelContext db;

        public UsersServiceManager(ManagementPanelContext _db)
        {
            db = _db;
        }

        public Result<User> GetUserList()
        {
            var userList = new Result<User>();

            try
            {
                userList.ObjectResult = db.Users.ToList();
                userList.Message = "Liste başarılı bir şekilde oluşturulmuştur.";
                userList.Status = true;
            }
            catch (Exception ex)
            {
                userList.ErrorMessage = "Liste oluşturulurken hata meydana gelmiştir.";
                userList.Status = false;
            }

            return userList;
        }

        public Result AddUser(UserModel model)
        {
            var result = new Result();

            var cnn = db.Database.Connection;

            cnn.Open();
            var tran = cnn.BeginTransaction();
            try
            {
                var user = new User()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Username = model.Username,
                    Password = Extension.Encrypt(model.Password, model.Username),
                    Phone = model.Phone,
                    Email = model.Email,
                    Date = model.Date
                };

                db.Users.Add(user);

                tran.Commit();

                db.SaveChanges();
                result.Message = "Kaydetme işlemi gerçekleştirilmiştir.";
                result.Status = true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                result.ErrorMessage = "Kaydetme işleminde hata meydana gelmiştir.";
                result.Status = false;
            }

            return result;
        }

        public Result<User> GetUser(int id)
        {
            var result = new Result<User>();

            var cnn = db.Database.Connection;
            cnn.Open();
            var cmd = cnn.CreateCommand();
            try
            {
                var sql = String.Format(@"SELECT * FROM ManagementPanel.dbo.[User](nolock) where ID={0}", id);

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
                    }
                    dr.Close();
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "Kaydetme işleminde hata meydana gelmiştir.";
                result.Status = false;
            }

            return result;
        }

        public Result UpdateUser(UserModel model)
        {
            var result = new Result();

            var cnn = db.Database.Connection;
            cnn.Open();
            var cmd = cnn.CreateCommand();
            var tran = cnn.BeginTransaction();
            try
            {
                //var sql = String.Format(@"UPDATE ManagementPanel.dbo.[User] SET [Name]=@name, Surname=@surname,
                //                        Password=@password, Email = @email, Phone=@phone, Date=@Date
                //                        where ID=@ID");

                //using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
                //{
                //    connection.Open();

                //    SqlCommand command = new SqlCommand(sql, connection);

                //    cmd.Parameters.Clear();

                //    command.Parameters.AddWithValue("@ID", model.ID);
                //    command.Parameters.AddWithValue("@Name", model.Name);
                //    command.Parameters.AddWithValue("@Surname", model.Surname);
                //    command.Parameters.AddWithValue("@Password", Extension.Encrypt(model.Password, model.Email));
                //    command.Parameters.AddWithValue("@Email", model.Email);
                //    command.Parameters.AddWithValue("@Phone", model.Phone);
                //    command.Parameters.AddWithValue("@Date", model.Date);

                //    command.ExecuteNonQuery();
                //}

                var user = new User()
                {
                    ID = model.ID,
                    Name = model.Name,
                    Surname = model.Surname,
                    Password = Extension.Encrypt(model.Password, model.Username),
                    Phone = model.Phone,
                    Email = model.Email,
                    Date = model.Date
                };

                db.Users.Attach(user);
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;


                tran.Commit();
                db.SaveChanges();
                result.Message = "Güncelleme işlemi gerçekleştirilmiştir.";
                result.Status = true;

            }
            catch (Exception ex)
            {
                result.ErrorMessage = "Güncelleme işleminde hata meydana gelmiştir.";
                result.Status = false;
            }

            return result;
        }

        public Result DeleteUser(int id)
        {
            var result = new Result();

            var cnn = db.Database.Connection;
            cnn.Open();
            var tran = cnn.BeginTransaction();
            try
            {
                var sql = String.Format(@"Delete From ManagementPanel.dbo.[User] 
                                        where ID={0}", id);

                using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);

                    command.ExecuteNonQuery();
                }
                result.Message = "Silme işlemi gerçekleşmiştir.";
                result.Status = true;
                tran.Commit();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                result.ErrorMessage = "Silme işleminde hata meydana gelmiştir.";
                result.Status = false;
            }

            return result;
        }


        public Result GetEmailPhoneUsernameControl(string email, string phone, string username)
        {
            var result = new Result();

            var cnn = db.Database.Connection;
            cnn.Open();
            var cmd = cnn.CreateCommand();
            try
            {
                var sql = String.Format(@"SELECT count(*) as count FROM ManagementPanel.dbo.[User](nolock) where Email='{0}' ", email);
                var sqlPhone = String.Format(@"SELECT count(*) as count FROM ManagementPanel.dbo.[User](nolock) where Phone='{0}' ", phone);
                var sqlUsername = String.Format(@"SELECT count(*) as count FROM ManagementPanel.dbo.[User](nolock) where Username='{0}' ", username);

                using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
                {
                    connection.Open();
                    result.Status = true;
                    SqlCommand cmdUsername = new SqlCommand(sqlUsername, connection);

                    var usernameCount = Convert.ToInt32(cmdUsername.ExecuteScalar());

                    if(usernameCount != 0)
                    {
                        result.ErrorMessage = "Kullanıcı kodu";
                        result.Status = false;
                    } 
                    
                    SqlCommand cmdEmail = new SqlCommand(sql, connection);

                    var emailCount = Convert.ToInt32(cmdEmail.ExecuteScalar());

                    if(emailCount != 0)
                    {
                        result.ErrorMessage += ", Email";
                        result.Status = false;
                    }

                    SqlCommand cmdPhone = new SqlCommand(sqlPhone, connection);

                    var phoneCount = Convert.ToInt32(cmdPhone.ExecuteScalar());

                    if (phoneCount != 0)
                    {
                        result.ErrorMessage += ", Telefon";
                        result.Status = false;
                    }

                    if(!result.Status)
                        result.ErrorMessage += " sistemde bulunmaktadır.";

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
