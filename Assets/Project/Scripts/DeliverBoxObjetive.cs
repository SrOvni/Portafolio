using System;

namespace RG.Systems.Test
{
    public class ObjectiveData
    {
        public string Name { get; set; } = "";
        public int TargetAmount { get; set; }
    }
    public class DeliverBoxObjetive : IObjective
    {
        public string Name { get; private set; }

        public bool IsCompleted { get; private set; }

        int deliveredBoxes;
        int target;

        public DeliverBoxObjetive(ObjectiveData data)
        {
            Name = data.Name;
            target = data.TargetAmount;
        }
        public int GetProgress()
        {
            //Collected boxes = obtain from... ¿manager?
            //return totalBoxesDelivered/target
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            deliveredBoxes = 0;
            IsCompleted = false;
        }
    }

}