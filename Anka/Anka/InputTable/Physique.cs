﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Data;

namespace Anka
{
    public partial class MainWindow
    {

        Indicator BMIIndicator;
        Indicator FMIIndicator;
        Indicator TBWIndicator;
        Indicator BCWIndicator;
        Indicator EBWIndicator;
        Indicator BodyIndicator;
        Indicator LAIndicator;
        Indicator TKIndicator;
        Indicator RAIndicator;
        Indicator LLIndicator;
        Indicator RLIndicator;
        Indicator VATIndicator;
        Indicator WCIndicator;

        private void BtPhysiqueSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.txPhysiqueLoop.Text.Trim().Length > 0)
            {
                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        SQLiteHelper sh = new SQLiteHelper(cmd);
                        var dicData = new Dictionary<string, object>();

                        PhysiqueResultSave(dicData);

                        string PhysiqueNumber = DataAdapter.Number + "-" + this.txPhysiqueLoop.Text.Trim();

                        string sql = string.Format("SELECT * FROM physique where PhysiqueNumber='{0}';", PhysiqueNumber);
                        DataTable dt = sh.Select(sql);
                        try
                        {
                            if (dt.Rows.Count > 0)
                            {
                                var dicCondition = new Dictionary<string, object>();
                                dicCondition["basicinfo_Number"] = DataAdapter.Number;
                                dicCondition["PhysiqueNumber"] = PhysiqueNumber;
                                sh.Update("physique", dicData, dicCondition);
                            }
                            else
                            {
                                dicData["PhysiqueNumber"] = PhysiqueNumber;
                                dicData["basicinfo_Number"] = DataAdapter.Number;
                                sh.Insert("physique", dicData);
                                txPhysiqueLoop.Items.Add(this.txPhysiqueLoop.Text.Trim());
                            }
                        }
                        catch (SQLiteException ex)
                        {
                            MessageBox.Show(string.Format("数据更新错误。错误代码为:{0}", ex.ErrorCode), "数据更新错误");
                        }
                        finally
                        {
                            conn.Close();
                        }

                    }
                }
               
                ((Button)sender).Background = new SolidColorBrush(Colors.LightGreen);

            }
            else
            {
                MessageBox.Show("请输入体质检测报告编号");
            }
           

        }
        private void BtPhysiqueLoad_Click(object sender, RoutedEventArgs e)
        {
            if (this.txPhysiqueLoop.Text.Trim().Length > 0)
            {
                string PhysiqueNumber = DataAdapter.Number + "-" + this.txPhysiqueLoop.Text.Trim();
                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        SQLiteHelper sh = new SQLiteHelper(cmd);

                        try
                        {
                            DataTable dt = sh.Select(string.Format("select * from physique where PhysiqueNumber='{0}';", PhysiqueNumber));
                            if (dt.Rows.Count > 0)
                            {
                                PhysiqueDataLoad(dt);                                
                            }
                            else
                            {
                                MessageBox.Show("该编号数据不存在。");
                            }

                        }
                        catch (SQLiteException ex)
                        {
                            MessageBox.Show(string.Format("数据更新错误。错误代码为:{0}", ex.ErrorCode), "数据更新错误");
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("请输入项目编号。");
            }
        }

        private void PhysiqueResultSave(Dictionary<string, object> dic)
        {
            dic["Weight"]= DataAdapter.IsNumber(this.bmiWeight.Text.Trim()) ? Convert.ToDouble(this.bmiWeight.Text.Trim()) : 0;
            dic["Hight"] = DataAdapter.IsNumber(this.bmiHight.Text.Trim()) ? Convert.ToDouble(this.bmiHight.Text.Trim()) : 0;
            dic["FM"] = DataAdapter.IsNumber(this.txFM.Text.Trim()) ? Convert.ToDouble(this.txFM.Text.Trim()) : 0;
            dic["PA"] = DataAdapter.IsNumber(this.txPA.Text.Trim()) ? Convert.ToDouble(this.txPA.Text.Trim()) : 0;
            dic["SMMAll"] = DataAdapter.IsNumber(this.txSMM.Text.Trim()) ? Convert.ToDouble(this.txSMM.Text.Trim()) : 0;
            dic["SMMArmLeft"] = DataAdapter.IsNumber(this.txLA.Text.Trim()) ? Convert.ToDouble(this.txLA.Text.Trim()) : 0;
            dic["SMMArmRight"] = DataAdapter.IsNumber(this.txRA.Text.Trim()) ? Convert.ToDouble(this.txRA.Text.Trim()) : 0;
            dic["SMMBody"] = DataAdapter.IsNumber(this.txTK.Text.Trim()) ? Convert.ToDouble(this.txTK.Text.Trim()) : 0;
            dic["SMMLegLeft"] = DataAdapter.IsNumber(this.txLL.Text.Trim()) ? Convert.ToDouble(this.txLL.Text.Trim()) : 0;
            dic["SMMLegRight"] = DataAdapter.IsNumber(this.txRL.Text.Trim()) ? Convert.ToDouble(this.txRL.Text.Trim()) : 0;

            dic["BCW"] = DataAdapter.IsNumber(this.txBCW.Text.Trim()) ? Convert.ToDouble(this.txBCW.Text.Trim()) : 0;
            dic["TBW"] = DataAdapter.IsNumber(this.txTBW.Text.Trim()) ? Convert.ToDouble(this.txTBW.Text.Trim()) : 0;

            dic["VAT"] = DataAdapter.IsNumber(this.txVAT.Text.Trim()) ? Convert.ToDouble(this.txVAT.Text.Trim()) : 0;
            dic["Waistline"] = DataAdapter.IsNumber(txWC.Text.Trim()) ? Convert.ToDouble(txWC.Text.Trim()) : 0;
            dic["PA"] = DataAdapter.IsNumber(this.txPA.Text.Trim()) ? Convert.ToDouble(this.txPA.Text.Trim()) : 0;
            dic["PAPercent"] = DataAdapter.IsNumber(this.txPAPercent.Text.Trim()) ? Convert.ToDouble(this.txPAPercent.Text.Trim()) : 0;
        }
        private void PhysiqueDataLoad(DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            bmiWeight.Text = dr["Weight"] == System.DBNull.Value ? "" : dr["Weight"].ToString();
            bmiHight.Text = dr["Hight"] == System.DBNull.Value ? "" : dr["Hight"].ToString();
            txFM.Text = dr["FM"] == System.DBNull.Value ? "" : dr["FM"].ToString();
            txPA.Text = dr["PA"] == System.DBNull.Value ? "" : dr["PA"].ToString();
            txSMM.Text = dr["SMMAll"] == System.DBNull.Value ? "" : dr["SMMAll"].ToString();
            txLA.Text = dr["SMMArmLeft"] == System.DBNull.Value ? "" : dr["SMMArmLeft"].ToString();
            txRA.Text = dr["SMMArmRight"] == System.DBNull.Value ? "" : dr["SMMArmRight"].ToString();
            txTK.Text = dr["SMMBody"] == System.DBNull.Value ? "" : dr["SMMBody"].ToString();
            txLL.Text = dr["SMMLegLeft"] == System.DBNull.Value ? "" : dr["SMMLegLeft"].ToString();
            txRL.Text = dr["SMMLegRight"] == System.DBNull.Value ? "" : dr["SMMLegRight"].ToString();

            txBCW.Text = dr["BCW"] == System.DBNull.Value ? "" : dr["BCW"].ToString();
            txTBW.Text = dr["TBW"] == System.DBNull.Value ? "" : dr["TBW"].ToString();

            txVAT.Text = dr["VAT"] == System.DBNull.Value ? "" : dr["VAT"].ToString();
            txWC.Text = dr["Waistline"] == System.DBNull.Value ? "" : dr["Waistline"].ToString();
            txPA.Text = dr["PA"] == System.DBNull.Value ? "" : dr["PA"].ToString();
            txPAPercent.Text = dr["PAPercent"] == System.DBNull.Value ? "" : dr["PAPercent"].ToString();

            double Weight = DataAdapter.IsNumber(bmiWeight.Text.Trim()) ? Convert.ToDouble(bmiWeight.Text.Trim()) : 0;
            double Hight = DataAdapter.IsNumber(bmiHight.Text.Trim()) ? Convert.ToDouble(bmiHight.Text.Trim()) : 0;
            double FM = DataAdapter.IsNumber(txFM.Text.Trim()) ? Convert.ToDouble(txFM.Text.Trim()) : 0;
            double TBW = DataAdapter.IsNumber(this.txTBW.Text.Trim()) ? Convert.ToDouble(this.txTBW.Text.Trim()) : 0;
            double BCW = DataAdapter.IsNumber(this.txBCW.Text.Trim()) ? Convert.ToDouble(this.txBCW.Text.Trim()) : 0;

            if (Weight > 0 && Hight > 0)
            {
                this.bmiBMI.Text = (Weight / (Hight * Hight)).ToString("0.0");
            }
            if (Weight > 0 && FM > 0)
            {
                this.txFM2.Content = ((FM / Weight) * 100.0).ToString("0.0");
            }
            if (TBW > 0 && BCW > 0)
            {
                this.txEBW.Text = ((BCW / TBW) * 100.0).ToString("0.0");
            }
            if (Weight > 0 && Hight > 0)
            {
                this.txFMI.Text = (FM / (Hight * Hight)).ToString("0.0");
            }
            

            BMIIndicatorBinding();
            FMIIndicatorBinding();
            TBWIndicatorBinding();
            BCWIndicatorBinding();
            EBWIndicatorBinding();
            BodyIndicatorBinding();
            LAIndicatorBinding();
            TKIndicatorBinding();
            RAIndicatorBinding();
            LLIndicatorBinding();
            RLIndicatorBinding();
            VATIndicatorBinding();
            WCIndicatorBinding();
        }


        private void InitBMI()
        {
            BMIIndicator = new Indicator();
            BMIIndicatorBinding();
        }

        private void BMIIndicatorBinding()
        {
            double Headvalue = 13.5;
            double Lower = 18.5;
            double Normal = 25.0;
            double Upper = 30.0;
            double Endvalue = 40.0;

            double PositionStar = 0;
            BMIIndicator.IndicatorValue = 0;

            double Hight = DataAdapter.IsNumber(this.bmiHight.Text.Trim()) ? Convert.ToDouble(this.bmiHight.Text.Trim()) : 0;
            double Weight = DataAdapter.IsNumber(this.bmiWeight.Text.Trim()) ? Convert.ToDouble(this.bmiWeight.Text.Trim()) : 0;



            if (Hight > 0 && Hight > 0)
            {
                BMIIndicator.IndicatorValue = Weight / (Hight * Hight);
            }

            if (BMIIndicator.IndicatorValue < Headvalue)
            {
                PositionStar = 10;
            }
            else if (BMIIndicator.IndicatorValue < Lower)
            {
                PositionStar = (BMIIndicator.IndicatorValue / Lower) * 75 + 10;
            }
            else if (BMIIndicator.IndicatorValue >= Lower && BMIIndicator.IndicatorValue < Normal)
            {
                PositionStar = 75 + ((BMIIndicator.IndicatorValue - Lower) / (Normal - Lower)) * (155 - 75);
            }
            else if (BMIIndicator.IndicatorValue >= Normal && BMIIndicator.IndicatorValue < Upper)
            {
                PositionStar = 155 + ((BMIIndicator.IndicatorValue - Normal) / (Upper - Normal)) * (215 - 155);
            }
            else if (BMIIndicator.IndicatorValue >= Upper && BMIIndicator.IndicatorValue < Endvalue)
            {
                PositionStar = 215 + ((BMIIndicator.IndicatorValue - Upper) / (Endvalue - Upper)) * (350 - 215);
            }
            else if (BMIIndicator.IndicatorValue >= Endvalue)
            {
                PositionStar = 300;
            }

            Indicator.IndicatorTri indicatorStar = new Indicator.IndicatorTri(
                new Point(PositionStar - 16 * 0.5, 25),
                new Point(PositionStar + 16 * 0.5, 25),
                new Point(PositionStar, 40)
                );

            this.bmiUpper.StartPoint = indicatorStar.StartUpper;
            this.bmiUpperEnd.Point = indicatorStar.EndUpper;
            this.bmiLower.Point = indicatorStar.Lower;

            this.lbBMIIndicator.SetValue(Canvas.LeftProperty, indicatorStar.Lower.X - 30);
            this.lbBMIIndicator.Content = BMIIndicator.IndicatorValue.ToString("0.0");

            if (BMIIndicator.IndicatorValue < Lower)
            {
                this.bmiPath1.Fill = new SolidColorBrush(Colors.LightGray);
                this.bmiPath2.Fill = new SolidColorBrush(Colors.White);
                this.bmiPath3.Fill = new SolidColorBrush(Colors.White);
                this.bmiPath4.Fill = new SolidColorBrush(Colors.White);
            }
            if (BMIIndicator.IndicatorValue >= Lower && BMIIndicator.IndicatorValue < Normal)
            {
                this.bmiPath1.Fill = new SolidColorBrush(Colors.White);
                this.bmiPath2.Fill = new SolidColorBrush(Colors.LightGreen);
                this.bmiPath3.Fill = new SolidColorBrush(Colors.White);
                this.bmiPath4.Fill = new SolidColorBrush(Colors.White);
            }
            if (BMIIndicator.IndicatorValue >= Normal && BMIIndicator.IndicatorValue < Upper)
            {
                this.bmiPath1.Fill = new SolidColorBrush(Colors.White);
                this.bmiPath2.Fill = new SolidColorBrush(Colors.White);
                this.bmiPath3.Fill = new SolidColorBrush(Colors.Orange);
                this.bmiPath4.Fill = new SolidColorBrush(Colors.White);
            }
            if (BMIIndicator.IndicatorValue >= Upper)
            {
                this.bmiPath1.Fill = new SolidColorBrush(Colors.White);
                this.bmiPath2.Fill = new SolidColorBrush(Colors.White);
                this.bmiPath3.Fill = new SolidColorBrush(Colors.White);
                this.bmiPath4.Fill = new SolidColorBrush(Colors.OrangeRed);
            }


        }

        private void InitFMI()
        {
            FMIIndicator = new Indicator();
            FMIIndicatorBinding();
        }

        private void FMIIndicatorBinding()
        {
            double Headvalue = 0;
            double Lower = 1.5;
            double Normal = 5.6;
            double Upper = 8.8;
            double Endvalue = 20.0;

            double PositionStar = 10;
            FMIIndicator.IndicatorValue = 0;
            double FM = DataAdapter.IsNumber(this.txFM.Text.Trim()) ? Convert.ToDouble(this.txFM.Text.Trim()) : 0;
            double Hight = DataAdapter.IsNumber(this.bmiHight.Text.Trim()) ? Convert.ToDouble(this.bmiHight.Text.Trim()) : 0;

            if (FM > 0 && Hight > 0)
            {
                FMIIndicator.IndicatorValue = FM / (Hight * Hight);
            }

            if (FMIIndicator.IndicatorValue < Headvalue)
            {
                PositionStar = 10;
            }
            else if (FMIIndicator.IndicatorValue >= Headvalue && FMIIndicator.IndicatorValue < Lower)
            {
                PositionStar = (FMIIndicator.IndicatorValue / Lower) * 75 + 10;
            }
            else if (FMIIndicator.IndicatorValue >= Lower && FMIIndicator.IndicatorValue < Normal)
            {
                PositionStar = 75 + ((FMIIndicator.IndicatorValue - Lower) / (Normal - Lower)) * (155 - 75);
            }
            else if (FMIIndicator.IndicatorValue >= Normal && FMIIndicator.IndicatorValue < Upper)
            {
                PositionStar = 155 + ((FMIIndicator.IndicatorValue - Normal) / (Upper - Normal)) * (215 - 155);
            }
            else if (FMIIndicator.IndicatorValue >= Upper && FMIIndicator.IndicatorValue < Endvalue)
            {
                PositionStar = 215 + ((FMIIndicator.IndicatorValue - Upper) / (Endvalue - Upper)) * (350 - 215);
            }
            else if (FMIIndicator.IndicatorValue >= Endvalue)
            {
                PositionStar = 300;
            }


            Indicator.IndicatorTri indicatorStar = new Indicator.IndicatorTri(
                new Point(PositionStar - 16 * 0.5, 25),
                new Point(PositionStar + 16 * 0.5, 25),
                new Point(PositionStar, 40)
                );

            this.fmiUpper.StartPoint = indicatorStar.StartUpper;
            this.fmiUpperEnd.Point = indicatorStar.EndUpper;
            this.fmiLower.Point = indicatorStar.Lower;

            this.lbFMIIndicator.SetValue(Canvas.LeftProperty, indicatorStar.Lower.X - 30);
            this.lbFMIIndicator.Content = FMIIndicator.IndicatorValue.ToString("0.0");

            if (FMIIndicator.IndicatorValue < Lower)
            {
                this.fmiPath1.Fill = new SolidColorBrush(Colors.LightGray);
                this.fmiPath2.Fill = new SolidColorBrush(Colors.White);
                this.fmiPath3.Fill = new SolidColorBrush(Colors.White);
                this.fmiPath4.Fill = new SolidColorBrush(Colors.White);
            }
            if (FMIIndicator.IndicatorValue >= Lower && FMIIndicator.IndicatorValue < Normal)
            {
                this.fmiPath1.Fill = new SolidColorBrush(Colors.White);
                this.fmiPath2.Fill = new SolidColorBrush(Colors.LightGreen);
                this.fmiPath3.Fill = new SolidColorBrush(Colors.White);
                this.fmiPath4.Fill = new SolidColorBrush(Colors.White);
            }
            if (FMIIndicator.IndicatorValue >= Normal && FMIIndicator.IndicatorValue < Upper)
            {
                this.fmiPath1.Fill = new SolidColorBrush(Colors.White);
                this.fmiPath2.Fill = new SolidColorBrush(Colors.White);
                this.fmiPath3.Fill = new SolidColorBrush(Colors.Orange);
                this.fmiPath4.Fill = new SolidColorBrush(Colors.White);
            }
            if (FMIIndicator.IndicatorValue >= Upper)
            {
                this.fmiPath1.Fill = new SolidColorBrush(Colors.White);
                this.fmiPath2.Fill = new SolidColorBrush(Colors.White);
                this.fmiPath3.Fill = new SolidColorBrush(Colors.White);
                this.fmiPath4.Fill = new SolidColorBrush(Colors.OrangeRed);
            }


        }

        private void InitTBW()
        {
            TBWIndicator = new Indicator();
            TBWIndicatorBinding();
        }

        private void TBWIndicatorBinding()
        {
            double Headvalue = 0;
            double Lower = 32.9;
            double Normal = 36.6;
            double Upper = 40.2;
            double Endvalue = 60.0;

            double PositionStar = 0;
            double TBW = DataAdapter.IsNumber(this.txTBW.Text.Trim()) ? Convert.ToDouble(this.txTBW.Text.Trim()) : 0;
            TBWIndicator.IndicatorValue = TBW;
            if (TBWIndicator.IndicatorValue < Headvalue)
            {
                PositionStar = 10;
            }
            else if (TBWIndicator.IndicatorValue < Lower)
            {
                PositionStar = (TBWIndicator.IndicatorValue / Lower) * 75 + 10;
            }
            else if (TBWIndicator.IndicatorValue >= Lower && TBWIndicator.IndicatorValue < Normal)
            {
                PositionStar = 75 + ((TBWIndicator.IndicatorValue - Lower) / (Normal - Lower)) * (175 - 75);
            }
            else if (TBWIndicator.IndicatorValue >= Normal && TBWIndicator.IndicatorValue < Upper)
            {
                PositionStar = 175 + ((TBWIndicator.IndicatorValue - Normal) / (Upper - Normal)) * (275 - 175);
            }
            else if (TBWIndicator.IndicatorValue >= Upper && TBWIndicator.IndicatorValue < Endvalue)
            {
                PositionStar = 275 + ((TBWIndicator.IndicatorValue - Upper) / (Endvalue - Upper)) * (350 - 275);
            }
            else if (TBWIndicator.IndicatorValue >= Endvalue)
            {
                PositionStar = 300;
            }


            Indicator.IndicatorTri indicatorStar = new Indicator.IndicatorTri(
                new Point(PositionStar - 16 * 0.5, 25),
                new Point(PositionStar + 16 * 0.5, 25),
                new Point(PositionStar, 40)
                );

            this.tbwUpper.StartPoint = indicatorStar.StartUpper;
            this.tbwUpperEnd.Point = indicatorStar.EndUpper;
            this.tbwLower.Point = indicatorStar.Lower;

            this.lbTBWIndicator.SetValue(Canvas.LeftProperty, indicatorStar.Lower.X - 30);
            this.lbTBWIndicator.Content = TBWIndicator.IndicatorValue.ToString("0.0");
        }

        private void InitBCW()
        {
            BCWIndicator = new Indicator();
            BCWIndicatorBinding();
        }

        private void BCWIndicatorBinding()
        {
            double Headvalue = 0;
            double Lower = 13.6;
            double Normal = 15.2;
            double Upper = 16.9;
            double Endvalue = 25.0;

            double PositionStar = 0;
            BCWIndicator.IndicatorValue = 0;

            double BCW = DataAdapter.IsNumber(this.txBCW.Text.Trim()) ? Convert.ToDouble(this.txBCW.Text.Trim()) : 0;
            BCWIndicator.IndicatorValue = BCW;
            if (BCWIndicator.IndicatorValue < Headvalue)
            {
                PositionStar = 10;
            }
            else if (BCWIndicator.IndicatorValue < Lower)
            {
                PositionStar = (BCWIndicator.IndicatorValue / Lower) * 75 + 10;
            }
            else if (BCWIndicator.IndicatorValue >= Lower && BCWIndicator.IndicatorValue < Normal)
            {
                PositionStar = 75 + ((BCWIndicator.IndicatorValue - Lower) / (Normal - Lower)) * (175 - 75);
            }
            else if (BCWIndicator.IndicatorValue >= Normal && BCWIndicator.IndicatorValue < Upper)
            {
                PositionStar = 175 + ((BCWIndicator.IndicatorValue - Normal) / (Upper - Normal)) * (275 - 175);
            }
            else if (BCWIndicator.IndicatorValue >= Upper && BCWIndicator.IndicatorValue < Endvalue)
            {
                PositionStar = 275 + ((BCWIndicator.IndicatorValue - Upper) / (Endvalue - Upper)) * (350 - 275);
            }
            else if (BCWIndicator.IndicatorValue >= Endvalue)
            {
                PositionStar = 300;
            }


            Indicator.IndicatorTri indicatorStar = new Indicator.IndicatorTri(
                new Point(PositionStar - 16 * 0.5, 25),
                new Point(PositionStar + 16 * 0.5, 25),
                new Point(PositionStar, 40)
                );

            this.bcwUpper.StartPoint = indicatorStar.StartUpper;
            this.bcwUpperEnd.Point = indicatorStar.EndUpper;
            this.bcwLower.Point = indicatorStar.Lower;

            this.lbBCWIndicator.SetValue(Canvas.LeftProperty, indicatorStar.Lower.X - 30);
            this.lbBCWIndicator.Content = BCWIndicator.IndicatorValue.ToString("0.0");

        }

        private void InitEBW()
        {
            EBWIndicator = new Indicator();
            EBWIndicatorBinding();
        }

        private void EBWIndicatorBinding()
        {
            double Headvalue = 0;
            double Lower = 39.7;
            double Normal = 41.8;
            double Upper = 44.0;
            double Endvalue = 60.0;

            double PositionStar = 0;

            double BCW = DataAdapter.IsNumber(this.txBCW.Text.Trim()) ? Convert.ToDouble(this.txBCW.Text.Trim()) : 0;
            double TBW = DataAdapter.IsNumber(this.txTBW.Text.Trim()) ? Convert.ToDouble(this.txTBW.Text.Trim()) : 0;
            EBWIndicator.IndicatorValue = 0;

            if (BCW > 0 && TBW > 0)
            {
                EBWIndicator.IndicatorValue = (BCW / TBW) * 100;
            }

            if (EBWIndicator.IndicatorValue < Headvalue)
            {
                PositionStar = 10;
            }
            else if (EBWIndicator.IndicatorValue < Lower)
            {
                PositionStar = (EBWIndicator.IndicatorValue / Lower) * 75 + 10;
            }
            else if (EBWIndicator.IndicatorValue >= Lower && EBWIndicator.IndicatorValue < Normal)
            {
                PositionStar = 75 + ((EBWIndicator.IndicatorValue - Lower) / (Normal - Lower)) * (175 - 75);
            }
            else if (EBWIndicator.IndicatorValue >= Normal && EBWIndicator.IndicatorValue < Upper)
            {
                PositionStar = 175 + ((EBWIndicator.IndicatorValue - Normal) / (Upper - Normal)) * (275 - 175);
            }
            else if (EBWIndicator.IndicatorValue >= Upper && EBWIndicator.IndicatorValue < Endvalue)
            {
                PositionStar = 275 + ((EBWIndicator.IndicatorValue - Upper) / (Endvalue - Upper)) * (350 - 275);
            }
            else if (EBWIndicator.IndicatorValue >= Endvalue)
            {
                PositionStar = 300;
            }


            Indicator.IndicatorTri indicatorStar = new Indicator.IndicatorTri(
                new Point(PositionStar - 16 * 0.5, 25),
                new Point(PositionStar + 16 * 0.5, 25),
                new Point(PositionStar, 40)
                );

            this.ebwUpper.StartPoint = indicatorStar.StartUpper;
            this.ebwUpperEnd.Point = indicatorStar.EndUpper;
            this.ebwLower.Point = indicatorStar.Lower;

            this.lbEBWIndicator.SetValue(Canvas.LeftProperty, indicatorStar.Lower.X - 30);
            this.lbEBWIndicator.Content = EBWIndicator.IndicatorValue.ToString("0.0");
        }

        private void InitBody()
        {
            BodyIndicator = new Indicator();
            BodyIndicatorBinding();
        }

        private void BodyIndicatorBinding()
        {
            double Headvalue = 0;
            double Lower = 20.7;
            double Normal = 25.2;
            double Upper = 30.7;
            double Endvalue = 40.0;

            double PositionStar = 0;
            BodyIndicator.IndicatorValue = 0;

            double SMMAll = DataAdapter.IsNumber(this.txSMM.Text.Trim()) ? Convert.ToDouble(this.txSMM.Text.Trim()) : 0;
            BodyIndicator.IndicatorValue = SMMAll;

            if (BodyIndicator.IndicatorValue < Headvalue)
            {
                PositionStar = 0;
            }
            else if (BodyIndicator.IndicatorValue < Lower)
            {
                PositionStar = 120 - (BodyIndicator.IndicatorValue / Lower) * 30;
            }
            else if (BodyIndicator.IndicatorValue >= Lower && BodyIndicator.IndicatorValue < Normal)
            {
                PositionStar = 90 - ((BodyIndicator.IndicatorValue - Lower) / (Normal - Lower)) * 30;
            }
            else if (BodyIndicator.IndicatorValue >= Normal && BodyIndicator.IndicatorValue < Upper)
            {
                PositionStar = 60 - ((BodyIndicator.IndicatorValue - Normal) / (Upper - Normal)) * 30;
            }
            else if (BodyIndicator.IndicatorValue >= Upper && BodyIndicator.IndicatorValue < Endvalue)
            {
                PositionStar = 30 - ((BodyIndicator.IndicatorValue - Upper) / (Endvalue - Upper)) * 30;
            }
            else if (BodyIndicator.IndicatorValue >= Endvalue)
            {
                PositionStar = 0;
            }


            Indicator.IndicatorTri indicatorStar = new Indicator.IndicatorTri(
                new Point(40, PositionStar - 10 * 0.5),
                new Point(40, PositionStar + 10 * 0.5),
                new Point(50, PositionStar)
                );

            this.BodyUpper.StartPoint = indicatorStar.StartUpper;
            this.BodyUpperEnd.Point = indicatorStar.EndUpper;
            this.BodyLower.Point = indicatorStar.Lower;

            this.lbBodyIndicator.SetValue(Canvas.TopProperty, indicatorStar.Lower.Y - 15);
            this.lbBodyIndicator.Content = BodyIndicator.IndicatorValue.ToString("0.0");
        }


        private void InitLA()
        {
            LAIndicator = new Indicator();
            LAIndicatorBinding();
        }

        private void LAIndicatorBinding()
        {
            double Headvalue = 0;
            double Lower = 1.21;
            double Normal = 1.59;
            double Upper = 2.04;
            double Endvalue = 3.0;

            double PositionStar = 0;
            LAIndicator.IndicatorValue = 0;

            double SMMArmLeft = DataAdapter.IsNumber(this.txLA.Text.Trim()) ? Convert.ToDouble(this.txLA.Text.Trim()) : 0;

            LAIndicator.IndicatorValue = SMMArmLeft;


            if (LAIndicator.IndicatorValue < Headvalue)
            {
                PositionStar = 0;
            }
            else if (LAIndicator.IndicatorValue < Lower)
            {
                PositionStar = 60 - (LAIndicator.IndicatorValue / Lower) * 15;
            }
            else if (LAIndicator.IndicatorValue >= Lower && LAIndicator.IndicatorValue < Normal)
            {
                PositionStar = 45 - ((LAIndicator.IndicatorValue - Lower) / (Normal - Lower)) * 15;
            }
            else if (LAIndicator.IndicatorValue >= Normal && LAIndicator.IndicatorValue < Upper)
            {
                PositionStar = 30 - ((LAIndicator.IndicatorValue - Normal) / (Upper - Normal)) * 15;
            }
            else if (LAIndicator.IndicatorValue >= Upper && LAIndicator.IndicatorValue < Endvalue)
            {
                PositionStar = 15 - ((LAIndicator.IndicatorValue - Upper) / (Endvalue - Upper)) * 15;
            }
            else if (LAIndicator.IndicatorValue >= Endvalue)
            {
                PositionStar = 0;
            }


            Indicator.IndicatorTri indicatorStar = new Indicator.IndicatorTri(
                new Point(30, PositionStar - 10 * 0.5),
                new Point(30, PositionStar + 10 * 0.5),
                new Point(40, PositionStar)
                );

            this.LAUpper.StartPoint = indicatorStar.StartUpper;
            this.LAUpperEnd.Point = indicatorStar.EndUpper;
            this.LALower.Point = indicatorStar.Lower;

            this.lbLAIndicator.SetValue(Canvas.TopProperty, indicatorStar.Lower.Y - 12);
            this.lbLAIndicator.Content = LAIndicator.IndicatorValue.ToString("0.00");
        }

        private void InitTK()
        {
            TKIndicator = new Indicator();
            TKIndicatorBinding();
        }

        private void TKIndicatorBinding()
        {
            double Headvalue = 0;
            double Lower = 9.5;
            double Normal = 11.5;
            double Upper = 13.9;
            double Endvalue = 20.0;

            double PositionStar = 0;
            TKIndicator.IndicatorValue = 0;

            double SMMBody = DataAdapter.IsNumber(this.txTK.Text.Trim()) ? Convert.ToDouble(this.txTK.Text.Trim()) : 0;

            TKIndicator.IndicatorValue = SMMBody;


            if (TKIndicator.IndicatorValue < Headvalue)
            {
                PositionStar = 0;
            }
            else if (TKIndicator.IndicatorValue < Lower)
            {
                PositionStar = 60 - (TKIndicator.IndicatorValue / Lower) * 15;
            }
            else if (TKIndicator.IndicatorValue >= Lower && TKIndicator.IndicatorValue < Normal)
            {
                PositionStar = 45 - ((TKIndicator.IndicatorValue - Lower) / (Normal - Lower)) * 15;
            }
            else if (TKIndicator.IndicatorValue >= Normal && TKIndicator.IndicatorValue < Upper)
            {
                PositionStar = 30 - ((TKIndicator.IndicatorValue - Normal) / (Upper - Normal)) * 15;
            }
            else if (TKIndicator.IndicatorValue >= Upper && TKIndicator.IndicatorValue < Endvalue)
            {
                PositionStar = 15 - ((TKIndicator.IndicatorValue - Upper) / (Endvalue - Upper)) * 15;
            }
            else if (TKIndicator.IndicatorValue >= Endvalue)
            {
                PositionStar = 0;
            }


            Indicator.IndicatorTri indicatorStar = new Indicator.IndicatorTri(
                new Point(30, PositionStar - 10 * 0.5),
                new Point(30, PositionStar + 10 * 0.5),
                new Point(40, PositionStar)
                );

            this.TKUpper.StartPoint = indicatorStar.StartUpper;
            this.TKUpperEnd.Point = indicatorStar.EndUpper;
            this.TKLower.Point = indicatorStar.Lower;

            this.lbTKIndicator.SetValue(Canvas.TopProperty, indicatorStar.Lower.Y - 12);
            this.lbTKIndicator.Content = TKIndicator.IndicatorValue.ToString("0.0");
        }

        private void InitRA()
        {
            RAIndicator = new Indicator();
            RAIndicatorBinding();
        }

        private void RAIndicatorBinding()
        {
            double Headvalue = 0;
            double Lower = 1.23;
            double Normal = 1.63;
            double Upper = 2.11;
            double Endvalue = 3.0;

            double PositionStar = 0;
            RAIndicator.IndicatorValue = 0;

            double SMMArmRight = DataAdapter.IsNumber(this.txRA.Text.Trim()) ? Convert.ToDouble(this.txRA.Text.Trim()) : 0;
            RAIndicator.IndicatorValue = SMMArmRight;

            if (RAIndicator.IndicatorValue < Headvalue)
            {
                PositionStar = 0;
            }
            else if (RAIndicator.IndicatorValue < Lower)
            {
                PositionStar = 60 - (RAIndicator.IndicatorValue / Lower) * 15;
            }
            else if (RAIndicator.IndicatorValue >= Lower && RAIndicator.IndicatorValue < Normal)
            {
                PositionStar = 45 - ((RAIndicator.IndicatorValue - Lower) / (Normal - Lower)) * 15;
            }
            else if (RAIndicator.IndicatorValue >= Normal && RAIndicator.IndicatorValue < Upper)
            {
                PositionStar = 30 - ((RAIndicator.IndicatorValue - Normal) / (Upper - Normal)) * 15;
            }
            else if (RAIndicator.IndicatorValue >= Upper && RAIndicator.IndicatorValue < Endvalue)
            {
                PositionStar = 15 - ((RAIndicator.IndicatorValue - Upper) / (Endvalue - Upper)) * 15;
            }
            else if (RAIndicator.IndicatorValue >= Endvalue)
            {
                PositionStar = 0;
            }


            Indicator.IndicatorTri indicatorStar = new Indicator.IndicatorTri(
                new Point(30, PositionStar - 10 * 0.5),
                new Point(30, PositionStar + 10 * 0.5),
                new Point(40, PositionStar)
                );

            this.RAUpper.StartPoint = indicatorStar.StartUpper;
            this.RAUpperEnd.Point = indicatorStar.EndUpper;
            this.RALower.Point = indicatorStar.Lower;

            this.lbRAIndicator.SetValue(Canvas.TopProperty, indicatorStar.Lower.Y - 12);
            this.lbRAIndicator.Content = RAIndicator.IndicatorValue.ToString("0.00");
        }

        private void InitLL()
        {
            LLIndicator = new Indicator();
            LLIndicatorBinding();
        }

        private void LLIndicatorBinding()
        {
            double Headvalue = 0;
            double Lower = 4.19;
            double Normal = 5.26;
            double Upper = 6.55;
            double Endvalue = 10.0;

            double PositionStar = 0;
            LLIndicator.IndicatorValue = 0;

            double SMMLegLeft = DataAdapter.IsNumber(this.txLL.Text.Trim()) ? Convert.ToDouble(this.txLL.Text.Trim()) : 0;
            LLIndicator.IndicatorValue = SMMLegLeft;

            if (LLIndicator.IndicatorValue < Headvalue)
            {
                PositionStar = 0;
            }
            else if (LLIndicator.IndicatorValue < Lower)
            {
                PositionStar = 60 - (LLIndicator.IndicatorValue / Lower) * 15;
            }
            else if (LLIndicator.IndicatorValue >= Lower && LLIndicator.IndicatorValue < Normal)
            {
                PositionStar = 45 - ((LLIndicator.IndicatorValue - Lower) / (Normal - Lower)) * 15;
            }
            else if (LLIndicator.IndicatorValue >= Normal && LLIndicator.IndicatorValue < Upper)
            {
                PositionStar = 30 - ((LLIndicator.IndicatorValue - Normal) / (Upper - Normal)) * 15;
            }
            else if (LLIndicator.IndicatorValue >= Upper && LLIndicator.IndicatorValue < Endvalue)
            {
                PositionStar = 15 - ((LLIndicator.IndicatorValue - Upper) / (Endvalue - Upper)) * 15;
            }
            else if (LLIndicator.IndicatorValue >= Endvalue)
            {
                PositionStar = 0;
            }


            Indicator.IndicatorTri indicatorStar = new Indicator.IndicatorTri(
                new Point(30, PositionStar - 10 * 0.5),
                new Point(30, PositionStar + 10 * 0.5),
                new Point(40, PositionStar)
                );

            this.LLUpper.StartPoint = indicatorStar.StartUpper;
            this.LLUpperEnd.Point = indicatorStar.EndUpper;
            this.LLLower.Point = indicatorStar.Lower;

            this.lbLLIndicator.SetValue(Canvas.TopProperty, indicatorStar.Lower.Y - 12);
            this.lbLLIndicator.Content = LLIndicator.IndicatorValue.ToString("0.00");
        }

        private void InitRL()
        {
            RLIndicator = new Indicator();
            RLIndicatorBinding();
        }

        private void RLIndicatorBinding()
        {
            double Headvalue = 0;
            double Lower = 4.16;
            double Normal = 5.24;
            double Upper = 6.54;
            double Endvalue = 10.0;

            double PositionStar = 0;
            RLIndicator.IndicatorValue = 0;

            double SMMLegRight = DataAdapter.IsNumber(this.txRL.Text.Trim()) ? Convert.ToDouble(this.txRL.Text.Trim()) : 0;
            RLIndicator.IndicatorValue = SMMLegRight;


            if (RLIndicator.IndicatorValue < Headvalue)
            {
                PositionStar = 0;
            }
            else if (RLIndicator.IndicatorValue < Lower)
            {
                PositionStar = 60 - (RLIndicator.IndicatorValue / Lower) * 15;
            }
            else if (RLIndicator.IndicatorValue >= Lower && RLIndicator.IndicatorValue < Normal)
            {
                PositionStar = 45 - ((RLIndicator.IndicatorValue - Lower) / (Normal - Lower)) * 15;
            }
            else if (RLIndicator.IndicatorValue >= Normal && RLIndicator.IndicatorValue < Upper)
            {
                PositionStar = 30 - ((RLIndicator.IndicatorValue - Normal) / (Upper - Normal)) * 15;
            }
            else if (RLIndicator.IndicatorValue >= Upper && RLIndicator.IndicatorValue < Endvalue)
            {
                PositionStar = 15 - ((RLIndicator.IndicatorValue - Upper) / (Endvalue - Upper)) * 15;
            }
            else if (RLIndicator.IndicatorValue >= Endvalue)
            {
                PositionStar = 0;
            }


            Indicator.IndicatorTri indicatorStar = new Indicator.IndicatorTri(
                new Point(30, PositionStar - 10 * 0.5),
                new Point(30, PositionStar + 10 * 0.5),
                new Point(40, PositionStar)
                );

            this.RLUpper.StartPoint = indicatorStar.StartUpper;
            this.RLUpperEnd.Point = indicatorStar.EndUpper;
            this.RLLower.Point = indicatorStar.Lower;

            this.lbRLIndicator.SetValue(Canvas.TopProperty, indicatorStar.Lower.Y - 12);
            this.lbRLIndicator.Content = RLIndicator.IndicatorValue.ToString("0.00");
        }

        private void InitVAT()
        {
            VATIndicator = new Indicator();
            VATIndicatorBinding();
        }

        private void VATIndicatorBinding()
        {
            double Headvalue = 0;
            double Normal = 2.3;
            double Upper = 3.8;
            double Endvalue = 5.0;

            double PositionStar = 0;
            VATIndicator.IndicatorValue = 0;

            double VAT = DataAdapter.IsNumber(this.txVAT.Text.Trim()) ? Convert.ToDouble(this.txVAT.Text.Trim()) : 0;


            VATIndicator.IndicatorValue = VAT;


            if (VATIndicator.IndicatorValue < Headvalue)
            {
                PositionStar = 10;
            }
            else if (VATIndicator.IndicatorValue >= Headvalue && VATIndicator.IndicatorValue < Normal)
            {
                PositionStar = 10 + ((VATIndicator.IndicatorValue - Headvalue) / (Normal - Headvalue)) * (125 - 15);
            }
            else if (VATIndicator.IndicatorValue >= Normal && VATIndicator.IndicatorValue < Upper)
            {
                PositionStar = 125 + ((VATIndicator.IndicatorValue - Normal) / (Upper - Normal)) * (225 - 125);
            }
            else if (VATIndicator.IndicatorValue >= Upper && VATIndicator.IndicatorValue < Endvalue)
            {
                PositionStar = 225 + ((VATIndicator.IndicatorValue - Upper) / (Endvalue - Upper)) * (350 - 225);
            }
            else if (VATIndicator.IndicatorValue >= Endvalue)
            {
                PositionStar = 300;
            }


            Indicator.IndicatorTri indicatorStar = new Indicator.IndicatorTri(
                new Point(PositionStar - 16 * 0.5, 25),
                new Point(PositionStar + 16 * 0.5, 25),
                new Point(PositionStar, 40)
                );

            this.vatUpper.StartPoint = indicatorStar.StartUpper;
            this.vatUpperEnd.Point = indicatorStar.EndUpper;
            this.vatLower.Point = indicatorStar.Lower;

            this.lbVATIndicator.SetValue(Canvas.LeftProperty, indicatorStar.Lower.X - 30);
            this.lbVATIndicator.Content = VATIndicator.IndicatorValue.ToString("0.0");

            if (VATIndicator.IndicatorValue <= Headvalue)
            {
                this.vatPath1.Fill = new SolidColorBrush(Colors.White);
                this.vatPath2.Fill = new SolidColorBrush(Colors.White);
                this.vatPath3.Fill = new SolidColorBrush(Colors.White);
            }

            if (VATIndicator.IndicatorValue > Headvalue && VATIndicator.IndicatorValue <= Normal)
            {
                this.vatPath1.Fill = new SolidColorBrush(Colors.LightGreen);
                this.vatPath2.Fill = new SolidColorBrush(Colors.White);
                this.vatPath3.Fill = new SolidColorBrush(Colors.White);
            }
            if (VATIndicator.IndicatorValue > Normal && VATIndicator.IndicatorValue <= Upper)
            {
                this.vatPath1.Fill = new SolidColorBrush(Colors.White);
                this.vatPath2.Fill = new SolidColorBrush(Colors.Orange);
                this.vatPath3.Fill = new SolidColorBrush(Colors.White);
            }
            if (VATIndicator.IndicatorValue > Upper)
            {
                this.vatPath1.Fill = new SolidColorBrush(Colors.White);
                this.vatPath2.Fill = new SolidColorBrush(Colors.White);
                this.vatPath3.Fill = new SolidColorBrush(Colors.OrangeRed);
            }
        }

        private void InitWC()
        {
            WCIndicator = new Indicator();
            WCIndicatorBinding();
        }

        private void WCIndicatorBinding()
        {
            double Headvalue = 0;
            double Normal = 0.9;
            double Endvalue = 5.0;

            double PositionStar = 0;
            WCIndicator.IndicatorValue = 0;

            double Waistline = DataAdapter.IsNumber(this.txWC.Text.Trim()) ? Convert.ToDouble(this.txWC.Text.Trim()) : 0;

            WCIndicator.IndicatorValue = Waistline;


            if (WCIndicator.IndicatorValue < Headvalue)
            {
                PositionStar = 10;
            }
            else if (WCIndicator.IndicatorValue >= Headvalue && WCIndicator.IndicatorValue < Normal)
            {
                PositionStar = 10 + ((WCIndicator.IndicatorValue - Headvalue) / (Normal - Headvalue)) * (175 - 15);
            }
            else if (WCIndicator.IndicatorValue >= Normal && WCIndicator.IndicatorValue < Endvalue)
            {
                PositionStar = 225 + ((WCIndicator.IndicatorValue - Normal) / (Endvalue - Normal)) * (350 - 175);
            }
            else if (WCIndicator.IndicatorValue >= Endvalue)
            {
                PositionStar = 300;
            }


            Indicator.IndicatorTri indicatorStar = new Indicator.IndicatorTri(
                new Point(PositionStar - 16 * 0.5, 25),
                new Point(PositionStar + 16 * 0.5, 25),
                new Point(PositionStar, 40)
                );

            this.wcUpper.StartPoint = indicatorStar.StartUpper;
            this.wcUpperEnd.Point = indicatorStar.EndUpper;
            this.wcLower.Point = indicatorStar.Lower;

            this.lbWCIndicator.SetValue(Canvas.LeftProperty, indicatorStar.Lower.X - 30);
            this.lbWCIndicator.Content = WCIndicator.IndicatorValue.ToString("0.00");

            if (WCIndicator.IndicatorValue <= Headvalue)
            {
                this.wcPath1.Fill = new SolidColorBrush(Colors.White);
                this.wcPath2.Fill = new SolidColorBrush(Colors.White);
            }

            if (WCIndicator.IndicatorValue > Headvalue && WCIndicator.IndicatorValue <= Normal)
            {
                this.wcPath1.Fill = new SolidColorBrush(Colors.LightGreen);
                this.wcPath2.Fill = new SolidColorBrush(Colors.White);
            }
            if (WCIndicator.IndicatorValue > Normal && WCIndicator.IndicatorValue <= Endvalue)
            {
                this.wcPath1.Fill = new SolidColorBrush(Colors.White);
                this.wcPath2.Fill = new SolidColorBrush(Colors.Orange);
            }
            if (WCIndicator.IndicatorValue > Endvalue)
            {
                this.wcPath1.Fill = new SolidColorBrush(Colors.White);
                this.wcPath2.Fill = new SolidColorBrush(Colors.OrangeRed);
            }
        }


        private void OnBMIWeightChanged(object sender, RoutedEventArgs e)
        {
            double Weight = DataAdapter.IsNumber(bmiWeight.Text.Trim()) ? Convert.ToDouble(bmiWeight.Text.Trim()) : 0;
            double Hight = DataAdapter.IsNumber(bmiHight.Text.Trim()) ? Convert.ToDouble(bmiHight.Text.Trim()) : 0;
            double FM = DataAdapter.IsNumber(txFM.Text.Trim()) ? Convert.ToDouble(txFM.Text.Trim()) : 0;
            if (Hight >= 10)
            {
                Hight = Hight / 100;
            }


            if (Weight > 0 && Hight > 0)
            {
                this.bmiBMI.Text = (Weight / (Hight * Hight)).ToString("0.0");
            }

            BMIIndicatorBinding();
            if (Weight > 0 && FM > 0)
            {
                this.txFM2.Content = ((FM / Weight) * 100.0).ToString("0.0");
            }

        }

        private void OnFMITextChanged(object sender, RoutedEventArgs e)
        {
            double FM = DataAdapter.IsNumber(txFM.Text.Trim()) ? Convert.ToDouble(txFM.Text.Trim()) : 0;
            double Weight = DataAdapter.IsNumber(bmiWeight.Text.Trim()) ? Convert.ToDouble(bmiWeight.Text.Trim()) : 0;
            FMIIndicatorBinding();
            this.txFMI.Text = FMIIndicator.IndicatorValue.ToString("0.0");
            this.txFM2.Content = ((FM /Weight) * 100.0).ToString("0.0");
        }

        private void OnBMIHeightChanged(object sender, RoutedEventArgs e)
        {
            double Hight = DataAdapter.IsNumber(bmiHight.Text.Trim()) ? Convert.ToDouble(bmiHight.Text.Trim()) : 0;
            double Weight = DataAdapter.IsNumber(bmiWeight.Text.Trim()) ? Convert.ToDouble(bmiWeight.Text.Trim()) : 0;

            if (Hight >= 10)
            {
                Hight = Hight / 100;
            }           

            if (Weight > 0 && Hight > 0)
            {
                this.bmiBMI.Text = (Weight / (Hight * Hight)).ToString("0.0");
            }

            BMIIndicatorBinding();
            FMIIndicatorBinding();
            this.txFMI.Text = FMIIndicator.IndicatorValue.ToString("0.0");
        }

        private void OnTBWTextChanged(object sender, RoutedEventArgs e)
        {
            double TBW = DataAdapter.IsNumber(this.txTBW.Text.Trim()) ? Convert.ToDouble(this.txTBW.Text.Trim()) : 0;
            double Weight = DataAdapter.IsNumber(bmiWeight.Text.Trim()) ? Convert.ToDouble(bmiWeight.Text.Trim()) : 0;
            double BCW = DataAdapter.IsNumber(this.txBCW.Text.Trim()) ? Convert.ToDouble(this.txBCW.Text.Trim()) : 0;
            BMIIndicatorBinding();
            TBWIndicatorBinding();
            EBWIndicatorBinding();
            this.txTBW2.Content = ((TBW / Weight) * 100.0).ToString("0.0");
            if (TBW > 0 && BCW > 0)
            {
                this.txEBW.Text = ((BCW / TBW) * 100.0).ToString("0.0");
            }
            ((TextBox)sender).Text = TBW.ToString();
        }

        private void OnBCWTextChanged(object sender, RoutedEventArgs e)
        {
            double BCW = DataAdapter.IsNumber(this.txBCW.Text.Trim()) ? Convert.ToDouble(this.txBCW.Text.Trim()) : 0;
            double TBW = DataAdapter.IsNumber(this.txTBW.Text.Trim()) ? Convert.ToDouble(this.txTBW.Text.Trim()) : 0;
            double Weight = DataAdapter.IsNumber(bmiWeight.Text.Trim()) ? Convert.ToDouble(bmiWeight.Text.Trim()) : 0;
            BMIIndicatorBinding();
            BCWIndicatorBinding();
            EBWIndicatorBinding();
            this.txBCW2.Content = ((BCW / Weight) * 100.0).ToString("0.0");
            if (TBW > 0 && BCW > 0)
            {
                this.txEBW.Text = ((BCW / TBW) * 100.0).ToString("0.0");
            }

            ((TextBox)sender).Text = BCW.ToString();
        }

        private void OnBodyTextChanged(object sender, RoutedEventArgs e)
        {
            double SMMAll = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            BodyIndicatorBinding();
            ((TextBox)sender).Text = SMMAll.ToString();
        }

        private void OnLATextChanged(object sender, RoutedEventArgs e)
        {
            double SMMArmLeft = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            LAIndicatorBinding();
            ((TextBox)sender).Text = SMMArmLeft.ToString();
        }

        private void OnTKTextChanged(object sender, RoutedEventArgs e)
        {
            double SMMBody = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            TKIndicatorBinding();
            ((TextBox)sender).Text = SMMBody.ToString();
        }

        private void OnBodyRAChanged(object sender, RoutedEventArgs e)
        {
            double SMMArmRight = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            RAIndicatorBinding();
            ((TextBox)sender).Text = SMMArmRight.ToString();
        }

        private void OnLLTextChanged(object sender, RoutedEventArgs e)
        {
            double SMMLegLeft = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            LLIndicatorBinding();
            ((TextBox)sender).Text = SMMLegLeft.ToString();
        }

        private void OnRLTextChanged(object sender, RoutedEventArgs e)
        {
            double SMMLegRight = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            RLIndicatorBinding();
            ((TextBox)sender).Text = SMMLegRight.ToString();
        }

        private void OnVATTextChanged(object sender, RoutedEventArgs e)
        {
            double VAT = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            VATIndicatorBinding();
            ((TextBox)sender).Text = VAT.ToString();
        }

        private void OnWCTextChanged(object sender, RoutedEventArgs e)
        {
            double Waistline = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            if (Waistline >= 10)
                Waistline = Waistline / 100.0;            
            this.txWC.Text = Waistline.ToString("0.00");
            WCIndicatorBinding();
        }
       
        private void OnPATextChanged(object sender, RoutedEventArgs e)
        {
            double PA = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;            
            int Age = DataAdapter.Age;
            if (Age < 20)
            {
                Age = 20;
            }
            else if (Age > 65)
            {
                Age = 65;
            }

            PA = PA < 4.5 ? 4.5 : PA;
            PA = PA > 8.0 ? 8.0 : PA;
            double x = 50 + ((Age - 20) / 45.0) * 300.0;
            double y = 160 - ((PA - 4.2) / (8 - 4.2)) * 160.0;

            this.PAPoint.StartPoint = new Point(x, y);
            this.PAMid.Point = new Point(x + 5, y + 5);
            this.PAEnd.Point = this.PAPoint.StartPoint;
        }


    }
}
