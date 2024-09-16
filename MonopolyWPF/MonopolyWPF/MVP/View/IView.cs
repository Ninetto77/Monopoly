using MonopolyWPF.DB;
using System.Collections.Generic;

namespace MonopolyWPF.MVP.View
{
	public interface IView
	{
		IEnumerable<Pallet> PalletData_DG { get; set; }
	}
}
