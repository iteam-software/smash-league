﻿using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Hosting;

namespace SmashLeague.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSmashLeagueServices(this IServiceCollection collection, IHostingEnvironment env)
        {
            collection.AddTransient<IMatchManager, MatchManager>();
            collection.AddTransient<IPlayerManager, PlayerManager>();
            collection.AddTransient<IImageManager, ImageManager>();
            collection.AddTransient<ITeamManager, TeamManager>();
            collection.AddTransient<INotificationManager, NotificationManager>();
            collection.AddTransient<IRankManager, RankManager>();
            collection.AddTransient<ISeasonManager, SeasonManager>();
            collection.AddTransient<IFileProvider, PhysicalFileProvider>(x => new PhysicalFileProvider(env.WebRootPath));

            return collection;
        }
    }
}
