using Asp.Net.CoreApp.DbContext.ConsoleApp.PostgreSQL;
using Quartz;

public class DemoJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var db = new BloggingContext();
        var id = new Random().Next(0, 100_000_00);
        string message = "This job will be executed again at: " +
        context.NextFireTimeUtc.ToString();

        try
        {
            await db.Blogs.AddAsync(new Blog() { BlogId = id, Url = "myurl" + id });
            await db.SaveChangesAsync();
            //Console.WriteLine(message);
            //db.Blogs.ToList().ForEach(blog => Console.WriteLine(blog.BlogId));

        }
        catch (Exception ex)
        {
            throw; 
        }
        return;
        //try
        //{
        //    using (PgSqlConnection pgSqlConnection =
        //    new PgSqlConnection("User Id = postgres; Password = sa123#;" +
        //    "host=localhost;database=postgres;"))
        //    {
        //        using (PgSqlCommand cmd = new PgSqlCommand())
        //        {
        //            cmd.CommandText = "INSERT INTO public.demo " +
        //                "(id, message) VALUES(@id, @message)";

        //            cmd.Connection = pgSqlConnection;
        //            cmd.Parameters.AddWithValue("id", id);
        //            cmd.Parameters.AddWithValue("name", message);

        //            if (pgSqlConnection.State != System.Data.ConnectionState.Open)
        //                pgSqlConnection.Open();
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
        //catch
        //{
        //    throw;
        //}

    }
}