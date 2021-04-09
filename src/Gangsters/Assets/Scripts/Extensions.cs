using Messaging;
using QGame;

namespace Assets.Scripts
{
	// wraps ServiceLocator for easy access within project
    public static class Locator
    {
        //public static ISerializationHub Serialization
        //{
        //    get { return ServiceLocator.Get<ISerializationHub>(); }
        //}

        public static IMessageHub MessageHub => ServiceLocator.Get<IMessageHub>();
    }
}