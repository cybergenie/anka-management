using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Anka2.CustomControls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Anka2.CustomControls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Anka2.CustomControls;assembly=Anka2.CustomControls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:ValueBar/>
    ///
    /// </summary>
    public class ColorValueBar : Control
    {        
       
        static ColorValueBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorValueBar), new FrameworkPropertyMetadata(typeof(ColorValueBar)));
        }

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(SolidColorBrush), typeof(ColorValueBar));

        public SolidColorBrush Color
        {
            get { return (SolidColorBrush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        #region CellOne
        /// <summary>
        /// CellOne
        /// </summary>
        public static readonly DependencyProperty CellOneLengthProperty =
           DependencyProperty.Register("CellOneLength", typeof(double), typeof(ColorValueBar));

        
        public double CellOneLength
        {
            get { return (double)GetValue(CellOneLengthProperty); }
            set
            {
                SetValue(CellOneLengthProperty, value);               
            }
        }
       
        public static readonly DependencyProperty CellOneTextProperty =
          DependencyProperty.Register("CellOneText", typeof(string), typeof(ColorValueBar));

        public string CellOneText
        {
            get { return (string)GetValue(CellOneTextProperty); }
            set { SetValue(CellOneTextProperty, value); }
        }

         public static readonly DependencyProperty CellOneShowProperty =
          DependencyProperty.Register("CellOneShow", typeof(Visibility), typeof(ColorValueBar));

        public Visibility CellOneShow
        {
            get { return (Visibility)GetValue(CellOneShowProperty); }
            set { SetValue(CellOneShowProperty, value); }
        }
        #endregion
        #region CellTwo
        /// <summary>
        /// CellTwo
        /// </summary>
        public static readonly DependencyProperty CellTwoLengthProperty =
           DependencyProperty.Register("CellTwoLength", typeof(double), typeof(ColorValueBar));

        public double CellTwoLength
        {
            get { return (double)GetValue(CellTwoLengthProperty); }
            set {
                SetValue(CellTwoLengthProperty, value);                
            }
        }
       
        public static readonly DependencyProperty CellTwoTextProperty =
          DependencyProperty.Register("CellTwoText", typeof(string), typeof(ColorValueBar));

        public string CellTwoText
        {
            get { return (string)GetValue(CellTwoTextProperty); }
            set { SetValue(CellTwoTextProperty, value); }
        }

        public static readonly DependencyProperty CellTwoShowProperty =
          DependencyProperty.Register("CellTwoShow", typeof(Visibility), typeof(ColorValueBar));

        public Visibility CellTwoShow
        {
            get { return (Visibility)GetValue(CellTwoShowProperty); }
            set { SetValue(CellTwoShowProperty, value); }
        }
        #endregion
        #region CellThree
        /// <summary>
        /// CellThree
        /// </summary>
        public static readonly DependencyProperty CellThreeLengthProperty =
           DependencyProperty.Register("CellThreeLength", typeof(double), typeof(ColorValueBar));

        public double CellThreeLength
        {
            get { return (double)GetValue(CellThreeLengthProperty); }
            set { SetValue(CellThreeLengthProperty, value); }
        }
        
        public static readonly DependencyProperty CellThreeTextProperty =
         DependencyProperty.Register("CellThreeText", typeof(string), typeof(ColorValueBar));

        public string CellThreeText
        {
            get { return (string)GetValue(CellThreeTextProperty); }
            set { SetValue(CellThreeTextProperty, value); }
        }

        public static readonly DependencyProperty CellThreeShowProperty =
          DependencyProperty.Register("CellThreeShow", typeof(Visibility), typeof(ColorValueBar));

        public Visibility CellThreeShow
        {
            get { return (Visibility)GetValue(CellThreeShowProperty); }
            set { SetValue(CellThreeShowProperty, value); }
        }
        #endregion
        #region CellFour
        /// <summary>
        /// CellFour
        /// </summary>   
        public static readonly DependencyProperty CellFourLengthProperty =
           DependencyProperty.Register("CellFourLength", typeof(double), typeof(ColorValueBar));

        public double CellFourLength
        {
            get { return (double)GetValue(CellFourLengthProperty); }
            set { SetValue(CellFourLengthProperty, value); }
        }
       
        public static readonly DependencyProperty CellFourTextProperty =
         DependencyProperty.Register("CellFourText", typeof(string), typeof(ColorValueBar));

        public string CellFourText
        {
            get { return (string)GetValue(CellFourTextProperty); }
            set { SetValue(CellFourTextProperty, value); }
        }

        public static readonly DependencyProperty CellFourShowProperty =
          DependencyProperty.Register("CellFourShow", typeof(Visibility), typeof(ColorValueBar));

        public Visibility CellFourShow
        {
            get { return (Visibility)GetValue(CellFourShowProperty); }
            set { SetValue(CellFourShowProperty, value); }
        }
        #endregion
        #region IndicatorOne
        /// <summary>
        /// IndicatorOne
        /// </summary>    

        public static readonly DependencyProperty IndicatorOneTextProperty =
          DependencyProperty.Register("IndicatorOneText", typeof(string), typeof(ColorValueBar));

        public string IndicatorOneText
        {
            get { return (string)GetValue(IndicatorOneTextProperty); }
            set { SetValue(IndicatorOneTextProperty, value); }
        }

        public static readonly DependencyProperty IndicatorOneShowProperty =
         DependencyProperty.Register("IndicatorOneShow", typeof(Visibility), typeof(ColorValueBar));

        public Visibility IndicatorOneShow
        {
            get { return (Visibility)GetValue(IndicatorOneShowProperty); }
            set { SetValue(IndicatorOneShowProperty, value); }
        }
       
        public double IndicatorOnePosition
        {
            get { return CellOneLength; }            
        }
        #endregion
        #region IndicatorTwo
        /// <summary>
        /// IndicatorTwo
        /// </summary>    

        public static readonly DependencyProperty IndicatorTwoTextProperty =
          DependencyProperty.Register("IndicatorTwoText", typeof(string), typeof(ColorValueBar));

        public string IndicatorTwoText
        {
            get { return (string)GetValue(IndicatorTwoTextProperty); }
            set { SetValue(IndicatorTwoTextProperty, value); }
        }

        public static readonly DependencyProperty IndicatorTwoShowProperty =
         DependencyProperty.Register("IndicatorTwoShow", typeof(Visibility), typeof(ColorValueBar));

        public Visibility IndicatorTwoShow
        {
            get { return (Visibility)GetValue(IndicatorTwoShowProperty); }
            set { SetValue(IndicatorTwoShowProperty, value); }
        }
        public double IndicatorTwoPosition
        {
            get { return CellOneLength + CellTwoLength; }
        }
        #endregion
        #region IndicatorThree
        /// <summary>
        /// IndicatorThree
        /// </summary>    

        public static readonly DependencyProperty IndicatorThreeTextProperty =
          DependencyProperty.Register("IndicatorThreeText", typeof(string), typeof(ColorValueBar));

        public string IndicatorThreeText
        {
            get { return (string)GetValue(IndicatorThreeTextProperty); }
            set { SetValue(IndicatorThreeTextProperty, value); }
        }

        public static readonly DependencyProperty IndicatorThreeShowProperty =
         DependencyProperty.Register("IndicatorThreeShow", typeof(Visibility), typeof(ColorValueBar));

        public Visibility IndicatorThreeShow
        {
            get { return (Visibility)GetValue(IndicatorThreeShowProperty); }
            set { SetValue(IndicatorThreeShowProperty, value); }
        }
        public double IndicatorThreePosition
        {
            get { return IndicatorTwoPosition + CellThreeLength; }
        }
        #endregion
        #region IndicatorValue
        /// <summary>
        /// IndicatorValue
        /// </summary>    
        /// 

        public static readonly DependencyProperty IndicatorPositionProperty =
          DependencyProperty.Register("IndicatorPosition", typeof(double), typeof(ColorValueBar));

        public double IndicatorPosition
        {
            get { return (double)GetValue(IndicatorPositionProperty); }
            set { SetValue(IndicatorPositionProperty, value); }
        }

        public static readonly DependencyProperty IndicatorValueProperty =
          DependencyProperty.Register("IndicatorValue", typeof(string), typeof(ColorValueBar));

        public string IndicatorValue
        {
            get { return (string)GetValue(IndicatorValueProperty); }
            set { SetValue(IndicatorValueProperty, value); }
        }

        public static readonly DependencyProperty IndicatorShowProperty =
         DependencyProperty.Register("IndicatorShow", typeof(Visibility), typeof(ColorValueBar));

        public Visibility IndicatorShow
        {
            get { return (Visibility)GetValue(IndicatorShowProperty); }
            set { SetValue(IndicatorShowProperty, value); }
        }
        #endregion

        public static readonly DependencyProperty OrientationProperty =
        DependencyProperty.Register("Orientation", typeof(string), typeof(ColorValueBar));

        

        public string Orientation
        {
            get { return (string)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
    }




}
