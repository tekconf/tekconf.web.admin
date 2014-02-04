using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TekConf.Web.Admin.Code;

namespace TekConf.Web.Admin.ViewModels.Session
{
    [SessionEndDateValidator]
    public class CreateSessionDto
    {
        [Required]
        [CreateSessionTitleValidator]
        public string Title { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int? TalkLength { get; set; }
        public string Description { get; set; }
        public bool WillProvideVideo { get; set; }

        //public RoomDto Room { get; set; }
        //public DifficultyDto Difficulty { get; set; }
        //public SessionTypeDto SessionType { get; set; }
        //public virtual SessionSocialConnectionDto SocialConnection { get; set; }

        //public List<SessionLink> Links { get; set; }
        //public List<Tag> Tags { get; set; }
        //public List<string> Subjects { get; set; }
        //public List<string> Resources { get; set; }
        //public List<Prerequisite> Prerequisites { get; set; }
        //public List<FullSpeakerDto> speakers { get; set; }
        //public string speakerNames
        //{
        //    get
        //    {
        //        string names;
        //        if (speakers != null && speakers.Any())
        //        {
        //            names = speakers.Select(xs => xs.fullName).Aggregate((current, next) => current + ", " + next);
        //        }
        //        else
        //        {
        //            names = "No speakers assigned";
        //        }

        //        return names;
        //    }
        //}
        //public bool? isAddedToSchedule { get; set; }
        //public string startDescription
        //{
        //    get
        //    {
        //        if (start == default(DateTime))
        //        {
        //            return "Not scheduled yet";
        //        }

        //        return start.ToString("dddd h:mm tt");
        //    }
        //}
    }
}