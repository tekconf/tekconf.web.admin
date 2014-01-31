using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using TekConf.Web.Admin.ViewModels.Conference;

namespace TekConf.Web.Admin.Code
{
    public static class Helpers
    {
        public static string ValidationClass<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return helper.ViewData.ModelState.IsValidField(helper.NameFor(expression).ToString()) ? null : "has-error";
        }

	    public static string ToSafeString(this DateTime? dateTime, string format)
	    {
		    if (dateTime.HasValue)
			    return dateTime.Value.ToString(format);
		    else
			    return "";
	    }

        public static string GenerateSlug(this string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
                return string.Empty;

            string slug = phrase.RemoveAccent().ToLower();
            // invalid chars           
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            slug = Regex.Replace(slug, @"\s+", " ").Trim();
            // cut and trim 
            slug = slug.Substring(0, slug.Length <= 45 ? slug.Length : 45).Trim();
            slug = Regex.Replace(slug, @"\s", "-"); // hyphens   
            return slug;
        }

        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }
    }
}