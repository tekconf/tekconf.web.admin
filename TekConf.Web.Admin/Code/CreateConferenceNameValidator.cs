using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Ninject;
using TekConf.Data;
using TekConf.Web.Admin.ViewModels;

namespace TekConf.Web.Admin.Code
{

    public class ConferenceEndDateValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (!(value is CreateConferenceDto))
            {
                return true;
            }

            var dto = value as CreateConferenceDto;

            if (!dto.Start.HasValue || dto.Start.Value == default(DateTime))
                return false;
            if (!dto.End.HasValue || dto.End.Value == default(DateTime))
                return false;

            if (dto.Start.Value >= dto.End.Value)
            {
                this.ErrorMessage = "The End date must be after the Start date.";
                return false;
            }

            return true;
        }
    }

    public class CreateConferenceNameValidator : ValidationAttribute
    {
        [Inject]
        public ITekConfContext Context { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if ((value is string) && string.IsNullOrEmpty((string)value))
            {
                return true;
            }
            

            var conferenceName = value.ToString().ToLower();
            var exists = Context.Conferences.AsNoTracking().Any(c => c.Name.ToLower() == conferenceName);

            this.ErrorMessage = string.Format("The conference name must be unique. {0} has already been saved to TekConf.", value.ToString());

            return !exists;
        }
    }
}