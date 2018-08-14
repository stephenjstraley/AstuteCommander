using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstuteCommander.Classes
{
    public static class Raz
    {
        public static string root = string.Empty;

        public static string Div(string @class)
        {
            return $"<div class=\"{@class}\"> ";
        }
        public static string Div()
        {
            return "</div>";
        }
        public static string Img(string image, int width, int height)
        {
            var file = System.IO.Path.Combine(root, image);
            return $"<img src='{file}' width='{width}px' height='{height}px' /> ";
        }

        private static string D(string prefix, string @class, string title, string id)
        {
            return $"<{prefix} class=\"{@class}\">{title}: #:{id}# </{prefix}> ";
        }
        private static string DB(string prefix, string @class, string title, string id)
        {
            return $"<{prefix} class'{@class}'>{title}: #:{id} ? 'Yes' : 'No' # </{prefix}> ";
        }
        public static string DT(string @class, string title, string id)
        {
            return D("dt", @class, title, id);
        }
        public static string DD(string @class, string title, string id)
        {
            return D("dd", @class, title, id);
        }
        public static string DDLink(string url, string id)
        {
            return $"<a href='{url}' target='_blank'>#: {id} #</a>";
        }
        public static string DDLink(string id)
        {
            return $"<a href='#: {id} #' target='_blank'>#: {id} #</a>";
        }
        public static string DDB(string @class, string title, string id)
        {
            return DB("dd", @class, title, id);
        }

    }
}
