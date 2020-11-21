using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace ExamManagementSystem
{
    public class UnityRegistration
    {
        internal static void BuildContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IRepository<User>, UserRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}