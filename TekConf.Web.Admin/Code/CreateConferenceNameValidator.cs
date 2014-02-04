using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Ninject;
using TekConf.Data;
using TekConf.Web.Admin.ViewModels;
using TekConf.Web.Admin.ViewModels.Conference;
using TekConf.Web.Admin.ViewModels.Session;

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

    public class EditConferenceNameValidator : ValidationAttribute
    {

        [Inject]
        public ITekConfContext Context { get; set; }
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (!(value is EditConferenceDto))
            {
                return true;
            }

            var dto = value as EditConferenceDto;

            var conferenceName = value.ToString().ToLower();
            var conference = Context.Conferences.AsNoTracking().SingleOrDefault(c => c.Name.ToLower() == conferenceName);

            if (conference != null && conference.Id != dto.Id)
            {
                this.ErrorMessage = string.Format("The conference name must be unique. {0} has already been saved to TekConf.", value.ToString());
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
            //TODO: var exists = Context.Conferences.AsNoTracking().Any(c => c.Name.ToLower() == conferenceName);
            var exists = false;
            this.ErrorMessage = string.Format("The conference name must be unique. {0} has already been saved to TekConf.", value.ToString());

            return !exists;
        }
    }

    public class CreateSessionTitleValidator : ValidationAttribute
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


            var sessionName = value.ToString().ToLower();
            var exists = Context.Sessions.AsNoTracking().Any(s => s.Title.ToLower() == sessionName);

            this.ErrorMessage = string.Format("The session title must be unique. {0} has already been saved to TekConf.", value.ToString());

            return !exists;
        }
    }

    public class SessionEndDateValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (!(value is CreateSessionDto))
            {
                return true;
            }

            var dto = value as CreateSessionDto;

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
}