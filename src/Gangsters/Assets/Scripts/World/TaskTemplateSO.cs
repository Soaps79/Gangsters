using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World
{
    public enum TaskType
    {
        Unknown,
        Extortion,
        Purchase,
        Collect,
        Recruit
    }

    [Serializable]
    public class PropertyStateTaskChance
    {
        public WorldPropertyStatus Status;
        public float BasePercentChance;
    }

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TaskTemplate")]
    public class TaskTemplateSO : ScriptableObject
    {
        public TaskType Type;
        public string Verb;
        public List<PropertyStateTaskChance> Chances;
    }
}