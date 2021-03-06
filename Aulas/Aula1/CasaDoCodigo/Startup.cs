using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CasaDoCodigo
{
  public  class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; private set; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      services.AddDistributedMemoryCache();
      services.AddSession();
      
      //ISSO É CODE FIRST, PORRA!!!
      //Update-database -verbose atualizar a base, serve para criar as bases já físicas,
      //Add-Migration Inicial é como se  forma de inicia o migration 
      //AJUSTAR DEPOIS PARA BUSCAR DO CONNECTION STRING
      //string connectionString = Configuration.GetConnectionString("Default");
      services.AddDbContext<ApplicationContext>(options => options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CasaDoCodigo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

      services.AddTransient<IDataService, DataService>();
      services.AddTransient<IProdutoRepository, ProdutoRepository>();
      services.AddTransient<IPedidoRepository, PedidoRepository>();
      services.AddTransient<ICadastroRepository, CadastroRepository>();
      services.AddTransient<IItemPedidoRepository, ItemPedidoRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
    {
      if(env.IsDevelopment())
      {
        app.UseBrowserLink();
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

      app.UseStaticFiles();
      app.UseSession();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Pedido}/{action=Carrossel}/{codigo?}");
      });

      serviceProvider
      .GetService<IDataService>().InicializaDB();
    }
  }
}
