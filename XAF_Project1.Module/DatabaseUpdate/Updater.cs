using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using XAF_Project1.Module.BusinessObjects;


namespace XAF_Project1.Module.DatabaseUpdate
{
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }
    
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
            if (ObjectSpace.CanInstantiate(typeof(Person)))
            {
                Person person = ObjectSpace.FindObject<Person>(
                    CriteriaOperator.Parse("FirstName == ? && LastName == ?", "John", "Nilsen"));
                if (person == null)
                {
                    person = ObjectSpace.CreateObject<Person>();
                    person.FirstName = "John";
                    person.LastName = "Nilsen";
                }
            }
            if (ObjectSpace.CanInstantiate(typeof(DomainObjects)))
            {
                DomainObjects task = ObjectSpace.FindObject<DomainObjects>(
                    new BinaryOperator("Subject", "TODO: Conditional UI Customization"));
                if (task == null)
                {
                    task = ObjectSpace.CreateObject<DomainObjects>();
                    task.Subject = "TODO: Conditional UI Customization";
                    task.Status = ProjectTaskStatus.InProgress;
                    task.AssignedTo = ObjectSpace.FindObject<Person>(
                        CriteriaOperator.Parse("FirstName == ? && LastName == ?", "John", "Nilsen"));
                    task.StartDate = new DateTime(2019, 1, 30);
                    task.Notes = "OVERVIEW: http://www.devexpress.com/Products/NET/Application_Framework/features_appearance.xml";
                }
            }
            if (ObjectSpace.CanInstantiate(typeof(Project)))
            {
                Project project = ObjectSpace.FindObject<Project>(
                    new BinaryOperator("Name", "DevExpress XAF Features Overview"));
                if (project == null)
                {
                    project = ObjectSpace.CreateObject<Project>();
                    project.Name = "DevExpress XAF Features Overview";
                    project.Manager = ObjectSpace.FindObject<Person>(
                        CriteriaOperator.Parse("FirstName == ? && LastName == ?", "John", "Nilsen"));
                    project.Tasks.Add(ObjectSpace.FindObject<DomainObjects>(
                        new BinaryOperator("Subject", "TODO: Conditional UI Customization")));
                }
            }
            //if (ObjectSpace.CanInstantiate(typeof(Customer)))
            //{
            //    Customer customer = ObjectSpace.FindObject<Customer>(
            //        CriteriaOperator.Parse("FirstName == ? && LastName == ?", "Ann", "Devon"));
            //    if (customer == null)
            //    {
            //        customer = ObjectSpace.CreateObject<Customer>();
            //        customer.FirstName = "Ann";
            //        customer.LastName = "Devon";
            //        customer.Company = "Eastern Connection";
            //    }
            //}
            ObjectSpace.CommitChanges();
        }
    }
}
