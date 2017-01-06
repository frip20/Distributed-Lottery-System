
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Wesley.Lottery.Core.Extensions;

namespace Wesley.Lottery.Core.Helpers
{
    public static class SessionHelper
    {
        private static IHttpContextAccessor context;
        
        public static void Configure(IHttpContextAccessor accessor)
        {
            context = accessor;
        }
        
        /// <summary>
        /// 用户ID
        /// </summary>
        public static int MemberId {
            get {
                return context.HttpContext.Session.Get<int>("MemberId");
            }
            set {
                context.HttpContext.Session.Set<int>("MemberId", value);
            }
        }

        /// <summary>
        /// 用户帐号
        /// </summary>
        public static string UserName {
            get {
                return context.HttpContext.Session.Get<string>("UserName");
            }
            set {
                context.HttpContext.Session.Set<string>("UserName", value);
            }
        }

        /// <summary>
        /// 用户级别
        /// </summary>
        public static int MemberLevel {
            get
            {
                return context.HttpContext.Session.Get<int>("MemberLevel");
            }
            set {
                context.HttpContext.Session.Set<int>("MemberLevel", value);
            }
        }

    }
}
