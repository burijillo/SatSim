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
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

namespace HostingWPFUserControl
{
	public class SinConverter : IValueConverter
	{
		#region Implementation of IValueConverter

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				double angle = System.Convert.ToDouble(value);
				double angleRad = Math.PI * angle / 180;
				double radius = System.Convert.ToDouble(parameter);
				return radius * Math.Sin(angleRad);
			}
			catch
			{
				return Binding.DoNothing;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
	/// <summary>
	/// Lógica de interacción para UserControl1.xaml
	/// </summary>
	public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

		public void Test(double parsed)
		{
			//sstoryboard.Stop();
			//animation.Duration = new Duration(TimeSpan.FromSeconds(parsed));
			//sstoryboard.Begin();
			//Storyboard.Pause(this);
			//Storyboard.SetSpeedRatio(parsed);
			//animation.Duration = new Duration(TimeSpan.FromSeconds(parsed));
			//Storyboard.Resume();
		}
	}
}
