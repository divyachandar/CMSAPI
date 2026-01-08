using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using CMSAPI.Models;
using Mapster;

namespace CMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")
            );
        }

        // GET: api/student
        [HttpGet]
        public IActionResult GetAll()
        {
            var students = new List<StudentDto>();

            using var con = GetConnection();
            using var cmd = new SqlCommand("sp_Student_GetAll", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                students.Add(MapStudent(reader));
            }

            return Ok(students);
        }

        // GET: api/student/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            StudentDto student = null;

            using var con = GetConnection();
            using var cmd = new SqlCommand("sp_Student_GetById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", id);

            con.Open();
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                student = MapStudent(reader);
            }

            if (student == null)
                return NotFound();
            var response=student.Adapt<StudentDto>();

            return Ok(response);
        }

        // POST: api/student
        [HttpPost]
        public IActionResult Create(StudentDto dto)
        {
            using var con = GetConnection();
            using var cmd = new SqlCommand("sp_Student_Insert", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StudentCode", dto.StudentCode);
            cmd.Parameters.AddWithValue("@FullName", dto.FullName);
            cmd.Parameters.AddWithValue("@Email", dto.Email);
            cmd.Parameters.AddWithValue("@Phone", dto.Phone);
            cmd.Parameters.AddWithValue("@DateOfBirth", dto.DateOfBirth);
            cmd.Parameters.AddWithValue("@Address", dto.Address ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@CampusId", dto.CampusId);
            cmd.Parameters.AddWithValue("@DepartmentId", dto.DepartmentId);
            cmd.Parameters.AddWithValue("@CourseId", dto.CourseId);
            cmd.Parameters.AddWithValue("@Batch", dto.Batch);
            cmd.Parameters.AddWithValue("@AdmissionDate", dto.AdmissionDate);
            cmd.Parameters.AddWithValue("@GuardianName", dto.GuardianName ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@GuardianPhone", dto.GuardianPhone ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@GuardianEmail", dto.GuardianEmail ?? (object)DBNull.Value);

            con.Open();
            cmd.ExecuteNonQuery();

            return Ok(new { message = "Student created successfully" });
        }

        // PUT: api/student/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, StudentDto dto)
        {
            using var con = GetConnection();
            using var cmd = new SqlCommand("sp_Student_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StudentId", id);
            cmd.Parameters.AddWithValue("@FullName", dto.FullName);
            cmd.Parameters.AddWithValue("@Phone", dto.Phone);
            cmd.Parameters.AddWithValue("@Address", dto.Address ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@DepartmentId", dto.DepartmentId);
            cmd.Parameters.AddWithValue("@CourseId", dto.CourseId);
            cmd.Parameters.AddWithValue("@GuardianName", dto.GuardianName ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@GuardianPhone", dto.GuardianPhone ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@GuardianEmail", dto.GuardianEmail ?? (object)DBNull.Value);

            con.Open();
            cmd.ExecuteNonQuery();

            return Ok(new { message = "Student updated successfully" });
        }

        // PATCH: api/student/status/5
        [HttpPatch("status/{id}")]
        public IActionResult UpdateStatus(int id, [FromQuery] string status)
        {
            using var con = GetConnection();
            using var cmd = new SqlCommand("sp_Student_UpdateStatus", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StudentId", id);
            cmd.Parameters.AddWithValue("@Status", status);

            con.Open();
            cmd.ExecuteNonQuery();

            return Ok(new { message = "Status updated successfully" });
        }

        // DELETE: api/student/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using var con = GetConnection();
                using var cmd = new SqlCommand("sp_Student_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentId", id);

                con.Open();
                cmd.ExecuteNonQuery();

                return Ok(new { message = "Student deleted successfully" });
            }
            catch (SqlException ex)
            {
                // Return specific database error (e.g., constraint violations)
                return StatusCode(500, new { message = $"Database Error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        // Helper Mapper
        private StudentDto MapStudent(SqlDataReader reader)
        {
            return new StudentDto
            {
                StudentId = (int)reader["StudentId"],
                StudentCode = reader["StudentCode"].ToString(),
                FullName = reader["FullName"].ToString(),
                Email = reader["Email"].ToString(),
                Phone = reader["Phone"].ToString(),
                DateOfBirth = (DateTime)reader["DateOfBirth"],
                Address = reader["Address"] as string,
                CampusId = (int)reader["CampusId"],
                DepartmentId = (int)reader["DepartmentId"],
                CourseId = (int)reader["CourseId"],
                Batch = reader["Batch"].ToString(),
                AdmissionDate = (DateTime)reader["AdmissionDate"],
                Status = reader["Status"].ToString(),
                GuardianName = reader["GuardianName"] as string,
                GuardianPhone = reader["GuardianPhone"] as string,
                GuardianEmail = reader["GuardianEmail"] as string
            };
        }
    }
}
