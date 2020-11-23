using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;

namespace ExamManagementSystem
{
    public class UnityConfig
    {
        internal static void RegisterComponents()
        {
            var container = new UnityContainer();


            container.RegisterType<IRepository<User>, UserRepository>();
            container.RegisterType<UserRepository, UserRepository>();
            container.RegisterType<HttpCookie, HttpCookie>(new InjectionConstructor("userData"));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}