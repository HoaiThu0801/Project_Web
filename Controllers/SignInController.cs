using MailKit.Net.Smtp;
using MimeKit;
using MailKit;
using Project_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;

namespace Project_Web.Controllers
{
    public class SignInController : Controller
    {
        // GET: SignIn
        //public Database_PorridgeSellingManagementStore_v1_1Entities _db = new Database_PorridgeSellingManagementStore_v1_1Entities();
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
            EncryptionPW encryptionPW = new EncryptionPW(model.Password);
            string EncryptedPass = encryptionPW.EncryptPass();
            User user = _db.Users.SingleOrDefault(n => n.Username == model.Username && n.Password == EncryptedPass);
           
            if (user is null)
            {
                @ViewBag.Message = "Tên đăng nhập hoặc mật khẩu không chính xác";
            }
            else
            {
                Session["Is Login"] = 1;
                Session["User"] = user;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassWord(FogotPassWord model)
        {
            User user = _db.Users.SingleOrDefault(n => n.Username == model.Username);
            if (user is null)
            {
                @ViewBag.Message = "Tên đăng nhập không chính xác";
            }
            if (ModelState.IsValid)
            {
                user.Password = model.Password;
                _db.Users.AddOrUpdate(user);
                _db.SaveChanges();
                @ViewBag.Message = "Thành công"; 
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
    }
}