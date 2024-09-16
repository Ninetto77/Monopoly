using Monopoly.Repository;

namespace Monopoly.MPV.Presenter
{
	public class Presenter
	{
		DBRepository repository;

        public Presenter()
        {
            repository = new DBRepository();
            repository.CreateData();
        }
    }
}
