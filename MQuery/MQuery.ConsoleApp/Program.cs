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
            //GenericTest();

            InsertTest();

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

        public static void InsertTest()
        {
            Compiler compiler = new Compiler(true);

            var book = new { Name = "O topraklar bizimdi", Writer = "Cengiz Aytmatov", PublishDate = DateTime.Now.AddYears(-53), Pages = 157 };

            var q = new Query<Book>()
                        .Insert(book);

            var qq = new Query<Book>()
                        .Select(x => x.Name, x => x.Id);

            compiler.Compile(q);
            compiler.Compile(qq);

            Console.WriteLine(q.RawSql);
            Console.WriteLine(qq.RawSql);
        }
    }
}
