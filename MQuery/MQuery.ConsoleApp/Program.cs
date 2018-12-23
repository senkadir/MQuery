using System;
using System.Data.SqlClient;
using Dapper;
using MQuery;

namespace MQuery.ConsoleApp
{
    class Program
    {
        static string ConnectionString { get { return "Data Source=NYSPC011\\SQLEXPRESS;Initial Catalog=BookDb;Integrated Security=SSPI;"; } }

        static void Main(string[] args)
        {
            GenericTest();

            Console.Read();
        }

        public static void GenericTest()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = new Query<Book>()
                                .Select(x => x.Id, x => x.Name, x => x.Pages)
                                .From()
                                .Where()
                                .Where("Id", ">", 1)
                                .Where("Pages", ">", 300)
                                .ToString();

                var books = connection.Query<Book>(query);

                Console.WriteLine(query.ToString());
                foreach (var book in books)
                {
                    Console.WriteLine(book.Id);
                    Console.WriteLine(book.Name);
                    Console.WriteLine(book.Pages);
                }
            }

            Console.Read();
        }
    }
}
