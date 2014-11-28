using System;
using System.Data.Entity.Migrations;
using Pfizer.Domain.Constants;
using Pfizer.Domain.Models;
using Wizardsgroup.Repository;

namespace Pfizer.Repository.SeedData
{
    internal class SystemMessageSeeder : IDataSeeder
    {
        public void Seed(IContext context)
        {
            var record = new SystemMessage
            {
                Code = SystemMessageConstant.NotEditable,
                Message = "Revision is not allowed.",
                CreatedBy = "medicardadmin",
                CreatedDate = DateTime.Now
            };
            context.EntitySet<SystemMessage>().AddOrUpdate(c => new { c.Code }, record);
            

            record = new SystemMessage
            {
                Code = SystemMessageConstant.RecordAdded,
                Message = "Record successfully added.",
                CreatedBy = "medicardadmin",
                CreatedDate = DateTime.Now
            };
            context.EntitySet<SystemMessage>().AddOrUpdate(c => new { c.Code }, record);

            record = new SystemMessage
            {
                Code = SystemMessageConstant.RecordUpdated,
                Message = "Record successfully updated.",
                CreatedBy = "medicardadmin",
                CreatedDate = DateTime.Now
            };
            context.EntitySet<SystemMessage>().AddOrUpdate(c => new { c.Code }, record);

            record = new SystemMessage
            {
                Code = SystemMessageConstant.RecordAssigned,
                Message = "Record successfully assigned",
                CreatedBy = "medicardadmin",
                CreatedDate = DateTime.Now
            };
            context.EntitySet<SystemMessage>().AddOrUpdate(c => new { c.Code }, record);

            record = new SystemMessage
            {
                Code = SystemMessageConstant.RecordSaved,
                Message = "Record successfully saved",
                CreatedBy = "medicardadmin",
                CreatedDate = DateTime.Now
            };
            context.EntitySet<SystemMessage>().AddOrUpdate(c => new { c.Code }, record);

            #region Common Message

            record = new SystemMessage
            {
                Code = SystemMessageConstant.Toggle,
                Message = "Record(s) Successfully Updated",
                CreatedBy = "medicardadmin",
                CreatedDate = DateTime.Now
            };
            context.EntitySet<SystemMessage>().AddOrUpdate(c => new { c.Code }, record);

            record = new SystemMessage
            {
                Code = SystemMessageConstant.Delete,
                Message = "Record(s) Successfully Deleted",
                CreatedBy = "medicardadmin",
                CreatedDate = DateTime.Now
            };
            context.EntitySet<SystemMessage>().AddOrUpdate(c => new { c.Code }, record);

            record = new SystemMessage
            {
                Code = SystemMessageConstant.Rearrange,
                Message = "Records successfully rearranged.",
                CreatedBy = "medicardadmin",
                CreatedDate = DateTime.Now
            };
            context.EntitySet<SystemMessage>().AddOrUpdate(c => new { c.Code }, record);

            record = new SystemMessage
            {
                Code = SystemMessageConstant.NoRecordAdded,
                Message = "No record added.",
                CreatedBy = "medicardadmin",
                CreatedDate = DateTime.Now
            };
            context.EntitySet<SystemMessage>().AddOrUpdate(c => new { c.Code }, record);


            record = new SystemMessage
            {
                Code = SystemMessageConstant.NoRecordDeleted,
                Message = "No record deleted.",
                CreatedBy = "medicardadmin",
                CreatedDate = DateTime.Now
            };
            context.EntitySet<SystemMessage>().AddOrUpdate(c => new { c.Code }, record);
            #endregion

            // Save ~
            context.SaveChanges();
        }
    }
}