using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.RavenDB.London.May2013.Models;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Shard;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main()
		{
			var shards = new Dictionary<string, IDocumentStore>
				{
					{"vet1", new DocumentStore{Url = "http://localhost:8080", DefaultDatabase = "Vet1"}},
					{"vet2", new DocumentStore{Url = "http://localhost:8080", DefaultDatabase = "Vet2"}},
					{"vet3", new DocumentStore{Url = "http://localhost:8080", DefaultDatabase = "Vet3"}}
				};

			var shardStrategy = new ShardStrategy(shards)
				.ShardingOn<User>()
				.ShardingOn<Dog>(x => x.OwnerId)
				.ShardingOn<Cat>(x => x.OwnerId);

			using (var store = new ShardedDocumentStore(shardStrategy))
			{
				store.Initialize();

				for (int i = 0; i < 3; i++)
				{
					var user = new User();
					using (var session = store.OpenSession())
					{
						session.Store(user);
						session.SaveChanges();
					}

					using (var session = store.OpenSession())
					{
						session.Store(new Dog
							{
								OwnerId = user.Id
							});
						session.SaveChanges();
					}
				}
			}
		}
	}
}
