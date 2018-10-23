using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ComparaisonEFCoreADONet
{
    
    class Program
    {
        const int NUMBER_OF_RECORDS = 100000;
        const int NUMBER_OF_TRIALS = 5;
        const string CONNECTION_STRING_KEY="StackOverflow2";
        private static IConfigurationRoot config;

        static void Main(string[] args)
        {
            Configure();
            ExecuteNTimes(GetEveryPostUsingAdoNet,"ADO.NET");
            ExecuteNTimes(GetEveryPostUsingEFCore,"EF Core");
        }

        static void Configure(){
             config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        static void GetEveryPostUsingAdoNet()
        {
            using (var connection = new SqlConnection(config.GetConnectionString(CONNECTION_STRING_KEY)))
            {
                connection.Open();
                using (var cmd = new SqlCommand($@"SELECT TOP ({NUMBER_OF_RECORDS}) [Id]
                                            ,[AcceptedAnswerId]
                                            ,[AnswerCount]
                                            ,[Body]
                                            ,[ClosedDate]
                                            ,[CommentCount]
                                            ,[CommunityOwnedDate]
                                            ,[CreationDate]
                                            ,[FavoriteCount]
                                            ,[LastActivityDate]
                                            ,[LastEditDate]
                                            ,[LastEditorDisplayName]
                                            ,[LastEditorUserId]
                                            ,[OwnerUserId]
                                            ,[ParentId]
                                            ,[PostTypeId]
                                            ,[Score]
                                            ,[Tags]
                                            ,[Title]
                                            ,[ViewCount]
                                        FROM [dbo].[Posts]", connection))
                {
                    cmd.Prepare();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var posts = new List<Posts>();
                        while (reader.Read())
                        {
                            posts.Add(new Posts()
                            {
                                Id = reader.GetInt32(0),
                                AcceptedAnswerId = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                                AnswerCount = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                                Body = reader.IsDBNull(3) ? null : reader.GetString(3),
                                ClosedDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                                CommentCount = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                                CommunityOwnedDate = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
                                CreationDate = reader.GetDateTime(7),
                                FavoriteCount = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                                LastActivityDate = reader.GetDateTime(9),
                                LastEditDate = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10),
                                LastEditorDisplayName = reader.IsDBNull(11) ? null : reader.GetString(11),
                                LastEditorUserId = reader.IsDBNull(12) ? (int?)null : reader.GetInt32(12),
                                OwnerUserId = reader.IsDBNull(13) ? (int?)null : reader.GetInt32(13),
                                ParentId = reader.IsDBNull(14) ? (int?)null : reader.GetInt32(14),
                                PostTypeId = reader.GetInt32(15),
                                Score = reader.GetInt32(16),
                                Tags = reader.IsDBNull(17) ? null : reader.GetString(17),
                                Title = reader.IsDBNull(18) ? null : reader.GetString(18),
                                ViewCount = reader.GetInt32(19)
                            });
                        }
                        reader.Close();
                    }
                }
                connection.Close();
            }
        }

        static void GetEveryPostUsingEFCore()
        {
            var builder=new DbContextOptionsBuilder<StackOverflow2Context>();
            builder.UseSqlServer(config.GetConnectionString(CONNECTION_STRING_KEY));
            using (var context = new StackOverflow2Context(builder.Options))
            {
                context.Posts.Take(NUMBER_OF_RECORDS).ToList();
            }
        }

        static void ExecuteNTimes(Action actionToTime, string description)
        {
            TimeSpan totalTimeTaken = TimeSpan.Zero;
            for (int i = 0; i < NUMBER_OF_TRIALS; i++)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                actionToTime();
                watch.Stop();
                totalTimeTaken = totalTimeTaken.Add(watch.Elapsed);
            }
            Console.WriteLine($"{description}: Total : {totalTimeTaken.TotalMilliseconds} ms; Average : {totalTimeTaken.TotalMilliseconds/NUMBER_OF_TRIALS} ms");
        }
    }
}
