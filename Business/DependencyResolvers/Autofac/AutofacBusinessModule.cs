using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

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
    }
}