﻿namespace ET.Trans.Bus.Model
{
    public abstract class OptionBase
    {
        public abstract string OptionsName { get; }
    }

    public static class OptionConfigurationExtensions
    {
        public static void AddOption<TOptionBase>(this IServiceCollection services) where TOptionBase : OptionBase
        {
            services.AddOptions<TOptionBase>().
                Configure<IConfiguration>((settings, config) =>
                {
                    config.GetSection(typeof(TOptionBase).Name).Bind(settings);
                });
        }
    }
    public class AppOption: OptionBase
    {
        public override string OptionsName => "AppOption";
        public string DBConnectionString { get; set; }
    }
}
