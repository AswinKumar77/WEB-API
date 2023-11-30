// StudentRepository.cs
using Microsoft.Data.SqlClient;
using StudentDetails.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace StudentDetails.Model
{
    public class StudentRepository
    {
        private readonly string connectionStr;

        public StudentRepository(string connectionStr)
        {
            this.connectionStr = connectionStr;
        }

        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM StudentDetails", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Department = reader["Department"].ToString(),
                                Age = Convert.ToInt32(reader["Age"])
                            });
                        }
                    }
                }
            }

            return students;
        }

       

        public int InsertStudent(Student student)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO StudentDetails (Name, Department, Age) VALUES (@Name, @Department, @Age); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@Department", student.Department);
                    command.Parameters.AddWithValue("@Age", student.Age);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        // Add methods for Update and Delete as needed
    }
}
