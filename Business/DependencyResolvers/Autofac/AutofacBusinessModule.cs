using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Business.Mappers.AutoMapper;
using Business.Mappers.AutoMapper.Resolvers;
using Business.Mappers.AutoMapper.Resolvers.UserResolver;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Module = Autofac.Module;

namespace Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<BookManager>().As<IBookService>().SingleInstance();
        builder.RegisterType<EfBookDal>().As<IBookDal>().SingleInstance();

        builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
        builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

        builder.RegisterType<GenreManager>().As<IGenreService>().SingleInstance();
        builder.RegisterType<EfGenreDal>().As<IGenreDal>().SingleInstance();

        builder.RegisterType<AuthorManager>().As<IAuthorService>().SingleInstance();
        builder.RegisterType<EfAuthorDal>().As<IAuthorDal>().SingleInstance();

        builder.RegisterType<GenreNameResolver>().AsSelf().SingleInstance();
        builder.RegisterType<GenreIdResolver>().AsSelf().SingleInstance();

        builder.RegisterType<AuthorNameResolver>().AsSelf().SingleInstance();
        builder.RegisterType<AuthorIdResolver>().AsSelf().SingleInstance();

        builder.RegisterType<UserNameResolver>().AsSelf().SingleInstance();
        builder.RegisterType<UserIdResolver>().AsSelf().SingleInstance();


        var assembly = Assembly.GetExecutingAssembly();

        builder.Register(context => new MapperConfiguration(cfg => { cfg.AddProfile<MapperProfile>(); })).AsSelf()
            .SingleInstance();

        builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();


        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(
            new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
    }
}