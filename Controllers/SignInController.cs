using MailKit.Net.Smtp;
using MimeKit;
using MailKit;
using Facebook;
using Project_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;
using System.Configuration;
using System.Data;

namespace Project_Web.Controllers
{
    public class SignInController : BaseController
    {
        //Chuyển hướng URI
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        // GET: SignIn
        public Database_PorridgeSellingManagementStoreEntities _db = new Database_PorridgeSellingManagementStoreEntities();
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ForgotPassWord()
        {
            return View();
        }
        [HttpGet]
        public  ActionResult VerifyEmail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(User model)
        {
            Session["Is Login"] = 0;
            Session["User"] = null;
            Session["cart"] = null; 
            EncryptionPW encryptionPW = new EncryptionPW(model.Password);
            string EncryptedPass = encryptionPW.EncryptPass();
            User user = _db.Users.SingleOrDefault(n => n.Username == model.Username && n.Password == EncryptedPass);
           
            if (user is null)
            {
                SetAlert("Thông báo","Tên đăng nhập hoặc mật khẩu không chính xác", false);
            }
            else
            {
                Session["Is Login"] = 1;
                Session["User"] = user;
                var Bill = (from ot in _db.OrderTracks
                            join b in _db.Bills on ot.IDBill equals b.IDBill
                            where b.IDUser == user.IDUser && ot.IDOrderStatse == "OS-01"
                            select b).SingleOrDefault();
                if (Bill != null)
                {
                    DataTable cart = new DataTable();
                    cart.Columns.Add("IDBillDetails");
                    cart.Columns.Add("DishName");
                    cart.Columns.Add("Price");
                    cart.Columns.Add("Quantity");
                    cart.Columns.Add("Promotion");
                    cart.Columns.Add("PaidPrice");
                    cart.Columns.Add("IDBill");

                    List<BillDetail> billDetails = (from bd in _db.BillDetails
                                                    where bd.IDBill == Bill.IDBill
                                                    select bd).ToList();
                    foreach (BillDetail bd in billDetails)
                    {
                        DataRow dr = cart.NewRow();
                        dr["IDBillDetails"] = bd.IDBillDetail;
                        dr["DishName"] = bd.DishName;
                        dr["Price"] = bd.Price;
                        dr["Quantity"] = bd.Quantity;
                        dr["Promotion"] =  bd.Promotion;
                        dr["PaidPrice"] = bd.PaidPrice;
                        dr["IDBill"] = bd.IDBill;
                        cart.Rows.Add(dr);
                    }
                    Session["cart"] = cart;
                }
                SetAlert("Thông báo", "Đăng nhập thành công", true);
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassWord(FogotPassWord model)
        {
            User user = _db.Users.SingleOrDefault(n => n.Username == model.Username);
            if (user is null)
            {
                @ViewBag.MessageIsNotUser = "Tên đăng nhập không chính xác";
            }
            if (ModelState.IsValid)
            {
                EncryptionPW encryptionPW = new EncryptionPW(model.Password);
                model.Password = encryptionPW.EncryptPass();
                user.Password = model.Password;
                _db.Users.AddOrUpdate(user);
                _db.SaveChanges();
                @ViewBag.MessageSuccess = "Thành công";
                return RedirectToAction("SignIn", "SignIn");
            }
            return View();
        }
        [HttpPost]
        public ActionResult VerifyEmail( VerifyEmail verifyEmail)
        {
            var email = verifyEmail.Email;
            if (ModelState.IsValid)
            {
                var message = new MimeMessage();
                //From Address
                message.From.Add(new MailboxAddress("Huu Tuong", "huutuong1403@gmail.com"));
                //To Address
                message.To.Add(new MailboxAddress("Dot net", email));
                //Subject
                message.Subject = "Hello";
                //Body
                message.Body = new TextPart("plain")
                {
                    Text = "Link: https://localhost:44319/SignIn/ForgotPassWord"
                };
                //Configure send email
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("huutuong1403@gmail.com", "14032018");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            return View();
        }

        //Facebook API
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });
            return Redirect(loginUrl.AbsoluteUri);
        }
        public ActionResult FacebookCallback(string code)
        {
            Session["Is Login"] = 0;
            Session["User"] = null;
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code =code,
            });
            var accessToken = result.access_token;
            if(!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;

                dynamic me = fb.Get("me?fields=name,email,birthday,gender");
                string email = me.email;
                string name = me.name;
                DateTime birthday = Convert.ToDateTime(me.birthday);
                string datestring = String.Format("{0:yyyy-dd-MM}", birthday);
                string gender = me.gender;

                var user = new User();
                user.Email = email;
                user.Username = email;
                user.Fullname = name;
                user.DateofBirth = Convert.ToDateTime(datestring);
                user.Gender = gender;

                var resultInsert = new SignInController().InsertForFacebook(user);
                user.IDUser = resultInsert;
                if(resultInsert != null)
                {
                    Session["Is Login"] = 1;
                    Session["User"] = user;
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public string InsertForFacebook(User model)
        {
            var user = new User();
            if (model.Email != null)
            {
                user = _db.Users.SingleOrDefault(n => n.Email == model.Email);

            }
            else
            {
               user = _db.Users.SingleOrDefault(n => n.Fullname == model.Fullname);
            }
           

            var querryUsersCount = from User in _db.Users
                                   select User.IDUser;
            if (user == null)
            {
                model.IDUser = "U" + querryUsersCount.Count() + "-" + String.Format("{0:ddMMyyyyHHmmss}", DateTime.Now);
                model.Username = "DefaultUser" + querryUsersCount.Count();
                model.Password = "DefaultUser" + querryUsersCount.Count();
                model.Facebook = "1";
                if (model.Email == null )
                {
                    model.Email = "DefaultUser@gmail.com";
                }
                if (model.Address == null)
                {
                    model.Address = "DefaultUser";
                }
                if (model.PhoneNumber == null)
                {
                    model.PhoneNumber = "0949488160";
                }
                User_Roles user_role = new User_Roles();
                user_role.IDRole = "R03";
                user_role.IDUser = model.IDUser;
                _db.User_Roles.Add(user_role);
                _db.Users.Add(model);
                _db.SaveChanges();
                return model.IDUser;
            }
            else
            {
                var queryId = (from User in _db.Users
                               where User.Username == model.Username
                               select User.IDUser).SingleOrDefault();
                return user.IDUser;
            }
        }
    }
}