using System.Collections.Generic;
using Messaging;
using QGame;

namespace Assets.Scripts.World
{
    public class CampaignInitializer : QScript
    {
        public List<TaskTemplateSO> TaskTemplates;

        public WorldStateSO StartWorldState;

        public void Awake()
        {
            if(Locator.MessageHub == null)
                ServiceLocator.Register<IMessageHub>(new MessageHub());

            var gangManager = Locator.GangManager;
            if(gangManager == null)
            {
                gangManager = new GangManager();
                ServiceLocator.Register<GangManager>(gangManager);
            }

            var worldManager = Locator.WorldManager;
            if (worldManager == null)
            {
                worldManager = new WorldManager(TaskTemplates);
                worldManager.Initialize(StartWorldState);
                ServiceLocator.Register<WorldManager>(worldManager);
            }

            if (ServiceLocator.Get<ResultsManager>() == null)
            {
                var resultsManager = new ResultsManager(gangManager.Wallet, worldManager);
                ServiceLocator.Register<ResultsManager>(resultsManager);
            }
        }
    }
}
