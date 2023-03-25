using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace SmartMvcApp.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
    }

    public class AlbumInfoRepository
    {

        public IEnumerable<Album> GetData()
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [Id],[Title],[User_Id]
               FROM [dbo].[Albums]", connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new Album()
                            {
                                Id = x.GetInt32(0),
                                Title = x.GetString(1),
                                UserId = x.GetInt32(2)
                            }).ToList();



                }
            }
        }
        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            AlbumHub.Show();
        }


    }
}