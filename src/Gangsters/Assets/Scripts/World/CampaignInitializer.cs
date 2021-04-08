using Assets.Scripts.World;
using QGame;

public class CampaignInitializer : QScript
{
    public void Awake()
    {
        var gangManager = ServiceLocator.Get<GangManager>();
        if(gangManager == null)
        {
            gangManager = new GangManager();
            ServiceLocator.Register<GangManager>(gangManager);
        }

        if (ServiceLocator.Get<ResultsManager>() == null)
        {
            var resultsManager = new ResultsManager(gangManager, gangManager);
            ServiceLocator.Register<ResultsManager>(resultsManager);
        }
    }
}
