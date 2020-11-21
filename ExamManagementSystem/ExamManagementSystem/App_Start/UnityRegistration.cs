using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace ExamManagementSystem
{
    public class UnityConfig
    {
        internal static void RegisterComponents()
        {
            var container = new UnityContainer();


            container.RegisterType<IRepository<User>, UserRepository>();
            container.RegisterType<UserRepository, UserRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}