using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper.Tests.Models;
using MySql.Data.MySqlClient;

namespace Dapper.Tests
{
    public class DapperRepository
    {
        private const string ConnectionString="Server=localhost;Database=dapper;Uid=root;SslMode=none";

        public IEnumerable<Skill> GetSkills()
        {
            using (MySqlConnection mysqlConnection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    mysqlConnection.Open();
                    var skillsResult = mysqlConnection.Query<Skill>(
                        @"select Id, Name
                          from Skills").ToList();
                    return skillsResult;
                    
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    return new List<Skill>();
                }
                finally 
                {
                    mysqlConnection.Close();
                }               
               
            }            
        } 
        
        public void AddSkill(string name)
        {
            using (MySqlConnection mysqlConnection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    mysqlConnection.Open();
                    mysqlConnection.Execute(
                        "INSERT INTO Skills (name) VALUES (@Name);",new{Name=name});
                    
                    
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);                    
                }
                finally 
                {
                    mysqlConnection.Close();
                }               
               
            }            
        }
    }
}