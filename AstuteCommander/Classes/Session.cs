using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AstuteCommander
{
    public static class Session
    {
        public static readonly string USER = "user";
        public static readonly string TOKEN = "access";
        public static readonly string VSTS = "vsts";
        public static readonly string GIT = "gitExe";
        public static readonly string TEMPDIR = "tempDir";
        public static readonly string LOGGEDIN = "loggedIn";
        public static readonly string PROJECTS = "projects";
        public static readonly string SELECTED_PROJ = "selectedProject";
        public static readonly string HEARTBEAT = System.DateTime.Now.ToString();
    }

    public static class CACHE
    {
        public static readonly string BUILDS = "BuildDef";
        public static readonly string RELEASES = "ReleaseDef";
    }

    public static class SessionExtensions
    {
        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetComplexData(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static bool GetBool(this ISession session, string key)
        {
            bool retValue = false;

            var isBool = session.GetString(key);
            if (!string.IsNullOrEmpty(isBool))
            {
                retValue = isBool.ToUpper() != "0";
            }

            return retValue;
        }

        public static void SetBool(this ISession session, string key, bool value)
        {
            session.SetString(key, value ? "1" : "0");
        }

    }

    public class VSTSDataColumn : System.Data.DataColumn
    {
        public VSTSDataColumn(string name, System.Type theType) : base(name, theType) { }

        public bool IsLocked { get; set; }
    }
}
