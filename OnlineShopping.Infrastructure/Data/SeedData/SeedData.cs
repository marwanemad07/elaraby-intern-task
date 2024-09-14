
namespace OnlineShopping.Infrastructure.Data.SeedData
{
    public static class SeedData
    {
        public static async Task SeedProductsAndCategories(AppDbContext context)
        {
            try
            {
                await SeedEntities<Category>(context, "categories.json", nameof(context.Categories));

                await SeedEntities<Product>(context, "products.json", nameof(context.Proudcts));
            }
            catch(Exception e)
            {
                //TODO: log the exception
            }
        }

        public static async Task SeedRolesAndUsers(UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            try
            {
                await SeedRoles(roleManager);
                await SeedUsers(userManager);

            }
            catch (Exception e)
            {

            }
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            try
            {
                var roles = Enum.GetNames(typeof(UserRoles));
                if(roles == null)
                {
                    return;
                }

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
            catch(Exception e)
            {

            }
        }

        private static async Task SeedUsers(UserManager<AppUser> userManager)
        {
            try
            {
                var users = GetDeserializedObjects<List<AppUserDeserlizeObject>>("users.json");
                if(users == null)
                {
                    return;
                }

                foreach (var user in users)
                {
                    var appUser = new AppUser
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Street = user.Street,
                        City = user.City,
                        ZipCode = user.ZipCode,
                    };

                    var result = await userManager.CreateAsync(appUser, user.Password);

                    if (result.Succeeded)
                    {
                        foreach (var role in user.Roles)
                        {

                            if (Enum.IsDefined(typeof(UserRoles), role))
                            {
                                await userManager.AddToRoleAsync(appUser, role);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        private static async Task SeedEntities<T>(AppDbContext context, string fileName, string databaseTableName) where T : class
        {
            if (!context.Set<T>().Any())
            {
                try
                {
                    var entites = GetDeserializedObjects<List<T>>(fileName);

                    using (var transaction = await context.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {databaseTableName} ON");
                            await context.Set<T>().AddRangeAsync(entites);
                            await context.SaveChangesAsync();
                            context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {databaseTableName} OFF");
                            await transaction.CommitAsync();
                        }
                        catch (Exception)
                        {
                            await transaction.RollbackAsync();
                        }
                    }
                }
                catch (Exception e)
                {
                    //TODO: log the exception
                    return;
                }
            }
        }
        
        private static T? GetDeserializedObjects<T>(string fileName)
        {
            var dataPath = GetDataPath();
            if (dataPath == null)
            {
                return default;
            }

            var file = File.ReadAllText(Path.Combine(dataPath, fileName));
            return JsonSerializer.Deserialize<T>(file);
        }

        private static string? GetDataPath()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            var directoryInfo = new DirectoryInfo(basePath);

            while (directoryInfo != null && directoryInfo.Name != "OnlineShopping")
            {
                directoryInfo = directoryInfo.Parent;
            }

            string? dataPath;

            if (directoryInfo == null)
            {
                dataPath = Path.Combine(basePath, "Data", "SeedData");
                return Directory.Exists(dataPath) ? dataPath : null;
            }

            var solutionRoot = directoryInfo.FullName;

            dataPath = Path.Combine(solutionRoot, "OnlineShopping.Infrastructure", "Data", "SeedData");

            if (Directory.Exists(dataPath))
            {
                return dataPath;
            }

            return null;
        }

    }
}
