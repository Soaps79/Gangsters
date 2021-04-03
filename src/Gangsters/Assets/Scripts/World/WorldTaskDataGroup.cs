using System;
using System.Collections.Generic;
using Assets.Scripts.Execution;

namespace Assets.Scripts.World
{
    [Serializable]
    public class WorldTaskDataGroup
    {
        public string CrewDisplayName;
        public List<WorldTaskData> WorldTasks = new List<WorldTaskData>();
    }
}