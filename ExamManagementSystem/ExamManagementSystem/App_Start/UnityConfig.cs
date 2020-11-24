using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.UserServices;
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
        public static IUnityContainer container;
        internal static void RegisterComponents()
        {
            container = new UnityContainer();


            container.RegisterType<IRepository<User>, UserRepository>();
            container.RegisterType<IRepository<Teacher>, TeacherRepository>();
            container.RegisterType<IRepository<Student>, StudentRepository>();
            container.RegisterType<IRepository<Admin>, AdminRepository>();

            container.RegisterType<HttpCookie, HttpCookie>(new InjectionConstructor("userData"));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}