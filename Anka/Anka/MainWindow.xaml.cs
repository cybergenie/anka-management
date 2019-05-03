using System;
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

using MySql.Data.MySqlClient;


namespace Anka
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
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
        public MainWindow()
        {
            
            InitializeComponent();
            InitBMI();
            InitFMI();
            InitTBW();
            InitBCW();
            InitEBW();
            InitBody();
            InitLA();
            InitTK();
            InitRA();
            InitLL();
            InitRL();
            InitVAT();
            InitWC();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connString = "server=localhost;database=anka;uid=root;pwd=admin";
            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                conn.Open();
                MessageBox.Show("连接成功！", "测试结果");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        
        
        private void InitBMI()
        {
            BMIIndicator = new Indicator();            
            BMIIndicatorBinding();
        }

        private void  BMIIndicatorBinding()
        {
            double Headvalue = 13.5;
            double Lower = 18.5;
            double Normal = 25.0;
            double Upper = 30.0;
            double Endvalue = 40.0;

            double PositionStar = 0;
            BMIIndicator.IndicatorValue = 0;           

            if (DataAdapter.SizeResult.Hight > 0 && DataAdapter.SizeResult.Hight > 0)
            {
                BMIIndicator.IndicatorValue = DataAdapter.SizeResult.Weight / (DataAdapter.SizeResult.Hight * DataAdapter.SizeResult.Hight);               
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
                new Point(PositionStar - 16 *0.5, 25),
                new Point(PositionStar + 16 *0.5, 25),
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
            if (BMIIndicator.IndicatorValue >= Lower&& BMIIndicator.IndicatorValue <Normal)
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

            if (DataAdapter.PhysiqueResult.FM > 0 && DataAdapter.SizeResult.Hight > 0)
            {
                FMIIndicator.IndicatorValue = DataAdapter.PhysiqueResult.FM / (DataAdapter.SizeResult.Hight * DataAdapter.SizeResult.Hight);               
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
                new Point(PositionStar - 16 *0.5, 25),
                new Point(PositionStar + 16 *0.5, 25),
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
            DataAdapter.PhysiqueResult.TBW = DataAdapter.IsNumber(this.txTBW.Text.Trim()) ? Convert.ToDouble(this.txTBW.Text.Trim()) : 0;
            TBWIndicator.IndicatorValue = DataAdapter.PhysiqueResult.TBW;
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
                new Point(PositionStar - 16 *0.5, 25),
                new Point(PositionStar + 16 *0.5, 25),
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

            DataAdapter.PhysiqueResult.BCW = DataAdapter.IsNumber(this.txBCW.Text.Trim()) ? Convert.ToDouble(this.txBCW.Text.Trim()) : 0;
            BCWIndicator.IndicatorValue = DataAdapter.PhysiqueResult.BCW;
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
                new Point(PositionStar - 16 *0.5, 25),
                new Point(PositionStar + 16 *0.5, 25),
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
           
            EBWIndicator.IndicatorValue = 0;

            if (DataAdapter.PhysiqueResult.BCW > 0 && DataAdapter.PhysiqueResult.TBW > 0)
            {
                EBWIndicator.IndicatorValue = (DataAdapter.PhysiqueResult.BCW / DataAdapter.PhysiqueResult.TBW) * 100;
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
                new Point(PositionStar - 16 *0.5, 25),
                new Point(PositionStar + 16 *0.5, 25),
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

            DataAdapter.PhysiqueResult.SMMAll = DataAdapter.IsNumber(this.txSMM.Text.Trim()) ? Convert.ToDouble(this.txSMM.Text.Trim()) : 0;
            BodyIndicator.IndicatorValue = DataAdapter.PhysiqueResult.SMMAll;     

            if (BodyIndicator.IndicatorValue < Headvalue)
            {
                PositionStar = 0;
            }
            else if (BodyIndicator.IndicatorValue < Lower)
            {
                PositionStar = 120-(BodyIndicator.IndicatorValue / Lower) * 30;
            }
            else if (BodyIndicator.IndicatorValue >= Lower && BodyIndicator.IndicatorValue < Normal)
            {
                PositionStar =90 - ((BodyIndicator.IndicatorValue - Lower) / (Normal - Lower)) * 30;
            }
            else if (BodyIndicator.IndicatorValue >= Normal && BodyIndicator.IndicatorValue < Upper)
            {
                PositionStar = 60 - ((BodyIndicator.IndicatorValue - Normal) / (Upper - Normal)) * 30;
            }
            else if (BodyIndicator.IndicatorValue >= Upper && BodyIndicator.IndicatorValue < Endvalue)
            {
                PositionStar =30 - ((BodyIndicator.IndicatorValue - Upper) / (Endvalue - Upper)) * 30;
            }
            else if (BodyIndicator.IndicatorValue >= Endvalue)
            {
                PositionStar = 0;
            }


            Indicator.IndicatorTri indicatorStar = new Indicator.IndicatorTri(
                new Point(40,PositionStar - 10 *0.5),
                new Point(40,PositionStar + 10 *0.5),
                new Point(50,PositionStar)
                );

            this.BodyUpper.StartPoint = indicatorStar.StartUpper;
            this.BodyUpperEnd.Point = indicatorStar.EndUpper;
            this.BodyLower.Point = indicatorStar.Lower;

            this.lbBodyIndicator.SetValue(Canvas.TopProperty, indicatorStar.Lower.Y-15 );
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

            DataAdapter.PhysiqueResult.SMMArmLeft = DataAdapter.IsNumber(this.txLA.Text.Trim()) ? Convert.ToDouble(this.txLA.Text.Trim()) : 0;

            LAIndicator.IndicatorValue = DataAdapter.PhysiqueResult.SMMArmLeft;


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
                new Point(30, PositionStar - 10 *0.5),
                new Point(30, PositionStar + 10*0.5),
                new Point(40, PositionStar)
                );

            this.LAUpper.StartPoint = indicatorStar.StartUpper;
            this.LAUpperEnd.Point = indicatorStar.EndUpper;
            this.LALower.Point = indicatorStar.Lower;

            this.lbLAIndicator.SetValue(Canvas.TopProperty, indicatorStar.Lower.Y-12 );
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

            DataAdapter.PhysiqueResult.SMMBody = DataAdapter.IsNumber(this.txTK.Text.Trim()) ? Convert.ToDouble(this.txTK.Text.Trim()) : 0;

            TKIndicator.IndicatorValue = DataAdapter.PhysiqueResult.SMMBody;
            

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

            DataAdapter.PhysiqueResult.SMMArmRight = DataAdapter.IsNumber(this.txRA.Text.Trim()) ? Convert.ToDouble(this.txRA.Text.Trim()) : 0;
            RAIndicator.IndicatorValue = DataAdapter.PhysiqueResult.SMMArmRight;
           
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

            DataAdapter.PhysiqueResult.SMMLegLeft = DataAdapter.IsNumber(this.txLL.Text.Trim()) ? Convert.ToDouble(this.txLL.Text.Trim()) : 0;
            LLIndicator.IndicatorValue = DataAdapter.PhysiqueResult.SMMLegLeft;           

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

            DataAdapter.PhysiqueResult.SMMLegRight = DataAdapter.IsNumber(this.txRL.Text.Trim()) ? Convert.ToDouble(this.txRL.Text.Trim()) : 0;
            RLIndicator.IndicatorValue = DataAdapter.PhysiqueResult.SMMLegRight;
           

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

            DataAdapter.PhysiqueResult.VAT = DataAdapter.IsNumber(this.txVAT.Text.Trim()) ? Convert.ToDouble(this.txVAT.Text.Trim()) : 0;


            VATIndicator.IndicatorValue = DataAdapter.PhysiqueResult.VAT;
            

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

            DataAdapter.SizeResult.Waistline = DataAdapter.IsNumber(this.txWC.Text.Trim()) ? Convert.ToDouble(this.txWC.Text.Trim()) : 0;

            WCIndicator.IndicatorValue = DataAdapter.SizeResult.Waistline;
            

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
            DataAdapter.SizeResult.Weight = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            this.txSize2.Text = DataAdapter.SizeResult.Weight.ToString();
            this.bmiWeight.Text = DataAdapter.SizeResult.Weight.ToString();

            if (DataAdapter.SizeResult.Weight > 0 && DataAdapter.SizeResult.Hight > 0)
            {
                this.bmiBMI.Text = (DataAdapter.SizeResult.Weight / (DataAdapter.SizeResult.Hight * DataAdapter.SizeResult.Hight)).ToString("0.0");
            }

            BMIIndicatorBinding();
            if (DataAdapter.SizeResult.Weight > 0 && DataAdapter.PhysiqueResult.FM > 0)
            {
                this.txFM2.Content = ((DataAdapter.PhysiqueResult.FM / DataAdapter.SizeResult.Weight) * 100.0).ToString("0.0");
            }
            
        }

        private void OnFMITextChanged(object sender, RoutedEventArgs e)
        {
            DataAdapter.PhysiqueResult.FM = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            FMIIndicatorBinding();
            this.txFMI.Text = FMIIndicator.IndicatorValue.ToString("0.0");
            this.txFM2.Content = ((DataAdapter.PhysiqueResult.FM / DataAdapter.SizeResult.Weight) * 100.0).ToString("0.0");
        }

        private void OnBMIHeightChanged(object sender, RoutedEventArgs e)
        {
            double temp = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;

            if(temp<10)
            {
                DataAdapter.SizeResult.Hight = temp;
            }
            else if(temp>=10)
            {
                DataAdapter.SizeResult.Hight = temp/100;
            }

            this.txSize1.Text = (DataAdapter.SizeResult.Hight * 100).ToString();
            this.bmiHeight.Text = DataAdapter.SizeResult.Hight.ToString();

            if (DataAdapter.SizeResult.Weight > 0 && DataAdapter.SizeResult.Hight > 0)
            {
                this.bmiBMI.Text = (DataAdapter.SizeResult.Weight / (DataAdapter.SizeResult.Hight * DataAdapter.SizeResult.Hight)).ToString("0.0");
            }

            BMIIndicatorBinding();
            FMIIndicatorBinding();
            this.txFMI.Text = FMIIndicator.IndicatorValue.ToString("0.0");
        }

        private void OnTBWTextChanged(object sender, RoutedEventArgs e)
        {
            DataAdapter.PhysiqueResult.TBW = DataAdapter.IsNumber(this.txTBW.Text.Trim()) ? Convert.ToDouble(this.txTBW.Text.Trim()):0;
            BMIIndicatorBinding();
            TBWIndicatorBinding();
            EBWIndicatorBinding();
            this.txTBW2.Content = ((DataAdapter.PhysiqueResult.TBW / DataAdapter.SizeResult.Weight) * 100.0).ToString("0.0");
            if (DataAdapter.PhysiqueResult.TBW > 0 && DataAdapter.PhysiqueResult.BCW > 0)
            {
                this.txEBW.Text = ((DataAdapter.PhysiqueResult.BCW / DataAdapter.PhysiqueResult.TBW) * 100.0).ToString("0.0");
            }
            ((TextBox)sender).Text = DataAdapter.PhysiqueResult.TBW.ToString();
        }

        private void OnBCWTextChanged(object sender, RoutedEventArgs e)
        {
            DataAdapter.PhysiqueResult.BCW = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            BMIIndicatorBinding();
            BCWIndicatorBinding();
            EBWIndicatorBinding();
            this.txBCW2.Content = ((DataAdapter.PhysiqueResult.BCW / DataAdapter.SizeResult.Weight) * 100.0).ToString("0.0");
            if(DataAdapter.PhysiqueResult.TBW > 0 && DataAdapter.PhysiqueResult.BCW > 0)
            {
                this.txEBW.Text = ((DataAdapter.PhysiqueResult.BCW / DataAdapter.PhysiqueResult.TBW) * 100.0).ToString("0.0");
            }

            ((TextBox)sender).Text = DataAdapter.PhysiqueResult.BCW.ToString();
        }

        private void OnBodyTextChanged(object sender, RoutedEventArgs e)
        {
            DataAdapter.PhysiqueResult.SMMAll = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            BodyIndicatorBinding();
            ((TextBox)sender).Text = DataAdapter.PhysiqueResult.SMMAll.ToString();
        }

        private void OnLATextChanged(object sender, RoutedEventArgs e)
        {
            DataAdapter.PhysiqueResult.SMMArmLeft = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            LAIndicatorBinding();
            ((TextBox)sender).Text = DataAdapter.PhysiqueResult.SMMArmLeft.ToString();
        }

        private void OnTKTextChanged(object sender, RoutedEventArgs e)
        {
            DataAdapter.PhysiqueResult.SMMBody = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            TKIndicatorBinding();
            ((TextBox)sender).Text = DataAdapter.PhysiqueResult.SMMBody.ToString();
        }

        private void OnBodyRAChanged(object sender, RoutedEventArgs e)
        {
            DataAdapter.PhysiqueResult.SMMArmRight = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            RAIndicatorBinding();
            ((TextBox)sender).Text = DataAdapter.PhysiqueResult.SMMArmRight.ToString();
        }

        private void OnLLTextChanged(object sender, RoutedEventArgs e)
        {
            DataAdapter.PhysiqueResult.SMMLegLeft = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            LLIndicatorBinding();
            ((TextBox)sender).Text = DataAdapter.PhysiqueResult.SMMLegLeft.ToString();
        }        

        private void OnRLTextChanged(object sender, RoutedEventArgs e)
        {
            DataAdapter.PhysiqueResult.SMMLegRight = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            RLIndicatorBinding();
            ((TextBox)sender).Text = DataAdapter.PhysiqueResult.SMMLegRight.ToString();
        }

        private void OnVATTextChanged(object sender, RoutedEventArgs e)
        {
            DataAdapter.PhysiqueResult.VAT = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            VATIndicatorBinding();
            ((TextBox)sender).Text = DataAdapter.PhysiqueResult.VAT.ToString();
        }

        private void OnWCTextChanged(object sender, RoutedEventArgs e)
        {
            double temp = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            if (temp < 10)
                DataAdapter.SizeResult.Waistline = temp;
            else if(temp>=10)
                DataAdapter.SizeResult.Waistline = temp/100.0;            
            this.txSize3.Text = (DataAdapter.SizeResult.Waistline * 100.0).ToString();
            this.txWC.Text = DataAdapter.SizeResult.Waistline.ToString("0.00");
            WCIndicatorBinding();
        }

        private void DataConnection_Click(object sender, RoutedEventArgs e)
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();
            databaseConnection.ShowDialog();
            if(DatabaseInfo.ConStatus==true)
            {
                this.ConStatus.Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                this.ConStatus.Background = new SolidColorBrush(Colors.Red);
            }
            

        }

        private void MiNewDoc_Click(object sender, RoutedEventArgs e)
        {
            NewPerson newPerson = new NewPerson();
            newPerson.ShowDialog();
            this.lbBasicName.Content = DataAdapter.Name;
            this.lbBasicNumber.Content = DataAdapter.Number.ToString();
            if(DataAdapter.Male.Contains("男"))
            {
                this.lbBasicMale.Content = "男";
            }
            else if (DataAdapter.Male.Contains("女"))
            {
                this.lbBasicMale.Content = "女";
            }
            this.lbBasicAge.Content = DataAdapter.Age.ToString();
            {
                this.lbLoadName.Content = this.lbBasicName.Content;
                this.lbLoadAge.Content = this.lbBasicAge.Content;
                this.lbLoadMale.Content = this.lbBasicMale.Content;
                this.lbLoadNumber.Content = this.lbBasicNumber.Content+" -";
            }
            {
                this.lbPHQName.Content = this.lbBasicName.Content;
                this.lbPHQAge.Content = this.lbBasicAge.Content;
                this.lbPHQMale.Content = this.lbBasicMale.Content;
                this.lbPHQNumber.Content = this.lbBasicNumber.Content + " -";
            }
            {
                this.lbGADName.Content = this.lbBasicName.Content;
                this.lbGADAge.Content = this.lbBasicAge.Content;
                this.lbGADMale.Content = this.lbBasicMale.Content;
                this.lbGADNumber.Content = this.lbBasicNumber.Content + " -";
            }
            {
                this.lbIPAQName.Content = this.lbBasicName.Content;
                this.lbIPAQAge.Content = this.lbBasicAge.Content;
                this.lbIPAQMale.Content = this.lbBasicMale.Content;
                this.lbIPAQNumber.Content = this.lbBasicNumber.Content + " -";
            }
            {
                this.lbOHQName.Content = this.lbBasicName.Content;
                this.lbOHQAge.Content = this.lbBasicAge.Content;
                this.lbOHQMale.Content = this.lbBasicMale.Content;
                this.lbOHQNumber.Content = this.lbBasicNumber.Content + " -";
            }
            {
                this.lbSPPBName.Content = this.lbBasicName.Content;
                this.lbSPPBAge.Content = this.lbBasicAge.Content;
                this.lbSPPBMale.Content = this.lbBasicMale.Content;
                this.lbSPPBNumber.Content = this.lbBasicNumber.Content + " -";
            }
            {
                this.lbBIName.Content = this.lbBasicName.Content;
                this.lbBIAge.Content = this.lbBasicAge.Content;
                this.lbBIMale.Content = this.lbBasicMale.Content;
                this.lbBINumber.Content = this.lbBasicNumber.Content + " -";
            }

        }

        private void BtBasicSave_Click(object sender, RoutedEventArgs e)
        {
            DataAdapter.BasicInfoResult.Killip = this.txKillip.Text;
            DataAdapter.BasicInfoResult.EF = this.txEF.Text;
            DataAdapter.BasicInfoResult.LV = this.txLV.Text;
            DataAdapter.BasicInfoResult.BasicOther = this.txBasicOther.Text;
            DataAdapter.BasicInfoResult.BasicRisk = CbRiskClick();
            DataAdapter.BasicInfoResult.RiskOther = this.txRisk13.Text;    
            
            if (DataAdapter.IsNumber(this.txPCI.Text) == true)
                DataAdapter.BasicInfoResult.PCI = Convert.ToInt32(this.txPCI.Text);
              
            
            if (DataAdapter.IsNumber(this.txRS.Text) == true)
                DataAdapter.BasicInfoResult.ResidualStenosis = Convert.ToInt32(this.txRS.Text);
           

            if(this.rbDCL.IsChecked==true)
            {
                DataAdapter.BasicInfoResult.DominantCoronary = -1;
            }
            else if(this.rbDCB.IsChecked==true)
            {
                DataAdapter.BasicInfoResult.DominantCoronary = 0;
            }
            else if(this.rbDCR.IsChecked==true)
            {
                DataAdapter.BasicInfoResult.DominantCoronary = 1;
            }
            

            if(this.cbCC.IsChecked==true)
            {
                DataAdapter.BasicInfoResult.CollatCirc = true;
            }
            else
            {
                DataAdapter.BasicInfoResult.CollatCirc = false;
            }
        }

             

        private bool[] CbRiskClick()
        {

            bool[] tempCheck = new bool[13]{
                this.cbRisk1.IsChecked==true?true:false,
                this.cbRisk2.IsChecked==true?true:false,
                this.cbRisk3.IsChecked==true?true:false,
                this.cbRisk4.IsChecked==true?true:false,
                this.cbRisk5.IsChecked==true?true:false,
                this.cbRisk6.IsChecked==true?true:false,
                this.cbRisk7.IsChecked==true?true:false,
                this.cbRisk8.IsChecked==true?true:false,
                this.cbRisk9.IsChecked==true?true:false,
                this.cbRisk10.IsChecked==true?true:false,
                this.cbRisk11.IsChecked==true?true:false,
                this.cbRisk12.IsChecked==true?true:false,
                this.cbRisk13.IsChecked==true?true:false
            };

            return tempCheck;


        }
       

        private void CbCC_Click(object sender, RoutedEventArgs e)
        {
            if(this.cbCC.IsChecked==true)
            {
                this.cbCC.Content = "侧枝循环：有";
            }
            else if(this.cbCC.IsChecked == false)
            {
                this.cbCC.Content = "侧枝循环：无";
            }
        }

        private void BtExcerciseSave_Click(object sender, RoutedEventArgs e)
        {
            if(txExerciseLoop.Text.Trim(' ')=="")
            {
                MessageBox.Show("请输入是第几次运动记录。");                

            }

            DataAdapter.ExerciseNumber = DataAdapter.Number +"-"+ this.txExerciseLoop.Text.Trim();

            DataAdapter.ExerciseResult.Date = new string[9] { this.dpBedup1.Text.Trim(),
                this.dpBedup2.Text.Trim(),
                this.dpBedup3.Text.Trim(),
                this.dpBedup4.Text.Trim(),
                this.dpInRoom1.Text.Trim(),
                this.dpInRoom2.Text.Trim(),
                this.dpOutRoom1.Text.Trim(),
                this.dpOutRoom2.Text.Trim(),
                this.dpOutRoom3.Text.Trim()
            };

            int[] bpLower = new int[9]; int[] bpUpper = new int[9];
            string[] txBPUp = new string[9]{this.txBPBedUp1.Text.ToString(),
                this.txBPBedUp2.Text.ToString(),
                this.txBPBedUp3.Text.ToString(),
                this.txBPBedUp4.Text.ToString(),
                this.txBPInRoom1.Text.ToString(),
                this.txBPInRoom2.Text.ToString(),
                this.txBPOutRoom1.Text.ToString(),
                this.txBPOutRoom2.Text.ToString(),
                this.txBPOutRoom3.Text.ToString()
            };
            for (int i =0;i<9;i++)
            {
                DataAdapter.GetBloodPressure(txBPUp[i], bpLower[i], bpUpper[i]);
            }
            DataAdapter.ExerciseResult.BloodPressureLower = bpLower;
            DataAdapter.ExerciseResult.BloodPressureUpper = bpUpper;

            int[] HeartRate = new int[9];
            string[] txBMP = new string[9] {this.txBMPBedUp1.Text.Trim(),
                this.txBMPBedUp2.Text.Trim(),
                this.txBMPBedUp3.Text.Trim(),
                this.txBMPBedUp4.Text.Trim(),
                this.txBMPInRoom1.Text.Trim(),
                this.txBMPInRoom2.Text.Trim(),
                this.txBMPOutRoom1.Text.Trim(),
                this.txBMPOutRoom2.Text.Trim(),
                this.txBMPOutRoom3.Text.Trim()
            };
            for(int i=0;i<9;i++)
            {
                HeartRate[i] = DataAdapter.IsNumber(txBMP[i]) ? Convert.ToInt32(txBMP[i]) : 0;
            }
            DataAdapter.ExerciseResult.HeartRate = HeartRate;

            int[] BloodOxygen = new int[9];
            string[] txBloodOxygen = new string[9] {this.txBOBedUp1.Text.Trim(),
                this.txBOBedUp2.Text.Trim(),
                this.txBOBedUp3.Text.Trim(),
                this.txBOBedUp4.Text.Trim(),
                this.txBOInRoom1.Text.Trim(),
                this.txBOInRoom2.Text.Trim(),
                this.txBOOutRoom1.Text.Trim(),
                this.txBOOutRoom2.Text.Trim(),
                this.txBOOutRoom3.Text.Trim()
            };
            for (int i = 0; i < 9; i++)
            {
                BloodOxygen[i] = DataAdapter.IsNumber(txBloodOxygen[i]) ? Convert.ToInt32(txBloodOxygen[i]) : 0;
            }
            DataAdapter.ExerciseResult.BloodOxygen = BloodOxygen;

            int[] BorgIndex = new int[9];
            string[] txBorgIndex = new string[9] {this.txBorgBedUp1.Text.Trim(),
                this.txBorgBedUp2.Text.Trim(),
                this.txBorgBedUp3.Text.Trim(),
                this.txBorgBedUp4.Text.Trim(),
                this.txBorgInRoom1.Text.Trim(),
                this.txBorgInRoom2.Text.Trim(),
                this.txBorgOutRoom1.Text.Trim(),
                this.txBorgOutRoom2.Text.Trim(),
                this.txBorgOutRoom3.Text.Trim()
            };
            for (int i = 0; i < 9; i++)
            {
                BorgIndex[i] = DataAdapter.IsNumber(txBorgIndex[i]) ? Convert.ToInt32(txBorgIndex[i]) : 0;
            }
            DataAdapter.ExerciseResult.BorgIndex = BorgIndex;

            //string[] Remarks = new string[9];
            string[] Remarks = new string[9] {this.txAddBedUp1.Text.Trim(),
                this.txAddBedUp2.Text.Trim(),
                this.txAddBedUp3.Text.Trim(),
                this.txAddBedUp4.Text.Trim(),
                this.txAddInRoom1.Text.Trim(),
                this.txAddInRoom2.Text.Trim(),
                this.txAddOutRoom1.Text.Trim(),
                this.txAddOutRoom2.Text.Trim(),
                this.txAddOutRoom3.Text.Trim()
            };           
            DataAdapter.ExerciseResult.Remarks = Remarks;

            string[] ECGs = new string[9] {this.txECGBedUp1.Text.Trim(),
                this.txECGBedUp2.Text.Trim(),
                this.txECGBedUp3.Text.Trim(),
                this.txECGBedUp4.Text.Trim(),
                this.txECGInRoom1.Text.Trim(),
                this.txECGInRoom2.Text.Trim(),
                this.txECGOutRoom1.Text.Trim(),
                this.txECGOutRoom2.Text.Trim(),
                this.txECGOutRoom3.Text.Trim()
            };
            DataAdapter.ExerciseResult.ECGs = ECGs;

            RadioButton[] CheckYes = new RadioButton[9]{this.rbBedUpYes1,
                this.rbBedUpYes2,
                this.rbBedUpYes3,
                this.rbBedUpYes4,
                this.rbInRoomYes1,
                this.rbInRoomYes2,
                this.rbOutRoomYes1,
                this.rbOutRoomYes2,
                this.rbOutRoomYes3
            };
            RadioButton[] CheckNo = new RadioButton[9]{this.rbBedUpNo1,
                this.rbBedUpNo2,
                this.rbBedUpNo3,
                this.rbBedUpNo4,
                this.rbInRoomNo1,
                this.rbInRoomNo2,
                this.rbOutRoomNo1,
                this.rbOutRoomNo2,
                this.rbOutRoomNo3
            };
            for(int i=0;i<9;i++)
            {
                if (CheckYes[i].IsChecked == true)
                    DataAdapter.ExerciseResult.Checks[i] = true;
                else if (this.rbBedUpNo1.IsChecked == true)
                    DataAdapter.ExerciseResult.Checks[i]  = false;
            }   
        }

        private void TxBloodPressure_GotFocus(object sender, RoutedEventArgs e)
        {
            if(((TextBox)sender).Text.Trim(' ')=="/")
            {
                ((TextBox)sender).Text = "";
            }
        }

        private void TxBloodPressure_LostFocus(object sender, RoutedEventArgs e)
        {
            if(((TextBox)sender).Text.Trim(' ' )=="")
            {
                ((TextBox)sender).Text = "/";
            }
        }

        private void BtPHQSave_Click(object sender, RoutedEventArgs e)
        {
            DataAdapter.PHQNumber = DataAdapter.Number + "-" + this.txPHQLoop.Text.Trim();
            DataAdapter.PHQResult = PHQData();
        }

        private int[] PHQData()
        {
            int[] Data = new int[9];
            RadioButton[,] rbPHQs = new RadioButton[9, 4]
               {{this.rbPHQ11,this.rbPHQ12,this.rbPHQ13,this.rbPHQ14},
                {this.rbPHQ21,this.rbPHQ22,this.rbPHQ23,this.rbPHQ24 },
                {this.rbPHQ31,this.rbPHQ32,this.rbPHQ33,this.rbPHQ34 },
                {this.rbPHQ41,this.rbPHQ42,this.rbPHQ43,this.rbPHQ44 },
                {this.rbPHQ51,this.rbPHQ52,this.rbPHQ53,this.rbPHQ54 },
                {this.rbPHQ61,this.rbPHQ62,this.rbPHQ63,this.rbPHQ64 },
                {this.rbPHQ71,this.rbPHQ72,this.rbPHQ73,this.rbPHQ74 },
                {this.rbPHQ81,this.rbPHQ82,this.rbPHQ83,this.rbPHQ84 },
                {this.rbPHQ91,this.rbPHQ92,this.rbPHQ93,this.rbPHQ94 }
               };
            for(int i=0;i<9;i++)
            {
                int temp = -1;
                for (int j=0;j<4;j++)
                {
                    if (rbPHQs[i, j].IsChecked == true)
                        temp = j;
                }
                Data[i] = temp;
            }

            return Data;
        }

        private int[] GADData()
        {
            int[] Data = new int[7];
            RadioButton[,] rbGADs = new RadioButton[7, 4]
               {{this.rbGAD11,this.rbGAD12,this.rbGAD13,this.rbGAD14},
                {this.rbGAD21,this.rbGAD22,this.rbGAD23,this.rbGAD24 },
                {this.rbGAD31,this.rbGAD32,this.rbGAD33,this.rbGAD34 },
                {this.rbGAD41,this.rbGAD42,this.rbGAD43,this.rbGAD44 },
                {this.rbGAD51,this.rbGAD52,this.rbGAD53,this.rbGAD54 },
                {this.rbGAD61,this.rbGAD62,this.rbGAD63,this.rbGAD64 },
                {this.rbGAD71,this.rbGAD72,this.rbGAD73,this.rbGAD74 }                
               };
            for (int i = 0; i < 7; i++)
            {
                int temp = -1;
                for (int j = 0; j < 4; j++)
                {
                    if (rbGADs[i, j].IsChecked == true)
                        temp = j;
                }
                Data[i] = temp;
            }

            return Data;
        }


        private void RbPHQ_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            int[] Data = PHQData();
            foreach (int temp in Data)
            {
                if (temp >= 0)
                result += temp;
            }

            if (result >= 0 && result <= 4)
                this.rbPHQResult1.IsChecked = true;
            if (result >= 5 && result <= 9)
                this.rbPHQResult2.IsChecked = true;
            if (result >= 10 && result <= 13)
                this.rbPHQResult3.IsChecked = true;
            if (result >= 14 && result <= 18)
                this.rbPHQResult4.IsChecked = true;
            if (result >= 19 )
                this.rbPHQResult5.IsChecked = true;

            this.lbPHQResult.Content = result.ToString();
        }

        private void RbGAD_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            int[] Data = GADData();
            foreach (int temp in Data)
            {
                if (temp >= 0)
                    result += temp;
            }                       

            this.lbGADResult.Content = result.ToString();
        }

        private void BtGADSave_Click(object sender, RoutedEventArgs e)
        {
            DataAdapter.GADNumber = DataAdapter.Number + "-" + this.txGADLoop.Text.Trim();
            DataAdapter.GADResult = GADData();
        }

        private void BtIPAQSave_Click(object sender, RoutedEventArgs e)
        {
            DataAdapter.IPAQNumber = DataAdapter.Number + "-" + this.txIPAQLoop.Text.Trim();

            if (this.rbIPAQYes.IsChecked == true)
                DataAdapter.IPAQResult.IPAQ0 = true;
            if (this.rbIPAQNo.IsChecked == false)
                DataAdapter.IPAQResult.IPAQ0 = false;
            DataAdapter.IPAQResult.IPAQ1 = DataAdapter.IsNumber(this.txIPAQ1.Text.Trim()) ? Convert.ToInt32(this.txIPAQ1.Text.Trim()) : 0;
            DataAdapter.IPAQResult.IPAQ2 = DataAdapter.IsNumber(this.txIPAQ2.Text.Trim()) ? Convert.ToInt32(this.txIPAQ2.Text.Trim()) : 0;
            DataAdapter.IPAQResult.IPAQ3 = DataAdapter.IsNumber(this.txIPAQ3.Text.Trim()) ? Convert.ToInt32(this.txIPAQ3.Text.Trim()) : 0;
            DataAdapter.IPAQResult.IPAQ4 = DataAdapter.IsNumber(this.txIPAQ4.Text.Trim()) ? Convert.ToInt32(this.txIPAQ4.Text.Trim()) : 0;

            RadioButton[] rbIPAQ5 = new RadioButton[7] { this.rbIPAQ51, this.rbIPAQ52, this.rbIPAQ53, this.rbIPAQ54, this.rbIPAQ55, this.rbIPAQ56, this.rbIPAQ57 };
            for(int i=0;i<7;i++)
            {
                if (rbIPAQ5[i].IsChecked == true)
                    DataAdapter.IPAQResult.IPAQ5 = i;
            }

        }

        private void TxIPAQ4_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.txIPAQ4.Text.Contains('h')|| this.txIPAQ4.Text.Contains('H'))
                this.txIPAQ4.Text = (Convert.ToInt32(this.txIPAQ4.Text.Trim('h')) * 60).ToString();
        }

        private void BtOHQSave_Click(object sender, RoutedEventArgs e)
        {
            DataAdapter.OHQNumber = DataAdapter.Number + "-" + this.txOHQLoop.Text.Trim();
            OHQDataSave();
        }

        private void OHQDataSave()
        {
            RadioButton[] rbOHQ1 = new RadioButton[4] { this.rbOHQ11, this.rbOHQ12, this.rbOHQ13, this.rbOHQ14 };
            RadioButton[] rbOHQ2 = new RadioButton[3] { this.rbOHQ21, this.rbOHQ22, this.rbOHQ23 };
            RadioButton[] rbOHQ3 = new RadioButton[6] { this.rbOHQ31, this.rbOHQ32, this.rbOHQ33, this.rbOHQ34, this.rbOHQ35, this.rbOHQ36 };
            RadioButton[] rbOHQ4 = new RadioButton[3] { this.rbOHQ41, this.rbOHQ42, this.rbOHQ43 };
            RadioButton[] rbOHQ5 = new RadioButton[4] { this.rbOHQ51, this.rbOHQ52, this.rbOHQ53, this.rbOHQ54 };
            RadioButton[] rbOHQ6 = new RadioButton[3] { this.rbOHQ61, this.rbOHQ62, this.rbOHQ63 };
            RadioButton[] rbOHQ7 = new RadioButton[3] { this.rbOHQ71, this.rbOHQ72, this.rbOHQ73 };
            RadioButton[] rbOHQ8 = new RadioButton[3] { this.rbOHQ81, this.rbOHQ82, this.rbOHQ83 };
            RadioButton[] rbOHQ9 = new RadioButton[2] { this.rbOHQ91, this.rbOHQ92 };

            for(int i=0;i<rbOHQ1.Length;i++)
            {
                if (rbOHQ1[i].IsChecked == true)
                    DataAdapter.OHQResult.OHQ1 = i.ToString();
            }

            for (int i = 0; i < rbOHQ2.Length; i++)
            {
                if (rbOHQ2[i].IsChecked == true)
                    DataAdapter.OHQResult.OHQ2 = i.ToString();
            }

            for (int i = 0; i < rbOHQ3.Length; i++)
            {
                if (rbOHQ3[i].IsChecked == true)
                    DataAdapter.OHQResult.OHQ3 = i.ToString();
            }

            
            for (int i = 0; i < rbOHQ4.Length; i++)
            {
                if (rbOHQ4[i].IsChecked == true)
                {
                    switch (i)
                    {
                        case 0:
                            DataAdapter.OHQResult.OHQ4 = "A-" + this.txOHQ41.Text.Trim();
                            break;
                        case 1:
                            DataAdapter.OHQResult.OHQ4 = "B-" + this.txOHQ42.Text.Trim();
                            break;
                        case 2:
                            DataAdapter.OHQResult.OHQ4 = "C-0" ;
                            break;
                    }
                }
            }

            for (int i = 0; i < rbOHQ5.Length; i++)
            {
                if (rbOHQ5[i].IsChecked == true)
                    DataAdapter.OHQResult.OHQ5 = i.ToString();
            }

            for (int i = 0; i < rbOHQ6.Length; i++)
            {
                if (rbOHQ6[i].IsChecked == true)
                {
                    switch (i)
                    {
                        case 0:
                            DataAdapter.OHQResult.OHQ6 = "0";
                            break;
                        case 1:
                            DataAdapter.OHQResult.OHQ6 = this.txOHQ62.Text.Trim();
                            break;
                        case 2:
                            DataAdapter.OHQResult.OHQ6 = "99";
                            break;
                    }
                }
            }

            for (int i = 0; i < rbOHQ7.Length; i++)
            {
                if (rbOHQ7[i].IsChecked == true)
                    DataAdapter.OHQResult.OHQ7 = i.ToString();
            }

            for (int i = 0; i < rbOHQ8.Length; i++)
            {
                if (rbOHQ8[i].IsChecked == true)
                    DataAdapter.OHQResult.OHQ8 = i.ToString();
            }

            for (int i = 0; i < rbOHQ9.Length; i++)
            {
                if (rbOHQ9[i].IsChecked == true)
                {
                    switch (i)
                    {
                        case 0:
                            DataAdapter.OHQResult.OHQ9 = "A-0-0";
                            break;
                        case 1:
                            DataAdapter.OHQResult.OHQ9 = "B-"+this.txOHQ91.Text.Trim()+"-"+ this.txOHQ92.Text.Trim();
                            break;                        
                    }
                }
            }

        }

        private void RbOHQ62_Click(object sender, RoutedEventArgs e)
        {
            if (this.rbOHQ62.IsChecked == true)
            {
                this.txOHQ62.IsEnabled = true;
            }
            else
            {
                this.txOHQ62.Text = "";
                this.txOHQ62.IsEnabled = false;
            }
        }

        private void RbOHQ4_Click(object sender, RoutedEventArgs e)
        {
            if (this.rbOHQ41.IsChecked == true)
                this.txOHQ41.IsEnabled = true;
            else
            {
                this.txOHQ41.Text = "";
                this.txOHQ41.IsEnabled = false;
            }

            if (this.rbOHQ42.IsChecked == true)
                this.txOHQ42.IsEnabled = true;
            else
            {
                this.txOHQ42.Text = "";
                this.txOHQ42.IsEnabled = false;
            }
        }

        private void BtSPPBSave_Click(object sender, RoutedEventArgs e)
        {
            SPPBDataSave();
            BalanceCapabilitySave();
            SizeResultSave();
            VitalsResultSave();
            GripStrengthResultSave();
            LapStrengthResultSave();
        }

        private void SPPBDataSave()
        {
            if (this.rbSPPBYes1.IsChecked == true)
            {
                DataAdapter.SPPBResult.BalanceTesting1 = "A-0-0";
            }
            if (this.rbSPPBNo1.IsChecked == true)
            {
                DataAdapter.SPPBResult.BalanceTesting1 = "A-" + this.txSPPB11.Text.Trim() + "-" + this.txSPPB12.Text.Trim();
            }

            if (this.rbSPPBYes2.IsChecked == true)
            {
                DataAdapter.SPPBResult.BalanceTesting2 = "A-0-0";
            }
            if (this.rbSPPBNo2.IsChecked == true)
            {
                DataAdapter.SPPBResult.BalanceTesting2 = "A-" + this.txSPPB21.Text.Trim() + "-" + this.txSPPB22.Text.Trim();
            }

            if (this.rbSPPBYes3.IsChecked == true)
            {
                DataAdapter.SPPBResult.BalanceTesting3 = "A-0-0";
            }
            if (this.rbSPPBNo3.IsChecked == true)
            {
                DataAdapter.SPPBResult.BalanceTesting3 = "A-" + this.txSPPB31.Text.Trim() + "-" + this.txSPPB32.Text.Trim();
            }

            if (this.rbSPPBYes4.IsChecked == true)
            {
                DataAdapter.SPPBResult.walkingTesting1 = "A-" + this.txSPPB41.Text.Trim() + "-" + this.txSPPB42.Text.Trim();
                DataAdapter.SPPBResult.walkingTesting2 = "A-" + this.txSPPB51.Text.Trim() + "-" + this.txSPPB52.Text.Trim();
            }
            if (this.rbSPPBNo4.IsChecked == true)
            {
                DataAdapter.SPPBResult.walkingTesting1 = "B-" + this.txSPPB41.Text.Trim() + "-" + this.txSPPB42.Text.Trim();
                DataAdapter.SPPBResult.walkingTesting2 = "B-" + this.txSPPB51.Text.Trim() + "-" + this.txSPPB52.Text.Trim();
            }

            if (this.rbSPPBYes5.IsChecked == true)
            {
                DataAdapter.SPPBResult.SitUpTesting = "A-" + this.txSPPB61.Text.Trim() + "-" + this.txSPPB62.Text.Trim();
            }
            if (this.rbSPPBNo5.IsChecked == true)
            {
                DataAdapter.SPPBResult.SitUpTesting = "B-0-" + this.txSPPB7.Text.Trim();
            }

        }
        private void BalanceCapabilitySave()
        {
            if (this.rbBalanceYes.IsChecked == true)
            {
                DataAdapter.BalanceCapabilityResult.TUG = "A-" + this.txBalance11.Text.Trim() + "-" + this.txBalance12.Text.Trim();
            }
            if (this.rbBalanceNo.IsChecked == true)
            {
                DataAdapter.BalanceCapabilityResult.TUG = "B-" + this.txBalance11.Text.Trim() + "-" + this.txBalance12.Text.Trim();
            }
            DataAdapter.BalanceCapabilityResult.FRTLeft1 = this.txBalance21.Text.Trim();
            DataAdapter.BalanceCapabilityResult.FRTLeft2 = this.txBalance22.Text.Trim();
            DataAdapter.BalanceCapabilityResult.FRTRight1 = this.txBalance31.Text.Trim();
            DataAdapter.BalanceCapabilityResult.FRTRight2 = this.txBalance32.Text.Trim();
            DataAdapter.BalanceCapabilityResult.SFO1 = this.txBalance41.Text.Trim();
            DataAdapter.BalanceCapabilityResult.SFO2 = this.txBalance42.Text.Trim();
            DataAdapter.BalanceCapabilityResult.OneFootLeft1 = this.txBalance51.Text.Trim();
            DataAdapter.BalanceCapabilityResult.OneFootLeft2 = this.txBalance52.Text.Trim();
            DataAdapter.BalanceCapabilityResult.OneFootRight1 = this.txBalance61.Text.Trim();
            DataAdapter.BalanceCapabilityResult.OneFootRight2 = this.txBalance62.Text.Trim();
        }
        private void SizeResultSave()
        {
            DataAdapter.SizeResult.Hight = DataAdapter.IsNumber(this.txSize1.Text.Trim()) ? Convert.ToInt32(this.txSize1.Text.Trim()) : 0;
            DataAdapter.SizeResult.Weight = DataAdapter.IsNumber(this.txSize2.Text.Trim()) ? Convert.ToInt32(this.txSize2.Text.Trim()) : 0;
            DataAdapter.SizeResult.Waistline = DataAdapter.IsNumber(this.txSize3.Text.Trim()) ? Convert.ToInt32(this.txSize3.Text.Trim()) : 0;
            DataAdapter.SizeResult.Hipline = DataAdapter.IsNumber(this.txSize4.Text.Trim()) ? Convert.ToInt32(this.txSize4.Text.Trim()) : 0;
            DataAdapter.SizeResult.ArmlineLeft = DataAdapter.IsNumber(this.txSize5.Text.Trim()) ? Convert.ToInt32(this.txSize5.Text.Trim()) : 0;
            DataAdapter.SizeResult.ArmlineRight = DataAdapter.IsNumber(this.txSize6.Text.Trim()) ? Convert.ToInt32(this.txSize6.Text.Trim()) : 0;
            DataAdapter.SizeResult.LeglineLeft = DataAdapter.IsNumber(this.txSize7.Text.Trim()) ? Convert.ToInt32(this.txSize7.Text.Trim()) : 0;
            DataAdapter.SizeResult.LeglineRight = DataAdapter.IsNumber(this.txSize8.Text.Trim()) ? Convert.ToInt32(this.txSize8.Text.Trim()) : 0;
        }
        private void VitalsResultSave()
        {
            DataAdapter.VitalsResult.BloodPressureLower = DataAdapter.IsNumber(this.txVitals1.Text.Trim()) ? Convert.ToInt32(this.txVitals1.Text.Trim()) : 0;
            DataAdapter.VitalsResult.BloodPressureUpper = DataAdapter.IsNumber(this.txVitals2.Text.Trim()) ? Convert.ToInt32(this.txVitals2.Text.Trim()) : 0;
            DataAdapter.VitalsResult.HeartRate = DataAdapter.IsNumber(this.txVitals3.Text.Trim()) ? Convert.ToInt32(this.txVitals3.Text.Trim()) : 0;
            DataAdapter.VitalsResult.Temperature = DataAdapter.IsNumber(this.txVitals4.Text.Trim()) ? Convert.ToDouble(this.txVitals4.Text.Trim()) : 0;
            DataAdapter.VitalsResult.Breathe = DataAdapter.IsNumber(this.txVitals5.Text.Trim()) ? Convert.ToInt32(this.txVitals5.Text.Trim()) : 0;
        }
        private void GripStrengthResultSave()
        {
            DataAdapter.GripStrengthResult.GripStrengthLeft1 = DataAdapter.IsNumber(this.txGripStrength1.Text.Trim()) ? Convert.ToDouble(this.txGripStrength1.Text.Trim()) : 0;
            DataAdapter.GripStrengthResult.GripStrengthLeft2 = DataAdapter.IsNumber(this.txGripStrength2.Text.Trim()) ? Convert.ToDouble(this.txGripStrength2.Text.Trim()) : 0;
            DataAdapter.GripStrengthResult.GripStrengthRight1 = DataAdapter.IsNumber(this.txGripStrength3.Text.Trim()) ? Convert.ToDouble(this.txGripStrength3.Text.Trim()) : 0;
            DataAdapter.GripStrengthResult.GripStrengthRight2 = DataAdapter.IsNumber(this.txGripStrength4.Text.Trim()) ? Convert.ToDouble(this.txGripStrength4.Text.Trim()) : 0;
            if(this.rbGripStrengthYes1.IsChecked ==true)
            {
                DataAdapter.GripStrengthResult.LeftHandHurt = true;
            }
            else if(this.rbGripStrengthNo1.IsChecked == true)
            {
                DataAdapter.GripStrengthResult.LeftHandHurt = false;
            }
            else
            {
                MessageBox.Show("请确定左手外伤情况。");
            }

            if (this.rbGripStrengthYes2.IsChecked == true)
            {
                DataAdapter.GripStrengthResult.RightHandHurt = true;
            }
            else if (this.rbGripStrengthNo2.IsChecked == true)
            {
                DataAdapter.GripStrengthResult.RightHandHurt = false;
            }
            else
            {
                MessageBox.Show("请确定右手外伤情况。");
            }

        }
        private void LapStrengthResultSave()
        {
            DataAdapter.LapStrengthResult.LapStrengthLeft1 = DataAdapter.IsNumber(this.txLapStrength1.Text.Trim()) ? Convert.ToDouble(this.txLapStrength1.Text.Trim()) : 0;
            DataAdapter.LapStrengthResult.LapStrengthLeft2 = DataAdapter.IsNumber(this.txLapStrength2.Text.Trim()) ? Convert.ToDouble(this.txLapStrength2.Text.Trim()) : 0;
            DataAdapter.LapStrengthResult.LapStrengthRight1 = DataAdapter.IsNumber(this.txLapStrength3.Text.Trim()) ? Convert.ToDouble(this.txLapStrength3.Text.Trim()) : 0;
            DataAdapter.LapStrengthResult.LapStrengthRight2 = DataAdapter.IsNumber(this.txLapStrength4.Text.Trim()) ? Convert.ToDouble(this.txLapStrength4.Text.Trim()) : 0;
            if (this.rbLapStrengthYes1.IsChecked == true)
            {
                DataAdapter.LapStrengthResult.LeftLapHurt = true;
            }
            else if (this.rbLapStrengthNo1.IsChecked == true)
            {
                DataAdapter.LapStrengthResult.LeftLapHurt = false;
            }
            else
            {
                MessageBox.Show("请确定伸膝力左外伤等情况。");
            }

            if (this.rbLapStrengthYes2.IsChecked == true)
            {
                DataAdapter.LapStrengthResult.RightLapHurt = true;
            }
            else if (this.rbLapStrengthNo2.IsChecked == true)
            {
                DataAdapter.LapStrengthResult.RightLapHurt = false;
            }
            else
            {
                MessageBox.Show("请确定伸膝力右外伤情况。");
            }

        }

        private void BtPhysiqueSave_Click(object sender, RoutedEventArgs e)
        {
            DataAdapter.PhysiqueNumber = DataAdapter.Number + "-" + this.txPhysiqueLoop.Text.Trim();
            PhysiqueResultSave();
        }

        private void PhysiqueResultSave()
        {
            DataAdapter.PhysiqueResult.FM = DataAdapter.IsNumber(this.txFM.Text.Trim()) ? Convert.ToDouble(this.txFM.Text.Trim()) : 0;
            DataAdapter.PhysiqueResult.PA = DataAdapter.IsNumber(this.txPA.Text.Trim()) ? Convert.ToDouble(this.txPA.Text.Trim()) : 0;
            DataAdapter.PhysiqueResult.SMMAll = DataAdapter.IsNumber(this.txSMM.Text.Trim()) ? Convert.ToDouble(this.txSMM.Text.Trim()) : 0;
            DataAdapter.PhysiqueResult.SMMArmLeft = DataAdapter.IsNumber(this.txLA.Text.Trim()) ? Convert.ToDouble(this.txLA.Text.Trim()) : 0;
            DataAdapter.PhysiqueResult.SMMArmRight = DataAdapter.IsNumber(this.txRA.Text.Trim()) ? Convert.ToDouble(this.txRA.Text.Trim()) : 0;
            DataAdapter.PhysiqueResult.SMMBody = DataAdapter.IsNumber(this.txTK.Text.Trim()) ? Convert.ToDouble(this.txTK.Text.Trim()) : 0;
            DataAdapter.PhysiqueResult.SMMLegLeft = DataAdapter.IsNumber(this.txLL.Text.Trim()) ? Convert.ToDouble(this.txLL.Text.Trim()) : 0;
            DataAdapter.PhysiqueResult.SMMLegRight = DataAdapter.IsNumber(this.txRL.Text.Trim()) ? Convert.ToDouble(this.txRL.Text.Trim()) : 0;

            DataAdapter.PhysiqueResult.BCW = DataAdapter.IsNumber(this.txBCW.Text.Trim()) ? Convert.ToDouble(this.txBCW.Text.Trim()) : 0;
            DataAdapter.PhysiqueResult.TBW = DataAdapter.IsNumber(this.txTBW.Text.Trim()) ? Convert.ToDouble(this.txTBW.Text.Trim()) : 0;

            DataAdapter.PhysiqueResult.VAT = DataAdapter.IsNumber(this.txVAT.Text.Trim()) ? Convert.ToDouble(this.txVAT.Text.Trim()) : 0;
            DataAdapter.PhysiqueResult.PA = DataAdapter.IsNumber(this.txPA.Text.Trim()) ? Convert.ToDouble(this.txPA.Text.Trim()) : 0;
            DataAdapter.PhysiqueResult.PAPercent = DataAdapter.IsNumber(this.txPAPercent.Text.Trim()) ? Convert.ToDouble(this.txPAPercent.Text.Trim()) : 0;
        }



        private void RbSPPB1_Click(object sender, RoutedEventArgs e)
        {
            if(this.rbSPPBNo1.IsChecked==true)
            {
                this.txSPPB11.IsEnabled = true;
                this.txSPPB12.IsEnabled = true;
            }
            else
            {
                this.txSPPB11.Text = "";
                this.txSPPB12.Text = "";
                this.txSPPB11.IsEnabled = false;
                this.txSPPB12.IsEnabled = false;
            }
        }

        private void RbSPPB2_Click(object sender, RoutedEventArgs e)
        {
            if (this.rbSPPBNo2.IsChecked == true)
            {
                this.txSPPB21.IsEnabled = true;
                this.txSPPB22.IsEnabled = true;
            }
            else
            {
                this.txSPPB21.Text = "";
                this.txSPPB22.Text = "";
                this.txSPPB21.IsEnabled = false;
                this.txSPPB22.IsEnabled = false;
            }
        }

        private void RbSPPB3_Click(object sender, RoutedEventArgs e)
        {
            if (this.rbSPPBNo3.IsChecked == true)
            {
                this.txSPPB31.IsEnabled = true;
                this.txSPPB32.IsEnabled = true;
            }
            else
            {
                this.txSPPB31.Text = "";
                this.txSPPB32.Text = "";
                this.txSPPB31.IsEnabled = false;
                this.txSPPB32.IsEnabled = false;
            }
        }

        private void RbSPPB5_Click(object sender, RoutedEventArgs e)
        {
            if (this.rbSPPBYes5.IsChecked == true)
            {
                this.txSPPB61.IsEnabled = true;
                this.txSPPB62.IsEnabled = true;
                this.txSPPB7.Text = "";
                this.txSPPB7.IsEnabled = false;
            }
            else
            {
                this.txSPPB61.Text = "";
                this.txSPPB62.Text = "";
                this.txSPPB61.IsEnabled = false;
                this.txSPPB62.IsEnabled = false;
                this.txSPPB7.IsEnabled = true;
            }
        }

        

        private void OnPATextChanged(object sender, RoutedEventArgs e)
        {
            DataAdapter.PhysiqueResult.PA = DataAdapter.IsNumber(((TextBox)sender).Text.Trim()) ? Convert.ToDouble(((TextBox)sender).Text.Trim()) : 0;
            double PA = DataAdapter.PhysiqueResult.PA;
            int Age = DataAdapter.Age;
            if(Age < 20)
            {
                Age = 20;
            }
            else if(Age>65)
            {
                Age = 65;
            }
            
            PA = PA < 4.5 ? 4.5 : PA;
            PA = PA > 8.0 ? 8.0 : PA;
            double x = 50 + ((Age - 20) / 45.0) * 300.0;
            double y = 160-((PA - 4.2) / (8 - 4.2)) * 160.0;

            this.PAPoint.StartPoint = new Point(x, y);
            this.PAMid.Point = new Point(x + 5, y + 5);
            this.PAEnd.Point = this.PAPoint.StartPoint;
        }
    }
}
