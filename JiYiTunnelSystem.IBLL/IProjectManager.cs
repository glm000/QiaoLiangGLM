using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiYiTunnelSystem.Dto;

namespace JiYiTunnelSystem.IBLL
{
    public interface IProjectManager
    {
        Task CreateData(List<ReceiveDataDto> dataDtos);
        Task<List<EngineeringDto>> GetEngineeringSites();
        decimal GetOneAlarmValue(long engId);

        Task<List<SectionDto>> GetAllSections(string shaft);
        Task<List<SectionDto>> GetAllSections(string shaft, int pageIndex, int pageSize);
        Task<List<SectionDto>> GetSectionsByEngId(long engId, string shaft);
        Task<List<SectionDto>> GetSectionsByEngId(long engId, string shaft, int pageIndex, int pageSize);
        Task<SectionDto> GetOneSection(long sectionId);

        Task<List<LogDto>> GetLogs(DateTime beginTime, DateTime endTime, int pageIndex, int pageSize, bool asc,long uid);
        Task<int> GetLogCount(DateTime beginTime, DateTime endTime,long uid);
        Task RemoveLog(long[] ids);

        Task CreateSensor(long sectionId, string sensorType, string sensorNum, decimal? initialValue, string comment,long uid);
        Task EditSensor(long id, long sectionId, string sensorNum, string sensorType, decimal? initialValue, string comment, long userId);
        Task ChangeSensorAlarm(long id, bool alarm,long uid);
        Task DeleteSensor(long id, long userId);
        Task<SensorDto> GetOneSensor(long sensorId);
        Task<List<SensorDto>> GetAllSensors(string shaft, int pageIndex, int pageSize);
        Task<List<SensorDto>> GetSensors(string shaft, long? EngId, long? sectionId, string type);
        Task<List<SensorDto>> GetSensors(string shaft, long? EngId, long? sectionId, string type, int pageIndex, int pageSize);
        Task<int> GetSensorCount(string shaft, long? EngId, long? sectionId, string type);
        Task<int> GetSensorOnLineCount(string shaft, long? EngId, long? sectionId, string type);
        Task SetReferenceValue(string[] secIds, string[] references1, string[] references2, string[] references3, string[] references4, string type);
        Task SetReferenceValue(ReceiveDataDto receive);
        Task SetReferenceValue(long secId, decimal[] refs, int[] index, string type);
        Task<List<decimal?>> GetReferenceValue(long secId, string type);
        Task SetValue(long secId, string[] senNums,string type,long uid);
        Task SetAllValue(long engId,string shaft, string type, long uid);

        Task<List<AlarmLogDto>> GetAlarmLogs(string shaft, long? EngId, long? sectionId, string type, 
            DateTime beginTime, DateTime endTime, int pageIndex, int pageSize, bool asc = true);
        Task<List<AlarmLogDto>> GetAlarmLogs(int timespan);
        Task<AlarmSettingDto> GetAlarmValue();
        Task EditAlarmValue(decimal offset1, decimal offset2, decimal strain1, decimal strain2, decimal stress, decimal vibraYJ, decimal vibraZD, decimal vibraLJ);
        Task<int> GetAlarmLogCount(string shaft, long? EngId, long? sectionId, string type,
            DateTime beginTime, DateTime endTime);

        Task<List<VibrationDto>> GetLatestVibrations(long sectionId, int num, bool asc = true);
        Task<List<VibrationDto>> GetVibrationDatas(long sectionId, DateTime beginTime, DateTime endTime, bool asc = true);
        Task<List<VibrationDto>> GetVibrationDatas(long sectionId, DateTime beginTime, DateTime endTime, int pageIndex, int pageSize,bool asc=true);
        Task<int> GetVibrationDatasCount(long sectionId, DateTime beginTime, DateTime endTime);

        Task<List<OffsetDto>> GetLatestOffsets(long sectionId, int num, bool asc = true);
        Task<List<OffsetDto>> GetOffsetDatas(long sectionId, DateTime beginTime, DateTime endTime, bool asc = true);
        Task<List<OffsetDto>> GetOffsetDatas(long sectionId, DateTime beginTime, DateTime endTime, int pageIndex, int pageSize, bool asc = true);
        Task<int> GetOffsetDatasCount(long sectionId, DateTime beginTime, DateTime endTime);

        Task<List<StrainDto>> GetLatestStrains(long sectionId, int num, bool asc = true);
        Task<List<StrainDto>> GetStrainDatas(long sectionId, DateTime beginTime, DateTime endTime, bool asc = true);
        Task<List<StrainDto>> GetStrainDatas(long sectionId, DateTime beginTime, DateTime endTime, int pageIndex, int pageSize, bool asc = true);
        Task<int> GetStrainDatasCount(long sectionId, DateTime beginTime, DateTime endTime);

        Task<List<StressDto>> GetLatestStresses(long sectionId, int num, bool asc = true);
        Task<List<StressDto>> GetStressDatas(long sectionId, DateTime beginTime, DateTime endTime, bool asc = true);
        Task<List<StressDto>> GetStressDatas(long sectionId, DateTime beginTime, DateTime endTime, int pageIndex, int pageSize, bool asc = true);
        Task<int> GetStressDatasCount(long sectionId, DateTime beginTime, DateTime endTime);

        Task<List<SteelStressDto>> GetLatestSteelStrains(long sectionId, int num, bool asc = true);
        Task<List<SteelStressDto>> GetSteelStrainDatas(long sectionId, DateTime beginTime, DateTime endTime, bool asc = true);
        Task<List<SteelStressDto>> GetSteelStrainDatas(long sectionId, DateTime beginTime, DateTime endTime, int pageIndex, int pageSize, bool asc = true);
        Task<int> GetSteelStrainDatasCount(long sectionId, DateTime beginTime, DateTime endTime);
    }
}
