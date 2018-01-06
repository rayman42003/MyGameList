using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyGameList.Data;

namespace MyGameList
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private IHostingEnvironment _Environment { get; set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env) {
            Configuration = configuration;
            _Environment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            // Right now both dev and prod will use in-memory DB
            if (_Environment.IsDevelopment()) {
                services.AddDbContext<GameContext>(option => option.UseInMemoryDatabase("GameList"));
            } else {
                services.AddDbContext<GameContext>(option => option.UseInMemoryDatabase("GameList"));
            }
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
