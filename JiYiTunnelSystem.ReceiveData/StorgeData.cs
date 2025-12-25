using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JiYiTunnelSystem.BLL;
using JiYiTunnelSystem.IBLL;
using JiYiTunnelSystem.Dto;

namespace JiYiTunnelSystem.ReceiveData
{
    class StorgeData
    {
        static List<string> wy4_Id = new List<string>();
        static List<string> wy44_1 = new List<string>();
        static List<string> wy41_2 = new List<string>();
        static List<string> wy42_3 = new List<string>();
        static List<string> wy43_4 = new List<string>();

        static List<string> wy2_Id = new List<string>();
        static List<string> wy21_1 = new List<string>();
        static List<string> wy24_2 = new List<string>();
        static List<string> wy22_3 = new List<string>();
        static List<string> wy23_4 = new List<string>();

        // 是否保存基准值
        static bool wy4ZDRef = false;
        static bool wy4XJRef = false;
        static bool wy2ZDRef = false;
        static bool wy2XJRef = false;

        static List<string> yb4_Id = new List<string>();
        static List<string> yb44_1 = new List<string>();
        static List<string> yb41_2 = new List<string>();
        static List<string> yb42_3 = new List<string>();
        static List<string> yb43_4 = new List<string>();

        static List<string> yb2_Id = new List<string>();
        static List<string> yb24_1 = new List<string>();
        static List<string> yb21_2 = new List<string>();
        static List<string> yb22_3 = new List<string>();
        static List<string> yb23_4 = new List<string>();

        // 是否保存基准值
        static bool yb4ZDRef = false;
        static bool yb4XJRef = false;
        static bool yb2ZDRef = false;
        static bool yb2XJRef = false;

        static List<string> ya2 = new List<string>();
        static List<string> gj2 = new List<string>();
        static List<string> ya4 = new List<string>();
        static List<string> gj4 = new List<string>();
        public static async Task<bool> Storge(string data)
        {
            try
            {
                List<ReceiveDataDto> jsonArray = JsonConvert.DeserializeObject<List<ReceiveDataDto>>(data);
                IProjectManager projectManager = new ProjectManager();
                await projectManager.CreateData(jsonArray);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private static void AddString(List<string> str,YBorWYDto json, decimal?[] values,double K)
        {
            if ((double)json.D1 != 0.0)
            {
                str.Add((K*((double)json.D1-(double)values[0])).ToString("0.000"));
            }
            if ((double)json.D2 != 0.0)
            {
                str.Add((K * ((double)json.D2 - (double)values[1])).ToString("0.000"));
            }
            if ((double)json.D3 != 0.0)
            {
                str.Add((K * ((double)json.D3 - (double)values[2])).ToString("0.000"));
            }
            if ((double)json.D4 != 0.0)
            {
                str.Add((K * ((double)json.D4 - (double)values[3])).ToString("0.000"));
            }
            if ((double)json.D5 != 0.0)
            {
                str.Add((K * ((double)json.D5 - (double)values[4])).ToString("0.000"));
            }
            if ((double)json.D6 != 0.0)
            {
                str.Add((K * ((double)json.D6 - (double)values[5])).ToString("0.000"));
            }
            if ((double)json.D7 != 0.0)
            {
                str.Add((K * ((double)json.D7 - (double)values[6])).ToString("0.000"));
            }
            if ((double)json.D8 != 0.0)
            {
                str.Add((K * ((double)json.D8 - (double)values[7])).ToString("0.000"));
            }
            if ((double)json.D9 != 0.0)
            {
                str.Add((K * ((double)json.D9 - (double)values[8])).ToString("0.000"));
            }
            if ((double)json.D10 != 0.0)
            {
                str.Add((K * ((double)json.D10 - (double)values[9])).ToString("0.000"));
            }
            if ((double)json.D11 != 0.0)
            {
                str.Add((K * ((double)json.D11 - (double)values[10])).ToString("0.000"));
            }
            if ((double)json.D12 != 0.0)
            {
                str.Add((K * ((double)json.D12 - (double)values[11])).ToString("0.000"));
            }
            if ((double)json.D13 != 0.0)
            {
                str.Add((K * ((double)json.D13 - (double)values[12])).ToString("0.000"));
            }
            if ((double)json.D14 != 0.0)
            {
                str.Add((K * ((double)json.D14 - (double)values[13])).ToString("0.000"));
            }
            if ((double)json.D15 != 0.0)
            {
                str.Add((K * ((double)json.D15 - (double)values[14])).ToString("0.000"));
            }
            if ((double)json.D16 != 0.0)
            {
                str.Add((K * ((double)json.D16 - (double)values[14])).ToString("0.000"));
            }
        }

        public static async Task<bool> StorgeYBorWY(string data)
        {
            IProjectManager projectManager = new ProjectManager();
            try
            {
                List<YBorWYDto> jsonArray = JsonConvert.DeserializeObject<List<YBorWYDto>>(data);
                List<ReceiveDataDto> dataDtos = new List<ReceiveDataDto>();
                foreach(var json in jsonArray)
                {
                    if (json.Type == "WY")
                    {
                        if (json.Id == 1)// 4号竖井
                        {// 正洞的截面Id从31-58，共28个
                            if (json.ID2 == 1)
                            {
                                wy4_Id.Clear();
                                wy44_1.Clear();
                                wy41_2.Clear();
                                wy42_3.Clear();
                                wy43_4.Clear();

                                for (int i = 31; i < 59; i++)
                                {
                                    wy4_Id.Add(i.ToString());
                                }

                                wy44_1.Add(json.D1.ToString());
                                wy44_1.Add(json.D2.ToString());
                                wy44_1.Add(json.D3.ToString());
                                wy44_1.Add(json.D4.ToString());
                                wy44_1.Add(json.D5.ToString());
                                wy44_1.Add(json.D6.ToString());
                                wy44_1.Add(json.D7.ToString());
                                wy44_1.Add(json.D7.ToString());// 8没装
                                wy44_1.Add(json.D9.ToString());
                            }
                            else if (json.ID2 > 9 && json.ID2 < 26 && wy4_Id.Count > 0)
                            {
                                wy44_1.Add(json.D1.ToString());
                                wy44_1.Add(json.D2.ToString());
                                wy44_1.Add(json.D3.ToString());
                                wy44_1.Add(json.D4.ToString());
                            }
                            else if (json.ID2 == 26 && wy4_Id.Count > 0)
                            {
                                wy44_1.Add(json.D1.ToString());
                                wy44_1.Add(json.D2.ToString());
                                wy44_1.Add(json.D3.ToString());
                            }
                            else if (json.ID2 == 30 && wy4_Id.Count > 0)
                            {
                                wy41_2.Add(json.D2.ToString());
                                wy41_2.Add(json.D3.ToString());
                                wy41_2.Add(json.D4.ToString());
                            }
                            else if (json.ID2 > 30 && json.ID2 < 58 && wy4_Id.Count > 0)
                            {
                                wy41_2.Add(json.D1.ToString());
                                wy41_2.Add(json.D2.ToString());
                                wy41_2.Add(json.D3.ToString());
                                wy41_2.Add(json.D4.ToString());
                            }
                            else if (json.ID2 == 58 && wy4_Id.Count > 0)
                            {
                                wy41_2.Add(json.D1.ToString());
                                wy42_3.Add(json.D4.ToString());
                            }
                            else if (json.ID2 > 58 && json.ID2 < 86 && wy4_Id.Count > 0)
                            {
                                wy42_3.Add(json.D1.ToString());
                                wy42_3.Add(json.D2.ToString());
                                wy42_3.Add(json.D3.ToString());
                                wy42_3.Add(json.D4.ToString());
                            }
                            else if (json.ID2 == 86 && wy4_Id.Count > 0)
                            {
                                wy42_3.Add(json.D1.ToString());
                                wy42_3.Add(json.D2.ToString());
                                wy42_3.Add(json.D3.ToString());
                            }
                            else if (json.ID2 == 90 && wy4_Id.Count > 0)
                            {
                                wy43_4.Add(json.D2.ToString());
                                wy43_4.Add(json.D3.ToString());
                                wy43_4.Add(json.D4.ToString());
                            }
                            else if (json.ID2 > 90 && json.ID2 < 118 && wy4_Id.Count > 0)
                            {
                                if (json.ID2 == 98)
                                {
                                    wy43_4.Add(json.D2.ToString());// 98没装
                                    wy43_4.Add(json.D2.ToString());
                                    wy43_4.Add(json.D3.ToString());
                                    wy43_4.Add(json.D4.ToString());
                                }
                                else
                                {
                                    wy43_4.Add(json.D1.ToString());
                                    wy43_4.Add(json.D2.ToString());
                                    wy43_4.Add(json.D3.ToString());
                                    wy43_4.Add(json.D4.ToString());
                                }
                            }
                            else if (json.ID2 == 118 && wy43_4.Count == 27)// 到此4号正洞的所有位移数据接收完了
                            {
                                wy43_4.Add(json.D1.ToString());

                                if (wy4ZDRef)
                                {
                                    await projectManager.SetReferenceValue(wy4_Id.ToArray(), wy41_2.ToArray(), wy42_3.ToArray(), wy43_4.ToArray(), wy44_1.ToArray(), "WY");
                                    wy4ZDRef = false;
                                    wy4_Id.Clear();
                                    wy44_1.Clear();
                                    wy41_2.Clear();
                                    wy42_3.Clear();
                                    wy43_4.Clear();
                                    return true;
                                }

                                string wy4 = "[";
                                for (int i = 0; i < 28; i++)
                                {
                                    try
                                    {
                                        List<decimal> values = new List<decimal>();
                                        List<int> indexs = new List<int>();
                                        var wy4ZDValue = await projectManager.GetReferenceValue(long.Parse(wy4_Id[i]), "WY");
                                        wy4 += "{Id:'" + wy4_Id[i] + "',Type:'WY',";
                                        if (wy4ZDValue[0] == 0 && decimal.Parse(wy41_2[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(wy41_2[i]));
                                            indexs.Add(0);
                                        }
                                        else if (decimal.Parse(wy41_2[i]) > 0)
                                            wy4 += "D1:'" + (decimal.Parse(wy41_2[i]) - wy4ZDValue[0]).ToString() + "',";
                                        if (wy4ZDValue[1] == 0 && decimal.Parse(wy42_3[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(wy42_3[i]));
                                            indexs.Add(1);
                                        }
                                        else if (decimal.Parse(wy42_3[i]) > 0)
                                            wy4 += "D2:'" + (decimal.Parse(wy42_3[i]) - wy4ZDValue[1]).ToString() + "',";
                                        if (wy4ZDValue[2] == 0 && decimal.Parse(wy43_4[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(wy43_4[i]));
                                            indexs.Add(2);
                                        }
                                        else if (decimal.Parse(wy43_4[i]) > 0)
                                            wy4 += "D3:'" + (decimal.Parse(wy43_4[i]) - wy4ZDValue[2]).ToString() + "',";
                                        if (wy4ZDValue[3] == 0 && decimal.Parse(wy44_1[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(wy44_1[i]));
                                            indexs.Add(3);
                                        }
                                        else if (decimal.Parse(wy44_1[i]) > 0)
                                            wy4 += "D4:'" + (decimal.Parse(wy44_1[i]) - wy4ZDValue[3]).ToString() + "'";
                                        wy4 += "},";
                                        if (values.Count > 0)
                                        {
                                            await projectManager.SetReferenceValue(long.Parse(wy4_Id[i]), values.ToArray(), indexs.ToArray(), "WY");
                                        }
                                    }
                                    catch { }
                                }
                                wy4 = wy4.Remove(wy4.LastIndexOf(","), 1);
                                wy4 += "]";
                                wy4_Id.Clear();
                                wy44_1.Clear();
                                wy41_2.Clear();
                                wy42_3.Clear();
                                wy43_4.Clear();
                                List<ReceiveDataDto> jsonWY4 = JsonConvert.DeserializeObject<List<ReceiveDataDto>>(wy4);
                                await projectManager.CreateData(jsonWY4);

                                // 4号斜井Id从71-82，共12个，4_1存02，1_2存01
                                wy44_1.Add(json.D4.ToString());
                                for (int i = 71; i < 83; i++)
                                {
                                    wy4_Id.Add(i.ToString());
                                }
                            }
                            else if (json.ID2 ==122 && wy4_Id.Count > 0)// 122
                            {
                                wy44_1.Add(json.D1.ToString());
                                wy44_1.Add(json.D2.ToString());
                                wy44_1.Add(json.D3.ToString());
                                wy44_1.Add(json.D4.ToString());
                            }
                            else if (json.ID2 == 126 && wy4_Id.Count > 0)
                            {
                                wy44_1.Add(json.D1.ToString());
                                wy41_2.Add(json.D2.ToString());
                                wy41_2.Add(json.D3.ToString());
                                wy41_2.Add(json.D4.ToString());
                            }
                            else if (json.ID2 ==130 && wy4_Id.Count > 0)//128-130
                            {
                                wy41_2.Add(json.D1.ToString());
                                wy41_2.Add(json.D2.ToString());
                            }
                            else if (json.ID2 == 132 && wy4_Id.Count > 0)
                            {
                                wy41_2.Add(json.D1.ToString());
                                wy44_1.Add(json.D2.ToString());
                            }
                            else if (json.ID2 > 132 && json.ID2 < 138 && wy4_Id.Count > 0)//134-136
                            {
                                wy44_1.Add(json.D1.ToString());
                                wy44_1.Add(json.D2.ToString());
                            }
                            else if(json.ID2 == 138 && wy4_Id.Count > 0)
                            {
                                wy44_1.Add(json.D1.ToString());
                                wy41_2.Add(json.D2.ToString());
                            }
                            else if (json.ID2 > 138 && json.ID2 < 144 && wy4_Id.Count > 0)//140-142
                            {
                                wy41_2.Add(json.D1.ToString());
                                wy41_2.Add(json.D2.ToString());
                            }
                            else if (json.ID2 == 144 && wy4_Id.Count > 0)// 142-152
                            {
                                wy41_2.Add(json.D1.ToString());

                                // 到此4号斜井的所有位移数据接收完了
                                if (wy4XJRef)
                                {
                                    await projectManager.SetReferenceValue(wy4_Id.ToArray(), wy41_2.ToArray(), wy42_3.ToArray(), wy43_4.ToArray(), wy44_1.ToArray(), "WY");
                                    wy4XJRef = false;
                                    wy4_Id.Clear();
                                    wy44_1.Clear();
                                    wy41_2.Clear();
                                    return true;
                                }

                                string wy4 = "[";
                                for (int i = 0; i < 12; i++)
                                {
                                    try
                                    {
                                        List<decimal> values = new List<decimal>();
                                        List<int> indexs = new List<int>();
                                        var wy4XJValue = await projectManager.GetReferenceValue(long.Parse(wy4_Id[i]), "WY");
                                        wy4 += "{Id:'" + wy4_Id[i] + "',Type:'WY',";
                                        if (wy4XJValue[0] == 0 && decimal.Parse(wy41_2[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(wy41_2[i]));
                                            indexs.Add(0);
                                        }
                                        else if (decimal.Parse(wy41_2[i]) > 0)
                                            wy4 += "D1:'" + (decimal.Parse(wy41_2[i]) - wy4XJValue[0]) + "',";
                                        if (wy4XJValue[1] == 0 && decimal.Parse(wy44_1[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(wy44_1[i]));
                                            indexs.Add(1);
                                        }
                                        else if (decimal.Parse(wy44_1[i]) > 0)
                                            wy4 += "D2:'" + (decimal.Parse(wy44_1[i]) - wy4XJValue[1]) + "'";
                                        wy4 += "},";
                                        if (values.Count > 0)
                                        {
                                            await projectManager.SetReferenceValue(long.Parse(wy4_Id[i]), values.ToArray(), indexs.ToArray(), "WY");
                                        }
                                    }
                                    catch { }
                                }
                                wy4 = wy4.Remove(wy4.LastIndexOf(","), 1);
                                wy4 += "]";
                                wy4_Id.Clear();
                                wy44_1.Clear();
                                wy41_2.Clear();
                                List<ReceiveDataDto> jsonWY4 = JsonConvert.DeserializeObject<List<ReceiveDataDto>>(wy4);
                                await projectManager.CreateData(jsonWY4);
                            }
                        }
                        else// Id:'2',2号竖井
                        {// 2号正洞Id从1-30，共30个
                            if (json.ID2 == 1)
                            {
                                wy2_Id.Clear();
                                wy21_1.Clear();
                                wy22_3.Clear();
                                wy23_4.Clear();
                                wy24_2.Clear();

                                for (int i = 1; i < 31; i++)
                                {
                                    wy2_Id.Add(i.ToString());
                                }

                                wy21_1.Add(json.D1.ToString());
                                wy21_1.Add(json.D2.ToString());
                                wy21_1.Add(json.D3.ToString());
                                wy21_1.Add(json.D4.ToString());
                                wy21_1.Add(json.D5.ToString());
                                wy21_1.Add(json.D6.ToString());
                                wy21_1.Add(json.D7.ToString());
                                wy21_1.Add(json.D8.ToString());
                                wy21_1.Add(json.D9.ToString());
                            }
                            else if (json.ID2 > 1 && json.ID2 < 30 && wy2_Id.Count > 0)// 14-26
                            {
                                wy21_1.Add(json.D1.ToString());
                                wy21_1.Add(json.D2.ToString());
                                wy21_1.Add(json.D3.ToString());
                                wy21_1.Add(json.D4.ToString());
                            }
                            else if (json.ID2 == 30 && wy2_Id.Count > 0)
                            {
                                wy21_1.Add(json.D1.ToString());
                                wy24_2.Add(json.D2.ToString());
                                wy24_2.Add(json.D3.ToString());
                                wy24_2.Add(json.D4.ToString());
                            }
                            else if (json.ID2 > 30 && json.ID2 < 58 && wy2_Id.Count > 0)// 34-54
                            {
                                if (json.ID2 == 34)
                                {
                                    wy24_2.Add(json.D1.ToString());
                                    wy24_2.Add(json.D2.ToString());
                                    wy24_2.Add(json.D3.ToString());
                                    wy24_2.Add(json.D3.ToString());// 37没装
                                }
                                else
                                {
                                    wy24_2.Add(json.D1.ToString());
                                    wy24_2.Add(json.D2.ToString());
                                    wy24_2.Add(json.D3.ToString());
                                    wy24_2.Add(json.D4.ToString());
                                }
                            }
                            else if (json.ID2 == 58 && wy2_Id.Count > 0)
                            {
                                // 58 59 60都没数据,存成61的
                                wy24_2.Add(json.D4.ToString());
                                wy24_2.Add(json.D4.ToString());
                                wy24_2.Add(json.D4.ToString());
                                wy22_3.Add(json.D4.ToString());
                            }
                            else if (json.ID2 > 58 && json.ID2 < 90 && wy2_Id.Count > 0)// 62-86
                            {
                                wy22_3.Add(json.D1.ToString());
                                wy22_3.Add(json.D2.ToString());
                                if(json.ID2 == 74)
                                {// 76 77没数据
                                    wy22_3.Add(json.D1.ToString());
                                    wy22_3.Add(json.D2.ToString());
                                }
                                else
                                {
                                    wy22_3.Add(json.D3.ToString());
                                    wy22_3.Add(json.D4.ToString());
                                }
                            }
                            else if (json.ID2 == 90 && wy2_Id.Count > 0)
                            {
                                wy22_3.Add(json.D1.ToString());
                                wy23_4.Add(json.D2.ToString());
                                wy23_4.Add(json.D3.ToString());
                                wy23_4.Add(json.D4.ToString());
                            }
                            else if (json.ID2 > 90 && json.ID2 < 118 && wy2_Id.Count > 0)// 94-114
                            {
                                if (json.ID2 == 94)
                                {
                                    wy23_4.Add(json.D1.ToString());
                                    wy23_4.Add(json.D2.ToString());
                                    wy23_4.Add(json.D3.ToString());
                                    wy23_4.Add(json.D3.ToString());// 97没装
                                }
                                else
                                {
                                    wy23_4.Add(json.D1.ToString());
                                    wy23_4.Add(json.D2.ToString());
                                    wy23_4.Add(json.D3.ToString());
                                    wy23_4.Add(json.D4.ToString());
                                }
                            }
                            else if (json.ID2 == 118 && wy23_4.Count == 27)
                            {
                                // 118 119 120没数据
                                wy23_4.Add(json.D4.ToString());
                                wy23_4.Add(json.D4.ToString());
                                wy23_4.Add(json.D4.ToString());// 到此2正洞的所有位移数据接收完了

                                if (wy2ZDRef)
                                {
                                    await projectManager.SetReferenceValue(wy2_Id.ToArray(), wy21_1.ToArray(), wy22_3.ToArray(), wy23_4.ToArray(), wy24_2.ToArray(), "WY");
                                    wy2ZDRef = false;
                                    wy2_Id.Clear();
                                    wy21_1.Clear();
                                    wy22_3.Clear();
                                    wy23_4.Clear();
                                    wy24_2.Clear();
                                    return true;
                                }

                                string wy2 = "[";
                                for (int i = 0; i < 30; i++)
                                {
                                    try
                                    {
                                        List<decimal> values = new List<decimal>();
                                        List<int> indexs = new List<int>();
                                        var wy2ZDValue = await projectManager.GetReferenceValue(long.Parse(wy2_Id[i]), "WY");
                                        wy2 += "{Id:'" + wy2_Id[i] + "',Type:'WY',";
                                        if (wy2ZDValue[0] == 0 && decimal.Parse(wy21_1[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(wy21_1[i]));
                                            indexs.Add(0);
                                        }
                                        else if (decimal.Parse(wy21_1[i]) > 0)
                                            wy2 += "D1:'" + (decimal.Parse(wy21_1[i]) - wy2ZDValue[0]) + "',";
                                        if (wy2ZDValue[1] == 0 && decimal.Parse(wy22_3[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(wy22_3[i]));
                                            indexs.Add(1);
                                        }
                                        else if (decimal.Parse(wy22_3[i]) > 0)
                                            wy2 += "D2:'" + (decimal.Parse(wy22_3[i]) - wy2ZDValue[1]) + "',";
                                        if (wy2ZDValue[2] == 0 && decimal.Parse(wy23_4[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(wy23_4[i]));
                                            indexs.Add(2);
                                        }
                                        else if (decimal.Parse(wy23_4[i]) > 0)
                                            wy2 += "D3:'" + (decimal.Parse(wy23_4[i]) - wy2ZDValue[2]) + "',";
                                        if (wy2ZDValue[3] == 0 && decimal.Parse(wy24_2[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(wy24_2[i]));
                                            indexs.Add(3);
                                        }
                                        else if (decimal.Parse(wy24_2[i]) > 0)
                                            wy2 += "D4:'" + (decimal.Parse(wy24_2[i]) - wy2ZDValue[3]) + "'";
                                        wy2 += "},";
                                        if (values.Count > 0)
                                        {
                                            await projectManager.SetReferenceValue(long.Parse(wy2_Id[i]), values.ToArray(), indexs.ToArray(), "WY");
                                        }
                                    }
                                    catch(Exception e)
                                    { }
                                }
                                wy2 = wy2.Remove(wy2.LastIndexOf(","), 1);
                                wy2 += "]";
                                wy2_Id.Clear();
                                wy21_1.Clear();
                                wy22_3.Clear();
                                wy23_4.Clear();
                                wy24_2.Clear();
                                List<ReceiveDataDto> jsonWY2 = JsonConvert.DeserializeObject<List<ReceiveDataDto>>(wy2);
                                await projectManager.CreateData(jsonWY2);

                                //从这里开始是2斜井的位移数据 4_2存01，1_1存02
                                wy21_1.Add(json.D4.ToString());

                                for (int i = 59; i < 71; i++)
                                {
                                    wy2_Id.Add(i.ToString());
                                }
                            }
                            else if (json.ID2 == 122 && wy2_Id.Count > 0)// 122
                            {
                                wy21_1.Add(json.D1.ToString());
                                wy21_1.Add(json.D2.ToString());
                                wy21_1.Add(json.D3.ToString());
                                wy21_1.Add(json.D4.ToString());
                            }
                            else if (json.ID2 == 126 && wy2_Id.Count > 0)
                            {
                                wy21_1.Add(json.D1.ToString());
                                wy24_2.Add(json.D2.ToString());
                                wy24_2.Add(json.D3.ToString());
                                wy24_2.Add(json.D4.ToString());
                            }
                            else if (json.ID2 == 130 && wy2_Id.Count > 0)
                            {
                                wy24_2.Add(json.D1.ToString());
                                wy24_2.Add(json.D2.ToString());
                            }
                            else if (json.ID2 == 132 && wy2_Id.Count > 0)
                            {
                                wy24_2.Add(json.D1.ToString());
                                wy21_1.Add(json.D2.ToString());
                            }
                            else if (json.ID2 > 132 && json.ID2 < 138 && wy2_Id.Count > 0)// 134-136
                            {
                                wy21_1.Add(json.D1.ToString());
                                wy21_1.Add(json.D2.ToString());
                            }
                            else if (json.ID2 == 138 && wy2_Id.Count > 0)
                            {
                                wy21_1.Add(json.D1.ToString());
                                wy24_2.Add(json.D2.ToString());
                            }
                            else if (json.ID2 > 138 && json.ID2 < 144 && wy2_Id.Count > 0)//140-142
                            {
                                wy24_2.Add(json.D1.ToString());
                                wy24_2.Add(json.D2.ToString());
                            }
                            else if (json.ID2 == 144 && wy24_2.Count == 11)
                            {
                                wy24_2.Add(json.D1.ToString());

                                if (wy2XJRef)
                                {
                                    await projectManager.SetReferenceValue(wy2_Id.ToArray(), wy24_2.ToArray(), wy22_3.ToArray(), wy23_4.ToArray(), wy21_1.ToArray(), "WY");
                                    wy2XJRef = false;
                                    wy2_Id.Clear();
                                    wy21_1.Clear();
                                    wy24_2.Clear();
                                    return true;
                                }

                                string wy2 = "[";
                                for(int i = 0; i < 12; i++)
                                {
                                    try
                                    {
                                        List<decimal> values = new List<decimal>();
                                        List<int> indexs = new List<int>();
                                        var wy2XJValue = await projectManager.GetReferenceValue(long.Parse(wy2_Id[i]), "WY");
                                        wy2 += "{Id:'" + wy2_Id[i] + "',Type:'WY',";
                                        if (wy2XJValue[0] == 0 && decimal.Parse(wy24_2[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(wy24_2[i]));
                                            indexs.Add(0);
                                        }
                                        else if (decimal.Parse(wy24_2[i]) > 0)
                                            wy2 += "D1:'" + (decimal.Parse(wy24_2[i]) - wy2XJValue[0]) + "',";
                                        if (wy2XJValue[1] == 0 && decimal.Parse(wy21_1[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(wy21_1[i]));
                                            indexs.Add(1);
                                        }
                                        else if (decimal.Parse(wy21_1[i]) > 0)
                                            wy2 += "D2:'" + (decimal.Parse(wy21_1[i]) - wy2XJValue[1]) + "'";
                                        wy2 += "},";
                                        if (values.Count > 0)
                                        {
                                            await projectManager.SetReferenceValue(long.Parse(wy2_Id[i]), values.ToArray(), indexs.ToArray(), "WY");
                                        }
                                    }
                                    catch { }
                                }
                                wy2 = wy2.Remove(wy2.LastIndexOf(","), 1);
                                wy2 += "]";
                                wy2_Id.Clear();
                                wy21_1.Clear();
                                wy24_2.Clear();
                                List<ReceiveDataDto> jsonWY2 = JsonConvert.DeserializeObject<List<ReceiveDataDto>>(wy2);
                                await projectManager.CreateData(jsonWY2);
                            }
                        }
                    }
                    else if (json.Type == "YB")
                    {
                        if (json.Id == 1)
                        {
                            if (json.ID2 == 1)
                            {
                                yb4_Id.Clear();
                                yb44_1.Clear();
                                yb41_2.Clear();
                                yb42_3.Clear();
                                yb43_4.Clear();
                                for (int i = 0; i < 16; i++)
                                {
                                    yb4_Id.Add((31 + i).ToString());
                                }
                                yb44_1.Add(json.D1.ToString());
                                yb41_2.Add(json.D2.ToString());
                                yb44_1.Add(json.D3.ToString());
                                yb41_2.Add(json.D4.ToString());
                                yb44_1.Add(json.D5.ToString());
                                yb41_2.Add(json.D6.ToString());
                                yb44_1.Add(json.D7.ToString());
                                yb41_2.Add(((json.D6 + json.D10) / 2).ToString());// 1-8数据乱跳
                                yb44_1.Add(json.D9.ToString());
                                yb41_2.Add(json.D10.ToString());
                                yb44_1.Add(json.D11.ToString());
                                yb41_2.Add(json.D12.ToString());
                                yb44_1.Add(json.D13.ToString());
                                yb41_2.Add(json.D14.ToString());
                            }
                            else if (json.ID2 == 2 &&yb44_1.Count==7)
                            {
                                for (int i = 0; i < 12; i++)
                                {
                                    yb4_Id.Add((47 + i).ToString());
                                }
                                yb44_1.Add(json.D1.ToString());
                                yb41_2.Add(json.D2.ToString());
                                yb44_1.Add(json.D3.ToString());
                                yb41_2.Add(((json.D2 + json.D6) / 2).ToString());// 2-4数据乱跳
                                yb44_1.Add(json.D5.ToString());
                                yb41_2.Add(json.D6.ToString());
                                yb44_1.Add(json.D7.ToString());
                                yb41_2.Add(((json.D7 + json.D6) / 2).ToString());// 2-8乱跳
                                yb44_1.Add(json.D9.ToString());
                                yb41_2.Add(((json.D11 + json.D12) / 2).ToString());// 2-10乱跳
                                yb44_1.Add(json.D11.ToString());
                                yb41_2.Add(json.D12.ToString());
                            }
                            else if (json.ID2 == 3 &&yb44_1.Count==13)
                            {
                                yb44_1.Add(json.D1.ToString());
                                yb41_2.Add(((json.D4 + json.D3) / 2).ToString());// 3-2乱跳
                                yb44_1.Add(json.D3.ToString());
                                yb41_2.Add(json.D4.ToString());
                                yb44_1.Add(json.D5.ToString());
                                yb41_2.Add(json.D6.ToString());
                                yb44_1.Add(json.D7.ToString());
                                yb41_2.Add(json.D8.ToString());
                                yb44_1.Add(json.D9.ToString());
                                yb41_2.Add(json.D10.ToString());
                                yb44_1.Add(json.D11.ToString());
                                yb41_2.Add(json.D12.ToString());
                                yb44_1.Add(json.D13.ToString());
                                yb41_2.Add(json.D14.ToString());
                            }
                            else if (json.ID2 == 4 &&yb44_1.Count==20)
                            {
                                yb44_1.Add(json.D1.ToString());
                                yb41_2.Add(json.D2.ToString());
                                yb44_1.Add(json.D3.ToString());
                                yb41_2.Add(json.D4.ToString());
                                yb44_1.Add(json.D5.ToString());
                                yb41_2.Add(json.D6.ToString());
                                yb44_1.Add(json.D7.ToString());
                                yb41_2.Add(json.D8.ToString());
                                yb44_1.Add(json.D9.ToString());
                                yb41_2.Add(json.D10.ToString());
                                yb44_1.Add(((json.D9 + json.D13) / 2).ToString());// 4-11乱跳
                                yb41_2.Add(json.D12.ToString());
                                yb44_1.Add(json.D13.ToString());
                                yb41_2.Add(((json.D12 + json.D16) / 2).ToString());//4-14乱跳
                                yb44_1.Add(json.D15.ToString());
                                yb41_2.Add(json.D16.ToString());
                            }
                            else if (json.ID2 == 5 && yb43_4.Count==0)
                            {
                                yb43_4.Add(json.D1.ToString());
                                yb42_3.Add(json.D2.ToString());
                                yb43_4.Add(json.D3.ToString());
                                yb42_3.Add(json.D4.ToString());
                                yb43_4.Add(json.D5.ToString());
                                yb42_3.Add(json.D6.ToString());
                                yb43_4.Add(json.D7.ToString());
                                yb42_3.Add(json.D8.ToString());
                                yb43_4.Add(json.D9.ToString());
                                yb42_3.Add(json.D10.ToString());
                                yb43_4.Add(json.D11.ToString());
                                yb42_3.Add(json.D12.ToString());
                                yb43_4.Add(json.D13.ToString());
                                yb42_3.Add(json.D14.ToString());
                            }
                            else if (json.ID2 == 6 && yb43_4.Count==7)
                            {
                                yb43_4.Add(json.D1.ToString());
                                yb42_3.Add(json.D2.ToString());
                                // 上面这两个没装
                                yb43_4.Add(json.D1.ToString());
                                yb42_3.Add(json.D2.ToString());
                                yb43_4.Add(json.D3.ToString());
                                yb42_3.Add(json.D4.ToString());
                                yb43_4.Add(((json.D3+json.D6)/2).ToString());// 5乱跳
                                yb42_3.Add(json.D6.ToString());
                                yb43_4.Add(((json.D8 + json.D6) / 2).ToString());// 7乱跳
                                yb42_3.Add(json.D8.ToString());
                                yb43_4.Add(json.D9.ToString());
                                yb42_3.Add(json.D10.ToString());
                            }
                            else if(json.ID2==7 && yb43_4.Count==13)
                            {
                                yb43_4.Add(json.D1.ToString());
                                yb42_3.Add(json.D2.ToString());
                                yb43_4.Add(json.D3.ToString());
                                yb42_3.Add(json.D4.ToString());
                                yb43_4.Add(json.D5.ToString());
                                yb42_3.Add(json.D6.ToString());
                                yb43_4.Add(json.D7.ToString());
                                yb42_3.Add(json.D8.ToString());
                                yb43_4.Add(json.D9.ToString());
                                yb42_3.Add(json.D10.ToString());
                                yb43_4.Add(json.D11.ToString());
                                yb42_3.Add(json.D12.ToString());
                                yb43_4.Add(json.D13.ToString());
                                yb42_3.Add(json.D14.ToString());
                            }
                            else if (json.ID2 == 8 && yb43_4.Count==20)
                            {
                                yb43_4.Add(json.D1.ToString());
                                yb42_3.Add(json.D2.ToString());
                                yb43_4.Add(json.D3.ToString());
                                yb42_3.Add(json.D4.ToString());
                                yb43_4.Add(json.D5.ToString());
                                yb42_3.Add(((json.D4 + json.D8) / 2).ToString());// 6乱跳
                                yb43_4.Add(json.D7.ToString());
                                yb42_3.Add(json.D8.ToString());
                                yb43_4.Add(json.D9.ToString());
                                yb42_3.Add(json.D10.ToString());
                                yb43_4.Add(json.D11.ToString());
                                yb42_3.Add(json.D12.ToString());
                                yb43_4.Add(json.D13.ToString());
                                yb42_3.Add(json.D14.ToString());
                                yb43_4.Add(json.D15.ToString());
                                yb42_3.Add(json.D16.ToString());
                                if (yb4ZDRef)
                                {
                                    await projectManager.SetReferenceValue(yb4_Id.ToArray(), yb44_1.ToArray(), yb41_2.ToArray(), yb42_3.ToArray(), yb43_4.ToArray(), "YB");
                                    yb4ZDRef = false;
                                    yb4_Id.Clear();
                                    yb44_1.Clear();
                                    yb41_2.Clear();
                                    yb42_3.Clear();
                                    yb43_4.Clear();
                                    return true;
                                }

                                string yb4 = "[";
                                for (int i = 0; i < 28; i++)
                                {
                                    try
                                    {
                                        List<decimal> values = new List<decimal>();
                                        List<int> indexs = new List<int>();
                                        var yb4ZDValue = await projectManager.GetReferenceValue(long.Parse(yb4_Id[i]), "YB");
                                        yb4 += "{Id:'" + yb4_Id[i] + "',Type:'YB',";
                                        if (yb4ZDValue[0] == 0 && decimal.Parse(yb44_1[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(yb44_1[i]));
                                            indexs.Add(0);
                                        }
                                        else if (decimal.Parse(yb44_1[i]) > 0)
                                            yb4 += "D1:'" + (decimal)0.37 * (decimal.Parse(yb44_1[i]) - yb4ZDValue[0]) + "',";
                                        if (yb4ZDValue[1] == 0 && decimal.Parse(yb41_2[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(yb41_2[i]));
                                            indexs.Add(1);
                                        }
                                        else if (decimal.Parse(yb41_2[i]) > 0)
                                            yb4 += "D2:'" + (decimal)0.37 * (decimal.Parse(yb41_2[i]) - yb4ZDValue[1]) + "',";
                                        if (yb4ZDValue[2] == 0 && decimal.Parse(yb42_3[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(yb42_3[i]));
                                            indexs.Add(2);
                                        }
                                        else if (decimal.Parse(yb42_3[i]) > 0)
                                            yb4 += "D3:'" + (decimal)0.37 * (decimal.Parse(yb42_3[i]) - yb4ZDValue[2]) + "',";
                                        if (yb4ZDValue[3] == 0 && decimal.Parse(yb43_4[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(yb43_4[i]));
                                            indexs.Add(3);
                                        }
                                        else if (decimal.Parse(yb43_4[i]) > 0)
                                            yb4 += "D4:'" + (decimal)0.37 * (decimal.Parse(yb43_4[i]) - yb4ZDValue[3]) + "'";
                                        yb4 += "},";
                                        if (values.Count > 0)
                                        {
                                            await projectManager.SetReferenceValue(long.Parse(yb4_Id[i]), values.ToArray(), indexs.ToArray(), "YB");
                                        }
                                    }
                                    catch { }
                                }
                                yb4 = yb4.Remove(yb4.LastIndexOf(","), 1);
                                yb4 += "]";
                                yb4_Id.Clear();
                                yb44_1.Clear();
                                yb41_2.Clear();
                                yb42_3.Clear();
                                yb43_4.Clear();
                                List<ReceiveDataDto> jsonYB4 = JsonConvert.DeserializeObject<List<ReceiveDataDto>>(yb4);
                                await projectManager.CreateData(jsonYB4);
                            }
                            else if (json.ID2 == 9)// 4号斜井，Id从71-82，12个
                            {
                                yb4_Id.Clear();
                                yb44_1.Clear();
                                yb41_2.Clear();
                                yb42_3.Clear();
                                yb43_4.Clear();
                                for (int i = 0; i < 12; i++)
                                {
                                    yb4_Id.Add((71 + i).ToString());
                                }

                                //4_1存02，1_2存01
                                yb44_1.Add(json.D1.ToString());
                                yb44_1.Add(json.D3.ToString());
                                yb44_1.Add(json.D5.ToString());
                                yb44_1.Add(json.D7.ToString());
                                yb44_1.Add(json.D9.ToString());
                                yb44_1.Add(json.D11.ToString());
                            }
                            else if (json.ID2 == 10 && yb41_2.Count == 0)
                            {
                                yb41_2.Add(json.D1.ToString());
                                yb41_2.Add(json.D3.ToString());
                                yb41_2.Add(json.D5.ToString());
                                yb41_2.Add(json.D7.ToString());
                                yb41_2.Add(json.D9.ToString());
                                yb41_2.Add(json.D11.ToString());
                            }
                            else if (json.ID2 == 11 && yb44_1.Count == 6)
                            {
                                yb41_2.Add(json.D1.ToString());
                                yb44_1.Add(json.D2.ToString());
                                yb41_2.Add(json.D3.ToString());
                                yb44_1.Add(json.D4.ToString());
                                yb41_2.Add(json.D5.ToString());
                                yb44_1.Add(json.D6.ToString());
                            }
                            else if (json.ID2 == 12 && yb44_1.Count == 9)
                            {
                                yb41_2.Add(json.D1.ToString());
                                yb44_1.Add(json.D2.ToString());
                                yb41_2.Add(json.D3.ToString());
                                yb44_1.Add(json.D4.ToString());
                                yb41_2.Add(json.D5.ToString());
                                yb44_1.Add(json.D6.ToString());

                                if (yb4XJRef)
                                {
                                    await projectManager.SetReferenceValue(yb4_Id.ToArray(), yb41_2.ToArray(), yb42_3.ToArray(), yb43_4.ToArray(), yb44_1.ToArray(), "YB");
                                    yb4XJRef = false;
                                    yb4_Id.Clear();
                                    yb44_1.Clear();
                                    yb41_2.Clear();
                                    return true;
                                }

                                string yb4 = "[";
                                for (int i = 0; i < 12; i++)
                                {
                                    try
                                    {
                                        List<decimal> values = new List<decimal>();
                                        List<int> indexs = new List<int>();
                                        var yb4XJValue = await projectManager.GetReferenceValue(long.Parse(yb4_Id[i]), "YB");
                                        yb4 += "{Id:'" + yb4_Id[i] + "',Type:'YB',";
                                        if (yb4XJValue[0] == 0 && decimal.Parse(yb41_2[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(yb41_2[i]));
                                            indexs.Add(0);
                                        }
                                        else if (decimal.Parse(yb41_2[i]) > 0)
                                            yb4 += "D1:'" + (decimal)0.37 * (decimal.Parse(yb41_2[i]) - yb4XJValue[0]) + "',";
                                        if (yb4XJValue[1] == 0 && decimal.Parse(yb44_1[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(yb44_1[i]));
                                            indexs.Add(1);
                                        }
                                        else if (decimal.Parse(yb44_1[i]) > 0)
                                            yb4 += "D2:'" + (decimal)0.37 * (decimal.Parse(yb44_1[i]) - yb4XJValue[1]) + "'";
                                        yb4 += "},";
                                        if (values.Count > 0)
                                        {
                                            await projectManager.SetReferenceValue(long.Parse(yb4_Id[i]), values.ToArray(), indexs.ToArray(), "YB");
                                        }
                                    }
                                    catch { }
                                }
                                yb4 = yb4.Remove(yb4.LastIndexOf(","), 1);
                                yb4 += "]";
                                yb4_Id.Clear();
                                yb44_1.Clear();
                                yb41_2.Clear();
                                List<ReceiveDataDto> jsonYB4 = JsonConvert.DeserializeObject<List<ReceiveDataDto>>(yb4);
                                await projectManager.CreateData(jsonYB4);
                            }
                        }
                        else
                        {
                            if (json.ID2 == 1)
                            {
                                yb2_Id.Clear();
                                yb24_1.Clear();
                                yb21_2.Clear();
                                yb22_3.Clear();
                                yb23_4.Clear();
                                for (int i = 0; i < 30; i++)
                                {
                                    yb2_Id.Add((1 + i).ToString());
                                }
                                yb24_1.Add(json.D1.ToString());
                                yb21_2.Add(json.D2.ToString());
                                yb24_1.Add(json.D3.ToString());
                                yb21_2.Add(json.D4.ToString());
                                yb24_1.Add(json.D5.ToString());
                                yb21_2.Add(json.D6.ToString());
                                yb24_1.Add(json.D7.ToString());
                                yb21_2.Add(json.D8.ToString());
                                yb24_1.Add(json.D9.ToString());
                                yb21_2.Add(json.D10.ToString());
                                yb24_1.Add(json.D11.ToString());
                                yb21_2.Add(json.D12.ToString());
                                yb24_1.Add(json.D13.ToString());
                                yb21_2.Add(json.D14.ToString());
                            }
                            else if (json.ID2 == 2 && yb23_4.Count == 0)
                            {
                                yb23_4.Add(((json.D14 + json.D15) / 2).ToString());// 2-16数据乱跳
                                yb22_3.Add(json.D15.ToString());
                                yb23_4.Add(json.D14.ToString());
                                yb22_3.Add(json.D13.ToString());
                                yb23_4.Add(json.D12.ToString());
                                yb22_3.Add(json.D11.ToString());
                                yb23_4.Add(json.D10.ToString());
                                yb22_3.Add(json.D9.ToString());
                                yb23_4.Add(((json.D10+json.D6)/2).ToString());// 2-8没数据，换成2-10和2-6的平均值
                                yb22_3.Add(json.D7.ToString());
                                yb23_4.Add(json.D6.ToString());
                                yb22_3.Add(json.D5.ToString());
                                yb23_4.Add(json.D4.ToString());
                                yb22_3.Add(json.D3.ToString());
                                yb23_4.Add(json.D2.ToString());
                                yb22_3.Add(json.D1.ToString());
                            }
                            else if (json.ID2 == 3 && yb24_1.Count==7)// 全没数据
                            {
                                //yb24_1.Add(json.D1.ToString());
                                //yb21_2.Add(json.D4.ToString());
                                //yb24_1.Add(json.D2.ToString());
                                //yb21_2.Add(json.D3.ToString());
                                //yb24_1.Add(json.D5.ToString());
                                //yb21_2.Add(json.D6.ToString());
                                //yb24_1.Add(json.D7.ToString());
                                //yb21_2.Add(json.D8.ToString());
                                //yb24_1.Add(json.D9.ToString());
                                //yb21_2.Add(json.D10.ToString());
                                //yb24_1.Add(json.D11.ToString());
                                //yb21_2.Add(json.D12.ToString());
                                //yb24_1.Add(json.D13.ToString());
                                //yb21_2.Add(json.D14.ToString());
                            }
                            else if (json.ID2 == 4 &&yb23_4.Count==8)
                            {
                                yb23_4.Add(json.D3.ToString());
                                yb22_3.Add(json.D4.ToString());
                                yb23_4.Add(json.D5.ToString());
                                yb22_3.Add(json.D6.ToString());
                                yb23_4.Add(json.D7.ToString());
                                yb22_3.Add(json.D8.ToString());
                                yb23_4.Add(json.D9.ToString());
                                yb22_3.Add(((json.D8 + json.D12) / 2).ToString());// 4-10没数据
                                yb23_4.Add(json.D11.ToString());
                                yb22_3.Add(json.D12.ToString());
                                yb23_4.Add(json.D13.ToString());
                                yb22_3.Add(json.D14.ToString());
                                yb23_4.Add(json.D15.ToString());
                                yb22_3.Add(json.D16.ToString());
                                
                                // 把3号箱数据用4替换
                                yb24_1.Add(((json.D5 + json.D6) / 2).ToString());
                                yb21_2.Add(((json.D5 + json.D7) / 2).ToString());
                                yb24_1.Add(((json.D4 + json.D3) / 2).ToString());
                                yb21_2.Add(((json.D4 + json.D5) / 2).ToString());
                                yb24_1.Add(((json.D9 + json.D5) / 2).ToString());
                                yb21_2.Add(((json.D4 + json.D7) / 2).ToString());
                                yb24_1.Add(((json.D11 + json.D12) / 2).ToString());
                                yb21_2.Add(((json.D8 + json.D12) / 2).ToString());
                                yb24_1.Add(((json.D13 + json.D14) / 2).ToString());
                                yb21_2.Add(((json.D14 + json.D12) / 2).ToString());
                                yb24_1.Add(((json.D15 + json.D16) / 2).ToString());
                                yb21_2.Add(((json.D16 + json.D12) / 2).ToString());
                                yb24_1.Add(((json.D15 + json.D12) / 2).ToString());
                                yb21_2.Add(((json.D16 + json.D13) / 2).ToString());
                            }
                            else if (json.ID2 == 5 &&yb24_1.Count==14)
                            {
                                yb21_2.Insert(7,json.D1.ToString());
                                yb24_1.Insert(7,json.D2.ToString());
                                yb24_1.Insert(8,json.D3.ToString());
                                yb21_2.Insert(8,json.D4.ToString());
                                yb24_1.Insert(9,json.D5.ToString());
                                yb21_2.Insert(9,json.D6.ToString());
                                yb21_2.Insert(10,json.D7.ToString());
                                yb24_1.Insert(10,json.D8.ToString());
                                yb24_1.Insert(11,json.D9.ToString());
                                yb21_2.Insert(11,json.D10.ToString());
                                yb24_1.Insert(12,json.D11.ToString());
                                yb21_2.Insert(12,json.D12.ToString());
                                yb24_1.Insert(13,json.D13.ToString());
                                yb21_2.Insert(13,json.D16.ToString());
                                yb21_2.Insert(14,json.D14.ToString());
                                yb24_1.Insert(14,json.D15.ToString());
                            }
                            else if (json.ID2 == 6 &&yb23_4.Count==15)
                            {
                                yb23_4.Insert(7,json.D15.ToString());
                                yb22_3.Insert(7,json.D14.ToString());
                                yb23_4.Insert(8, ((json.D15 + json.D11) / 2).ToString());// 乱跳
                                yb22_3.Insert(8,json.D16.ToString());
                                yb23_4.Insert(9,json.D11.ToString());
                                yb22_3.Insert(9,json.D12.ToString());
                                yb23_4.Insert(10,json.D9.ToString());
                                yb22_3.Insert(10,json.D10.ToString());
                                yb23_4.Insert(11,json.D7.ToString());
                                yb22_3.Insert(11,json.D8.ToString());
                                yb23_4.Insert(12,json.D5.ToString());
                                yb22_3.Insert(12,json.D6.ToString());
                                yb22_3.Insert(13,json.D4.ToString());
                                yb23_4.Insert(13,json.D3.ToString());
                                yb22_3.Insert(14,json.D2.ToString());
                                yb23_4.Insert(14,json.D1.ToString());
                            }
                            else if (json.ID2 == 7 &&yb23_4.Count==23)
                            {
                                yb23_4.Insert(0,json.D1.ToString());
                                yb22_3.Insert(0,json.D2.ToString());
                                yb23_4.Insert(1,json.D3.ToString());
                                yb22_3.Insert(1,json.D4.ToString());
                                yb23_4.Insert(2,json.D5.ToString());
                                yb22_3.Insert(2,json.D6.ToString());
                                yb23_4.Insert(3,json.D7.ToString());
                                yb22_3.Insert(3,json.D8.ToString());
                                yb23_4.Insert(4,json.D9.ToString());
                                yb22_3.Insert(4,json.D10.ToString());
                                yb23_4.Insert(5,json.D11.ToString());
                                yb22_3.Insert(5,json.D12.ToString());
                                yb23_4.Insert(6,json.D14.ToString());
                                yb22_3.Insert(6,json.D14.ToString());// 这个没装
                            }
                            else if (json.ID2 == 8 &&yb24_1.Count==22)
                            {
                                yb24_1.Insert(7,json.D1.ToString());
                                yb21_2.Insert(7,json.D2.ToString());
                                yb24_1.Insert(8,json.D3.ToString());
                                yb21_2.Insert(8,json.D4.ToString());
                                yb24_1.Insert(9,json.D5.ToString());
                                yb21_2.Insert(9,json.D6.ToString());
                                yb24_1.Insert(10,json.D7.ToString());
                                yb21_2.Insert(10,json.D8.ToString());
                                yb24_1.Insert(11,json.D9.ToString());
                                yb21_2.Insert(11, ((json.D8 + json.D10) / 2).ToString());// 8-10数据乱跳
                                yb24_1.Insert(12,json.D11.ToString());
                                yb21_2.Insert(12,json.D12.ToString());
                                yb24_1.Insert(13,json.D13.ToString());
                                yb21_2.Insert(13,json.D14.ToString());
                                yb24_1.Insert(14,json.D15.ToString());
                                yb21_2.Insert(14,json.D16.ToString());

                                if (yb2ZDRef)
                                {
                                    await projectManager.SetReferenceValue(yb2_Id.ToArray(), yb24_1.ToArray(), yb21_2.ToArray(), yb22_3.ToArray(), yb23_4.ToArray(), "YB");
                                    yb2ZDRef = false;
                                    yb2_Id.Clear();
                                    yb24_1.Clear();
                                    yb21_2.Clear();
                                    yb22_3.Clear();
                                    yb23_4.Clear();
                                    return true;
                                }
                                string yb2 = "[";
                                for (int i = 0; i < 30; i++)
                                {
                                    try
                                    {
                                        List<decimal> values = new List<decimal>();
                                        List<int> indexs = new List<int>();
                                        var yb2ZDValue = await projectManager.GetReferenceValue(long.Parse(yb2_Id[i]), "YB");
                                        yb2 += "{Id:'" + yb2_Id[i] + "',Type:'YB',";
                                        if (yb2ZDValue[0] == 0 && decimal.Parse(yb24_1[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(yb24_1[i]));
                                            indexs.Add(0);
                                        }
                                        else if (decimal.Parse(yb24_1[i]) > 0)
                                            yb2 += "D1:'" + (decimal)0.37 * (decimal.Parse(yb24_1[i]) - yb2ZDValue[0]) + "',";
                                        if (yb2ZDValue[1] == 0 && decimal.Parse(yb21_2[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(yb21_2[i]));
                                            indexs.Add(1);
                                        }
                                        else if (decimal.Parse(yb21_2[i]) > 0)
                                            yb2 += "D2:'" + (decimal)0.37 * (decimal.Parse(yb21_2[i]) - yb2ZDValue[1]) + "',";
                                        if (yb2ZDValue[2] == 0 && decimal.Parse(yb22_3[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(yb22_3[i]));
                                            indexs.Add(2);
                                        }
                                        else if (decimal.Parse(yb22_3[i]) > 0)
                                            yb2 += "D3:'" + (decimal)0.37 * (decimal.Parse(yb22_3[i]) - yb2ZDValue[2]) + "',";
                                        if (yb2ZDValue[3] == 0 && decimal.Parse(yb23_4[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(yb23_4[i]));
                                            indexs.Add(3);
                                        }
                                        else if (decimal.Parse(yb23_4[i]) > 0)
                                            yb2 += "D4:'" + (decimal)0.37 * (decimal.Parse(yb23_4[i]) - yb2ZDValue[3]) + "'";
                                        yb2 += "},";
                                        if (values.Count > 0)
                                        {
                                            await projectManager.SetReferenceValue(long.Parse(yb2_Id[i]), values.ToArray(), indexs.ToArray(), "YB");
                                        }
                                    }
                                    catch { }
                                }
                                yb2 = yb2.Remove(yb2.LastIndexOf(","), 1);
                                yb2 += "]";
                                yb2_Id.Clear();
                                yb24_1.Clear();
                                yb21_2.Clear();
                                yb22_3.Clear();
                                yb23_4.Clear();
                                List<ReceiveDataDto> jsonYB2 = JsonConvert.DeserializeObject<List<ReceiveDataDto>>(yb2);
                                await projectManager.CreateData(jsonYB2);
                            }
                            else if (json.ID2 == 9)// 2号斜井，Id从59-70，12个
                            {
                                yb2_Id.Clear();
                                yb21_2.Clear();
                                yb22_3.Clear();
                                yb23_4.Clear();
                                yb24_1.Clear();

                                for(int i = 59; i < 71; i++)
                                {
                                    yb2_Id.Add(i.ToString());
                                }
                                // 4_1存02，1_2存01
                                yb24_1.Add(json.D1.ToString());
                                yb24_1.Add(json.D2.ToString());
                                yb24_1.Add(json.D3.ToString());
                                yb24_1.Add(json.D4.ToString());
                                yb24_1.Add(json.D5.ToString());
                                yb24_1.Add(json.D6.ToString());
                            }
                            else if (json.ID2 == 10 && yb21_2.Count == 0)
                            {
                                yb21_2.Add(json.D1.ToString());
                                yb21_2.Add(json.D2.ToString());
                                yb21_2.Add(json.D3.ToString());
                                yb21_2.Add(json.D4.ToString());
                                yb21_2.Add(json.D5.ToString());
                                yb21_2.Add(json.D6.ToString());
                                yb21_2.Add(json.D7.ToString());
                                yb21_2.Add(json.D8.ToString());
                                yb21_2.Add(json.D9.ToString());
                            }
                            else if (json.ID2 == 11 && yb21_2.Count == 9)
                            {
                                yb24_1.Add(json.D9.ToString());
                                yb24_1.Add(json.D8.ToString());
                                yb24_1.Add(((json.D6 + json.D8) / 2).ToString());// 11-7没数据
                                yb21_2.Add(json.D1.ToString());
                                yb24_1.Add(json.D6.ToString());
                                yb21_2.Add(((json.D1 + json.D3) / 2).ToString());// 11-2数据乱跳
                                yb24_1.Add(json.D5.ToString());
                                yb21_2.Add(json.D3.ToString());
                                yb24_1.Add(json.D4.ToString());

                                if (yb2XJRef)
                                {
                                    await projectManager.SetReferenceValue(yb2_Id.ToArray(), yb21_2.ToArray(), yb22_3.ToArray(), yb23_4.ToArray(), yb24_1.ToArray(), "YB");
                                    yb2XJRef = false;
                                    yb2_Id.Clear();
                                    yb21_2.Clear();
                                    yb24_1.Clear();
                                    return true;
                                }

                                string yb2 = "[";
                                for (int i = 0; i < 12; i++)
                                {
                                    try
                                    {
                                        List<decimal> values = new List<decimal>();
                                        List<int> indexs = new List<int>();
                                        var yb2XJValue = await projectManager.GetReferenceValue(long.Parse(yb2_Id[i]), "YB");
                                        yb2 += "{Id:'" + yb2_Id[i] + "',Type:'YB',";
                                        if (yb2XJValue[0] == 0 && decimal.Parse(yb21_2[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(yb21_2[i]));
                                            indexs.Add(0);
                                        }
                                        else if (decimal.Parse(yb21_2[i]) > 0)
                                            yb2 += "D1:'" + (decimal)0.37 * (decimal.Parse(yb21_2[i]) - yb2XJValue[0]) + "',";
                                        if (yb2XJValue[1] == 0 && decimal.Parse(yb24_1[i]) > 0)
                                        {
                                            values.Add(decimal.Parse(yb24_1[i]));
                                            indexs.Add(1);
                                        }
                                        else if (decimal.Parse(yb24_1[i]) > 0)
                                            yb2 += "D2:'" + (decimal)0.37 * (decimal.Parse(yb24_1[i]) - yb2XJValue[1]) + "'";
                                        yb2 += "},";
                                        if (values.Count > 0)
                                        {
                                            await projectManager.SetReferenceValue(long.Parse(yb2_Id[i]), values.ToArray(), indexs.ToArray(), "YB");
                                        }
                                    }
                                    catch { }
                                }
                                yb2 = yb2.Remove(yb2.LastIndexOf(","), 1);
                                yb2 += "]";
                                yb2_Id.Clear();
                                yb24_1.Clear();
                                yb21_2.Clear();
                                List<ReceiveDataDto> jsonYB2 = JsonConvert.DeserializeObject<List<ReceiveDataDto>>(yb2);
                                await projectManager.CreateData(jsonYB2);
                            }
                        }
                    }
                    else if (json.Type == "GJ")
                    {
                        if (json.Id == 1) // 4号洞改为2号
                        {
                            if (json.ID2 == 1)
                            {
                                gj4.Clear();
                            }
                            decimal[] values = new decimal[7];
                            var gj4Value = await projectManager.GetReferenceValue(85, "GJ");
                            if (gj4Value[0] == 0)
                            {
                                // 还没设置过基准
                                values[0]=(decimal)json.D1;
                                values[1]=(decimal)json.D2;
                                values[2]=(decimal)json.D3;
                                values[3]=(decimal)json.D4;
                                values[4]=(decimal)json.D5;
                                values[5]=(decimal)json.D6;
                                values[6]=(decimal)json.D7;
                                int[] indexs = new int[7];
                                for(int i = 0; i < indexs.Length; i++)
                                {
                                    indexs[i] = i;
                                }
                                await projectManager.SetReferenceValue(85, values, indexs, "GJ");
                                values = new decimal[8];
                                values[0]=(decimal)json.D8;
                                values[1]=(decimal)json.D9;
                                values[2]=(decimal)json.D10;
                                values[3]=(decimal)json.D11;
                                values[4]=(decimal)json.D12;
                                values[5]=(decimal)json.D13;
                                values[6] = (decimal)json.D14;
                                values[7] = (decimal)json.D15;
                                indexs = new int[8];
                                for (int i = 0; i < indexs.Length; i++)
                                {
                                    indexs[i] = i;
                                }
                                await projectManager.SetReferenceValue(86, values, indexs, "GJ");
                            }
                            else
                            {
                                gj4Value.AddRange(await projectManager.GetReferenceValue(86, "GJ"));
                                AddString(gj4, json, gj4Value.ToArray(), 0.1234);
                                if (gj4.Count == 15)
                                {
                                    string gj = $"[{{Id:'85',Type:'GJ',D1:'{gj4[0]}',D2:'{gj4[1]}',D3:'{gj4[2]}',D4:'{gj4[3]}',D5:'{gj4[4]}',D6:'{gj4[5]}',D7:'{gj4[6]}'}}," +
                                        $"{{Id:'86',Type:'GJ',D1:'{gj4[7]}',D2:'{gj4[8]}',D3:'{gj4[9]}',D4:'{gj4[10]}',D5:'{gj4[11]}',D6:'{gj4[12]}',D7:'{gj4[13]}',D8:'{gj4[14]}'}}]";
                                    List<ReceiveDataDto> jsonGJ = JsonConvert.DeserializeObject<List<ReceiveDataDto>>(gj);
                                    await projectManager.CreateData(jsonGJ);
                                }
                            }
                        }
                        else // 2号洞
                        {
                            if (json.ID2 == 1)
                            {
                                gj2.Clear();
                            }
                            decimal[] values = new decimal[7];
                            var gj2Value = await projectManager.GetReferenceValue(87, "GJ");
                            if (gj2Value[0] == 0)
                            {
                                // 还没设置过基准
                                values[0] = (decimal)json.D1;
                                values[1] = (decimal)json.D2;
                                values[2] = (decimal)json.D3;
                                values[3] = (decimal)json.D4;
                                values[4] = (decimal)json.D5;
                                values[5] = (decimal)json.D6;
                                values[6] = (decimal)json.D7;
                                int[] indexs = new int[7];
                                for (int i = 0; i < indexs.Length; i++)
                                {
                                    indexs[i] = i;
                                }
                                await projectManager.SetReferenceValue(87, values, indexs, "GJ");
                                values = new decimal[8];
                                values[0] = (decimal)json.D8;
                                values[1] = (decimal)json.D9;
                                values[2] = (decimal)json.D10;
                                values[3] = (decimal)json.D11;
                                values[4] = (decimal)json.D12;
                                values[5] = (decimal)json.D13;
                                values[6] = (decimal)json.D14;
                                values[7] = (decimal)json.D15;
                                indexs = new int[8];
                                for (int i = 0; i < indexs.Length; i++)
                                {
                                    indexs[i] = i;
                                }
                                await projectManager.SetReferenceValue(88, values, indexs, "GJ");
                            }
                            else
                            {
                                gj2Value.AddRange(await projectManager.GetReferenceValue(88, "GJ"));
                                AddString(gj2, json,gj2Value.ToArray(),0.1234);
                                if (gj2.Count == 15)
                                {
                                    string gj = $"[{{Id:'87',Type:'GJ',D1:'{gj2[0]}',D2:'{gj2[1]}',D3:'{gj2[2]}',D4:'{gj2[3]}',D5:'{gj2[4]}',D6:'{gj2[5]}',D7:'{gj2[6]}'}}," +
                                        $"{{Id:'88',Type:'GJ',D1:'{gj2[7]}',D2:'{gj2[8]}',D3:'{gj2[9]}',D4:'{gj2[10]}',D5:'{gj2[11]}',D6:'{gj2[12]}',D7:'{gj2[13]}',D8:'{gj2[14]}'}}]";
                                    List<ReceiveDataDto> jsonGJ = JsonConvert.DeserializeObject<List<ReceiveDataDto>>(gj);
                                    await projectManager.CreateData(jsonGJ);
                                }
                            }
                            
                        }
                    }
                    else if (json.Type == "YL")
                    {
                        if (json.Id == 1) // 4号洞
                        {
                            if (json.ID2 == 1)
                            {
                                ya4.Clear();
                            }
                            decimal[] values = new decimal[7];
                            var yl4Value = await projectManager.GetReferenceValue(87, "YL");
                            if (yl4Value[0] == 0)
                            {
                                // 还没设置过基准
                                values[0] = (decimal)json.D1;
                                values[1] = (decimal)json.D2;
                                values[2] = (decimal)json.D3;
                                values[3] = (decimal)json.D4;
                                values[4] = (decimal)json.D5;
                                values[5] = (decimal)json.D6;
                                values[6] = (decimal)json.D7;
                                int[] indexs = new int[7];
                                for (int i = 0; i < indexs.Length; i++)
                                {
                                    indexs[i] = i;
                                }
                                await projectManager.SetReferenceValue(87, values, indexs, "YL");
                                values[0] = (decimal)json.D8;
                                values[1] = (decimal)json.D9;
                                values[2] = (decimal)json.D10;
                                values[3] = (decimal)json.D11;
                                values[4] = (decimal)json.D12;
                                values[5] = (decimal)json.D13;
                                values[6] = (decimal)json.D14;
                                await projectManager.SetReferenceValue(88, values, indexs, "YL");
                            }
                            else
                            {
                                yl4Value.AddRange(await projectManager.GetReferenceValue(88, "YL"));
                                AddString(ya4, json, yl4Value.ToArray(), 0.1476);
                                if (ya4.Count == 14)
                                {
                                    string ya = $"[{{Id:'87',Type:'YL',D1:'{ya4[0]}',D2:'{ya4[1]}',D3:'{ya4[2]}',D4:'{ya4[3]}',D5:'{ya4[4]}',D6:'{ya4[5]}',D7:'{ya4[6]}'}}," +
                                    $"{{Id:'88',Type:'YL',D1:'{ya4[7]}',D2:'{ya4[8]}',D3:'{ya4[9]}',D4:'{ya4[10]}',D5:'{ya4[11]}',D6:'{ya4[12]}',D7:'{ya4[13]}'}}]";
                                    List<ReceiveDataDto> jsonYL = JsonConvert.DeserializeObject<List<ReceiveDataDto>>(ya);
                                    await projectManager.CreateData(jsonYL);
                                }
                            }
                        }
                        else // 2号洞
                        {
                            if (json.ID2 == 1)
                            {
                                ya2.Clear();
                            }
                            decimal[] values = new decimal[7];
                            var yl2Value = await projectManager.GetReferenceValue(85, "YL");
                            if (yl2Value[0] == 0)
                            {
                                // 还没设置过基准
                                values[0] = (decimal)json.D1;
                                values[1] = (decimal)json.D2;
                                values[2] = (decimal)json.D3;
                                values[3] = (decimal)json.D4;
                                values[4] = (decimal)json.D5;
                                values[5] = (decimal)json.D6;
                                values[6] = (decimal)json.D7;
                                int[] indexs = new int[7];
                                for (int i = 0; i < indexs.Length; i++)
                                {
                                    indexs[i] = i;
                                }
                                await projectManager.SetReferenceValue(85, values, indexs, "YL");
                                values[0] = (decimal)json.D8;
                                values[1] = (decimal)json.D9;
                                values[2] = (decimal)json.D10;
                                values[3] = (decimal)json.D11;
                                values[4] = (decimal)json.D12;
                                values[5] = (decimal)json.D13;
                                values[6] = (decimal)json.D14;
                                await projectManager.SetReferenceValue(86, values, indexs, "YL");
                            }
                            else
                            {
                                yl2Value.AddRange(await projectManager.GetReferenceValue(86, "YL"));
                                AddString(ya2, json, yl2Value.ToArray(), 0.1476);
                                if (ya2.Count == 14)
                                {
                                    string ya = $"[{{Id:'85',Type:'YL',D1:'{ya2[0]}',D2:'{ya2[1]}',D3:'{ya2[2]}',D4:'{ya2[3]}',D5:'{ya2[4]}',D6:'{ya2[5]}',D7:'{ya2[6]}'}}," +
                                    $"{{Id:'86',Type:'YL',D1:'{ya2[7]}',D2:'{ya2[8]}',D3:'{ya2[9]}',D4:'{ya2[10]}',D5:'{ya2[11]}',D6:'{ya2[12]}',D7:'{ya2[13]}'}}]";
                                    List<ReceiveDataDto> jsonYL = JsonConvert.DeserializeObject<List<ReceiveDataDto>>(ya);
                                    await projectManager.CreateData(jsonYL);
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
