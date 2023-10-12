using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
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

        builder.RegisterType<GenreManager>().As<IGenreService>().SingleInstance();
        builder.RegisterType<EfGenreDal>().As<IGenreDal>().SingleInstance();

        builder.RegisterType<AuthorManager>().As<IAuthorService>().SingleInstance();
        builder.RegisterType<EfAuthorDal>().As<IAuthorDal>().SingleInstance();

        var assembly = Assembly.GetExecutingAssembly();

        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(
            new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
    }
}