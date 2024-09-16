using MonopolyWPF.DB;
using System.ComponentModel;

namespace Monopoly.Repository
{
	public interface IRepository
	{
		BindingList<Box> GetBoxData();
		BindingList<Pallet> GetPalletData();
		void CreateData();
	}
}
