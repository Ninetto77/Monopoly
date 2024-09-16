using MonopolyWPF.DB;
using System;
using System.ComponentModel;
using System.Data.Entity;


namespace Monopoly.Repository
{
	public class DBRepository : IRepository
	{
		private readonly MSSQLWarehouseEntities db;

        public DBRepository()
        {
            db = new MSSQLWarehouseEntities();
			if (db.Box == null)
			{
				CreateData();
			}
		}

		/// <summary>
		/// Создание данных
		/// </summary>
		public void CreateData()
		{
			CreateBoxes();
			CreatePallets();
			CountParameters();
			CheckSize();
		}

		/// <summary>
		/// Посчитать параметры коробок и паллетов
		/// </summary>
		private void CountParameters()
		{
			var boxes = db.Box;
			var pallets = db.Pallet;
			
			if (boxes != null && pallets != null)
			{
				foreach (Box box in boxes)
				{
					foreach (Pallet pallet in pallets)
					{
						if (pallet.Id == box.PalletID)
						{
							pallet.Volume += box.Volume;
							pallet.Weight += box.Weight;
							if ( pallet.DataExpiration == null)
								pallet.DataExpiration = box.DataExpiration;
							else if (pallet.DataExpiration >= box.DataExpiration)
								pallet.DataExpiration = box.DataExpiration;

							continue;
						}
					}
				}
			}
			db.SaveChanges();
		}

		/// <summary>
		///  Создание паллетов
		/// </summary>
		private void CreatePallets()
		{
			for (int i = 0; i < 5; i++)
			{
				var pallet = new Pallet(
					i + 1,
					i + 1,
					i + 1,
					i + 1,
					new DateTime(2024, i + 1, i + 1)
					);
				pallet.Volume = pallet.Depth * pallet.Length * pallet.Width;
				db.Pallet.Add(pallet);
			}
			db.SaveChanges();
		}

		/// <summary>
		/// Создание коробок
		/// </summary>
		private void CreateBoxes()
		{
			for (int i = 0; i < 10; i++)
			{
				var box = new Box(
					i + 1,
					i + 1,
					i + 1,
					i + 1,
					i + 1,
					new DateTime(2024, i + 1, i + 1)
				);
				box.Volume = box.Depth * box.Length * box.Width;
				db.Box.Add(box);
			}
			db.SaveChanges();
		}

		/// <summary>
		/// Проверить на корректность размеры паллеты и коробки
		/// </summary>
		/// <returns></returns>
		private void CheckSize()
		{
			var boxes = db.Box;
			var pallets = db.Pallet;

			if (boxes != null && pallets != null)
			{
				foreach (Box box in boxes)
				{
					foreach (Pallet pallet in pallets)
					{
						if (pallet.Id == box.PalletID)
						{
							if (box.Width > pallet.Width || box.Depth > pallet.Depth)
							{
								box.PalletID = 0;
							}
						}
					}
				}
			}
			db.SaveChanges();
		}

		public BindingList<Box> GetBoxData()
		{
			db.Box.Load();
			return db.Box.Local.ToBindingList();
		}
		public BindingList<Pallet> GetPalletData()
		{
			db.Pallet.Load();
			return db.Pallet.Local.ToBindingList();
		}

		public DbSet<Pallet> GetPalletDbSet() => db.Pallet;
		public DbSet<Box> GetBoxDbSet() => db.Box;
	}
}
