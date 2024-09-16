using Monopoly.Repository;
using MonopolyWPF.DB;
using MonopolyWPF.MVP.View;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;


namespace Monopoly.MPV.Presenter
{
	public class Presenter
	{
		private readonly DBRepository  repository;
		private readonly IView  view;

        public Presenter(IView view)
        {
            repository = new DBRepository();
			this.view = view;
			this.view.PalletData_DG = GetPalletDb();
		}

		/// <summary>
		/// Показать все паллеты
		/// </summary>
        public void ShowAllPallets()
        {
			DbSet<Pallet> pallets = repository.GetPalletDbSet();
			var query = from pal in pallets
							group pal by pal.DataExpiration into expiryGroup
						   orderby expiryGroup.Key
						   select new
						   {
							   ExpiryDate = expiryGroup.Key,
							   Pallets = from p in expiryGroup
										 orderby p.Weight
										 select p
						   };

			BindingList<Pallet> newPallets = new BindingList<Pallet>();
			foreach (var group in query)
			{
				Console.WriteLine($"Expiry Date: {group.ExpiryDate}");
				foreach (var pallet in group.Pallets)
				{
					newPallets.Add(new Pallet(
						pallet.Id,
						pallet.Weight,
						pallet.Length,
						pallet.Width,
						pallet.Depth,
						pallet.DataProduction,
						pallet.DataExpiration,
						pallet.Volume
						));
					Console.WriteLine($"Pallet ID: {pallet.Id}, Weight: {pallet.Weight}");
				}
			}
			view.PalletData_DG = newPallets;
		}
		
		/// <summary>
		/// Показать три первые паллеты по запросу
		/// </summary>
		public void ShowFirstTreePallets()
		{
			var boxes = repository.GetBoxDbSet();
			var pallets = repository.GetPalletDbSet();

			DateTime maxdata = DateTime.MinValue;

			var query = (from b in boxes
						 group b by b.DataExpiration into expiryGroup
						 orderby expiryGroup.Key
						 select new
						 {
							 ExpiryDate = expiryGroup.Key,
							 Boxes = from b in expiryGroup
									 orderby b.PalletID
									 select b
						 });

			var result = boxes
			.GroupBy(x => x.DataExpiration)
			.SelectMany(g => g.OrderByDescending(x => x.PalletID))
			.Select(x => x.PalletID)
			.Distinct()
			.Take(3).
			ToList();


			BindingList<Pallet> newPallets = new BindingList<Pallet>();
			foreach (Pallet item in GetPalletDb())
				foreach (int i in result)
				{
					if (item.Id == i)
					{
						newPallets.Add(item);
						continue;
					}
				}

			view.PalletData_DG = newPallets;
		}

		public BindingList<Pallet> GetPalletDb()
		{
			return repository.GetPalletData();
		}
	}
}
