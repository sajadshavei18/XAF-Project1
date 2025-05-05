using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;

namespace XAF_Project1.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Planning")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class DomainObjects : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
       
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        public DomainObjects(Session session) : base(session) { }
        [Size(255)]
        public string Subject { get; set; }
        public ProjectTaskStatus Status { get; set; }
        public Person AssignedTo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Size(SizeAttribute.Unlimited)]
        public string Notes { get; set; }
        [Association]
        public Project Project { get; set; }
    }
    [NavigationItem("Planning")]
    public class Project : BaseObject
    {
        public Project(Session session) : base(session) { }
        public string Name { get; set; }
        public Person Manager { get; set; }
        [Size(SizeAttribute.Unlimited)]
        public string Description { get; set; }
        [Association, DevExpress.ExpressApp.DC.Aggregated]
        public XPCollection<DomainObjects> Tasks
        {
            get { return GetCollection<DomainObjects>("Tasks"); }
        }
    }
    public enum ProjectTaskStatus
    {
        NotStarted = 0,
        InProgress = 1,
        Completed = 2,
        Deferred = 3
    }
}