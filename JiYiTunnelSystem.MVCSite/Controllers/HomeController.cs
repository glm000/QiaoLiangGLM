using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using JiYiTunnelSystem.IBLL;
using JiYiTunnelSystem.BLL;
using JiYiTunnelSystem.Dto;
using Newtonsoft.Json;
using Webdiyer.WebControls.Mvc;
using JiYiTunnelSystem.MVCSite.Models.Home;
using JiYiTunnelSystem.MVCSite.Filters;
using FastReport.Web;
using FastReport.Utils;
using FastReport.Data;
using FastReport.Export.Pdf;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Web.UI.WebControls;
using JiYiTunnelSystem.MVCSite.HelperCode;
using FastReport;

namespace JiYiTunnelSystem.MVCSite.Controllers
{
    [LoginAuth]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> CreateSensor()
        {
            IProjectManager projectManager = new ProjectManager();
            ViewBag.Shafts = new List<SelectViewModel>
            {
                new SelectViewModel() { Id = "2", Name = "2号竖井" },
                new SelectViewModel() { Id = "4", Name = "4号竖井" }
            };
            ViewBag.Engineerings = await projectManager.GetEngineeringSites();
            ViewBag.Sections = await projectManager.GetSectionsByEngId(long.Parse("2"), "2");
            ViewBag.SensorTypes = new List<SelectViewModel>
            {
                new SelectViewModel() { Id = "YB", Name = "应变" },
                new SelectViewModel() { Id = "WY", Name = "位移" },
                new SelectViewModel() { Id = "ZD", Name = "振动" },
                new SelectViewModel() { Id = "YL", Name = "压力" },
                new SelectViewModel() { Id = "GJ", Name = "钢筋应力" }
            };
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateSensor(CreateSensorVM model)
        {
            if (!ModelState.IsValid)
            {
                Models.PartialMessageVM partial = new Models.PartialMessageVM { Message = "保存失败"};
                return PartialView("_PartialMessage", partial);
            }
            try
            {
                IProjectManager projectManager = new ProjectManager();
                long uid = long.Parse(Session["userId"].ToString());
                if (string.IsNullOrEmpty(model.InitialValue))
                    await projectManager.CreateSensor(model.SectionId, model.SensorType, model.SensorNum, null, model.Comment,uid);
                else
                    await projectManager.CreateSensor(model.SectionId, model.SensorType, model.SensorNum, decimal.Parse(model.InitialValue), model.Comment,uid);
                Models.PartialMessageVM partial = new Models.PartialMessageVM { Message = "添加成功", Icon = "success" };
                return PartialView("_PartialMessage", partial);
            }
            catch {
                Models.PartialMessageVM partial = new Models.PartialMessageVM { Message = "保存失败"};
                return PartialView("_PartialMessage", partial);
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditSensor(long? id,string url)
        {
            if (id == null)
            {
                Models.PartialMessageVM partial = new Models.PartialMessageVM { Message = "找不到该传感器数据" };
                return PartialView("_PartialMessage", partial);
            }
            ViewBag.Url = url;
            IProjectManager projectManager = new ProjectManager();
            var sensor = await projectManager.GetOneSensor(id.Value);
            ViewBag.Shafts = new List<SelectViewModel>
            {
                new SelectViewModel() { Id = "2", Name = "2号竖井" },
                new SelectViewModel() { Id = "4", Name = "4号竖井" }
            };
            ViewBag.SelectedShaft = sensor.Shaft;
            ViewBag.Engs = await projectManager.GetEngineeringSites();
            ViewBag.SelectedEng = sensor.EngId;
            //ViewBag.Sections = await projectManager.GetSectionsByEngId(long.Parse(ViewBag.SelectedEng), sensor.Shaft);
            ViewBag.SectionIds = new SelectList(await projectManager.GetSectionsByEngId(sensor.EngId,
                sensor.Shaft), "Id", "SectionNum", sensor.SectionId);
            ViewBag.SensorTypes = new List<SelectViewModel>
            {
                new SelectViewModel() { Id = "YB", Name = "应变" },
                new SelectViewModel() { Id = "WY", Name = "位移" },
                new SelectViewModel() { Id = "ZD", Name = "振动" },
                new SelectViewModel() { Id = "YL", Name = "压力" },
                new SelectViewModel() { Id = "GJ", Name = "钢筋应力" }
            };
            ViewBag.SelectedType = sensor.SensorType;
            return View(new CreateSensorVM()
            {
                Id=sensor.Id,
                SensorNum = sensor.SensorNum,
                InitialValue = sensor.InitialValue.ToString(),
                Comment = sensor.Comment
            });
        }
        [HttpPost]
        public async Task<ActionResult> EditSensor(CreateSensorVM model)
        {
            if (!ModelState.IsValid)
            {
                Models.PartialMessageVM partial = new Models.PartialMessageVM { Message = "修改失败" };
                return PartialView("_PartialMessage", partial);
            }
            try
            {
                model.SectionId = long.Parse(Request.Form["SecId"]);
                IProjectManager projectManager = new ProjectManager();
                long uid = long.Parse(Session["userId"].ToString());
                if (string.IsNullOrEmpty(model.InitialValue))
                    await projectManager.EditSensor(model.Id,model.SectionId, model.SensorNum, model.SensorType, null, model.Comment, uid);
                else
                    await projectManager.EditSensor(model.Id, model.SectionId, model.SensorNum, model.SensorType, decimal.Parse(model.InitialValue), model.Comment,uid);
                Models.PartialMessageVM partial = new Models.PartialMessageVM { Message = "修改成功", Icon = "success" ,Url= Request.Form["url"] };
                return PartialView("_PartialMessage", partial);
            }
            catch
            {
                Models.PartialMessageVM partial = new Models.PartialMessageVM { Message = "修改失败" };
                return PartialView("_PartialMessage", partial);
            }
        }
        [HttpGet]
        public async Task<ActionResult> DeleteSensor(long id)
        {
            try
            {
                IProjectManager projectManager = new ProjectManager();
                long userId = 1;
                await projectManager.DeleteSensor(id, userId);
                return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                Models.PartialMessageVM partial = new Models.PartialMessageVM { Message = "删除失败" };
                return PartialView("_PartialMessage", partial);
            }
        }
        public async Task<ActionResult> SensorList(string typeSelect="0", string shaftSelect="2", string engSelect="2", string sectionSelect="0", int pageIndex=1,int pageSize=15)
        {
            try
            {
                IProjectManager projectManager = new ProjectManager();
                ViewBag.Types = new List<MVCSite.Models.Home.SelectViewModel>
            {
                new Models.Home.SelectViewModel() { Id = "0", Name = "全部" },
                new Models.Home.SelectViewModel() { Id = "YB", Name = "应变" },
                new Models.Home.SelectViewModel() { Id = "WY", Name = "位移" },
                new Models.Home.SelectViewModel() { Id = "ZD", Name = "振动" },
                new Models.Home.SelectViewModel() { Id = "YL", Name = "压力" },
                new Models.Home.SelectViewModel() { Id = "GJ", Name = "钢筋应力" }
            };
                ViewBag.TypeSelect = typeSelect;
                ViewBag.Shafts = new List<MVCSite.Models.Home.SelectViewModel>
            {
                new Models.Home.SelectViewModel() { Id = "2", Name = "2号竖井" },
                new Models.Home.SelectViewModel() { Id = "4", Name = "4号竖井" }
            };
                ViewBag.ShaftSelect = shaftSelect;
                ViewBag.Engineerings = await projectManager.GetEngineeringSites();
                ViewBag.EngSelect = engSelect;
                ViewBag.Sections = await projectManager.GetSectionsByEngId(long.Parse(engSelect), shaftSelect);
                ((List<SectionDto>)ViewBag.Sections).Insert(0, new SectionDto() { SectionNum = "全部" });
                ViewBag.SectionSelect = sectionSelect;
                var datas = await projectManager.GetSensors(shaftSelect, long.Parse(engSelect), long.Parse(sectionSelect), typeSelect, pageIndex - 1, pageSize);
                int count = await projectManager.GetSensorCount(shaftSelect, long.Parse(engSelect), long.Parse(sectionSelect), typeSelect);
                ViewBag.TotalCount = count;
                ViewBag.OnlineCount = await projectManager.GetSensorOnLineCount(shaftSelect, long.Parse(engSelect), long.Parse(sectionSelect), typeSelect);
                return View(new PagedList<SensorDto>(datas, pageIndex, pageSize, count));
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<ActionResult> Test(string typeSelect,string shaftSelect, string engSelect, string sectionSelect, int pageIndex = 1, int pageSize = 15)
        {
            IProjectManager projectManager = new ProjectManager();
            if (string.IsNullOrEmpty(typeSelect))
            {
                typeSelect = "0";
            }
            if (string.IsNullOrEmpty(shaftSelect))
            {
                shaftSelect = "2";
            }
            if (string.IsNullOrEmpty(engSelect))
            {
                engSelect = "2";
            }
            if (string.IsNullOrEmpty(sectionSelect))
            {
                sectionSelect = "0";
            }
            var datas = await projectManager.GetSensors(shaftSelect, long.Parse(engSelect), long.Parse(sectionSelect), typeSelect, pageIndex - 1, pageSize);
            int count = await projectManager.GetSensorCount(shaftSelect, long.Parse(engSelect), long.Parse(sectionSelect), typeSelect);
            ViewBag.TotalCount = count;
            ViewBag.OnlineCount = await projectManager.GetSensorOnLineCount(shaftSelect, long.Parse(engSelect), long.Parse(sectionSelect), typeSelect);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_SensorTableData", new PagedList<SensorDto>(datas, pageIndex, pageSize, count));
            }
            return View(new PagedList<SensorDto>(datas, pageIndex, pageSize, count));
        }
        [HttpGet]
        public async Task<ActionResult> LogList(string beginTime, string endTime, int pageIndex = 1, int pageSize = 15, bool timeOrder=true)
        {
            IProjectManager projectManager = new ProjectManager();
            if (string.IsNullOrEmpty(beginTime))
            {
                beginTime = "1990-01-01 00:00:00";
            }
            else
            {
                ViewBag.beginTime = beginTime;
            }
            if (string.IsNullOrEmpty(endTime))
            {
                endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                ViewBag.endTime = endTime;
            }
            long uid = long.Parse(Session["userId"].ToString());
            var datas = await projectManager.GetLogs(Convert.ToDateTime(beginTime), Convert.ToDateTime(endTime), pageIndex - 1, pageSize,timeOrder,uid);
            int count = await projectManager.GetLogCount(Convert.ToDateTime(beginTime), Convert.ToDateTime(endTime),uid);
            return View(new PagedList<LogDto>(datas, pageIndex, pageSize, count));
        }
        [HttpPost]
        public async Task<ActionResult> DeleteLog(long[] ids)
        {
            IProjectManager projectManager = new ProjectManager();
            try
            {
                await projectManager.RemoveLog(ids);
                return Json(new { result = "ok" });
            }
            catch
            {
                return Json(new { result = "error" });
            }
        }

        [ManagerAuth]
        [HttpGet]
        public async Task<ActionResult> UserList(int pageIndex=1,int pageSize=15)
        {
            IUserManager userManager = new UserManager();
            var users = await userManager.GetUsers(pageIndex - 1, pageSize);
            var count = await userManager.GetUserCount();
            ViewBag.Roles = new List<MVCSite.Models.Home.SelectViewModel>
            {
                new Models.Home.SelectViewModel() { Id = "0", Name = "普通用户" },
                new Models.Home.SelectViewModel() { Id = "1", Name = "管理员" }
            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_UserList", new PagedList<UserDto>(users, pageIndex, pageSize, count));
            }
            return View(new PagedList<UserDto>(users, pageIndex, pageSize, count));
        }

        [ManagerAuth]
        [HttpPost]
        public async Task<ActionResult> ChangeUserAuth(long id,int role)
        {
            IUserManager userManager = new UserManager();
            try
            {
                long uid = long.Parse(Session["userId"].ToString());
                await userManager.ChangeUserAuth(id, role, uid);
                return Json(new { res = "ok" });
            }
            catch
            {
                return Json(new { res = "err" });
            }
        }

        [ManagerAuth]
        [HttpGet]
        public async Task<ActionResult> AlarmLogList(string beginTime, string endTime, string typeSelect = "0", string shaftSelect = "0", string engSelect = "0", string sectionSelect = "0", int pageIndex = 1, int pageSize = 15, bool timeOrder = true)
        {
            if (string.IsNullOrEmpty(beginTime))
            {
                beginTime = "1990-01-01 00:00:00";
            }
            else
            {
                ViewBag.beginTime = beginTime;
            }
            if (string.IsNullOrEmpty(endTime))
            {
                endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                ViewBag.endTime = endTime;
            }
            IProjectManager projectManager = new ProjectManager();
            ViewBag.Types = new List<MVCSite.Models.Home.SelectViewModel>
            {
                new Models.Home.SelectViewModel() { Id = "0", Name = "全部" },
                new Models.Home.SelectViewModel() { Id = "YB", Name = "应变" },
                new Models.Home.SelectViewModel() { Id = "WY", Name = "位移" },
                new Models.Home.SelectViewModel() { Id = "ZD", Name = "振动" },
                new Models.Home.SelectViewModel() { Id = "YL", Name = "压力" },
                new Models.Home.SelectViewModel() { Id = "GJ", Name = "钢筋应力" }
            };
            ViewBag.TypeSelect = typeSelect;
            ViewBag.Shafts = new List<MVCSite.Models.Home.SelectViewModel>
            {
                new Models.Home.SelectViewModel() { Id = "0", Name = "全部" },
                new Models.Home.SelectViewModel() { Id = "2", Name = "2号竖井" },
                new Models.Home.SelectViewModel() { Id = "4", Name = "4号竖井" }
            };
            ViewBag.ShaftSelect= shaftSelect;
            ViewBag.Engineerings = await projectManager.GetEngineeringSites();
            ((List<EngineeringDto>)ViewBag.Engineerings).Insert(0, new EngineeringDto() { Name = "全部" });
            if (shaftSelect == "0")
                ViewBag.EngSelect = "0";
            else
                ViewBag.EngSelect = engSelect;
            ViewBag.Sections = await projectManager.GetSectionsByEngId(long.Parse(engSelect), shaftSelect);
            ((List<SectionDto>)ViewBag.Sections).Insert(0, new SectionDto() { SectionNum = "全部" });
            ViewBag.SectionSelect = sectionSelect;
            List<AlarmLogDto> datas = await projectManager.GetAlarmLogs(shaftSelect, long.Parse(engSelect), long.Parse(sectionSelect), typeSelect, Convert.ToDateTime(beginTime), Convert.ToDateTime(endTime), pageIndex - 1, pageSize,timeOrder);
            int count = await projectManager.GetAlarmLogCount(shaftSelect, long.Parse(engSelect), long.Parse(sectionSelect), typeSelect, Convert.ToDateTime(beginTime), Convert.ToDateTime(endTime));
            return View(new PagedList<AlarmLogDto>(datas,pageIndex,pageSize,count));
        }
        [ManagerAuth]
        [HttpGet]
        public async Task<ActionResult> AlarmSetting()
        {
            IProjectManager projectManager = new ProjectManager();
            AlarmSettingDto data = await projectManager.GetAlarmValue();
            return View(data);
        }
        [ManagerAuth]
        [HttpPost]
        public async Task<ActionResult> AlarmSetting(AlarmSettingDto model)
        {
            IProjectManager projectManager = new ProjectManager();
            try
            {
                await projectManager.EditAlarmValue(model.OffsetAlarm.Value, model.OffsetControl.Value, model.StrainAlarm.Value,
                model.StrainControl.Value, model.StressAlarm.Value, model.VibrationAlarm_YJ.Value,
                model.VibrationAlarm_ZD.Value, model.VibrationAlarm_LJ.Value);
                Models.PartialMessageVM partial = new Models.PartialMessageVM { Message = "设置成功", Icon = "success" };
                return PartialView("_PartialMessage", partial);
            }
            catch
            {
                Models.PartialMessageVM partial = new Models.PartialMessageVM { Message = "设置失败", Icon = "warning" };
                return PartialView("_PartialMessage", partial);
            }
        }

        [ManagerAuth]
        public async Task<ActionResult> AlarmUserSetting(string alarm="1",int pageIndex=1,int pageSize=8)
        {
            IUserManager userManager = new UserManager();
            var users = await userManager.GetUsers(sbyte.Parse(alarm), pageIndex - 1, pageSize);
            int count = await userManager.GetUserCountByAlarm(sbyte.Parse(alarm));
            if (Request.IsAjaxRequest())
            {
                return PartialView("_UserListByAlarm", new PagedList<UserDto>(users, pageIndex, pageSize, count));
            }
            return View(new PagedList<UserDto>(users, pageIndex, pageSize, count));
        }

        [ManagerAuth]
        [HttpPost]
        public async Task<ActionResult> ChangeUserIsAlarm(long id,bool alarm)
        {
            IUserManager userManager = new UserManager();
            try
            {
                long uid = long.Parse(Session["userId"].ToString());
                await userManager.ChangeUserIsAlarm(id, alarm, uid);
                return Json(new { res = "ok" });
            }
            catch(Exception e)
            {
                return Json(new { res = "err" });
            }
        }

        public ActionResult AuthError()
        {
            Models.PartialMessageVM partial = new Models.PartialMessageVM { Message = "您的权限不足", Icon = "warning" };
            return PartialView("_PartialMessage", partial);
        }

        public async Task<ActionResult> VibrationListBySection(string beginTime, string endTime, string sectionSelect, int pageIndex = 1, int pageSize = 15)
        {
            IProjectManager projectManager = new ProjectManager();
            if (!string.IsNullOrEmpty(beginTime))
            {
                ViewBag.beginTime = beginTime;
            }
            else
            {
                ViewBag.beginTime = DateTime.Now.AddHours(-3).ToString("yyyy-MM-dd HH:mm:ss");
                beginTime = "2021-05-14 10:00:00";
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                ViewBag.endTime = endTime;
            }
            else
            {
                endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                ViewBag.endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (string.IsNullOrEmpty(sectionSelect))
            {
                sectionSelect = "85";
            }
            var datas = await projectManager.GetVibrationDatas(long.Parse(sectionSelect), Convert.ToDateTime(beginTime), Convert.ToDateTime(endTime), pageIndex - 1, 15);
            var count = await projectManager.GetVibrationDatasCount(long.Parse(sectionSelect), Convert.ToDateTime(beginTime), Convert.ToDateTime(endTime));
            if (Request.IsAjaxRequest())
            {
                return PartialView("VData", new PagedList<VibrationDto>(datas, pageIndex, pageSize, count));
            }
            return View(new PagedList<VibrationDto>(datas, pageIndex, pageSize, count));
        }

        public async Task<ActionResult> OffsetListBySection(string beginTime, string endTime,string sectionSelect, int pageIndex = 1, int pageSize = 15)
        {
            IProjectManager projectManager = new ProjectManager();
            if (string.IsNullOrEmpty(beginTime))
            {
                beginTime = "1990-01-01 00:00:00";
            }
            else
            {
                ViewBag.beginTime = beginTime;
            }
            if (string.IsNullOrEmpty(endTime))
            {
                endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                ViewBag.endTime = endTime;
            }
            if (string.IsNullOrEmpty(sectionSelect))
            {
                sectionSelect = "85";
            }
            var datas = await projectManager.GetOffsetDatas(long.Parse(sectionSelect), Convert.ToDateTime(beginTime), Convert.ToDateTime(endTime), pageIndex - 1, pageSize);
            var count = await projectManager.GetOffsetDatasCount(long.Parse(sectionSelect), Convert.ToDateTime(beginTime), Convert.ToDateTime(endTime));
            if (Request.IsAjaxRequest())
            {
                return PartialView("_OffsetTableData", new PagedList<OffsetDto>(datas, pageIndex, pageSize, count));
            }
            return View(new PagedList<OffsetDto>(datas, pageIndex, pageSize, count));
        }

        public async Task<ActionResult> StrainListBySection(string beginTime, string endTime,string sectionSelect, int pageIndex = 1, int pageSize = 15)
        {
            IProjectManager projectManager = new ProjectManager();
            if (string.IsNullOrEmpty(beginTime))
            {
                beginTime = "1990-01-01 00:00:00";
            }
            else
            {
                ViewBag.beginTime = beginTime;
            }
            if (string.IsNullOrEmpty(endTime))
            {
                endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                ViewBag.endTime = endTime;
            }
            if (string.IsNullOrEmpty(sectionSelect))
            {
                sectionSelect = "85";
            }
            var datas = await projectManager.GetStrainDatas(long.Parse(sectionSelect), Convert.ToDateTime(beginTime), Convert.ToDateTime(endTime), pageIndex - 1, pageSize);
            var count = await projectManager.GetStrainDatasCount(long.Parse(sectionSelect), Convert.ToDateTime(beginTime), Convert.ToDateTime(endTime));
            if (Request.IsAjaxRequest())
            {
                return PartialView("_StrainTableData", new PagedList<StrainDto>(datas, pageIndex, pageSize, count));
            }
            return View(new PagedList<StrainDto>(datas, pageIndex, pageSize, count));
        }

        public async Task<ActionResult> StressListBySection(string beginTime, string endTime, string sectionSelect, int pageIndex = 1, int pageSize = 15)
        {
            IProjectManager projectManager = new ProjectManager();
            if (string.IsNullOrEmpty(beginTime))
            {
                beginTime = "1990-01-01 00:00:00";
            }
            else
            {
                ViewBag.beginTime = beginTime;
            }
            if (string.IsNullOrEmpty(endTime))
            {
                endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                ViewBag.endTime = endTime;
            }
            if (string.IsNullOrEmpty(sectionSelect))
            {
                sectionSelect = "85";
            }
            var datas = await projectManager.GetStressDatas(long.Parse(sectionSelect), Convert.ToDateTime(beginTime), Convert.ToDateTime(endTime), pageIndex - 1, pageSize);
            var count = await projectManager.GetStressDatasCount(long.Parse(sectionSelect), Convert.ToDateTime(beginTime), Convert.ToDateTime(endTime));
            if (Request.IsAjaxRequest())
            {
                return PartialView("_StressTableData", new PagedList<StressDto>(datas, pageIndex, pageSize, count));
            }
            return View(new PagedList<StressDto>(datas, pageIndex, pageSize, count));
        }

        public async Task<ActionResult> SteelStrainListBySection(string beginTime, string endTime, string sectionSelect, int pageIndex = 1, int pageSize = 15)
        {
            IProjectManager projectManager = new ProjectManager();
            if (string.IsNullOrEmpty(beginTime))
            {
                beginTime = "1990-01-01 00:00:00";
            }
            else
            {
                ViewBag.beginTime = beginTime;
            }
            if (string.IsNullOrEmpty(endTime))
            {
                endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                ViewBag.endTime = endTime;
            }
            if (string.IsNullOrEmpty(sectionSelect))
            {
                sectionSelect = "85";
            }
            var datas = await projectManager.GetSteelStrainDatas(long.Parse(sectionSelect), Convert.ToDateTime(beginTime), Convert.ToDateTime(endTime), pageIndex - 1, pageSize);
            var count = await projectManager.GetSteelStrainDatasCount(long.Parse(sectionSelect), Convert.ToDateTime(beginTime), Convert.ToDateTime(endTime));
            if (Request.IsAjaxRequest())
            {
                return PartialView("_SteelTableData", new PagedList<SteelStressDto>(datas, pageIndex, pageSize, count));
            }
            return View(new PagedList<SteelStressDto>(datas, pageIndex, pageSize, count));
        }

        [HttpGet]
        public async Task<ActionResult> GetHistoryData(string beginTime, string endTime, string sectionId, string factor)
        {
            IProjectManager projectManager = new ProjectManager();
            if (string.IsNullOrEmpty(beginTime))
            {
                beginTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
                if (factor == "4")
                {
                    beginTime = DateTime.Now.AddHours(-3).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            if (string.IsNullOrEmpty(endTime))
            {
                endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            try
            {
                if (factor == "2")// 应变
                {
                    var datas = await projectManager.GetStrainDatas(long.Parse(sectionId), Convert.ToDateTime(beginTime), Convert.ToDateTime(endTime));
                    if (datas == null)
                    {
                        return Json("", JsonRequestBehavior.AllowGet);
                    }
                    return new JsonResult() { Data = datas, MaxJsonLength = Int32.MaxValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else if (factor == "3")// 位移
                {
                    var datas = await projectManager.GetOffsetDatas(long.Parse(sectionId), Convert.ToDateTime(beginTime), Convert.ToDateTime(endTime));
                    if (datas == null)
                    {
                        return Json("", JsonRequestBehavior.AllowGet);
                    }
                    return new JsonResult() { Data = datas, MaxJsonLength = Int32.MaxValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else if (factor == "4")// 振动
                {
                    var datas = await projectManager.GetVibrationDatas(long.Parse(sectionId), Convert.ToDateTime(beginTime), Convert.ToDateTime(endTime));
                    if (datas == null)
                    {
                        return Json("", JsonRequestBehavior.AllowGet);
                    }
                    return new JsonResult() { Data = datas, MaxJsonLength = Int32.MaxValue ,JsonRequestBehavior = JsonRequestBehavior.AllowGet};
                }
                else if (factor == "5")// 压力
                {
                    var datas = await projectManager.GetStressDatas(long.Parse(sectionId), Convert.ToDateTime(beginTime), Convert.ToDateTime(endTime));
                    if (datas == null)
                    {
                        return Json("", JsonRequestBehavior.AllowGet);
                    }
                    return new JsonResult() { Data = datas, MaxJsonLength = Int32.MaxValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else if (factor == "6")// 钢筋应力
                {
                    var datas = await projectManager.GetSteelStrainDatas(long.Parse(sectionId), Convert.ToDateTime(beginTime), Convert.ToDateTime(endTime));
                    if (datas == null)
                    {
                        return Json("", JsonRequestBehavior.AllowGet);
                    }
                    return new JsonResult() { Data = datas, MaxJsonLength = Int32.MaxValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetLatestDatas(string sectionId, string factor, int num = 20)
        {
            IProjectManager projectManager = new ProjectManager();
            if (factor == "2")// 应变
            {
                var datas = await projectManager.GetLatestStrains(long.Parse(sectionId), num, false);
                if (datas == null)
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                string jsonTxt = JsonConvert.SerializeObject(datas);
                return Json(datas, JsonRequestBehavior.AllowGet);
            }
            else if (factor == "3")// 位移
            {
                var datas = await projectManager.GetLatestOffsets(long.Parse(sectionId), num, false);
                if (datas == null)
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                string jsonTxt = JsonConvert.SerializeObject(datas);
                return Json(datas, JsonRequestBehavior.AllowGet);
            }
            else if (factor == "4")// 振动
            {
                var datas = await projectManager.GetLatestVibrations(long.Parse(sectionId), num, false);
                if (datas == null)
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                string jsonTxt = JsonConvert.SerializeObject(datas);
                return Json(datas, JsonRequestBehavior.AllowGet);
            }
            else if (factor == "5")// 压力
            {
                var datas = await projectManager.GetLatestStresses(long.Parse(sectionId), num, false);
                if (datas == null)
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                string jsonTxt = JsonConvert.SerializeObject(datas);
                return Json(datas, JsonRequestBehavior.AllowGet);
            }
            else if (factor == "6")// 钢筋应力
            {
                var datas = await projectManager.GetLatestSteelStrains(long.Parse(sectionId), num, false);
                if (datas == null)
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                string jsonTxt = JsonConvert.SerializeObject(datas);
                return Json(datas, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetLatestAlarmLogs(int time)
        {
            IProjectManager projectManager = new ProjectManager();
            try
            {
                var datas = await projectManager.GetAlarmLogs(0 - time);
                if (datas == null)
                    return Json("", JsonRequestBehavior.AllowGet);
                string jsonTxt = JsonConvert.SerializeObject(datas);
                return Json(datas, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> SetValue(long secId,string[] senNums,string type)
        {
            IProjectManager projectManager = new ProjectManager();
            try
            {
                long uid = long.Parse(Session["userId"].ToString());
                if(string.IsNullOrEmpty(type))
                    return Json(new { res = "err" });
                await projectManager.SetValue(secId, senNums,type,uid);
                return Json(new { res = "ok" });
            }
            catch
            {
                return Json(new { res = "err" });
            }
        }
        [HttpPost]
        public async Task<ActionResult> SetAllValue(long engId,string shaft,string type)
        {
            IProjectManager projectManager = new ProjectManager();
            try
            {
                long uid = long.Parse(Session["userId"].ToString());
                await projectManager.SetAllValue(engId,shaft,type, uid);
                return Json(new { res = "ok" });
            }
            catch
            {
                return Json(new { res = "err" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> ChangeSensorIsAlarm(long id,bool alarm)
        {
            IProjectManager projectManager = new ProjectManager();
            try
            {
                long uid = long.Parse(Session["userId"].ToString());
                await projectManager.ChangeSensorAlarm(id, alarm,uid);
                return Json(new { res = "ok" });
            }
            catch
            {
                return Json(new { res = "err" });
            }
        }
        public async Task<ActionResult> EngineeringSelect()
        {
            IProjectManager projectManager = new ProjectManager();
            var datas = await projectManager.GetEngineeringSites();
            return PartialView(datas);
        }
        public async Task<ActionResult> EngineeringSelect2()
        {
            IProjectManager projectManager = new ProjectManager();
            var datas = await projectManager.GetEngineeringSites();
            datas.Insert(0, new EngineeringDto() { Name = "全部" });
            return PartialView(datas);
        }
        public async Task<ActionResult> SectionSelect(long engId, string shaft)
        {
            IProjectManager projectManager = new ProjectManager();
            var datas = await projectManager.GetSectionsByEngId(engId, shaft);
            return PartialView(datas);
        }
        public async Task<ActionResult> SectionSelect2(long engId, string shaft)
        {
            IProjectManager projectManager = new ProjectManager();
            var datas = await projectManager.GetSectionsByEngId(engId, shaft);
            datas.Insert(0, new SectionDto() { SectionNum = "全部" });
            return PartialView(datas);
        }

        public ActionResult FastReport()
        {
            return View();
        }


        public ActionResult VibraFR()
        {
            try
            {
                RegisteredObjects.AddConnection(typeof(MySQLCon.MySqlDataConnection));
                //Report report = new Report();
                //report.Load(Server.MapPath("~/FastReport/爆破震速监测报表2.frx"));
                //report.Report.SetParameterValue("FR中的参数名", 123);
                //PDFExport pdf = new PDFExport();
                //report.Export(pdf, "E:/123.pdf"); 
                WebReport webReport = new WebReport();
                string shaft = Request["shaftSelect"];
                if (Request["factorSelect"] == "ZS")
                {
                    if (Request["zsSelect"] == "day")
                    {
                        string path = Server.MapPath("~/FastReport/爆破震速监测报表.frx");
                        webReport.Report.Load(path);
                        string zsTime = "";
                        if (string.IsNullOrEmpty(Request["zsTime"]))
                        {
                            zsTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                            zsTime = Request["zsTime"];
                        DateTime time = Convert.ToDateTime(zsTime);
                        webReport.Report.SetParameterValue("shaft", shaft);
                        webReport.Report.SetParameterValue("zsTime", time.AddMinutes(-2).ToString("yyyy-MM-dd HH:mm:ss"));
                        webReport.Report.SetParameterValue("zsTime2", time.AddMinutes(2).ToString("yyyy-MM-dd HH:mm:ss"));
                        webReport.Report.SetParameterValue("time", zsTime);
                    }
                    else
                    {
                        string path = Server.MapPath("~/FastReport/爆破震速监测报表2.frx");
                        webReport.Report.Load(path);
                        string spantime = "";
                        if (string.IsNullOrEmpty(Request["spanTime"]))
                        {
                            return Json(new { res = "time" });
                        }
                        else
                            spantime = Request["spanTime"];
                        string[] date1 = spantime.Split('-');
                        DateTime beginTime = Convert.ToDateTime(date1[0]);
                        DateTime endTime = Convert.ToDateTime(date1[1]);
                        webReport.Report.SetParameterValue("shaft", shaft);
                        webReport.Report.SetParameterValue("zsTime", beginTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        webReport.Report.SetParameterValue("zsTime2", endTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    IProjectManager projectManager = new ProjectManager();
                    // 获取正洞的振动预警值
                    decimal value = projectManager.GetOneAlarmValue(3);
                    webReport.Report.SetParameterValue("value", value);
                }
                else 
                {
                    if (Request["factorSelect"] == "WY")
                    {
                        string path = Server.MapPath("~/FastReport/衬砌位移监测日报表.frx");
                        webReport.Report.Load(path);
                    }
                    else
                    {
                        string path = Server.MapPath("~/FastReport/衬砌应变监测日报表.frx");
                        webReport.Report.Load(path);
                    }
                    webReport.Report.SetParameterValue("shaft", shaft);
                    webReport.Report.SetParameterValue("day", Request["daySelect"]);
                    if (Request["daySelect"] == "day")
                    {
                        string time= string.IsNullOrEmpty(Request["dayTime"])? DateTime.Now.ToString("yyyy-MM-dd"): Request["dayTime"];
                        DateTime date = Convert.ToDateTime(time);
                        if (date == DateTime.Now.Date)
                        {
                            date = DateTime.Now;
                            webReport.Report.SetParameterValue("nowEndTime", date.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        else
                        {
                            date = Convert.ToDateTime(time + " 18:00:00");
                            webReport.Report.SetParameterValue("nowEndTime", date.ToString("yyyy-MM-dd")+" 18:00:00");
                        }
                        webReport.Report.SetParameterValue("nowBeginTime", date.AddDays(-1).ToString("yyyy-MM-dd") + " 18:00:00");
                        webReport.Report.SetParameterValue("lastEndTime", date.AddDays(-1).ToString("yyyy-MM-dd")+" 18:00:00");
                        webReport.Report.SetParameterValue("lastBeginTime", date.AddDays(-2).ToString("yyyy-MM-dd") + " 18:00:00");
                    }
                    else 
                    {
                        string date = "";
                        if (string.IsNullOrEmpty(Request["spanTime"]))
                        {
                            return Json(new { res = "time" });
                        }
                        else
                            date = Request["spanTime"];
                        string[] date1 = date.Split('-');
                        DateTime beginTime = Convert.ToDateTime(date1[0]);
                        DateTime endTime = Convert.ToDateTime(date1[1]);
                        //DateTime time = DateTime.Now.AddDays(6 - Convert.ToInt32(DateTime.Now.DayOfWeek));
                        webReport.Report.SetParameterValue("nowBeginTime", endTime.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss"));
                        webReport.Report.SetParameterValue("nowEndTime", endTime.ToString("yyyy-MM-dd") + " 18:00:00");
                        webReport.Report.SetParameterValue("lastEndTime", beginTime.ToString("yyyy-MM-dd") + " 18:00:00");
                        webReport.Report.SetParameterValue("lastBeginTime", beginTime.AddDays(-1).ToString("yyyy-MM-dd") + " 18:00:00");
                    }
                }
                webReport.Width = Unit.Percentage(100);
                webReport.Height = Unit.Percentage(100);
                ViewBag.WebReport = webReport;

                return PartialView("VibraFR");
            }
            catch(Exception e)
            {
                return Json(new { res = "error" });
            }
        }
    }
}