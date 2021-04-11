using System.Collections.Generic;
using Assets.Scripts.World;
using UIWidgets;

namespace Assets.Scripts.Planning.UI
{
    public class PlanningTaskListView : ListViewCustom<PlanningTaskViewModel, AssignableTask>
    {
        public void UpdateList(List<AssignableTask> assignableTasks)
        {
            DataSource = assignableTasks.ToObservableList();
        }
    }
}