using Monopoly.MPV.Presenter;
using System.Windows;
using System.Collections.Generic;
using MonopolyWPF.DB;
using MonopolyWPF.MVP.View;

namespace MonopolyWPF
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, IView
	{
		public IEnumerable<Pallet> PalletData_DG { get => PalletDataGrid.ItemsSource as IEnumerable<Pallet>; set => PalletDataGrid.ItemsSource = value; }
		private readonly Presenter p;

		public MainWindow()
		{
			InitializeComponent();
			p = new Presenter(this);
			InitButtons();
		}

		/// <summary>
		/// Инициализация кнопок
		/// </summary>
		private void InitButtons()
		{
			AllPallets.Click += (s, e) => p.ShowAllPallets();
			FirstTreePallets.Click += (s, e) => p.ShowFirstTreePallets();
		}
	}
}
