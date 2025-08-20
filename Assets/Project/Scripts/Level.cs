using System;
using System.Collections.Generic;
using System.Linq;
using RG.Systems.Test;
using UnityEngine;
namespace RG.Systems
{
    public class LevelSO : ScriptableObject
    {
        public List<ObjectiveData> Objectives { get; set; }
    }
    [Serializable]
    public class Level
    {
        public LevelSO ObjectiveData { get; private set; }
        public List<IObjective> ActiveObjectives { get; private set; }

        public Level(LevelSO data)
        {
            ObjectiveData = data;
            ActiveObjectives = new List<IObjective>(10);

            foreach (var objectiveData in ObjectiveData.Objectives)
            {
                IObjective objective = ObjetiveFactory.CreateGoal(objectiveData);
                ActiveObjectives.Add(objective);
            }
        }
        public bool IsCompleted => ActiveObjectives.All(ObjetivesWereCompleted);
        private bool ObjetivesWereCompleted(IObjective objective)
        {
            throw new NotImplementedException();
        }
    }

    public class ObjetiveFactory
    {
        public static IObjective CreateGoal(ObjectiveData objectiveData)
        {
            throw new NotImplementedException();
        }
    }
}
