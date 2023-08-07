using Autofac.Integration.Mvc;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PlayPal.DAL;
using PlayPal.Service;
using PlayPal.Service.Common;
using PlayPal.Repository;
using PlayPal.DAL;
using PlayPal.Service;
using PlayPal.Service.Common;
using PlayPal.Repository.Common;

namespace PlayPal.MVC.App_Start
{
    public class DIContainer
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new Service.DIModule());
            builder.RegisterModule(new Repository.DIModule());
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<EFContext>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<CoreUserService>().As<ICoreUserService>().InstancePerRequest();
            builder.RegisterType<EFCoreUserRepository>().As<ICoreUserRepository>().InstancePerRequest();

            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();

            builder.RegisterType<UserNotificationService>().As<IUserNotificationService>().InstancePerRequest();
            builder.RegisterType<UserNotificationRepository>().As<IUserNotificationRepository>().InstancePerRequest();

            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerRequest();

            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerRequest();

            builder.RegisterType<BoardGameService>().As<IBoardGameService>();
            builder.RegisterType<RentedBoardGameService>().As<IRentedBoardGameService>();


            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MVCMapperProfile>();
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            builder.RegisterType<ReviewService>().As<IReviewService>();
            builder.RegisterType<ReviewRepository>().As<IReviewRepository>();

            builder.RegisterType<NotificationService>().As<INotificationService>();
            builder.RegisterType<NotificationRepository>().As<INotificationRepository>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}