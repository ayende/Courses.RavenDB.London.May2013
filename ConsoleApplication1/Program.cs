using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.RavenDB.London.May2013.Models;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Client.Linq;
using Raven.Client.Shard;

namespace ConsoleApplication1
{
	internal class Program
	{
		private static void Main()
		{

			using (var store = new DocumentStore
				{
					Url = "http://localhost:8080",
					DefaultDatabase = "Courses",
					Conventions =
						{
							FailoverBehavior = FailoverBehavior.FailImmediately
						}
				})
			{
				store.Initialize();

				using (store.AggressivelyCache())
				{
					while (true)
					{
						using (var s = store.OpenSession())
						{
							var load = s.Load<Dog>(1);
							Console.WriteLine(load.Name);
						}

						Console.ReadLine();
					}
				}
			}
		}
	}
}