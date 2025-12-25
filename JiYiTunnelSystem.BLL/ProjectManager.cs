using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using JiYiTunnelSystem.IBLL;
using JiYiTunnelSystem.DAL;
using JiYiTunnelSystem.IDAL;
using JiYiTunnelSystem.Dto;
using JiYiTunnelSystem.Models;
using System.Net.Mail;

namespace JiYiTunnelSystem.BLL
{
    public class ProjectManager : IProjectManager
    {
        #region 存储数据
        private void SendMail(List<string> toPeople,string bodyTxt)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("2746605116@qq.com");
            foreach(var item in toPeople)
            {
                mailMessage.To.Add(item);
            }
            mailMessage.Subject = "隧道监控";
            mailMessage.Body = bodyTxt;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = new System.Net.NetworkCredential("2746605116@qq.com", "cpvxarlyixjwdfig");
            smtpClient.Host = "smtp.qq.com";
            smtpClient.Port = 587;
            smtpClient.Send(mailMessage);
        }
        public async Task CreateData(List<ReceiveDataDto> dataDtos)
        {
            using (ISectionService sectionService = new SectionService())
            {
                string mailTxt = "";
                try
                {
                    using (IMessageService messageService = new MessageService())
                    {
                        var time = await messageService.GetAllAsync().OrderByDescending(m => m.CreateTime).Select(m => m.CreateTime).FirstOrDefaultAsync();
                        bool sendMail = time < DateTime.Now.AddHours(-4);//如果4小时内发送过邮件，则false
                        //decimal strainAlarm_YJ = 1, strainAlarm_ZD = 1, strainAlarm_LJ = 1;
                        //decimal strainControl_YJ = (decimal)1.8, strainControl_ZD = (decimal)1.8, strainControl_LJ = (decimal)1.8;
                        //decimal offsetAlarm_YJ = 1, offsetAlarm_ZD = 1, offsetAlarm_LJ = 1;
                        //decimal offsetControl_YJ = 2, offsetControl_ZD = 2, offsetControl_LJ = 2;
                        //decimal vibrationAlarm_YJ = 1, vibrationAlarm_ZD = (decimal)3.5, vibrationAlarm_LJ = 15;
                        //decimal steelAlarm_SJ = 80;
                        //先取得预警值和报警值
                        using (IEngineeringService engineeringService = new EngineeringService())
                        {
                            var engs = await engineeringService.GetAllAsync().ToListAsync();
                            decimal strainAlarm_YJ = (decimal)engs.Where(m => m.Id == 1).Select(m => m.StrainAlarmValue).FirstOrDefault();
                            decimal strainControl_YJ = (decimal)engs.Where(m => m.Id == 1).Select(m => m.StrainContorlValue).FirstOrDefault();
                            decimal offsetAlarm_YJ = (decimal)engs.Where(m => m.Id == 1).Select(m => m.OffsetAlarmValue).FirstOrDefault();
                            decimal offsetControl_YJ = (decimal)engs.Where(m => m.Id == 1).Select(m => m.OffsetControlValue).FirstOrDefault();
                            decimal vibrationAlarm_YJ = (decimal)engs.Where(m => m.Id == 1).Select(m => m.VibrationAlarmValue).FirstOrDefault();
                            decimal vibrationAlarm_ZD = (decimal)engs.Where(m => m.Id == 3).Select(m => m.VibrationAlarmValue).FirstOrDefault();
                            decimal vibrationAlarm_LJ = (decimal)engs.Where(m => m.Id == 4).Select(m => m.VibrationAlarmValue).FirstOrDefault();
                            decimal steelAlarm_SJ = (decimal)engs.Where(m => m.Id == 2).Select(m => m.SteelStressAlarmValue).FirstOrDefault();
                            using (ISensorService sensorService = new SensorService())
                            {
                                using (IAlarmlogService alarmlogService = new AlarmlogService())
                                {
                                    try
                                    {
                                        if (dataDtos[0].Type == "ZD")//振动的数据单独传输
                                        {
                                            using(IVibrationService vibrationSvc=new VibrationService())
                                            {
                                                try
                                                {
                                                    foreach (var dataDto in dataDtos)
                                                    {
                                                        try
                                                        {
                                                            if (string.IsNullOrWhiteSpace(dataDto.D1.ToString()))
                                                            {
                                                                continue;
                                                            }
                                                            var section = await sectionService.GetAllAsync().Where(m => m.Id == dataDto.Id).FirstOrDefaultAsync();
                                                            var sensors = await sensorService.GetAllAsync().Where(m => m.SectionId == dataDto.Id && m.SensorType == dataDto.Type)
                                                                .OrderBy(m => m.SensorNumber).ToListAsync();
                                                            await vibrationSvc.CreateAsync(new vibrations()
                                                            {
                                                                SectionId = section.Id,
                                                                Data1 = dataDto.D1,
                                                                Data2 = dataDto.D2,
                                                                Data3 = dataDto.D3,
                                                                Data4 = dataDto.D4,
                                                                Data5 = dataDto.D5
                                                            }, false);
                                                            if (sensors.Count > 0)
                                                            {
                                                                if (section.EngId == 1)
                                                                {
                                                                    try
                                                                    {
                                                                        if (dataDto.D1 != null)
                                                                        {
                                                                            if (dataDto.D1 >= vibrationAlarm_YJ && dataDto.D1 < 3 * vibrationAlarm_YJ)
                                                                            {
                                                                                try
                                                                                {
                                                                                    if(sensors[0].IsAlarm == 1) 
                                                                                    {
                                                                                        mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                    + "--" + sensors[0].SensorNumber.Substring(sensors[0].SensorNumber.Length - 4, 4) + "(爆破振速)" + dataDto.D1
                                                                                    + "mm/s 预警" + "(预警值" + vibrationAlarm_YJ + "mm/s)\r\n";
                                                                                    }
                                                                                    await alarmlogService.CreateAsync(new alarmlogs()
                                                                                    {
                                                                                        SectionId = section.Id,
                                                                                        SensorId = sensors[0].Id,
                                                                                        Data = dataDto.D1,
                                                                                        Grade = 0
                                                                                    }, false);
                                                                                }
                                                                                catch (Exception e) { }
                                                                            }
                                                                        }
                                                                        if (dataDto.D2 != null)
                                                                        {
                                                                            try
                                                                            {
                                                                                if (dataDto.D2 >= vibrationAlarm_YJ && dataDto.D2 < 3 * vibrationAlarm_YJ)
                                                                                {
                                                                                    if(sensors[1].IsAlarm == 1)
                                                                                    {
                                                                                        mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                    + "--" + sensors[1].SensorNumber.Substring(sensors[1].SensorNumber.Length - 4, 4) + "(爆破振速)" + dataDto.D2
                                                                                    + "mm/s 预警" + "(预警值" + vibrationAlarm_YJ + "mm/s)\r\n";
                                                                                    }
                                                                                    await alarmlogService.CreateAsync(new alarmlogs()
                                                                                    {
                                                                                        SectionId = section.Id,
                                                                                        SensorId = sensors[1].Id,
                                                                                        Data = dataDto.D2,
                                                                                        Grade = 0
                                                                                    }, false);
                                                                                }
                                                                            }
                                                                            catch (Exception e)
                                                                            {

                                                                            }
                                                                        }
                                                                        if (dataDto.D3 != null)
                                                                        {
                                                                            try
                                                                            {
                                                                                if (dataDto.D3 >= vibrationAlarm_YJ && dataDto.D3 < 3 * vibrationAlarm_YJ)
                                                                                {
                                                                                    if(sensors[2].IsAlarm == 1)
                                                                                    {
                                                                                        mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                    + "--" + sensors[2].SensorNumber.Substring(sensors[2].SensorNumber.Length - 4, 4) + "(爆破振速)" + dataDto.D3
                                                                                    + "mm/s 预警" + "(预警值" + vibrationAlarm_YJ + "mm/s)\r\n";
                                                                                    }
                                                                                    await alarmlogService.CreateAsync(new alarmlogs()
                                                                                    {
                                                                                        SectionId = section.Id,
                                                                                        SensorId = sensors[2].Id,
                                                                                        Data = dataDto.D3,
                                                                                        Grade = 0
                                                                                    }, false);
                                                                                }
                                                                            }
                                                                            catch (Exception e)
                                                                            {

                                                                            }
                                                                        }

                                                                        if (dataDto.D4 != null)
                                                                        {
                                                                            try
                                                                            {
                                                                                if (dataDto.D4 >= vibrationAlarm_YJ && dataDto.D4 < 3 * vibrationAlarm_YJ)
                                                                                {
                                                                                    if(sensors[3].IsAlarm == 1)
                                                                                    {
                                                                                        mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                    + "--" + sensors[3].SensorNumber.Substring(sensors[3].SensorNumber.Length - 4, 4) + "(爆破振速)" + dataDto.D4
                                                                                    + "mm/s 预警" + "(预警值" + vibrationAlarm_YJ + "mm/s)\r\n";
                                                                                    }
                                                                                    await alarmlogService.CreateAsync(new alarmlogs()
                                                                                    {
                                                                                        SectionId = section.Id,
                                                                                        SensorId = sensors[3].Id,
                                                                                        Data = dataDto.D4,
                                                                                        Grade = 0
                                                                                    }, false);
                                                                                }
                                                                            }
                                                                            catch (Exception e)
                                                                            {

                                                                            }
                                                                        }
                                                                        if (dataDto.D5 != null)
                                                                        {
                                                                            try
                                                                            {
                                                                                if (dataDto.D5 >= vibrationAlarm_YJ && dataDto.D5 < 3 * vibrationAlarm_YJ)
                                                                                {
                                                                                    if(sensors[4].IsAlarm == 1)
                                                                                    {
                                                                                        mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                    + "--" + sensors[4].SensorNumber.Substring(sensors[4].SensorNumber.Length - 4, 4) + "(爆破振速)" + dataDto.D5
                                                                                    + "mm/s 预警" + "(预警值" + vibrationAlarm_YJ + "mm/s)\r\n";
                                                                                    }
                                                                                    await alarmlogService.CreateAsync(new alarmlogs()
                                                                                    {
                                                                                        SectionId = section.Id,
                                                                                        SensorId = sensors[4].Id,
                                                                                        Data = dataDto.D5,
                                                                                        Grade = 0
                                                                                    }, false);
                                                                                }
                                                                            }
                                                                            catch
                                                                            {

                                                                            }
                                                                        }
                                                                    }
                                                                    catch { }
                                                                }
                                                                else if (section.EngId == 3)
                                                                {
                                                                    if (dataDto.D1 != null)
                                                                    {
                                                                        try
                                                                        {
                                                                            if (dataDto.D1 >= vibrationAlarm_ZD && dataDto.D1 < 3 * vibrationAlarm_ZD)
                                                                            {
                                                                                if(sensors[0].IsAlarm == 1)
                                                                                {
                                                                                    mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                    + "--" + sensors[0].SensorNumber.Substring(sensors[0].SensorNumber.Length - 4, 4) + "(爆破振速)" + dataDto.D1
                                                                                    + "mm/s 预警" + "(预警值" + vibrationAlarm_ZD + "mm/s)\r\n";
                                                                                }
                                                                                await alarmlogService.CreateAsync(new alarmlogs()
                                                                                {
                                                                                    SectionId = section.Id,
                                                                                    SensorId = sensors[0].Id,
                                                                                    Data = dataDto.D1,
                                                                                    Grade = 0
                                                                                }, false);
                                                                            }
                                                                        }
                                                                        catch { }
                                                                    }
                                                                }
                                                                else if (section.EngId == 4)
                                                                {
                                                                    if (dataDto.D1 != null)
                                                                    {
                                                                        try
                                                                        {
                                                                            if ((dataDto.Id > 58 && dataDto.Id < 65) || (dataDto.Id > 70 && dataDto.Id < 77))
                                                                            {// 斜井靠近正洞的六个截面量程和正洞一样
                                                                                if (dataDto.D1 >= vibrationAlarm_ZD && dataDto.D1 < 3 * vibrationAlarm_ZD)
                                                                                {
                                                                                    if(sensors[0].IsAlarm == 1)
                                                                                    {
                                                                                        mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                        + "--" + sensors[0].SensorNumber.Substring(sensors[0].SensorNumber.Length - 4, 4) + "(爆破振速)" + dataDto.D1
                                                                                        + "mm/s 预警" + "(预警值" + vibrationAlarm_ZD + "mm/s)\r\n";
                                                                                    }
                                                                                    await alarmlogService.CreateAsync(new alarmlogs()
                                                                                    {
                                                                                        SectionId = section.Id,
                                                                                        SensorId = sensors[0].Id,
                                                                                        Data = dataDto.D1,
                                                                                        Grade = 0
                                                                                    }, false);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (dataDto.D1 >= vibrationAlarm_LJ && dataDto.D1 < 3 * vibrationAlarm_LJ)
                                                                                {
                                                                                    if (sensors[0].IsAlarm==1)
                                                                                    {
                                                                                        mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                        + "--" + sensors[0].SensorNumber.Substring(sensors[0].SensorNumber.Length - 4, 4) + "(爆破振速)" + dataDto.D1
                                                                                        + "mm/s 预警" + "(预警值" + vibrationAlarm_LJ + "mm/s)\r\n";
                                                                                    }
                                                                                    await alarmlogService.CreateAsync(new alarmlogs()
                                                                                    {
                                                                                        SectionId = section.Id,
                                                                                        SensorId = sensors[0].Id,
                                                                                        Data = dataDto.D1,
                                                                                        Grade = 0
                                                                                    }, false);
                                                                                }
                                                                            }
                                                                        }
                                                                        catch { }
                                                                    }
                                                                    if (dataDto.D2 != null)
                                                                    {
                                                                        try
                                                                        {
                                                                            if ((dataDto.Id > 58 && dataDto.Id < 65) || (dataDto.Id > 70 && dataDto.Id < 77))
                                                                            {// 斜井靠近正洞的六个截面量程和正洞一样
                                                                                if (dataDto.D2 >= vibrationAlarm_ZD && dataDto.D2 < 3 * vibrationAlarm_ZD)
                                                                                {
                                                                                    if(sensors[1].IsAlarm == 1)
                                                                                    {
                                                                                        mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                        + "--" + sensors[1].SensorNumber.Substring(sensors[1].SensorNumber.Length - 4, 4) + "(爆破振速)" + dataDto.D2
                                                                                        + "mm/s 预警" + "(预警值" + vibrationAlarm_ZD + "mm/s)\r\n";
                                                                                    }
                                                                                    await alarmlogService.CreateAsync(new alarmlogs()
                                                                                    {
                                                                                        SectionId = section.Id,
                                                                                        SensorId = sensors[1].Id,
                                                                                        Data = dataDto.D2,
                                                                                        Grade = 0
                                                                                    }, false);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (dataDto.D2 >= vibrationAlarm_LJ && dataDto.D2 < 3 * vibrationAlarm_LJ)
                                                                                {
                                                                                    if(sensors[1].IsAlarm == 1)
                                                                                    {
                                                                                        mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                        + "--" + sensors[1].SensorNumber.Substring(sensors[1].SensorNumber.Length - 4, 4) + "(爆破振速)" + dataDto.D2
                                                                                        + "mm/s 预警" + "(预警值" + vibrationAlarm_LJ + "mm/s)\r\n";
                                                                                    }
                                                                                    await alarmlogService.CreateAsync(new alarmlogs()
                                                                                    {
                                                                                        SectionId = section.Id,
                                                                                        SensorId = sensors[1].Id,
                                                                                        Data = dataDto.D2,
                                                                                        Grade = 0
                                                                                    }, false);
                                                                                }
                                                                            }
                                                                        }
                                                                        catch { }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                    await vibrationSvc.Save();
                                                }
                                                catch
                                                {
                                                    await vibrationSvc.Save();
                                                }
                                            }
                                        }
                                        else if (dataDtos[0].Type == "WY")
                                        {
                                            using(IOffsetService offsetService=new OffsetService())
                                            {
                                                try
                                                {
                                                    foreach (var dataDto in dataDtos)
                                                    {
                                                        try
                                                        {
                                                            if (dataDto.D1 != null || dataDto.D2 != null || dataDto.D3 != null ||
                                                                dataDto.D4 != null || dataDto.D5 != null || dataDto.D6 != null ||
                                                                dataDto.D7 != null || dataDto.D8 != null || dataDto.D9 != null)
                                                            {
                                                                var section = await sectionService.GetAllAsync().Where(m => m.Id == dataDto.Id).FirstOrDefaultAsync();
                                                                var sensors = await sensorService.GetAllAsync().Where(m => m.SectionId == dataDto.Id && m.SensorType == dataDto.Type)
                                                                    .OrderBy(m => m.SensorNumber).ToListAsync();
                                                                await offsetService.CreateAsync(new offsets()
                                                                {
                                                                    SectionId = section.Id,
                                                                    Data1 = dataDto.D1,
                                                                    Data2 = dataDto.D2,
                                                                    Data3 = dataDto.D3,
                                                                    Data4 = dataDto.D4,
                                                                    Data5 = dataDto.D5,
                                                                    Data6 = dataDto.D6,
                                                                    Data7 = dataDto.D7,
                                                                    Data8 = dataDto.D8,
                                                                    Data9 = dataDto.D9
                                                                }, false);
                                                                if (sensors.Count > 0)
                                                                {
                                                                    if (dataDto.D1 != null)
                                                                    {
                                                                        decimal d = Math.Abs((decimal)dataDto.D1);
                                                                        try
                                                                        {
                                                                            if (d >= offsetAlarm_YJ && d < offsetControl_YJ)
                                                                            {
                                                                                if (sensors[0].IsAlarm == 1)
                                                                                {
                                                                                    mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                                + "--" + sensors[0].SensorNumber.Substring(sensors[0].SensorNumber.Length - 4, 4) + "(位移)" + dataDto.D1
                                                                                                + "mm 预警" + "(预警值" + offsetAlarm_YJ + "mm，控制值" + offsetControl_YJ + "mm)\r\n";
                                                                                }
                                                                                await alarmlogService.CreateAsync(new alarmlogs()
                                                                                {
                                                                                    SectionId = section.Id,
                                                                                    SensorId = sensors[0].Id,
                                                                                    Data = dataDto.D1,
                                                                                    Grade = 0
                                                                                }, false);
                                                                            }
                                                                            else if (d >= offsetControl_YJ && d <= 3*offsetControl_YJ)
                                                                            {
                                                                                if (sensors[0].IsAlarm == 1)
                                                                                {
                                                                                    mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                                + "--" + sensors[0].SensorNumber.Substring(sensors[0].SensorNumber.Length - 4, 4) + "(位移)" + dataDto.D1
                                                                                                + "mm 危险" + "(预警值" + offsetAlarm_YJ + "mm，控制值" + offsetControl_YJ + "mm)\r\n";
                                                                                }
                                                                                await alarmlogService.CreateAsync(new alarmlogs()
                                                                                {
                                                                                    SectionId = section.Id,
                                                                                    SensorId = sensors[0].Id,
                                                                                    Data = dataDto.D1,
                                                                                    Grade = 1
                                                                                }, false);
                                                                            }
                                                                        }
                                                                        catch { }
                                                                    }
                                                                    if (dataDto.D2 != null)
                                                                    {
                                                                        decimal d = Math.Abs((decimal)dataDto.D2);
                                                                        try
                                                                        {
                                                                            if (d >= offsetAlarm_YJ && d < offsetControl_YJ)
                                                                            {
                                                                                if (sensors[1].IsAlarm == 1)
                                                                                {
                                                                                    mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                                + "--" + sensors[1].SensorNumber.Substring(sensors[1].SensorNumber.Length - 4, 4) + "(位移)" + dataDto.D2
                                                                                                + "mm 预警" + "(预警值" + offsetAlarm_YJ + "mm，控制值" + offsetControl_YJ + "mm)\r\n";
                                                                                }
                                                                                await alarmlogService.CreateAsync(new alarmlogs()
                                                                                {
                                                                                    SectionId = section.Id,
                                                                                    SensorId = sensors[1].Id,
                                                                                    Data = dataDto.D2,
                                                                                    Grade = 0
                                                                                }, false);
                                                                            }
                                                                            else if (d >= offsetControl_YJ && d < 3*offsetControl_YJ)
                                                                            {
                                                                                if(sensors[1].IsAlarm == 1)
                                                                                {
                                                                                    mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                                + "--" + sensors[1].SensorNumber.Substring(sensors[1].SensorNumber.Length - 4, 4) + "(位移)" + dataDto.D2
                                                                                                + "mm 危险" + "(预警值" + offsetAlarm_YJ + "mm，控制值" + offsetControl_YJ + "mm)\r\n";
                                                                                }
                                                                                await alarmlogService.CreateAsync(new alarmlogs()
                                                                                {
                                                                                    SectionId = section.Id,
                                                                                    SensorId = sensors[1].Id,
                                                                                    Data = dataDto.D2,
                                                                                    Grade = 1
                                                                                }, false);
                                                                            }
                                                                        }
                                                                        catch { }
                                                                    }
                                                                    if (dataDto.D3 != null)
                                                                    {
                                                                        decimal d = Math.Abs((decimal)dataDto.D3);
                                                                        try
                                                                        {
                                                                            if (d >= offsetAlarm_YJ && d < offsetControl_YJ)
                                                                            {
                                                                                if(sensors[2].IsAlarm == 1)
                                                                                {
                                                                                    mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                                + "--" + sensors[2].SensorNumber.Substring(sensors[2].SensorNumber.Length - 4, 4) + "(位移)" + dataDto.D3
                                                                                                + "mm 预警" + "(预警值" + offsetAlarm_YJ + "mm，控制值" + offsetControl_YJ + "mm)\r\n";
                                                                                }
                                                                                await alarmlogService.CreateAsync(new alarmlogs()
                                                                                {
                                                                                    SectionId = section.Id,
                                                                                    SensorId = sensors[2].Id,
                                                                                    Data = dataDto.D3,
                                                                                    Grade = 0
                                                                                }, false);
                                                                            }
                                                                            else if (d >= offsetControl_YJ && d < 3*offsetControl_YJ)
                                                                            {
                                                                                if (sensors[2].IsAlarm == 1)
                                                                                {
                                                                                    mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                                + "--" + sensors[2].SensorNumber.Substring(sensors[2].SensorNumber.Length - 4, 4) + "(位移)" + dataDto.D3
                                                                                                + "mm 危险" + "(预警值" + offsetAlarm_YJ + "mm，控制值" + offsetControl_YJ + "mm)\r\n";
                                                                                }
                                                                                await alarmlogService.CreateAsync(new alarmlogs()
                                                                                {
                                                                                    SectionId = section.Id,
                                                                                    SensorId = sensors[2].Id,
                                                                                    Data = dataDto.D3,
                                                                                    Grade = 1
                                                                                }, false);
                                                                            }
                                                                        }
                                                                        catch { }
                                                                    }
                                                                    if (dataDto.D4 != null)
                                                                    {
                                                                        decimal d = Math.Abs((decimal)dataDto.D4);
                                                                        try
                                                                        {
                                                                            if (d >= offsetAlarm_YJ && d < offsetControl_YJ)
                                                                            {
                                                                                if(sensors[3].IsAlarm == 1)
                                                                                {
                                                                                    mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                                + "--" + sensors[3].SensorNumber.Substring(sensors[3].SensorNumber.Length - 4, 4) + "(位移)" + dataDto.D4
                                                                                                + "mm 预警" + "(预警值" + offsetAlarm_YJ + "mm，控制值" + offsetControl_YJ + "mm)\r\n";
                                                                                }
                                                                                await alarmlogService.CreateAsync(new alarmlogs()
                                                                                {
                                                                                    SectionId = section.Id,
                                                                                    SensorId = sensors[3].Id,
                                                                                    Data = dataDto.D4,
                                                                                    Grade = 0
                                                                                }, false);
                                                                            }
                                                                            else if (d >= offsetControl_YJ && d < 3*offsetControl_YJ)
                                                                            {
                                                                                if (sensors[3].IsAlarm == 1)
                                                                                {
                                                                                    mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                                + "--" + sensors[3].SensorNumber.Substring(sensors[3].SensorNumber.Length - 4, 4) + "(位移)" + dataDto.D4
                                                                                                + "mm 危险" + "(预警值" + offsetAlarm_YJ + "mm，控制值" + offsetControl_YJ + "mm)\r\n";
                                                                                }
                                                                                await alarmlogService.CreateAsync(new alarmlogs()
                                                                                {
                                                                                    SectionId = section.Id,
                                                                                    SensorId = sensors[3].Id,
                                                                                    Data = dataDto.D4,
                                                                                    Grade = 1
                                                                                }, false);
                                                                            }
                                                                        }
                                                                        catch { }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                    await offsetService.Save();
                                                }
                                                catch
                                                {
                                                    await offsetService.Save();
                                                }
                                            }
                                        }
                                        else if (dataDtos[0].Type == "YB")
                                        {
                                            using(IStrainService strainService=new StrainService())
                                            {
                                                try
                                                {
                                                    foreach (var dataDto in dataDtos)
                                                    {
                                                        try
                                                        {
                                                            if (dataDto.D1 != null || dataDto.D2 != null || dataDto.D3 != null ||
                                                                dataDto.D4 != null || dataDto.D5 != null || dataDto.D6 != null ||
                                                                dataDto.D7 != null || dataDto.D8 != null || dataDto.D9 != null)
                                                            {
                                                                var section = await sectionService.GetAllAsync().Where(m => m.Id == dataDto.Id).FirstOrDefaultAsync();
                                                                var sensors = await sensorService.GetAllAsync().Where(m => m.SectionId == dataDto.Id && m.SensorType == dataDto.Type)
                                                                    .OrderBy(m => m.SensorNumber).ToListAsync();
                                                                await strainService.CreateAsync(new strains()
                                                                {
                                                                    SectionId = section.Id,
                                                                    Data1 = dataDto.D1,
                                                                    Data2 = dataDto.D2,
                                                                    Data3 = dataDto.D3,
                                                                    Data4 = dataDto.D4,
                                                                    Data5 = dataDto.D5,
                                                                    Data6 = dataDto.D6,
                                                                    Data7 = dataDto.D7,
                                                                    Data8 = dataDto.D8,
                                                                    Data9 = dataDto.D9
                                                                }, false);
                                                                if (sensors.Count > 0)
                                                                {
                                                                    if (dataDto.D1 != null)
                                                                    {
                                                                        decimal d = Math.Abs((decimal)dataDto.D1);
                                                                        try
                                                                        {
                                                                            if (d >= strainAlarm_YJ && d < strainControl_YJ)
                                                                            {
                                                                                if(sensors[0].IsAlarm == 1)
                                                                                {
                                                                                    mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                                + "--" + sensors[0].SensorNumber.Substring(sensors[0].SensorNumber.Length - 4, 4) + "(应变)" + dataDto.D1
                                                                                                + " 预警" + "(预警值" + strainAlarm_YJ + "，控制值" + strainControl_YJ + ")\r\n";
                                                                                }
                                                                                await alarmlogService.CreateAsync(new alarmlogs()
                                                                                {
                                                                                    SectionId = section.Id,
                                                                                    SensorId = sensors[0].Id,
                                                                                    Data = dataDto.D1,
                                                                                    Grade = 0
                                                                                }, false);
                                                                            }
                                                                            else if (d >= strainControl_YJ && d < 3*strainControl_YJ)
                                                                            {
                                                                                if(sensors[0].IsAlarm == 1)
                                                                                {
                                                                                    mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                                + "--" + sensors[0].SensorNumber.Substring(sensors[0].SensorNumber.Length - 4, 4) + "(应变)" + dataDto.D1
                                                                                                + " 危险" + "(预警值" + strainAlarm_YJ + "，控制值" + strainControl_YJ + ")\r\n";
                                                                                }
                                                                                await alarmlogService.CreateAsync(new alarmlogs()
                                                                                {
                                                                                    SectionId = section.Id,
                                                                                    SensorId = sensors[0].Id,
                                                                                    Data = dataDto.D1,
                                                                                    Grade = 1
                                                                                }, false);
                                                                            }
                                                                        }
                                                                        catch { }
                                                                    }
                                                                    if (dataDto.D2 != null)
                                                                    {
                                                                        decimal d = Math.Abs((decimal)dataDto.D2);
                                                                        try
                                                                        {
                                                                            if (d >= strainAlarm_YJ && d < strainControl_YJ)
                                                                            {
                                                                                if(sensors[1].IsAlarm == 1)
                                                                                {
                                                                                    mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                                + "--" + sensors[1].SensorNumber.Substring(sensors[1].SensorNumber.Length - 4, 4) + "(应变)" + dataDto.D2
                                                                                                + " 预警" + "(预警值" + strainAlarm_YJ + "，控制值" + strainControl_YJ + ")\r\n";
                                                                                }
                                                                                await alarmlogService.CreateAsync(new alarmlogs()
                                                                                {
                                                                                    SectionId = section.Id,
                                                                                    SensorId = sensors[1].Id,
                                                                                    Data = dataDto.D2,
                                                                                    Grade = 0
                                                                                }, false);
                                                                            }
                                                                            else if (d >= strainControl_YJ && d < 3*strainControl_YJ)
                                                                            {
                                                                                if(sensors[1].IsAlarm == 1)
                                                                                {
                                                                                    mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                                + "--" + sensors[1].SensorNumber.Substring(sensors[1].SensorNumber.Length - 4, 4) + "(应变)" + dataDto.D2
                                                                                                + " 危险" + "(预警值" + strainAlarm_YJ + "，控制值" + strainControl_YJ + ")\r\n";
                                                                                }
                                                                                await alarmlogService.CreateAsync(new alarmlogs()
                                                                                {
                                                                                    SectionId = section.Id,
                                                                                    SensorId = sensors[1].Id,
                                                                                    Data = dataDto.D2,
                                                                                    Grade = 1
                                                                                }, false);
                                                                            }
                                                                        }
                                                                        catch { }
                                                                    }
                                                                    if (dataDto.D3 != null)
                                                                    {
                                                                        decimal d = Math.Abs((decimal)dataDto.D3);
                                                                        try
                                                                        {
                                                                            if (d >= strainAlarm_YJ && d < strainControl_YJ)
                                                                            {
                                                                                if(sensors[2].IsAlarm == 1)
                                                                                {
                                                                                    mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                                + "--" + sensors[2].SensorNumber.Substring(sensors[2].SensorNumber.Length - 4, 4) + "(应变)" + dataDto.D3
                                                                                                + " 预警" + "(预警值" + strainAlarm_YJ + "，控制值" + strainControl_YJ + ")\r\n";
                                                                                }
                                                                                await alarmlogService.CreateAsync(new alarmlogs()
                                                                                {
                                                                                    SectionId = section.Id,
                                                                                    SensorId = sensors[2].Id,
                                                                                    Data = dataDto.D3,
                                                                                    Grade = 0
                                                                                }, false);
                                                                            }
                                                                            else if (d >= strainControl_YJ && d < 3*strainControl_YJ)
                                                                            {
                                                                                if(sensors[2].IsAlarm == 1)
                                                                                {
                                                                                    mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                                + "--" + sensors[2].SensorNumber.Substring(sensors[2].SensorNumber.Length - 4, 4) + "(应变)" + dataDto.D3
                                                                                                + " 危险" + "(预警值" + strainAlarm_YJ + "，控制值" + strainControl_YJ + ")\r\n";
                                                                                }
                                                                                await alarmlogService.CreateAsync(new alarmlogs()
                                                                                {
                                                                                    SectionId = section.Id,
                                                                                    SensorId = sensors[2].Id,
                                                                                    Data = dataDto.D3,
                                                                                    Grade = 1
                                                                                }, false);
                                                                            }
                                                                        }
                                                                        catch { }
                                                                    }
                                                                    if (dataDto.D4 != null)
                                                                    {
                                                                        decimal d = Math.Abs((decimal)dataDto.D4);
                                                                        try
                                                                        {
                                                                            if (d >= strainAlarm_YJ && d < strainControl_YJ)
                                                                            {
                                                                                if(sensors[3].IsAlarm == 1)
                                                                                {
                                                                                    mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                                + "--" + sensors[3].SensorNumber.Substring(sensors[3].SensorNumber.Length - 4, 4) + "(应变)" + dataDto.D4
                                                                                                + " 预警" + "(预警值" + strainAlarm_YJ + "，控制值" + strainControl_YJ + ")\r\n";
                                                                                }
                                                                                await alarmlogService.CreateAsync(new alarmlogs()
                                                                                {
                                                                                    SectionId = section.Id,
                                                                                    SensorId = sensors[3].Id,
                                                                                    Data = dataDto.D4,
                                                                                    Grade = 0
                                                                                }, false);
                                                                            }
                                                                            else if (d >= strainControl_YJ && d < 3*strainControl_YJ)
                                                                            {
                                                                                if(sensors[3].IsAlarm == 1)
                                                                                {
                                                                                    mailTxt += "\t" + section.Shaft + "号竖井--" + section.engineeringsites.Name + "--" + section.SectionNumber
                                                                                                + "--" + sensors[3].SensorNumber.Substring(sensors[3].SensorNumber.Length - 4, 4) + "(应变)" + dataDto.D4
                                                                                                + " 危险" + "(预警值" + strainAlarm_YJ + "，控制值" + strainControl_YJ + ")\r\n";
                                                                                }
                                                                                await alarmlogService.CreateAsync(new alarmlogs()
                                                                                {
                                                                                    SectionId = section.Id,
                                                                                    SensorId = sensors[3].Id,
                                                                                    Data = dataDto.D4,
                                                                                    Grade = 1
                                                                                }, false);
                                                                            }
                                                                        }
                                                                        catch { }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                    await strainService.Save();
                                                }
                                                catch
                                                {
                                                    await strainService.Save();
                                                }
                                            }
                                        }
                                        else if (dataDtos[0].Type == "GJ")
                                        {
                                            using (ISteelStressService steelService = new SteelStressService())
                                            {
                                                try
                                                {
                                                    foreach (var dataDto in dataDtos)
                                                    {
                                                        try
                                                        {
                                                            if (dataDto.D1 != null || dataDto.D2 != null || dataDto.D3 != null ||
                                                                dataDto.D4 != null || dataDto.D5 != null || dataDto.D6 != null ||
                                                                dataDto.D7 != null)
                                                            {
                                                                var section = await sectionService.GetAllAsync().Where(m => m.Id == dataDto.Id).FirstOrDefaultAsync();
                                                                var sensors = await sensorService.GetAllAsync().Where(m => m.SectionId == dataDto.Id && m.SensorType == dataDto.Type)
                                                                    .OrderBy(m => m.SensorNumber).ToListAsync();
                                                                await steelService.CreateAsync(new steelstresses()
                                                                {
                                                                    SectionId = section.Id,
                                                                    Data1 = dataDto.D1,
                                                                    Data2 = dataDto.D2,
                                                                    Data3 = dataDto.D3,
                                                                    Data4 = dataDto.D4,
                                                                    Data5 = dataDto.D5,
                                                                    Data6 = dataDto.D6,
                                                                    Data7 = dataDto.D7,
                                                                    Data8 = dataDto.D8
                                                                }, false);
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                    await steelService.Save();
                                                }
                                                catch
                                                {
                                                    await steelService.Save();
                                                }
                                            }
                                        }
                                        else if (dataDtos[0].Type == "YL")
                                        {
                                            using (IStressService stressService = new StressService())
                                            {
                                                try
                                                {
                                                    foreach (var dataDto in dataDtos)
                                                    {
                                                        try
                                                        {
                                                            if (dataDto.D1 != null || dataDto.D2 != null || dataDto.D3 != null ||
                                                                dataDto.D4 != null || dataDto.D5 != null || dataDto.D6 != null ||
                                                                dataDto.D7 != null)
                                                            {
                                                                var section = await sectionService.GetAllAsync().Where(m => m.Id == dataDto.Id).FirstOrDefaultAsync();
                                                                var sensors = await sensorService.GetAllAsync().Where(m => m.SectionId == dataDto.Id && m.SensorType == dataDto.Type)
                                                                    .OrderBy(m => m.SensorNumber).ToListAsync();
                                                                await stressService.CreateAsync(new stresses()
                                                                {
                                                                    SectionId = section.Id,
                                                                    Data1 = dataDto.D1,
                                                                    Data2 = dataDto.D2,
                                                                    Data3 = dataDto.D3,
                                                                    Data4 = dataDto.D4,
                                                                    Data5 = dataDto.D5,
                                                                    Data6 = dataDto.D6,
                                                                    Data7 = dataDto.D7
                                                                }, false);
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                    await stressService.Save();
                                                }
                                                catch
                                                {
                                                    await stressService.Save();
                                                }
                                            }
                                        }
                                    }
                                    catch { }
                                    await alarmlogService.Save();
                                }
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(mailTxt) && sendMail)
                        {
                            using (IUserService userService = new UserService())
                            {
                                try
                                {
                                    mailTxt += "\t" + DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分");
                                    List<string> mails = await userService.GetAllAsync().Where(m=>m.IsAlarm==1).Select(m => m.Mail).ToListAsync();
                                    SendMail(mails, mailTxt);
                                    sendMail = false;
                                    await messageService.CreateAsync(new messages() { Comment = "发送邮件" });
                                }
                                catch { }
                            }
                        }
                    }
                }
                catch(Exception ex)
                {

                }
            }
        }
        #endregion

        #region 数据处理

        #endregion

        #region 工程部位
        public async Task<List<EngineeringDto>> GetEngineeringSites()
        {
            using (IEngineeringService engineeringSvc = new EngineeringService())
            {
                var datas = engineeringSvc.GetAllAsync();
                var list = await datas.Select(m => new EngineeringDto()
                {
                    Id = m.Id,
                    Name = m.Name,
                }).ToListAsync();
                list = list.OrderBy(m => m.Name).ToList();
                return list;
            }
        }

        public decimal GetOneAlarmValue(long engId)
        {
            using (IEngineeringService engineeringSvc = new EngineeringService())
            {
                var data = engineeringSvc.GetAllAsync().Where(m => m.Id == engId).Select(m => m.VibrationAlarmValue).First();
                decimal v = (decimal)data;
                return v;
            }
        }
        #endregion

        #region 日志
        public async Task<List<LogDto>> GetLogs(int pageIndex,int pageSize)
        {
            using(ILogService logService=new LogService())
            {
                var datas = logService.GetAllAsync();
                var list = await datas.Skip(pageIndex * pageSize).Take(pageSize).
                    Select(m => new LogDto()
                    {
                        Id=m.Id,
                        UserId=m.UserId,
                        UserName=m.users.Name,
                        Phone=m.users.Phone,
                        Behavior=m.Behavior,
                        CreateTime=m.CreateTime
                    }).ToListAsync();
                return list;
            }
        }
        public async Task<List<LogDto>> GetLogs(DateTime beginTime, DateTime endTime, int pageIndex, int pageSize,bool asc,long uid)
        {
            using (ILogService logService = new LogService())
            {
                var datas = logService.GetAllAsync().Where(m => m.UserId==uid&&m.CreateTime >= beginTime && m.CreateTime <= endTime);
                datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
                var list = await datas.Skip(pageIndex * pageSize).Take(pageSize).
                    Select(m => new LogDto()
                    {
                        Id = m.Id,
                        UserId = m.UserId,
                        UserName = m.users.Name,
                        Phone = m.users.Phone,
                        Behavior = m.Behavior,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
                return list;
            }
        }
        public async Task<int> GetLogCount(DateTime beginTime, DateTime endTime, long uid)
        {
            using (ILogService logService = new LogService())
            {
                var datas = logService.GetAllAsync().Where(m =>m.UserId==uid && m.CreateTime >= beginTime && m.CreateTime <= endTime);
                return await datas.CountAsync();
            }
        }

        public async Task RemoveLog(long[] ids)
        {
            using(ILogService logService=new LogService())
            {
                foreach(long id in ids)
                {
                    await logService.RemoveAsync(id, false);
                }
                await logService.Save();
            }
        }
        #endregion

        #region 截面
        public async Task<List<SectionDto>> GetAllSections(string shaft)
        {
            using (ISectionService sectionService = new SectionService())
            {
                var datas = sectionService.GetAllAsync();
                if (!(shaft == "" || shaft == null))
                {
                    datas = datas.Where(m => m.Shaft == shaft);
                }
                var list = await datas.Select(m => new SectionDto()
                {
                    Id = m.Id,
                    Shaft = m.Shaft,
                    EngId = m.EngId,
                    EngName = m.engineeringsites.Name,
                    SectionNum = m.SectionNumber,
                    CreateTime = m.CreateTime
                }).ToListAsync();
                return list;
            }
        }

        public async Task<List<SectionDto>> GetAllSections(string shaft, int pageIndex, int pageSize)
        {
            using (ISectionService sectionService = new SectionService())
            {
                var datas = sectionService.GetAllAsync();
                if (!(shaft == "" || shaft == null))
                {
                    datas = datas.Where(m => m.Shaft == shaft);
                }
                datas = datas.OrderBy(m => m.SectionNumber).Skip(pageIndex * pageSize).Take(pageSize);
                var list = await datas.Select(m => new SectionDto()
                {
                    Id = m.Id,
                    Shaft = m.Shaft,
                    EngId = m.EngId,
                    EngName = m.engineeringsites.Name,
                    SectionNum = m.SectionNumber,
                    CreateTime = m.CreateTime
                }).ToListAsync();
                return list;
            }
        }

        public async Task<SectionDto> GetOneSection(long sectionId)
        {
            using (ISectionService sectionService = new SectionService())
            {
                var data = await sectionService.GetOneByIdAsync(sectionId);
                SectionDto section = new SectionDto()
                {
                    Id = data.Id,
                    Shaft = data.Shaft,
                    EngId = data.EngId,
                    EngName = data.engineeringsites.Name,
                    SectionNum = data.SectionNumber,
                    CreateTime = data.CreateTime
                };
                return section;
            }
        }

        public async Task<List<SectionDto>> GetSectionsByEngId(long engId, string shaft)
        {
            using (ISectionService sectionService = new SectionService())
            {
                var datas = sectionService.GetAllAsync();
                if (engId != 0)
                {
                    datas = datas.Where(m => m.EngId == engId);
                }
                if (!string.IsNullOrEmpty(shaft) && shaft!="0")
                {
                    datas = datas.Where(m => m.Shaft == shaft);
                }
                datas = datas.OrderBy(m => m.SectionNumber);
                var list = await datas.Select(m => new SectionDto()
                {
                    Id = m.Id,
                    Shaft = m.Shaft,
                    EngId = m.EngId,
                    EngName = m.engineeringsites.Name,
                    SectionNum = m.SectionNumber,
                    CreateTime = m.CreateTime
                }).ToListAsync();
                return list;
            }
        }

        public async Task<List<SectionDto>> GetSectionsByEngId(long engId, string shaft, int pageIndex, int pageSize)
        {
            using (ISectionService sectionService = new SectionService())
            {
                var datas = sectionService.GetAllAsync().Where(m => m.EngId == engId);
                if (!(shaft == "" || shaft == null))
                {
                    datas = datas.Where(m => m.Shaft == shaft);
                }
                datas = datas.OrderBy(m => m.SectionNumber).Skip(pageIndex * pageSize).Take(pageSize);
                var list = await datas.Select(m => new SectionDto()
                {
                    Id = m.Id,
                    Shaft = m.Shaft,
                    EngId = m.EngId,
                    EngName = m.engineeringsites.Name,
                    SectionNum = m.SectionNumber,
                    CreateTime = m.CreateTime
                }).ToListAsync();
                return list;
            }
        }
        #endregion


        #region 传感器
        public async Task CreateSensor(long sectionId,string sensorType,string sensorNum,decimal? initialValue,string comment,long uid)
        {
            using(ISensorService sensorService=new SensorService())
            {
                await sensorService.CreateAsync(new sensors()
                {
                    SectionId = sectionId,
                    SensorType = sensorType,
                    SensorNumber = sensorNum,
                    InitialValue = initialValue,
                    Comment = comment
                });
                using (ILogService logService = new LogService())
                {
                    await logService.CreateAsync(new logs()
                    {
                        UserId = uid,
                        Behavior = "添加设备" + sensorNum
                    });
                }
            }
        }
        public async Task EditSensor(long id, long sectionId, string sensorNum,string sensorType, decimal? initialValue,string comment, long userId)
        {
            using(ISensorService sensorService=new SensorService())
            {
                var sensor = await sensorService.GetOneByIdAsync(id);
                sensor.SensorNumber = sensorNum;
                sensor.SensorType = sensorType;
                sensor.SectionId = sectionId;
                sensor.InitialValue = initialValue;
                sensor.Comment = comment;
                await sensorService.EditAsync(sensor);
                using(ILogService logService =new LogService())
                {
                    await logService.CreateAsync(new logs()
                    {
                        UserId = userId,
                        Behavior = "修改"+sensorNum+"信息"
                    });
                }
            }
        }

        public async Task ChangeSensorAlarm(long id,bool alarm,long uid)
        {
            using(ISensorService sensorService = new SensorService())
            {
                var sensor = await sensorService.GetOneByIdAsync(id);
                sensor.IsAlarm = alarm ? (sbyte)1 : (sbyte)0;
                await sensorService.EditAsync(sensor);
                using (ILogService logService = new LogService())
                {
                    if (alarm)
                    {
                        await logService.CreateAsync(new logs()
                        {
                            UserId = uid,
                            Behavior = "设置" + sensor.SensorNumber + "报警"
                        });
                    }
                    else
                    {
                        await logService.CreateAsync(new logs()
                        {
                            UserId = uid,
                            Behavior = "取消" + sensor.SensorNumber + "报警"
                        });
                    }
                }
            }
        }

        public async Task SetValue(long secId,string[] senNums,string type,long uid)
        {
            using (ISensorService sensorService = new SensorService())
            {
                var sensors = sensorService.GetAllAsync().Where(m => m.SectionId == secId&&m.SensorType==type);
                using(ILogService logService=new LogService())
                {
                    using(ISetValueLogService setValueLogSvc=new SetValueLogService())
                    {
                        if (type == "WY")
                        {
                            using (IOffsetService offsetService = new OffsetService())
                            {
                                foreach (var sen in sensors)
                                {
                                    if (senNums.Contains(sen.SensorNumber))
                                    {
                                        try
                                        {
                                            var datas = offsetService.GetAllAsync().Where(m => m.SectionId == secId)
                                            .OrderByDescending(m => m.CreateTime).Take(6);
                                            decimal?[] array = new decimal?[6];
                                            if (sen.SensorNumber.Last() == '1')
                                            {
                                                array = datas.Select(m => m.Data1).ToArray();
                                            }
                                            else if (sen.SensorNumber.Last() == '2')
                                            {
                                                array = datas.Select(m => m.Data2).ToArray();
                                            }
                                            else if (sen.SensorNumber.Last() == '3')
                                            {
                                                array = datas.Select(m => m.Data3).ToArray();
                                            }
                                            else if (sen.SensorNumber.Last() == '4')
                                            {
                                                array = datas.Select(m => m.Data4).ToArray();
                                            }
                                            for (int i = 0; i < array.Length; i++)
                                            {
                                                if (array[i] == null)
                                                    array[i] = 0;
                                            }
                                            if (sen.InitialValue == null)
                                                sen.InitialValue = 0;
                                            sen.InitialValue += (array.Sum() - array.Max() - array.Min()) / (array.Length - 2);
                                            //sen.IsSetValue = 1;
                                            await sensorService.EditAsync(sen, false);
                                            await logService.CreateAsync(new logs()
                                            {
                                                UserId = uid,
                                                Behavior = "校准" + sen.SensorNumber
                                            }, false);
                                            await setValueLogSvc.CreateAsync(new setvaluelogs()
                                            {
                                                SectionId = secId,
                                                SensorId = sen.Id,
                                                InitialValue = sen.InitialValue
                                            }, false);
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }
                        else
                        {
                            using(IStrainService strainService=new StrainService())
                            {
                                foreach (var sen in sensors)
                                {
                                    if (senNums.Contains(sen.SensorNumber))
                                    {
                                        try
                                        {
                                            var datas = strainService.GetAllAsync().Where(m => m.SectionId == secId)
                                            .OrderByDescending(m => m.CreateTime).Take(6);
                                            decimal?[] array = new decimal?[6];
                                            if (sen.SensorNumber.Last() == '1')
                                            {
                                                array = datas.Select(m => m.Data1).ToArray();
                                            }
                                            else if (sen.SensorNumber.Last() == '2')
                                            {
                                                array = datas.Select(m => m.Data2).ToArray();
                                            }
                                            else if (sen.SensorNumber.Last() == '3')
                                            {
                                                array = datas.Select(m => m.Data3).ToArray();
                                            }
                                            else if (sen.SensorNumber.Last() == '4')
                                            {
                                                array = datas.Select(m => m.Data4).ToArray();
                                            }
                                            for (int i = 0; i < array.Length; i++)
                                            {
                                                if (array[i] == null)
                                                    array[i] = 0;
                                            }
                                            if (sen.InitialValue == null)
                                                sen.InitialValue = 0;
                                            sen.InitialValue += (array.Sum() - array.Max() - array.Min()) / (array.Length - 2)/sen.K;
                                            //sen.IsSetValue = 1;
                                            await sensorService.EditAsync(sen, false);
                                            await logService.CreateAsync(new logs()
                                            {
                                                UserId = uid,
                                                Behavior = "校准" + sen.SensorNumber
                                            }, false);
                                            await setValueLogSvc.CreateAsync(new setvaluelogs()
                                            {
                                                SectionId = secId,
                                                SensorId = sen.Id,
                                                InitialValue = sen.InitialValue
                                            }, false);
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }
                        await setValueLogSvc.Save();
                    }
                    await logService.Save();
                }
                await sensorService.Save();
            }
        }

        public async Task SetAllValue(long engId,string shaft, string type, long uid)
        {
            using (ISensorService sensorService = new SensorService())
            {
                var sensors = sensorService.GetAllAsync().Where(m => m.sections.Shaft==shaft&&m.sections.EngId == engId && m.SensorType == type);
                string behavior = sensors.First().sections.engineeringsites.Name;
                using (ISetValueLogService setValueLogSvc = new SetValueLogService())
                {
                    if (type == "WY")
                    {
                        behavior += "位移";
                        using (IOffsetService offsetService = new OffsetService())
                        {
                            foreach (var sen in sensors)
                            {
                                try
                                {
                                    var datas = offsetService.GetAllAsync().Where(m => m.SectionId == sen.SectionId)
                                    .OrderByDescending(m => m.CreateTime).Take(6);
                                    decimal?[] array = new decimal?[6];
                                    if (sen.SensorNumber.Last() == '1')
                                    {
                                        array = datas.Select(m => m.Data1).ToArray();
                                    }
                                    else if (sen.SensorNumber.Last() == '2')
                                    {
                                        array = datas.Select(m => m.Data2).ToArray();
                                    }
                                    else if (sen.SensorNumber.Last() == '3')
                                    {
                                        array = datas.Select(m => m.Data3).ToArray();
                                    }
                                    else if (sen.SensorNumber.Last() == '4')
                                    {
                                        array = datas.Select(m => m.Data4).ToArray();
                                    }
                                    for (int i = 0; i < array.Length; i++)
                                    {
                                        if (array[i] == null)
                                            array[i] = 0;
                                    }
                                    if (sen.InitialValue == null)
                                        sen.InitialValue = 0;
                                    sen.InitialValue += (array.Sum() - array.Max() - array.Min()) / (array.Length - 2);
                                    //sen.IsSetValue = 1;
                                    await sensorService.EditAsync(sen, false);
                                    await setValueLogSvc.CreateAsync(new setvaluelogs()
                                    {
                                        SectionId = sen.SectionId,
                                        SensorId = sen.Id,
                                        InitialValue = sen.InitialValue
                                    }, false);
                                }
                                catch { }
                            }
                            await setValueLogSvc.Save();
                            await sensorService.Save();
                        }
                    }
                    else
                    {
                        behavior += "应变";
                        using (IStrainService strainService = new StrainService())
                        {
                            foreach (var sen in sensors)
                            {
                                try
                                {
                                    var datas = strainService.GetAllAsync().Where(m => m.SectionId == sen.SectionId)
                                    .OrderByDescending(m => m.CreateTime).Take(6);
                                    decimal?[] array = new decimal?[6];
                                    if (sen.SensorNumber.Last() == '1')
                                    {
                                        array = datas.Select(m => m.Data1).ToArray();
                                    }
                                    else if (sen.SensorNumber.Last() == '2')
                                    {
                                        array = datas.Select(m => m.Data2).ToArray();
                                    }
                                    else if (sen.SensorNumber.Last() == '3')
                                    {
                                        array = datas.Select(m => m.Data3).ToArray();
                                    }
                                    else if (sen.SensorNumber.Last() == '4')
                                    {
                                        array = datas.Select(m => m.Data4).ToArray();
                                    }
                                    for (int i = 0; i < array.Length; i++)
                                    {
                                        if (array[i] == null)
                                            array[i] = 0;
                                    }
                                    if (sen.InitialValue == null)
                                        sen.InitialValue = 0;
                                    sen.InitialValue += (array.Sum() - array.Max() - array.Min()) / (array.Length - 2) / sen.K;
                                    //sen.IsSetValue = 1;
                                    await sensorService.EditAsync(sen, false);
                                    await setValueLogSvc.CreateAsync(new setvaluelogs()
                                    {
                                        SectionId = sen.SectionId,
                                        SensorId = sen.Id,
                                        InitialValue = sen.InitialValue
                                    }, false);
                                }
                                catch { }
                            }
                            await setValueLogSvc.Save();
                            await sensorService.Save();
                        }
                    }
                }
                using(ILogService logService=new LogService())
                {
                    await logService.CreateAsync(new logs()
                    {
                        UserId = uid,
                        Behavior = "校准"+shaft+"#" + behavior + "传感器"
                    });
                }
            }
        }

        public async Task<List<decimal?>> GetReferenceValue(long secId,string type)
        {
            using (ISensorService sensorService = new SensorService())
            {
                List<decimal?> value = await sensorService.GetAllAsync().Where(m => m.SectionId == secId && m.SensorType == type)
                    .OrderBy(m => m.SensorNumber).Select(m => m.InitialValue).ToListAsync();
                for(int i = 0; i < value.Count; i++)
                {
                    if (value[i] == null) value[i] = 0;
                }
                return value;
            }
        }

        public async Task SetReferenceValue(long secId,decimal[] refs,int[] index,string type)
        {
            using (ISensorService sensorService = new SensorService())
            {
                var sensors = await sensorService.GetAllAsync().Where(m => m.SectionId == secId && m.SensorType == type)
                            .OrderBy(m => m.SensorNumber).ToListAsync();
                for(int i = 0; i < refs.Length; i++)
                {
                    sensors[index[i]].InitialValue = refs[i];
                    await sensorService.EditAsync(sensors[index[i]], false);
                }
                await sensorService.Save();
            }
        }
        public async Task SetReferenceValue(string[] secIds,string[] references1, string[] references2, string[] references3, string[] references4, string type)
        {
            using (ISensorService sensorService = new SensorService())
            {
                secIds = secIds.Distinct().ToArray();
                for(int i = 0; i < secIds.Length; i++)
                {
                    try
                    {
                        long secId = long.Parse(secIds[i]);
                        var sensors = await sensorService.GetAllAsync().Where(m => m.SectionId == secId && m.SensorType == type)
                            .OrderBy(m => m.SensorNumber).ToListAsync();
                        if (sensors.Count == 4)
                        {
                            if (i < references1.Length && !string.IsNullOrEmpty(references1[i]))
                            {
                                sensors[0].InitialValue = decimal.Parse(references1[i]);
                                await sensorService.EditAsync(sensors[0], false);
                            }
                            if (i < references2.Length && !string.IsNullOrEmpty(references2[i]))
                            {
                                sensors[1].InitialValue = decimal.Parse(references2[i]);
                                await sensorService.EditAsync(sensors[1], false);
                            }
                            if (i < references3.Length && !string.IsNullOrEmpty(references3[i]))
                            {
                                sensors[2].InitialValue = decimal.Parse(references3[i]);
                                await sensorService.EditAsync(sensors[2], false);
                            }
                            if (i < references4.Length && !string.IsNullOrEmpty(references4[i]))
                            {
                                sensors[3].InitialValue = decimal.Parse(references4[i]);
                                await sensorService.EditAsync(sensors[3], false);
                            }
                        }
                        else if (sensors.Count == 2)
                        {
                            if (i < references1.Length && !string.IsNullOrEmpty(references1[i]))
                            {
                                sensors[0].InitialValue = decimal.Parse(references1[i]);
                                await sensorService.EditAsync(sensors[0], false);
                            }
                            if (i < references4.Length && !string.IsNullOrEmpty(references4[i]))
                            {
                                sensors[1].InitialValue = decimal.Parse(references4[i]);
                                await sensorService.EditAsync(sensors[1], false);
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                await sensorService.Save();
            }
        }

        public async Task SetReferenceValue(ReceiveDataDto receive)
        {
            using (ISensorService sensorService = new SensorService())
            {
                var sensors = await sensorService.GetAllAsync().Where(m => m.SectionId == receive.Id && m.SensorType == receive.Type)
                    .OrderBy(m => m.SensorNumber).ToArrayAsync();
                try
                {
                    sensors[0].InitialValue = receive.D1;
                    await sensorService.EditAsync(sensors[0],false);
                }
                catch { }
                try
                {
                    sensors[1].InitialValue = receive.D2;
                    await sensorService.EditAsync(sensors[1], false);
                }
                catch { }
                try
                {
                    sensors[2].InitialValue = receive.D3;
                    await sensorService.EditAsync(sensors[2], false);
                }
                catch { }
                try
                {
                    sensors[3].InitialValue = receive.D4;
                    await sensorService.EditAsync(sensors[3], false);
                }
                catch { }
                try
                {
                    sensors[4].InitialValue = receive.D5;
                    await sensorService.EditAsync(sensors[4], false);
                }
                catch { }
                try
                {
                    sensors[5].InitialValue = receive.D6;
                    await sensorService.EditAsync(sensors[5], false);
                }
                catch { }
                try
                {
                    sensors[6].InitialValue = receive.D7;
                    await sensorService.EditAsync(sensors[6], false);
                }
                catch { }
                try
                {
                    sensors[7].InitialValue = receive.D8;
                    await sensorService.EditAsync(sensors[7], false);
                }
                catch { }
                try
                {
                    sensors[8].InitialValue = receive.D9;
                    await sensorService.EditAsync(sensors[8], false);
                }
                catch { }
                await sensorService.Save();
            }
        }

        public async Task DeleteSensor(long id,long userId)
        {
            using(ISensorService sensorService =new SensorService())
            {
                var sensor = await sensorService.GetOneByIdAsync(id);
                await sensorService.RemoveAsync(id);
                using (ILogService logService = new LogService())
                {
                    await logService.CreateAsync(new logs()
                    {
                        UserId = userId,
                        Behavior = "删除" + sensor.SensorNumber
                    });
                }
            }
        }

        public Task<List<SensorDto>> GetAllSensors(string shaft, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<SensorDto> GetOneSensor(long sensorId)
        {
            using(ISensorService sensorService=new SensorService())
            {
                var sensor = await sensorService.GetOneByIdAsync(sensorId);
                SensorDto sensorDto = new SensorDto()
                {
                    Id = sensor.Id,
                    SectionId = sensor.SectionId,
                    SectionNum = sensor.sections.SectionNumber,
                    SensorNum = sensor.SensorNumber,
                    SensorType = sensor.SensorType,
                    InitialValue = sensor.InitialValue,
                    Comment = sensor.Comment,
                    EngId=sensor.sections.engineeringsites.Id,
                    EngName = sensor.sections.engineeringsites.Name,
                    Shaft = sensor.sections.Shaft
                };
                return sensorDto;
            }
        }

        public Task<List<SensorDto>> GetSensors(string shaft, long? EngId, long? sectionId, string type)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SensorDto>> GetSensors(string shaft, long? EngId, long? sectionId, string type, int pageIndex, int pageSize)
        {
            using(ISensorService sensorService=new SensorService())
            {
                var datas = sensorService.GetAllAsync().Where(m => m.sections.Shaft == shaft && m.sections.EngId == EngId);
                if (!string.IsNullOrEmpty(type) && type!="0")
                {
                    datas = datas.Where(m => m.SensorType == type);
                }
                if (sectionId != 0)
                {
                    datas = datas.Where(m => m.SectionId == sectionId);
                }
                datas = datas.OrderBy(m => m.SensorNumber);
                var list = await datas.Skip(pageIndex * pageSize).Take(pageSize).
                    Select(m => new SensorDto()
                    {
                        Id = m.Id,
                        SectionId = m.SectionId,
                        SectionNum = m.sections.SectionNumber,
                        SensorNum = m.SensorNumber,
                        SensorType=m.SensorType,
                        InitialValue = m.InitialValue,
                        Comment = m.Comment,
                        EngName = m.sections.engineeringsites.Name,
                        Shaft = m.sections.Shaft,
                        IsAlarm=m.IsAlarm
                    }).ToListAsync();
                if (list.Any(m => m.SensorType == "ZD"))
                {
                    using (IVibrationService vibrationService = new VibrationService())
                    {
                        foreach (var item in list.Where(m => m.SensorType == "ZD"))
                        {
                            if (await vibrationService.GetAllAsync().AnyAsync(m => m.SectionId == item.SectionId))
                            {
                                var vibra = await vibrationService.GetAllAsync().Where(m => m.SectionId == item.SectionId).OrderByDescending(m => m.CreateTime).FirstAsync();
                                
                                item.LatestTime = vibra.CreateTime;
                                if (item.SensorNum.Last() == '1')
                                {
                                    item.LatestData = vibra.Data1;
                                    item.State = (vibra.Data1!=null&&vibra.CreateTime > DateTime.Now.AddHours(-0.5));
                                }
                                else if (item.SensorNum.Last() == '2')
                                {
                                    item.LatestData = vibra.Data2;
                                    item.State = (vibra.Data2 != null && vibra.CreateTime > DateTime.Now.AddHours(-0.5));
                                }
                                else if (item.SensorNum.Last() == '3')
                                {
                                    item.LatestData = vibra.Data3;
                                    item.State = (vibra.Data3 != null && vibra.CreateTime > DateTime.Now.AddHours(-0.5));
                                }
                                else if (item.SensorNum.Last() == '4')
                                {
                                    item.LatestData = vibra.Data4;
                                    item.State = (vibra.Data4 != null && vibra.CreateTime > DateTime.Now.AddHours(-0.5));
                                }
                                else if (item.SensorNum.Last() == '5')
                                {
                                    item.LatestData = vibra.Data5;
                                    item.State = (vibra.Data5 != null && vibra.CreateTime > DateTime.Now.AddHours(-0.5));
                                }
                            }
                        }
                    }
                }
                if (list.Any(m => m.SensorType == "YB"))
                {
                    using (IStrainService strainService = new StrainService())
                    {
                        foreach (var item in list.Where(m => m.SensorType == "YB"))
                        {
                            if (await strainService.GetAllAsync().AnyAsync(m => m.SectionId == item.SectionId))
                            {
                                var data = await strainService.GetAllAsync().Where(m => m.SectionId == item.SectionId).OrderByDescending(m => m.CreateTime).FirstAsync();
                                
                                item.LatestTime = data.CreateTime;
                                if (item.SensorNum.Last() == '1')
                                {
                                    item.LatestData = data.Data1;
                                    item.State = (data.Data1 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '2')
                                {
                                    item.LatestData = data.Data2;
                                    item.State = (data.Data2 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '3')
                                {
                                    item.LatestData = data.Data3;
                                    item.State = (data.Data3 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '4')
                                {
                                    item.LatestData = data.Data4;
                                    item.State = (data.Data4 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '5')
                                {
                                    item.LatestData = data.Data5;
                                    item.State = (data.Data5 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '6')
                                {
                                    item.LatestData = data.Data6;
                                    item.State = (data.Data6 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '7')
                                {
                                    item.LatestData = data.Data7;
                                    item.State = (data.Data1 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '8')
                                {
                                    item.LatestData = data.Data8;
                                    item.State = (data.Data8 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '9')
                                {
                                    item.LatestData = data.Data9;
                                    item.State = (data.Data9 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                            }
                        }
                    }
                }
                if (list.Any(m => m.SensorType == "WY"))
                {
                    using (IOffsetService offsetService = new OffsetService())
                    {
                        foreach (var item in list.Where(m => m.SensorType == "WY"))
                        {
                            if (await offsetService.GetAllAsync().AnyAsync(m => m.SectionId == item.SectionId))
                            {
                                var data = await offsetService.GetAllAsync().Where(m => m.SectionId == item.SectionId).OrderByDescending(m => m.CreateTime).FirstAsync();
                                
                                item.LatestTime = data.CreateTime;
                                if (item.SensorNum.Last() == '1')
                                {
                                    item.LatestData = data.Data1;
                                    item.State = (data.Data1 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '2')
                                {
                                    item.LatestData = data.Data2;
                                    item.State = (data.Data2 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '3')
                                {
                                    item.LatestData = data.Data3;
                                    item.State = (data.Data3 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '4')
                                {
                                    item.LatestData = data.Data4;
                                    item.State = (data.Data4 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '5')
                                {
                                    item.LatestData = data.Data5;
                                    item.State = (data.Data5 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '6')
                                {
                                    item.LatestData = data.Data6;
                                    item.State = (data.Data6 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '7')
                                {
                                    item.LatestData = data.Data7;
                                    item.State = (data.Data1 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '8')
                                {
                                    item.LatestData = data.Data8;
                                    item.State = (data.Data8 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '9')
                                {
                                    item.LatestData = data.Data9;
                                    item.State = (data.Data9 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                            }
                        }
                    }
                }
                if (list.Any(m => m.SensorType == "YL"))
                {
                    using (IStressService stressService = new StressService())
                    {
                        foreach (var item in list.Where(m => m.SensorType == "YL"))
                        {
                            if (await stressService.GetAllAsync().AnyAsync(m => m.SectionId == item.SectionId))
                            {
                                var data = await stressService.GetAllAsync().Where(m => m.SectionId == item.SectionId).OrderByDescending(m => m.CreateTime).FirstAsync();
                                
                                item.LatestTime = data.CreateTime;
                                if (item.SensorNum.Last() == '1')
                                {
                                    item.LatestData = data.Data1;
                                    item.State = (data.Data1 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '2')
                                {
                                    item.LatestData = data.Data2;
                                    item.State = (data.Data2 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '3')
                                {
                                    item.LatestData = data.Data3;
                                    item.State = (data.Data3 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '4')
                                {
                                    item.LatestData = data.Data4;
                                    item.State = (data.Data4 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '5')
                                {
                                    item.LatestData = data.Data5;
                                    item.State = (data.Data5 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '6')
                                {
                                    item.LatestData = data.Data6;
                                    item.State = (data.Data6 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '7')
                                {
                                    item.LatestData = data.Data7;
                                    item.State = (data.Data1 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                            }
                        }
                    }
                }
                if (list.Any(m => m.SensorType == "GJ"))
                {
                    using (ISteelStressService service = new SteelStressService())
                    {
                        foreach (var item in list.Where(m => m.SensorType == "GJ"))
                        {
                            if (await service.GetAllAsync().AnyAsync(m => m.SectionId == item.SectionId))
                            {
                                var data = await service.GetAllAsync().Where(m => m.SectionId == item.SectionId).OrderByDescending(m => m.CreateTime).FirstAsync();
                                item.State = data.CreateTime > (DateTime.Now.AddHours(-2));
                                item.LatestTime = data.CreateTime;
                                if (item.SensorNum.Last() == '1')
                                {
                                    item.LatestData = data.Data1;
                                    item.State = (data.Data1 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '2')
                                {
                                    item.LatestData = data.Data2;
                                    item.State = (data.Data2 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '3')
                                {
                                    item.LatestData = data.Data3;
                                    item.State = (data.Data3 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '4')
                                {
                                    item.LatestData = data.Data4;
                                    item.State = (data.Data4 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '5')
                                {
                                    item.LatestData = data.Data5;
                                    item.State = (data.Data5 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '6')
                                {
                                    item.LatestData = data.Data6;
                                    item.State = (data.Data6 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                                else if (item.SensorNum.Last() == '7')
                                {
                                    item.LatestData = data.Data7;
                                    item.State = (data.Data1 != null && data.CreateTime > DateTime.Now.AddHours(-1));
                                }
                            }
                        }
                    }
                }
                return list;
            }
        }

        public async Task<int> GetSensorCount(string shaft, long? EngId, long? sectionId, string type)
        {
            using (ISensorService sensorService = new SensorService())
            {
                var datas = sensorService.GetAllAsync().Where(m => m.sections.Shaft == shaft && m.sections.EngId == EngId);
                if (!string.IsNullOrEmpty(type) && type != "0")
                {
                    datas = datas.Where(m => m.SensorType == type);
                }
                if (sectionId != 0)
                {
                    datas = datas.Where(m => m.SectionId == sectionId);
                }
                return await datas.CountAsync();
            }
        }

        public async Task<int> GetSensorOnLineCount(string shaft, long? EngId, long? sectionId, string type)
        {
            using(ISensorService sensorService =new SensorService())
            {
                try
                {
                    var datas = sensorService.GetAllAsync().Where(m => m.sections.Shaft == shaft && m.sections.EngId == EngId);
                    if (!string.IsNullOrEmpty(type) && type != "0")
                    {
                        datas = datas.Where(m => m.SensorType == type);
                    }
                    if (sectionId != 0)
                    {
                        datas = datas.Where(m => m.SectionId == sectionId);
                    }
                    var list = await datas.Select(m => new SensorDto()
                    {
                        Id = m.Id,
                        SensorType = m.SensorType,
                        SectionId = m.SectionId
                    }).ToListAsync();
                    int i = 0;
                    var secId = list.Where(m => m.SensorType == "ZD").Select(m => m.SectionId).Distinct().ToArray();
                    if (secId.Length > 0)
                    {
                        using (IVibrationService vibrationService = new VibrationService())
                        {
                            foreach (var id in secId)
                            {
                                if (await vibrationService.GetAllAsync().AnyAsync(m => m.SectionId == id))
                                {
                                    var vibra = await vibrationService.GetAllAsync().Where(m => m.SectionId == id).OrderByDescending(m => m.CreateTime).FirstAsync();
                                    if (vibra.CreateTime > DateTime.Now.AddHours(-0.5))
                                    {
                                        if (vibra.Data1 != null) i++;
                                        if (vibra.Data2 != null) i++;
                                        if (vibra.Data3 != null) i++;
                                        if (vibra.Data4 != null) i++;
                                        if (vibra.Data5 != null) i++;
                                    }
                                }
                            }
                        }
                    }
                    secId = list.Where(m => m.SensorType == "YB").Select(m => m.SectionId).Distinct().ToArray();
                    if (secId.Length > 0)
                    {
                        using (IStrainService strainService = new StrainService())
                        {
                            foreach (var id in secId)
                            {
                                if (await strainService.GetAllAsync().AnyAsync(m => m.SectionId == id))
                                {
                                    var strain = await strainService.GetAllAsync().Where(m => m.SectionId == id).OrderByDescending(m => m.CreateTime).FirstAsync();
                                    if (strain.CreateTime > DateTime.Now.AddHours(-1))
                                    {
                                        if (strain.Data1 != null) i++;
                                        if (strain.Data2 != null) i++;
                                        if (strain.Data3 != null) i++;
                                        if (strain.Data4 != null) i++;
                                        if (strain.Data5 != null) i++;
                                        if (strain.Data6 != null) i++;
                                        if (strain.Data7 != null) i++;
                                        if (strain.Data8 != null) i++;
                                        if (strain.Data9 != null) i++;
                                    }
                                }
                            }
                        }
                    }
                    secId = list.Where(m => m.SensorType == "WY").Select(m => m.SectionId).Distinct().ToArray();
                    if (secId.Length > 0)
                    {
                        using (IOffsetService offsetService = new OffsetService())
                        {
                            foreach (var id in secId)
                            {
                                if (await offsetService.GetAllAsync().AnyAsync(m => m.SectionId == id))
                                {
                                    var offset = await offsetService.GetAllAsync().Where(m => m.SectionId == id).OrderByDescending(m => m.CreateTime).FirstAsync();
                                    if (offset.CreateTime > DateTime.Now.AddHours(-1))
                                    {
                                        if (offset.Data1 != null) i++;
                                        if (offset.Data2 != null) i++;
                                        if (offset.Data3 != null) i++;
                                        if (offset.Data4 != null) i++;
                                        if (offset.Data5 != null) i++;
                                        if (offset.Data6 != null) i++;
                                        if (offset.Data7 != null) i++;
                                        if (offset.Data8 != null) i++;
                                        if (offset.Data9 != null) i++;
                                    }
                                }
                            }
                        }
                    }
                    secId = list.Where(m => m.SensorType == "YL").Select(m => m.SectionId).Distinct().ToArray();
                    if (secId.Length > 0)
                    {
                        using (IStressService stressService = new StressService())
                        {
                            foreach (var id in secId)
                            {
                                if (await stressService.GetAllAsync().AnyAsync(m => m.SectionId == id))
                                {
                                    var stress = await stressService.GetAllAsync().Where(m => m.SectionId == id).OrderByDescending(m => m.CreateTime).FirstAsync();
                                    if (stress.CreateTime > DateTime.Now.AddHours(-1))
                                    {
                                        if (stress.Data1 != null) i++;
                                        if (stress.Data2 != null) i++;
                                        if (stress.Data3 != null) i++;
                                        if (stress.Data4 != null) i++;
                                        if (stress.Data5 != null) i++;
                                        if (stress.Data6 != null) i++;
                                        if (stress.Data7 != null) i++;
                                    }
                                }
                            }
                        }
                    }
                    secId = list.Where(m => m.SensorType == "GJ").Select(m => m.SectionId).Distinct().ToArray();
                    if (secId.Length > 0)
                    {
                        using (ISteelStressService steelService = new SteelStressService())
                        {
                            foreach (var id in secId)
                            {
                                if (await steelService.GetAllAsync().AnyAsync(m => m.SectionId == id))
                                {
                                    var steel = await steelService.GetAllAsync().Where(m => m.SectionId == id).OrderByDescending(m => m.CreateTime).FirstAsync();
                                    if (steel.CreateTime > DateTime.Now.AddHours(-1))
                                    {
                                        if (steel.Data1 != null) i++;
                                        if (steel.Data2 != null) i++;
                                        if (steel.Data3 != null) i++;
                                        if (steel.Data4 != null) i++;
                                        if (steel.Data5 != null) i++;
                                        if (steel.Data6 != null) i++;
                                        if (steel.Data7 != null) i++;
                                    }
                                }
                            }
                        }
                    }
                    return i;
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
        #endregion



        #region 振动数据
        
        public async Task<List<VibrationDto>> GetVibrationDatas(long sectionId, DateTime beginTime, DateTime endTime,bool asc=true)
        {
            using (IVibrationService vibrationService = new VibrationService())
            {
                var datas = vibrationService.GetAllAsync().Where(m => m.SectionId == sectionId && m.CreateTime >= beginTime && m.CreateTime <= endTime);
                datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
                using (ISensorService sensorService = new SensorService())
                {
                    var sensorNums = await sensorService.GetAllAsync().Where(m => m.SectionId == sectionId&&m.SensorType=="ZD").
                        OrderBy(m => m.SensorNumber).Select(m => m.SensorNumber).ToArrayAsync();
                    var list = await datas.Select(m => new VibrationDto()
                    {
                        Id = m.Id,
                        SectionId = m.SectionId,
                        SectionNum = m.sections.SectionNumber,
                        Shaft = m.sections.Shaft,
                        EngId = m.sections.EngId,
                        EngName = m.sections.engineeringsites.Name,
                        Data1 = m.Data1,
                        Data2 = m.Data2,
                        Data3 = m.Data3,
                        Data4 = m.Data4,
                        Data5 = m.Data5,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
                    if (list.Count > 0)
                    {
                        if (sensorNums.Length == 1)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                        }
                        else if (sensorNums.Length == 2)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                        }
                        else if (sensorNums.Length == 5)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                            list[0].SensorNum5 = sensorNums[4];
                        }
                    }
                    return list;
                }
            }
        }

        public async Task<List<VibrationDto>> GetVibrationDatas(long sectionId, DateTime beginTime, DateTime endTime, int pageIndex, int pageSize,bool asc=true)
        {
            using (IVibrationService vibrationService = new VibrationService())
            {
                var datas = vibrationService.GetAllAsync().Where(m => m.SectionId == sectionId && m.CreateTime >= beginTime && m.CreateTime <= endTime);
                datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
                using (ISensorService sensorService = new SensorService())
                {
                    var sensorNums = await sensorService.GetAllAsync().Where(m => m.SectionId == sectionId&&m.SensorType=="ZD").OrderBy(m => m.SensorNumber).Select(m => m.SensorNumber).ToArrayAsync();
                    var list = await datas.Skip(pageIndex*pageSize).Take(pageSize).Select(m => new VibrationDto()
                    {
                        Id = m.Id,
                        SectionId = m.SectionId,
                        SectionNum = m.sections.SectionNumber,
                        Shaft = m.sections.Shaft,
                        EngId=m.sections.EngId,
                        EngName=m.sections.engineeringsites.Name,
                        Data1 = m.Data1,
                        Data2 = m.Data2,
                        Data3 = m.Data3,
                        Data4 = m.Data4,
                        Data5 = m.Data5,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
                    if (list.Count > 0)
                    {
                        if (sensorNums.Length == 1)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                        }
                        else if (sensorNums.Length == 2)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                        }
                        else if (sensorNums.Length == 5)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                            list[0].SensorNum5 = sensorNums[4];
                        }
                    }
                    return list;
                }
            }
        }

        public async Task<List<VibrationDto>> GetLatestVibrations(long sectionId, int num,bool asc=true)
        {
            using(IVibrationService vibrationService=new VibrationService())
            {
                var datas = vibrationService.GetAllAsync().Where(m => m.SectionId == sectionId);
                datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
                using(ISensorService sensorService=new SensorService())
                {
                    var sensorNums = await sensorService.GetAllAsync().Where(m => m.SectionId == sectionId&&m.SensorType=="ZD").
                        OrderBy(m => m.SensorNumber).Select(m => m.SensorNumber).ToArrayAsync();
                    var list = await datas.Take(num).Select(m => new VibrationDto()
                    {
                        Id = m.Id,
                        SectionId = m.SectionId,
                        SectionNum = m.sections.SectionNumber,
                        Shaft = m.sections.Shaft,
                        EngId = m.sections.EngId,
                        EngName = m.sections.engineeringsites.Name,
                        Data1 = m.Data1,
                        Data2 = m.Data2,
                        Data3 = m.Data3,
                        Data4 = m.Data4,
                        Data5 = m.Data5,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
                    if (list.Count > 0)
                    {
                        if (sensorNums.Length == 1)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                        }
                        else if (sensorNums.Length == 2)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                        }
                        else if (sensorNums.Length == 5)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                            list[0].SensorNum5 = sensorNums[4];
                        }
                    }
                    return list;
                }
            }
        }

        public async Task<int> GetVibrationDatasCount(long sectionId, DateTime beginTime, DateTime endTime)
        {
            using (IVibrationService vibrationService = new VibrationService())
            {
                var datas = vibrationService.GetAllAsync().Where(m => m.SectionId == sectionId && m.CreateTime >= beginTime && m.CreateTime <= endTime);
                return await datas.CountAsync();
            }
        }
        #endregion

        #region 位移数据
        public async Task<List<OffsetDto>> GetOffsetDatas(long sectionId, DateTime beginTime, DateTime endTime, bool asc = true)
        {
            using(IOffsetService offsetService=new OffsetService())
            {
                var datas = offsetService.GetAllAsync().Where(m => m.SectionId == sectionId && m.CreateTime >= beginTime && m.CreateTime <= endTime);
                datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
                using(ISensorService sensorService=new SensorService())
                {
                    var sensorNums = await sensorService.GetAllAsync().Where(m => m.SectionId == sectionId && m.SensorType == "WY").
                        OrderBy(m => m.SensorNumber).Select(m => m.SensorNumber).ToArrayAsync();
                    var list = await datas.Select(m => new OffsetDto()
                    {
                        Id = m.Id,
                        SectionId = m.SectionId,
                        SectionNum = m.sections.SectionNumber,
                        Shaft = m.sections.Shaft,
                        EngId = m.sections.EngId,
                        EngName = m.sections.engineeringsites.Name,
                        Data1 = m.Data1,
                        Data2 = m.Data2,
                        Data3 = m.Data3,
                        Data4 = m.Data4,
                        Data5 = m.Data5,
                        Data6 = m.Data6,
                        Data7 = m.Data7,
                        Data8 = m.Data8,
                        Data9 = m.Data9,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
                    if (list.Count > 0)
                    {
                        if (sensorNums.Length == 2)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                        }
                        else if (sensorNums.Length == 4)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                        }
                        else if (sensorNums.Length == 9)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                            list[0].SensorNum5 = sensorNums[4];
                            list[0].SensorNum6 = sensorNums[5];
                            list[0].SensorNum7 = sensorNums[6];
                            list[0].SensorNum8 = sensorNums[7];
                            list[0].SensorNum9 = sensorNums[8];
                        }
                    }
                    return list;
                }
            }
        }

        public async Task<List<OffsetDto>> GetOffsetDatas(long sectionId, DateTime beginTime, DateTime endTime, int pageIndex, int pageSize, bool asc = true)
        {
            using (IOffsetService offsetService = new OffsetService())
            {
                var datas = offsetService.GetAllAsync().Where(m => m.SectionId == sectionId && m.CreateTime >= beginTime && m.CreateTime <= endTime);
                datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
                using (ISensorService sensorService = new SensorService())
                {
                    var sensorNums = await sensorService.GetAllAsync().Where(m => m.SectionId == sectionId && m.SensorType == "WY").
                        OrderBy(m => m.SensorNumber).Select(m => m.SensorNumber).ToArrayAsync();
                    var list = await datas.Skip(pageIndex*pageSize).Take(pageSize).Select(m => new OffsetDto()
                    {
                        Id = m.Id,
                        SectionId = m.SectionId,
                        SectionNum = m.sections.SectionNumber,
                        Shaft = m.sections.Shaft,
                        EngId = m.sections.EngId,
                        EngName = m.sections.engineeringsites.Name,
                        Data1 = m.Data1,
                        Data2 = m.Data2,
                        Data3 = m.Data3,
                        Data4 = m.Data4,
                        Data5 = m.Data5,
                        Data6 = m.Data6,
                        Data7 = m.Data7,
                        Data8 = m.Data8,
                        Data9 = m.Data9,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
                    if (list.Count > 0)
                    {
                        if (sensorNums.Length == 2)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                        }
                        else if (sensorNums.Length == 4)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                        }
                        else if (sensorNums.Length == 9)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                            list[0].SensorNum5 = sensorNums[4];
                            list[0].SensorNum6 = sensorNums[5];
                            list[0].SensorNum7 = sensorNums[6];
                            list[0].SensorNum8 = sensorNums[7];
                            list[0].SensorNum9 = sensorNums[8];
                        }
                    }
                    return list;
                }
            }
        }

        public async Task<List<OffsetDto>> GetLatestOffsets(long sectionId, int num, bool asc = true)
        {
            using(IOffsetService offsetService=new OffsetService())
            {
                var datas = offsetService.GetAllAsync().Where(m => m.SectionId == sectionId);
                datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
                using(ISensorService sensorService=new SensorService())
                {
                    var sensorNums = await sensorService.GetAllAsync().Where(m => m.SectionId == sectionId && m.SensorType == "WY").
                        OrderBy(m => m.SensorNumber).Select(m => m.SensorNumber).ToArrayAsync();
                    var list = await datas.Take(num).Select(m => new OffsetDto()
                    {
                        Id = m.Id,
                        SectionId = m.SectionId,
                        SectionNum = m.sections.SectionNumber,
                        Shaft = m.sections.Shaft,
                        EngId = m.sections.EngId,
                        EngName = m.sections.engineeringsites.Name,
                        Data1 = m.Data1,
                        Data2 = m.Data2,
                        Data3 = m.Data3,
                        Data4 = m.Data4,
                        Data5 = m.Data5,
                        Data6 = m.Data6,
                        Data7 = m.Data7,
                        Data8 = m.Data8,
                        Data9 = m.Data9,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
                    if (list.Count > 0)
                    {
                        if (sensorNums.Length == 2)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                        }
                        else if (sensorNums.Length == 4)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                        }
                        else if (sensorNums.Length == 9)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                            list[0].SensorNum5 = sensorNums[4];
                            list[0].SensorNum6 = sensorNums[5];
                            list[0].SensorNum7 = sensorNums[6];
                            list[0].SensorNum8 = sensorNums[7];
                            list[0].SensorNum9 = sensorNums[8];
                        }
                    }
                    return list;
                }
            }
        }

        public async Task<int> GetOffsetDatasCount(long sectionId, DateTime beginTime, DateTime endTime)
        {
            using(IOffsetService offsetService=new OffsetService())
            {
                var datas = offsetService.GetAllAsync().Where(m => m.SectionId == sectionId && m.CreateTime >= beginTime && m.CreateTime <= endTime);
                return await datas.CountAsync();
            }
        }
        #endregion

        #region 应变数据
        public async Task<List<StrainDto>> GetStrainDatas(long sectionId, DateTime beginTime, DateTime endTime, bool asc = true)
        {
            using (IStrainService strainService = new StrainService())
            {
                var datas = strainService.GetAllAsync().Where(m => m.SectionId == sectionId && m.CreateTime >= beginTime && m.CreateTime <= endTime);
                datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
                using (ISensorService sensorService = new SensorService())
                {
                    var sensorNums = await sensorService.GetAllAsync().Where(m => m.SectionId == sectionId && m.SensorType == "YB").
                        OrderBy(m => m.SensorNumber).Select(m => m.SensorNumber).ToArrayAsync();
                    var list = await datas.Select(m => new StrainDto()
                    {
                        Id = m.Id,
                        SectionId = m.SectionId,
                        SectionNum = m.sections.SectionNumber,
                        Shaft = m.sections.Shaft,
                        EngId = m.sections.EngId,
                        EngName = m.sections.engineeringsites.Name,
                        Data1 = m.Data1,
                        Data2 = m.Data2,
                        Data3 = m.Data3,
                        Data4 = m.Data4,
                        Data5 = m.Data5,
                        Data6 = m.Data6,
                        Data7 = m.Data7,
                        Data8 = m.Data8,
                        Data9 = m.Data9,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
                    if (list.Count > 0)
                    {
                        if (sensorNums.Length == 2)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                        }
                        else if (sensorNums.Length == 4)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                        }
                        else if (sensorNums.Length == 9)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                            list[0].SensorNum5 = sensorNums[4];
                            list[0].SensorNum6 = sensorNums[5];
                            list[0].SensorNum7 = sensorNums[6];
                            list[0].SensorNum8 = sensorNums[7];
                            list[0].SensorNum9 = sensorNums[8];
                        }
                    }
                    return list;
                }
            }
        }

        public async Task<List<StrainDto>> GetStrainDatas(long sectionId, DateTime beginTime, DateTime endTime, int pageIndex, int pageSize, bool asc = true)
        {
            using (IStrainService strainService = new StrainService())
            {
                var datas = strainService.GetAllAsync().Where(m => m.SectionId == sectionId && m.CreateTime >= beginTime && m.CreateTime <= endTime);
                datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
                using (ISensorService sensorService = new SensorService())
                {
                    var sensorNums = await sensorService.GetAllAsync().Where(m => m.SectionId == sectionId && m.SensorType == "YB").
                        OrderBy(m => m.SensorNumber).Select(m => m.SensorNumber).ToArrayAsync();
                    var list = await datas.Skip(pageIndex * pageSize).Take(pageSize).Select(m => new StrainDto()
                    {
                        Id = m.Id,
                        SectionId = m.SectionId,
                        SectionNum = m.sections.SectionNumber,
                        Shaft = m.sections.Shaft,
                        EngId = m.sections.EngId,
                        EngName = m.sections.engineeringsites.Name,
                        Data1 = m.Data1,
                        Data2 = m.Data2,
                        Data3 = m.Data3,
                        Data4 = m.Data4,
                        Data5 = m.Data5,
                        Data6 = m.Data6,
                        Data7 = m.Data7,
                        Data8 = m.Data8,
                        Data9 = m.Data9,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
                    if (list.Count > 0)
                    {
                        if (sensorNums.Length == 2)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                        }
                        else if (sensorNums.Length == 4)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                        }
                        else if (sensorNums.Length == 9)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                            list[0].SensorNum5 = sensorNums[4];
                            list[0].SensorNum6 = sensorNums[5];
                            list[0].SensorNum7 = sensorNums[6];
                            list[0].SensorNum8 = sensorNums[7];
                            list[0].SensorNum9 = sensorNums[8];
                        }
                    }
                    return list;
                }
            }
        }

        public async Task<List<StrainDto>> GetLatestStrains(long sectionId, int num, bool asc = true)
        {
            using (IStrainService strainService = new StrainService())
            {
                var datas = strainService.GetAllAsync().Where(m => m.SectionId == sectionId);
                datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
                using (ISensorService sensorService = new SensorService())
                {
                    var sensorNums = await sensorService.GetAllAsync().Where(m => m.SectionId == sectionId && m.SensorType == "YB").
                        OrderBy(m => m.SensorNumber).Select(m => m.SensorNumber).ToArrayAsync();
                    var list = await datas.Take(num).Select(m => new StrainDto()
                    {
                        Id = m.Id,
                        SectionId = m.SectionId,
                        SectionNum = m.sections.SectionNumber,
                        Shaft = m.sections.Shaft,
                        EngId = m.sections.EngId,
                        EngName = m.sections.engineeringsites.Name,
                        Data1 = m.Data1,
                        Data2 = m.Data2,
                        Data3 = m.Data3,
                        Data4 = m.Data4,
                        Data5 = m.Data5,
                        Data6 = m.Data6,
                        Data7 = m.Data7,
                        Data8 = m.Data8,
                        Data9 = m.Data9,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
                    if (list.Count > 0)
                    {
                        if (sensorNums.Length == 2)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                        }
                        else if (sensorNums.Length == 4)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                        }
                        else if (sensorNums.Length == 9)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                            list[0].SensorNum5 = sensorNums[4];
                            list[0].SensorNum6 = sensorNums[5];
                            list[0].SensorNum7 = sensorNums[6];
                            list[0].SensorNum8 = sensorNums[7];
                            list[0].SensorNum9 = sensorNums[8];
                        }
                    }
                    return list;
                }
            }
        }

        public async Task<int> GetStrainDatasCount(long sectionId, DateTime beginTime, DateTime endTime)
        {
            using (IStrainService strainService = new StrainService())
            {
                var datas = strainService.GetAllAsync().Where(m => m.SectionId == sectionId && m.CreateTime >= beginTime && m.CreateTime <= endTime);
                return await datas.CountAsync();
            }
        }
        #endregion

        #region 压力数据
        public async Task<List<StressDto>> GetStressDatas(long sectionId, DateTime beginTime, DateTime endTime, bool asc = true)
        {
            using (IStressService stressService = new StressService())
            {
                var datas = stressService.GetAllAsync().Where(m => m.SectionId == sectionId && m.CreateTime >= beginTime && m.CreateTime <= endTime);
                datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
                using (ISensorService sensorService = new SensorService())
                {
                    var sensorNums = await sensorService.GetAllAsync().Where(m => m.SectionId == sectionId && m.SensorType == "YL").
                        OrderBy(m => m.SensorNumber).Select(m => m.SensorNumber).ToArrayAsync();
                    var list = await datas.Select(m => new StressDto()
                    {
                        Id = m.Id,
                        SectionId = m.SectionId,
                        SectionNum = m.sections.SectionNumber,
                        Shaft = m.sections.Shaft,
                        EngId = m.sections.EngId,
                        EngName = m.sections.engineeringsites.Name,
                        Data1 = m.Data1,
                        Data2 = m.Data2,
                        Data3 = m.Data3,
                        Data4 = m.Data4,
                        Data5 = m.Data5,
                        Data6 = m.Data6,
                        Data7 = m.Data7,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
                    if (list.Count > 0)
                    {
                        if (sensorNums.Length == 7)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                            list[0].SensorNum5 = sensorNums[4];
                            list[0].SensorNum6 = sensorNums[5];
                            list[0].SensorNum7 = sensorNums[6];
                        }
                    }
                    return list;
                }
            }
        }

        public async Task<List<StressDto>> GetStressDatas(long sectionId, DateTime beginTime, DateTime endTime, int pageIndex, int pageSize, bool asc = true)
        {
            using (IStressService stressService = new StressService())
            {
                var datas = stressService.GetAllAsync().Where(m => m.SectionId == sectionId && m.CreateTime >= beginTime && m.CreateTime <= endTime);
                datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
                using (ISensorService sensorService = new SensorService())
                {
                    var sensorNums = await sensorService.GetAllAsync().Where(m => m.SectionId == sectionId && m.SensorType == "YL").
                        OrderBy(m => m.SensorNumber).Select(m => m.SensorNumber).ToArrayAsync();
                    var list = await datas.Skip(pageIndex * pageSize).Take(pageSize).Select(m => new StressDto()
                    {
                        Id = m.Id,
                        SectionId = m.SectionId,
                        SectionNum = m.sections.SectionNumber,
                        Shaft = m.sections.Shaft,
                        EngId = m.sections.EngId,
                        EngName = m.sections.engineeringsites.Name,
                        Data1 = m.Data1,
                        Data2 = m.Data2,
                        Data3 = m.Data3,
                        Data4 = m.Data4,
                        Data5 = m.Data5,
                        Data6 = m.Data6,
                        Data7 = m.Data7,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
                    if (list.Count > 0)
                    {
                        if (sensorNums.Length == 7)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                            list[0].SensorNum5 = sensorNums[4];
                            list[0].SensorNum6 = sensorNums[5];
                            list[0].SensorNum7 = sensorNums[6];
                        }
                    }
                    return list;
                }
            }
        }

        public async Task<List<StressDto>> GetLatestStresses(long sectionId, int num, bool asc = true)
        {
            using (IStressService stressService = new StressService())
            {
                var datas = stressService.GetAllAsync().Where(m => m.SectionId == sectionId);
                datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
                using (ISensorService sensorService = new SensorService())
                {
                    var sensorNums = await sensorService.GetAllAsync().Where(m => m.SectionId == sectionId && m.SensorType == "YL").
                        OrderBy(m => m.SensorNumber).Select(m => m.SensorNumber).ToArrayAsync();
                    var list = await datas.Take(num).Select(m => new StressDto()
                    {
                        Id = m.Id,
                        SectionId = m.SectionId,
                        SectionNum = m.sections.SectionNumber,
                        Shaft = m.sections.Shaft,
                        EngId = m.sections.EngId,
                        EngName = m.sections.engineeringsites.Name,
                        Data1 = m.Data1,
                        Data2 = m.Data2,
                        Data3 = m.Data3,
                        Data4 = m.Data4,
                        Data5 = m.Data5,
                        Data6 = m.Data6,
                        Data7 = m.Data7,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
                    if (list.Count > 0)
                    {
                        if (sensorNums.Length == 7)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                            list[0].SensorNum5 = sensorNums[4];
                            list[0].SensorNum6 = sensorNums[5];
                            list[0].SensorNum7 = sensorNums[6];
                        }
                    }
                    return list;
                }
            }
        }

        public async Task<int> GetStressDatasCount(long sectionId, DateTime beginTime, DateTime endTime)
        {
            using (IStressService stressService = new StressService())
            {
                var datas = stressService.GetAllAsync().Where(m => m.SectionId == sectionId && m.CreateTime >= beginTime && m.CreateTime <= endTime);
                return await datas.CountAsync();
            }
        }
        #endregion

        #region 钢筋应力数据
        public async Task<List<SteelStressDto>> GetSteelStrainDatas(long sectionId, DateTime beginTime, DateTime endTime, bool asc = true)
        {
            using (ISteelStressService steelStressSvc = new SteelStressService())
            {
                var datas = steelStressSvc.GetAllAsync().Where(m => m.SectionId == sectionId && m.CreateTime >= beginTime && m.CreateTime <= endTime);
                datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
                using (ISensorService sensorService = new SensorService())
                {
                    var sensorNums = await sensorService.GetAllAsync().Where(m => m.SectionId == sectionId && m.SensorType == "GJ").
                        OrderBy(m => m.SensorNumber).Select(m => m.SensorNumber).ToArrayAsync();
                    var list = await datas.Select(m => new SteelStressDto()
                    {
                        Id = m.Id,
                        SectionId = m.SectionId,
                        SectionNum = m.sections.SectionNumber,
                        Shaft = m.sections.Shaft,
                        EngId = m.sections.EngId,
                        EngName = m.sections.engineeringsites.Name,
                        Data1 = m.Data1,
                        Data2 = m.Data2,
                        Data3 = m.Data3,
                        Data4 = m.Data4,
                        Data5 = m.Data5,
                        Data6 = m.Data6,
                        Data7 = m.Data7,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
                    if (list.Count > 0)
                    {
                        if (sensorNums.Length == 7)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                            list[0].SensorNum5 = sensorNums[4];
                            list[0].SensorNum6 = sensorNums[5];
                            list[0].SensorNum7 = sensorNums[6];
                        }
                        else if(sensorNums.Length == 8)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                            list[0].SensorNum5 = sensorNums[4];
                            list[0].SensorNum6 = sensorNums[5];
                            list[0].SensorNum7 = sensorNums[6];
                            list[0].SensorNum8 = sensorNums[7];
                        }
                    }
                    return list;
                }
            }
        }

        public async Task<List<SteelStressDto>> GetSteelStrainDatas(long sectionId, DateTime beginTime, DateTime endTime, int pageIndex, int pageSize, bool asc = true)
        {
            using (ISteelStressService steelStressSvc = new SteelStressService())
            {
                var datas = steelStressSvc.GetAllAsync().Where(m => m.SectionId == sectionId && m.CreateTime >= beginTime && m.CreateTime <= endTime);
                datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
                using (ISensorService sensorService = new SensorService())
                {
                    var sensorNums = await sensorService.GetAllAsync().Where(m => m.SectionId == sectionId && m.SensorType == "GJ").
                        OrderBy(m => m.SensorNumber).Select(m => m.SensorNumber).ToArrayAsync();
                    var list = await datas.Skip(pageIndex * pageSize).Take(pageSize).Select(m => new SteelStressDto()
                    {
                        Id = m.Id,
                        SectionId = m.SectionId,
                        SectionNum = m.sections.SectionNumber,
                        Shaft = m.sections.Shaft,
                        EngId = m.sections.EngId,
                        EngName = m.sections.engineeringsites.Name,
                        Data1 = m.Data1,
                        Data2 = m.Data2,
                        Data3 = m.Data3,
                        Data4 = m.Data4,
                        Data5 = m.Data5,
                        Data6 = m.Data6,
                        Data7 = m.Data7,
                        Data8 = m.Data8,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
                    if (list.Count > 0)
                    {
                        if (sensorNums.Length == 7)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                            list[0].SensorNum5 = sensorNums[4];
                            list[0].SensorNum6 = sensorNums[5];
                            list[0].SensorNum7 = sensorNums[6];
                        }
                        else if (sensorNums.Length == 8)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                            list[0].SensorNum5 = sensorNums[4];
                            list[0].SensorNum6 = sensorNums[5];
                            list[0].SensorNum7 = sensorNums[6];
                            list[0].SensorNum8 = sensorNums[7];
                        }
                    }
                    return list;
                }
            }
        }

        public async Task<List<SteelStressDto>> GetLatestSteelStrains(long sectionId, int num, bool asc = true)
        {
            using (ISteelStressService steelStressSvc = new SteelStressService())
            {
                var datas = steelStressSvc.GetAllAsync().Where(m => m.SectionId == sectionId);
                datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
                using (ISensorService sensorService = new SensorService())
                {
                    var sensorNums = await sensorService.GetAllAsync().Where(m => m.SectionId == sectionId && m.SensorType == "GJ").
                        OrderBy(m => m.SensorNumber).Select(m => m.SensorNumber).ToArrayAsync();
                    var list = await datas.Take(num).Select(m => new SteelStressDto()
                    {
                        Id = m.Id,
                        SectionId = m.SectionId,
                        SectionNum = m.sections.SectionNumber,
                        Shaft = m.sections.Shaft,
                        EngId = m.sections.EngId,
                        EngName = m.sections.engineeringsites.Name,
                        Data1 = m.Data1,
                        Data2 = m.Data2,
                        Data3 = m.Data3,
                        Data4 = m.Data4,
                        Data5 = m.Data5,
                        Data6 = m.Data6,
                        Data7 = m.Data7,
                        Data8 = m.Data8,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
                    if (list.Count > 0)
                    {
                        if (sensorNums.Length == 7)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                            list[0].SensorNum5 = sensorNums[4];
                            list[0].SensorNum6 = sensorNums[5];
                            list[0].SensorNum7 = sensorNums[6];
                        }
                        else if (sensorNums.Length == 8)
                        {
                            list[0].SensorNum1 = sensorNums[0];
                            list[0].SensorNum2 = sensorNums[1];
                            list[0].SensorNum3 = sensorNums[2];
                            list[0].SensorNum4 = sensorNums[3];
                            list[0].SensorNum5 = sensorNums[4];
                            list[0].SensorNum6 = sensorNums[5];
                            list[0].SensorNum7 = sensorNums[6];
                            list[0].SensorNum8 = sensorNums[7];
                        }
                    }
                    return list;
                }
            }
        }

        public async Task<int> GetSteelStrainDatasCount(long sectionId, DateTime beginTime, DateTime endTime)
        {
            using (ISteelStressService steelStressSvc = new SteelStressService())
            {
                var datas = steelStressSvc.GetAllAsync().Where(m => m.SectionId == sectionId && m.CreateTime >= beginTime && m.CreateTime <= endTime);
                return await datas.CountAsync();
            }
        }
        #endregion

        #region 报警记录
        public async Task<List<AlarmLogDto>> GetAlarmLogs(string shaft, long? EngId, long? sectionId, string type, 
            DateTime beginTime, DateTime endTime, int pageIndex, int pageSize,bool asc=true)
        {
            using(IAlarmlogService alarmlogService=new AlarmlogService())
            {
                var datas = alarmlogService.GetAllAsync().Where(m=>m.CreateTime>=beginTime&&m.CreateTime<=endTime);
                if (!string.IsNullOrEmpty(type) && type != "0")
                {
                    datas = datas.Where(m => m.sensors.SensorType == type);
                }
                if (shaft != "0")
                {
                    datas = datas.Where(m => m.sections.Shaft == shaft);
                }
                if (EngId != 0)
                {
                    datas = datas.Where(m => m.sections.EngId == EngId);
                }
                if (sectionId != 0)
                {
                    datas = datas.Where(m => m.SectionId == sectionId);
                }
                datas = asc ? datas.OrderBy(m => m.CreateTime) : datas.OrderByDescending(m => m.CreateTime);
                var list = await datas.Skip(pageIndex * pageSize).Take(pageSize).Select(m => new AlarmLogDto()
                {
                    Id = m.Id,
                    Shaft = m.sections.Shaft,
                    EngId = m.sections.EngId,
                    EngName = m.sections.engineeringsites.Name,
                    SectionId = m.SectionId,
                    SectionNum = m.sections.SectionNumber,
                    SensorId = m.SectionId,
                    SensorNum = m.sensors.SensorNumber,
                    Type = m.sensors.SensorType,
                    Data = m.Data,
                    Grade = m.Grade,
                    CreateTime = m.CreateTime
                }).ToListAsync();
                return list;
            }
        }
        public async Task<List<AlarmLogDto>> GetAlarmLogs(int timespan)
        {
            using(IAlarmlogService alarmlogService=new AlarmlogService())
            {
                DateTime time = DateTime.Now.AddHours(timespan);
                var datas = alarmlogService.GetAllAsync().Where(m => m.CreateTime > time);
                List<AlarmLogDto> list = new List<AlarmLogDto>();
                if(await datas.CountAsync() == 0)
                {
                    return list;
                }
                long[] sensorIds = await datas.Select(m => m.SensorId).Distinct().ToArrayAsync();// 获取不同的传感器id
                foreach(var sensorId in sensorIds)
                {
                    // 找到该传感器的最大报警值
                    var max = datas.Where(m => m.SensorId == sensorId).OrderByDescending(m => m.Data).FirstOrDefault();
                    list.Add(new AlarmLogDto()
                    {
                        Id = max.Id,
                        SectionId = max.SectionId,
                        SectionNum = max.sections.SectionNumber,
                        SensorId = sensorId,
                        SensorNum=max.sensors.SensorNumber,
                        Type = max.sensors.SensorType,
                        Data = max.Data,
                        Grade=max.Grade,
                        CreateTime = max.CreateTime
                    });
                }
                list = list.OrderByDescending(m => m.Data).ToList();
                return list;
            }
        }
        public async Task<int> GetAlarmLogCount(string shaft, long? EngId, long? sectionId, string type,
            DateTime beginTime, DateTime endTime)
        {
            using(IAlarmlogService alarmlogService=new AlarmlogService())
            {
                var datas = alarmlogService.GetAllAsync().Where(m => m.CreateTime >= beginTime && m.CreateTime <= endTime);
                if (!string.IsNullOrEmpty(type) && type != "0")
                {
                    datas = datas.Where(m => m.sensors.SensorType == type);
                }
                if (shaft != "0")
                {
                    datas = datas.Where(m => m.sections.Shaft == shaft);
                }
                if (EngId != 0)
                {
                    datas = datas.Where(m => m.sections.EngId == EngId);
                }
                if (sectionId != 0)
                {
                    datas = datas.Where(m => m.SectionId == sectionId);
                }
                return await datas.CountAsync();
            }
        }
        public async Task<AlarmSettingDto> GetAlarmValue()
        {
            using (IEngineeringService engSvc = new EngineeringService())
            {

                AlarmSettingDto alarm = await engSvc.GetAllAsync().Where(m => m.Id == 1).Select(m => new AlarmSettingDto()
                {
                    StrainAlarm = m.StrainAlarmValue,
                    StrainControl = m.StrainContorlValue,
                    OffsetAlarm = m.OffsetAlarmValue,
                    OffsetControl = m.OffsetControlValue,
                    VibrationAlarm_YJ = m.VibrationAlarmValue
                }).FirstOrDefaultAsync();
                alarm.StressAlarm = await engSvc.GetAllAsync().Where(m => m.Id == 2).Select(m => m.SteelStressAlarmValue).FirstOrDefaultAsync();
                alarm.VibrationAlarm_ZD = await engSvc.GetAllAsync().Where(m => m.Id == 3).Select(m => m.VibrationAlarmValue).FirstOrDefaultAsync();
                alarm.VibrationAlarm_LJ = await engSvc.GetAllAsync().Where(m => m.Id == 4).Select(m => m.VibrationAlarmValue).FirstOrDefaultAsync();
                return alarm;
            }
        }
        public async Task EditAlarmValue(decimal offset1, decimal offset2, decimal strain1, decimal strain2, decimal stress, decimal vibraYJ, decimal vibraZD, decimal vibraLJ)
        {
            using(IEngineeringService engSvc=new EngineeringService())
            {
                string behavior = "";
                var datas = engSvc.GetAllAsync();
                foreach(var item in datas)
                {
                    if (item.Id == 1)
                    {
                        if (item.OffsetAlarmValue != offset1)
                        {
                            behavior += ",位移预警"+item.OffsetAlarmValue + "改为" + offset1;
                            item.OffsetAlarmValue = offset1;
                        }
                        if(item.OffsetControlValue != offset2)
                        {
                            behavior += ",位移控制" + item.OffsetControlValue + "改为" + offset2;
                            item.OffsetControlValue = offset2;
                        }
                        if (item.StrainAlarmValue != strain1)
                        {
                            behavior += ",应变预警" + item.StrainAlarmValue + "改为" + strain1;
                            item.StrainAlarmValue = strain1;
                        }
                        if (item.StrainContorlValue != strain2)
                        {
                            behavior += ",应变控制" + item.StrainContorlValue + "改为" + strain2;
                            item.StrainContorlValue = strain2;
                        }
                        if (item.VibrationAlarmValue != vibraYJ)
                        {
                            behavior += ",应急振动" + item.VibrationAlarmValue + "改为" + vibraYJ;
                            item.VibrationAlarmValue = vibraYJ;
                        }
                    }
                    else if (item.Id == 2)
                    {
                        if (item.SteelStressAlarmValue != stress)
                        {
                            behavior += ",应力预警" + item.SteelStressAlarmValue + "改为" + stress;
                            item.SteelStressAlarmValue = stress;
                        }
                        if (item.OffsetAlarmValue != offset1)
                        {
                            item.OffsetAlarmValue = offset1;
                        }
                        if(item.OffsetControlValue != offset2)
                        {
                            item.OffsetControlValue = offset2;
                        }
                        if (item.StrainAlarmValue != strain1)
                        {
                            item.StrainAlarmValue = strain1;
                        }
                        if (item.StrainContorlValue != strain2)
                        {
                            item.StrainContorlValue = strain2;
                        }
                    }
                    else if (item.Id == 3)
                    {
                        if (item.VibrationAlarmValue != vibraZD)
                        {
                            behavior += ",正洞振动" + item.VibrationAlarmValue + "改为" + vibraZD;
                            item.VibrationAlarmValue = vibraZD;
                        }

                        if (item.OffsetAlarmValue != offset1)
                        {
                            item.OffsetAlarmValue = offset1;
                        }
                        if (item.OffsetControlValue != offset2)
                        {
                            item.OffsetControlValue = offset2;
                        }
                        if (item.StrainAlarmValue != strain1)
                        {
                            item.StrainAlarmValue = strain1;
                        }
                        if (item.StrainContorlValue != strain2)
                        {
                            item.StrainContorlValue = strain2;
                        }
                    }
                    else if (item.Id == 4)
                    {
                        if (item.VibrationAlarmValue != vibraLJ)
                        {
                            behavior += ",斜井振动" + item.VibrationAlarmValue + "改为" + vibraLJ;
                            item.VibrationAlarmValue = vibraLJ;
                        }

                        if (item.OffsetAlarmValue != offset1)
                        {
                            item.OffsetAlarmValue = offset1;
                        }
                        if (item.OffsetControlValue != offset2)
                        {
                            item.OffsetControlValue = offset2;
                        }
                        if (item.StrainAlarmValue != strain1)
                        {
                            item.StrainAlarmValue = strain1;
                        }
                        if (item.StrainContorlValue != strain2)
                        {
                            item.StrainContorlValue = strain2;
                        }
                    }
                    await engSvc.EditAsync(item, false);
                }
                await engSvc.Save();
                if (behavior != "")
                {
                    behavior = behavior.Remove(0, 1);
                }
            }
        }
        #endregion
    }
}
