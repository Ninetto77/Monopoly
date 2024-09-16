using Monopoly.MPV.Presenter;
using System.Windows;

namespace Monopoly
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Presenter p;
		public MainWindow()
		{
			InitializeComponent();
			p = new Presenter();
		}
	}
}
