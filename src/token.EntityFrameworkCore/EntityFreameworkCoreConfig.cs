﻿using Microsoft.EntityFrameworkCore;
using token.Domain;

namespace token.EntityFrameworkCore;

public static class EntityFreameworkCoreConfig
{
    public static void AddConfig(this ModelBuilder builder)
    {
        builder.Entity<Users>(x =>
                              {
                                  x.ToTable("Users");
                                  x.HasIndex(x => x.Id);
                                  x.HasKey(x => x.Id);
                              });

        builder.Entity<AppVersion>(x =>
                                   {
                                       x.ToTable("AppVersion");
                                       x.HasIndex(x => x.Id);
                                       x.HasKey(x => x.Id);
                                   });

        builder.Entity<token.Domain.Records.FacilityLogger>(x =>
                                                            {
                                                                x.ToTable("facilityLogger");

                                                                x.HasIndex(x => x.FacilityId);
                                                            });

        builder.Entity<token.Domain.Records.WordLogs>(x =>
                                                      {
                                                          x.ToTable(nameof(token.Domain.Records.WordLogs));
                                                          x.HasIndex(x => x.Id);
                                                          x.HasKey(x => x.Id);

                                                          x.HasIndex(x => x.Type);
                                                          x.HasIndex(x => x.Device);
                                                      });
    }
}