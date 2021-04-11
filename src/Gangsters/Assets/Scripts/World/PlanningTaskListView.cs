using System.Collections.Generic;
using Assets.Scripts.Planning.UI;
using UIWidgets;

namespace Assets.Scripts.World
{
    public class PlanningTaskListView : ListViewCustom<PlanningTaskViewModel, AssignablePlanningTask>
    {
        public void UpdateList(List<AssignablePlanningTask> assignableTasks)
        {
            DataSource = assignableTasks.ToObservableList();
        }
    }
}