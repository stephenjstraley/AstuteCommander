using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using AstuteCommander.Models;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace AstuteCommander.Controllers
{
    public partial class ActionController : AstuteController
    {
        static string _search = string.Empty;
        static string _fileTypes = string.Empty;
        static string _secondSearch = string.Empty;
        static string _condition = string.Empty;
        static string _excludedText = string.Empty;
        static bool _wholeWord = false;
        static bool _matchCase = false;
        static Classes.VSTS.VstsRepositories _repos = null;
        public ActionController(IMemoryCache memCache, IAppSettings settings, IHostingEnvironment env) : base(memCache, settings, env) { }

    }
}