using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    [Serializable]
    public class WorldTaskDataGroup
    {
        public string CrewDisplayName;
        public List<WorldTaskData> WorldTasks = new List<WorldTaskData>();
    }
}