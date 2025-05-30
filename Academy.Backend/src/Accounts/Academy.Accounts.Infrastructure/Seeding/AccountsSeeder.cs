﻿using Microsoft.Extensions.DependencyInjection;

namespace Academy.Accounts.Infrastructure.Seeding
{

    public class AccountsSeeder
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AccountsSeeder(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task SeedAsync()
        {
            var scope = _serviceScopeFactory.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<AccountSeederService>();
            await service.SeedAsync();
        }
    }
}
