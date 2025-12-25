using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using JiYiTunnelSystem.Models;
using JiYiTunnelSystem.IDAL;
using JiYiTunnelSystem.DAL;
using JiYiTunnelSystem.IBLL;
using JiYiTunnelSystem.Dto;

namespace JiYiTunnelSystem.BLL
{
    public class UserManager : IUserManager
    {
        public async Task ChangePassword(long userId, string oldPwd, string newPwd)
        {
            using(IUserService userService=new UserService())
            {
                using (ILogService logService = new LogService())
                {
                    var user = await userService.GetOneByIdAsync(userId);
                    if (user.Password == oldPwd)
                    {
                        user.Password = newPwd;
                        await userService.EditAsync(user);
                        await logService.CreateAsync(new logs()
                        {
                            UserId = userId,
                            Behavior = "修改密码"
                        });
                    }
                    else
                    {
                        throw new Exception("密码不正确");
                    }
                }
            }
        }

        public async Task<UserDto> GetUserById(long id)
        {
            using(IUserService userService=new UserService())
            {
                if(await userService.GetAllAsync().AnyAsync(m => m.Id == id))
                {
                    var user = await userService.GetOneByIdAsync(id);
                    UserDto userDto = new UserDto()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Phone = user.Phone,
                        Email = user.Mail,
                        Password = user.Password,
                        IsAlarm = user.IsAlarm,
                        CreateTime = user.CreateTime
                    };
                    return userDto;
                }
                else
                {
                    throw new ArgumentException("该用户不存在");
                }
            }
        }

        public bool Login(string phone, string password, out long userId,out int authority)
        {
            using(IUserService userService=new UserService())
            {
                var user = userService.GetAllAsync().FirstOrDefaultAsync(m => m.Phone == phone && m.Password == password);
                user.Wait();
                var data = user.Result;
                if (data == null)
                {
                    userId = -1;
                    authority = -1;
                    return false;
                }
                else
                {
                    userId = data.Id;
                    authority = data.Authority;
                    return true;
                }
            }
        }

        public async Task Register(string name, string phone, string mail, string password)
        {
            using(IUserService userService=new UserService())
            {
                if(await userService.GetAllAsync().AnyAsync(m => m.Phone == phone))
                {
                    throw new ArgumentException("该手机号已被注册");
                }
                else if(await userService.GetAllAsync().AnyAsync(m => m.Mail == mail))
                {
                    throw new ArgumentException("该邮箱已被注册");
                }
                else
                {
                    await userService.CreateAsync(new users()
                    {
                        Name = name,
                        Phone = phone,
                        Mail = mail,
                        Password = password
                    });
                    using (ILogService logService = new LogService())
                    {
                        var user = await userService.GetAllAsync().FirstOrDefaultAsync(m => m.Name == name && m.Phone == phone);
                        await logService.CreateAsync(new logs()
                        {
                            UserId = user.Id,
                            Behavior = "注册"
                        });
                    }
                }
            }
        }

        public bool RetrivePassword(string name, string email)
        {
            using (IUserService userService = new UserService())
            {
                var user = userService.GetAllAsync().FirstOrDefaultAsync(m => m.Name == name && m.Mail == email);
                user.Wait();
                if (user.Result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task RenamePwd(string email,string pwd)
        {
            using(IUserService userService=new UserService())
            {
                var user = await userService.GetAllAsync().FirstOrDefaultAsync(m => m.Mail == email);
                user.Password = pwd;
                await userService.EditAsync(user);
            }
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            using(IUserService userService = new UserService())
            {
                var user = await userService.GetAllAsync().FirstOrDefaultAsync(m => m.Mail == email);
                if (user != null)
                {
                    UserDto userDto = new UserDto
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Phone = user.Phone,
                        Email = user.Mail,
                        Authority=user.Authority,
                        IsAlarm=user.IsAlarm,
                        CreateTime=user.CreateTime
                    };
                    return userDto;
                }
                else
                {
                    throw new ArgumentException("该用户不存在");
                }
            }
        }
        public async Task<List<UserDto>> GetUsers(int pageIndex, int pageSize)
        {
            using (IUserService userService = new UserService())
            {
                var temp = userService.GetAllAsync().Where(m => m.Authority < 2).OrderBy(m => m.CreateTime);
                var datas = await temp.Skip(pageIndex*pageSize).Take(pageSize)
                    .Select(m => new UserDto()
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Phone = m.Phone,
                        Email = m.Mail,
                        Authority = m.Authority,
                        IsAlarm = m.IsAlarm,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
                return datas;
            }
        }

        public async Task<int> GetUserCount()
        {
            using(IUserService userService=new UserService())
            {
                return await userService.GetAllAsync().CountAsync();
            }
        }
        public async Task<List<UserDto>> GetUsers(sbyte alarm, int pageIndex, int pageSize)
        {
            using(IUserService userService=new UserService())
            {
                var datas = userService.GetAllAsync().Where(m => m.IsAlarm == alarm).OrderBy(m => m.Name);
                var users = await datas.Skip(pageIndex * pageSize).Take(pageSize)
                    .Select(m => new UserDto()
                    {
                        Id=m.Id,
                        Name = m.Name,
                        Phone = m.Phone,
                        Email = m.Mail,
                        IsAlarm = m.IsAlarm,
                        Authority=m.Authority,
                        CreateTime=m.CreateTime
                    }).ToListAsync();
                return users;
            }
        }

        public async Task<int> GetUserCountByAlarm(sbyte alarm)
        {
            using (IUserService userService = new UserService())
            {
                return await userService.GetAllAsync().Where(m => m.IsAlarm == alarm).CountAsync();
            }
        }

        public async Task ChangeUserIsAlarm(long id,bool alarm,long uid)
        {
            using(IUserService userService=new UserService())
            {
                var user = await userService.GetOneByIdAsync(id);
                user.IsAlarm = alarm ? (sbyte)1 : (sbyte)0;
                await userService.EditAsync(user);
                using(ILogService logService=new LogService())
                {
                    if (alarm)
                    {
                        await logService.CreateAsync(new logs()
                        {
                            UserId = uid,
                            Behavior = "设置" + user.Name + "为报警联系人"
                        });
                    }
                    else
                    {
                        await logService.CreateAsync(new logs()
                        {
                            UserId = uid,
                            Behavior = "取消" + user.Name + "为报警联系人"
                        });
                    }
                }
            }
        }

        public async Task ChangeUserAuth(long id,int role,long uid)
        {
            using (IUserService userService = new UserService())
            {
                var user = await userService.GetOneByIdAsync(id);
                user.Authority = role;
                await userService.EditAsync(user);
                using (ILogService logService = new LogService())
                {
                    string str1 = "";
                    string str2 = "";
                    if (role == 0)
                    {
                        str1 = "降级";
                        str2 = "为普通用户";
                    }
                    else if (role == 1)
                    {
                        str1 = "升级";
                        str2 = "为管理员";
                    }
                    await logService.CreateAsync(new logs()
                    {
                        UserId = uid,
                        Behavior = str1 + user.Name + str2
                    });
                }
            }
        }
    }
}
