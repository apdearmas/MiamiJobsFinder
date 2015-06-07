using System.Web.Mvc;
using BDL;
using BusinessDomain;
using DAL;
using MiamiJobsFinder.Controllers;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

namespace MiamiJobsFinder
{
  public static class Bootstrapper
  {

    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
        container.RegisterType<AccountController>(new InjectionConstructor());

        container.RegisterType<ISendJobOffersService, SendJobOffersService>();
        container.RegisterType<ICustomerService, CustomerService>();
        container.RegisterType<IRepository<Customer>, Repository<Customer>>();
        container.RegisterType<IJobOffersService, JobOffersService>();
        container.RegisterType<IRepository<JobOffer>, Repository<JobOffer>>();
        container.RegisterType<IEmailService, BDL.EmailService>();
        container.RegisterType<ISmtpClientWrapper, SmtpClientWrapper>();

        container.RegisterInstance(typeof(IAzureStorageService), AzureStorageService.Instance);
    }
  }
}