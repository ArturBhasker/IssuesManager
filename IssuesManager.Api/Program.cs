Host.CreateDefaultBuilder()
    .ConfigureWebHostDefaults(
        web => web.UseStartup<Startup>())
    .Build()
    .Run();