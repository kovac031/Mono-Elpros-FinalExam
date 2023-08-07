using Autofac;
using AutoMapper;
using PlayPal.DAL;
using PlayPal.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Repository
{
    public class DIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // builder.RegisterType za Repository
            builder.Register(c => new EFContext()).AsSelf().InstancePerLifetimeScope();

           // builder.RegisterType<EFContext>().AsSelf().InstancePerLifetimeScope();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RepositoryMapperProfile>();
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}
