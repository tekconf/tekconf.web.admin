﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TekConf.Web.Admin.ViewModels.Conference
{
    public class ConferenceIndexViewModel
    {
        public List<ConferenceDto> Conferences { get; set; }
    }

    public class ConferenceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Slug { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}