using Autofac;
using PlayPal.Repository;
using PlayPal.Repository.Common;
using PlayPal.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace PlayPal.Service
{
    public class DIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // builder.RegisterType za Service
            //builder.Register(c => new BoardGameRepository()).As<IBoardGameRepository>();
            builder.RegisterType<BoardGameRepository>().As<IBoardGameRepository>();
            builder.RegisterType<RentedBoardGameRepository>().As<IRentedBoardGameRepository>();
        }
    }
}
