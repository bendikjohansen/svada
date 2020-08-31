using System.Data;
using Dapper;
using Microsoft.AspNetCore.Builder;

namespace Pemacy.Svada.Generator.Web
{
    public static class DatabaseSetup
    {
        public static IApplicationBuilder ConfigureDatabase(this IApplicationBuilder app)
        {
            var connection = app.ApplicationServices.GetService(typeof(IDbConnection)) as IDbConnection;

            connection.Execute(@"
                DROP TABLE IF EXISTS Word; 
                DROP TABLE IF EXISTS Category; 
                
                CREATE TABLE Category (
                    Id SERIAL UNIQUE,
                    Name VARCHAR(64) NOT NULL);
                    
                CREATE TABLE Word (
                    Id SERIAL UNIQUE,
                    Content VARCHAR(64) NOT NULL,
                    SentencePosition INT NOT NULL,
                    CategoryId INT NOT NULL,
                    FOREIGN KEY (CategoryId) REFERENCES Category (Id)


                );        
            ");

            return app;
        }
    }
}

