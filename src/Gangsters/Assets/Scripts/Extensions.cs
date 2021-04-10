using Assets.Scripts.World;
using Messaging;
using QGame;
using UnityEngine;

namespace Assets.Scripts
{
    // wraps ServiceLocator for easy access within project
    public static class Locator
    {
        public static IMessageHub MessageHub => ServiceLocator.Get<IMessageHub>();
        public static GangManager GangManager => ServiceLocator.Get<GangManager>();
        public static WorldManager WorldManager => ServiceLocator.Get<WorldManager>();
    }

    public static class GameObjectExtensions
    {
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            var result = go.GetComponent<T>();
            if (result == null) result = go.AddComponent<T>();
            return result;
        }

        public static void DestroyAllChildren(this GameObject go)
        {
            foreach (Transform child in go.transform)
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}