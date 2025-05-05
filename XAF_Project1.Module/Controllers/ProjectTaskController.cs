using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using XAF_Project1.Module.BusinessObjects;

namespace XAF_Project1.Module.Controllers
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [Appearance("Completed1", TargetItems = "Subject",
    Criteria = "Status = 'Completed'", FontStyle = FontStyle.Strikeout, FontColor = "ForestGreen")]
    [Appearance("Completed2", TargetItems = "*;Status;AssignedTo",
    Criteria = "Status = 'Completed'", Enabled = false)]
    [Appearance("InProgress", TargetItems = "Subject;AssignedTo",
    Criteria = "Status = 'InProgress'", BackColor = "LemonChiffon")]
    [Appearance("Deferred", TargetItems = "Subject",
    Criteria = "Status = 'Deferred'", BackColor = "MistyRose")]
    [RuleCriteria("EndDate >= StartDate")]
    public class ProjectTaskController : ViewController
    {
        public ProjectTaskController()
        {
            TargetObjectType = typeof(DomainObjects);
            TargetViewType = ViewType.Any;
            SimpleAction markCompletedAction = new SimpleAction(
                this, "MarkCompleted",
                DevExpress.Persistent.Base.PredefinedCategory.RecordEdit)
            {
                TargetObjectsCriteria =
                    (CriteriaOperator.Parse("Status != ?", ProjectTaskStatus.Completed)).ToString(),
                ConfirmationMessage =
                            "Are you sure you want to mark the selected task(s) as 'Completed'?",
                ImageName = "State_Task_Completed"
            };
            markCompletedAction.Execute += (s, e) =>
            {
                foreach (DomainObjects task in e.SelectedObjects)
                {
                    task.EndDate = DateTime.Now;
                    task.Status = ProjectTaskStatus.Completed;
                    View.ObjectSpace.SetModified(task);
                }
                View.ObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            };
        }
    }
}