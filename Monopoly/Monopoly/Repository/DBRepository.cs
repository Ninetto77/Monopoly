using Monopoly.DB;
using System;
using System.Security.Cryptography;
using System.Windows.Input;

namespace Monopoly.Repository
{
	public class DBRepository : IRepository
	{
		private readonly MSSQLWarehouseEntities db;

        public DBRepository()
        {
            db = new MSSQLWarehouseEntities();
			
        }
		public void CreateData()
		{
			CreateBoxes();
			CreatePallets();
			CountParameters();
		}

		private void CountParameters()
		{
			foreach (var box in db.Box)
			{
				foreach (var pallet in db.Pallet)
				{
					if (pallet.Id == box.PalletID)
					{
						pallet.Volume += box.Volume;
						pallet.Weight += box.Weight;
						continue;
					}
				}
			}
			db.SaveChanges();
		}

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

		public void GetData()
		{
			throw new NotImplementedException();
		}
	}
}
