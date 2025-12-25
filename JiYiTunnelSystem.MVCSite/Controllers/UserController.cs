using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using JiYiTunnelSystem.MVCSite.Models.User;
using JiYiTunnelSystem.BLL;
using JiYiTunnelSystem.IBLL;
using JiYiTunnelSystem.Dto;
using JiYiTunnelSystem.MVCSite.Filters;

namespace JiYiTunnelSystem.MVCSite.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public async Task<ActionResult> Login()
        {
            LoginViewModel model = new LoginViewModel();
            if (Request.Cookies["userId"] != null&&Request.Cookies["authority"]!=null)
            {
                IUserManager userManager = new UserManager();
                var user = await userManager.GetUserById(long.Parse(Request.Cookies["userId"].Value));
                model.Phone = user.Phone;
                model.Password = user.Password;
                model.RemeberMe = true;
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                IUserManager userManager = new UserManager();
                if (userManager.Login(model.Phone, model.Password, out long userid,out int authority))
                {
                    if (model.RemeberMe)
                    {
                        Response.Cookies.Add(new HttpCookie("userId")
                        {
                            Value = userid.ToString(),
                            Expires = DateTime.Now.AddDays(7)
                        });
                        Response.Cookies.Add(new HttpCookie("authority")
                        {
                            Value = authority.ToString(),
                            Expires = DateTime.Now.AddDays(7)
                        });
                    }
                    else
                    {
                        Session["userId"] = userid;
                        Session["authority"] = authority;
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Models.PartialMessageVM partial = new Models.PartialMessageVM
                    {
                        Message = "您的账号密码有误"
                    };
                    return PartialView("_PartialMessage", partial);
                }
            }
            Models.PartialMessageVM partial2 = new Models.PartialMessageVM
            {
                Message = "您的账号密码有误"
            };
            return PartialView("_PartialMessage", partial2);
        }

        public ActionResult CreateVerifyCode()
        {
            byte[] img = VerifyCode.GetVerifyCode(4, 300, 150);
            return File(img, @"image/jpeg");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IUserManager userManager = new UserManager();
                try
                {
                    if (model.VerifyCode == HttpContext.Session["verifyCode"].ToString())
                    {
                        try
                        {
                            await userManager.Register(model.Name, model.Phone, model.Email, model.Password);
                            return RedirectToAction(nameof(Login));
                        }
                        catch (Exception e)
                        {
                            Models.PartialMessageVM partial3 = new Models.PartialMessageVM
                            {
                                Message = e.Message
                            };
                            return PartialView("_PartialMessage", partial3);
                        }
                    }
                    else
                    {
                        Models.PartialMessageVM partial2 = new Models.PartialMessageVM
                        {
                            Message = "请输入正确的验证码"
                        };
                        return PartialView("_PartialMessage", partial2);
                    }
                }
                catch
                {
                    Models.PartialMessageVM partial4 = new Models.PartialMessageVM
                    {
                        Message = "请刷新验证码"
                    };
                    return PartialView("_PartialMessage", partial4);
                }
            }
            Models.PartialMessageVM partial = new Models.PartialMessageVM
            {
                Message = "您的信息填写有误"
            };
            return PartialView("_PartialMessage", partial);
        }

        [HttpGet]
        public ActionResult Retrieve()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Retrieve(RetrieveViewModel model)
        {
            IUserManager userManager = new UserManager();
            if (ModelState.IsValid)
            {
                if (userManager.RetrivePassword(model.Name, model.Email))
                {
                    if(model.VerifyCode == HttpContext.Session["verifyCode"].ToString())
                    {
                        await userManager.RenamePwd(model.Email, model.Password);
                        return RedirectToAction(nameof(Login));
                    }
                    
                }
                else
                {
                    Models.PartialMessageVM partial2 = new Models.PartialMessageVM
                    {
                        Message = "该用户不存在"
                    };
                    return PartialView("_PartialMessage", partial2);
                }      
            }
            if (userManager.RetrivePassword(model.Name, model.Email))
            {
                // 给用户发验证码
                try
                {
                    MailHelp.SendVerify(4, model.Email);
                }
                catch
                {
                    Models.PartialMessageVM partial2 = new Models.PartialMessageVM
                    {
                        Message = "验证码获取失败"
                    };
                    return PartialView("_PartialMessage", partial2);
                }
                Models.PartialMessageVM partial3 = new Models.PartialMessageVM
                {
                    Message = "验证码已发送至您的邮箱",
                    Icon="success"
                };
                return PartialView("_PartialMessage", partial3);
            }
            Models.PartialMessageVM partial = new Models.PartialMessageVM
            {
                Message = "该用户不存在"
            };
            return PartialView("_PartialMessage", partial);
        }

        [HttpGet]
        [LoginAuth]
        public async Task<ActionResult> UserInformation()
        {
            IUserManager userManager = new UserManager();
            long userId = long.Parse(Session["userId"].ToString());
            try
            {
                var user = await userManager.GetUserById(userId);
                return View(user);
            }
            catch(Exception e)
            {
                Models.PartialMessageVM partial = new Models.PartialMessageVM
                {
                    Message = e.Message
                };
                return PartialView("_PartialMessage", partial);
            }
        }

        [HttpGet]
        [LoginAuth]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoginAuth]
        public async Task<ActionResult> ChangePassword(ChangePasswordVM model)
        {
            IUserManager userManager = new UserManager();
            long userId = long.Parse(Session["userId"].ToString());
            try
            {
                await userManager.ChangePassword(userId, model.OldPwd, model.NewPwd);
            }
            catch(Exception e)
            {
                Models.PartialMessageVM partial = new Models.PartialMessageVM
                {
                    Message = e.Message
                };
                return PartialView("_PartialMessage", partial);
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult ExitLogin()
        {
            Session["userId"] = null;
            return View("Login");
        }
    }
}