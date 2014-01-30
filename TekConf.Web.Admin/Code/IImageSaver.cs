using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TekConf.Web.Admin.Code
{
    public interface IImageSaver
    {
        string SaveImage(string imageName, HttpPostedFileBase image);
    }
}