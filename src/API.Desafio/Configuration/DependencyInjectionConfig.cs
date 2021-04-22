using Business.Desafio.Interfaces;
using Business.Desafio.Notificacoes;
using Business.Desafio.Service;
using Data.Desafio.Context;
using Data.Desafio.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace API.Desafio.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<DbAPIContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ILojaRepository, LojaRepository>();
            services.AddScoped<IEstoqueRepository, EstoqueRepository>();
            //
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<ILojaService, LojaService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            //
            services.AddScoped<INotificador, Notificador>();
            //
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //
            return services;
        }
    }
}