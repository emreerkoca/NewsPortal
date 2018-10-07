using NewsPortal.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPortal.Admin.Helper
{
    public static class ImageUpload
    {
        public static string Image(HttpPostedFileBase imageURL, Slider slider)
        {
            string fileName = new Guid().ToString().Replace("-", "");
            string[] exts = imageURL.ContentType.Split('/');
            string fullPath = "/External/Slider/" + fileName + exts[1]; //exts[1] = file extension  

            imageURL.SaveAs(System.Web.HttpContext.Current.Server.MapPath(fullPath));
            slider.ImageURL = fullPath;

            return slider.ImageURL;
        }
    }
}