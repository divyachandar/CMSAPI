using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSAPI.Data;
using CMSAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CMSAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly CampusAdmissionContext _db;

        public StudentService(CampusAdmissionContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            return await _db.Students
                .AsNoTracking()
                .Select(s => MapToDto(s))
                .ToListAsync();
        }

        public async Task<StudentDto?> GetByIdAsync(int id)
        {
            var s = await _db.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.StudentId == id);

            return s is null ? null : MapToDto(s);
        }

        public async Task CreateAsync(StudentDto dto)
        {
            var entity = new Student
            {
                StudentCode = dto.StudentCode ?? string.Empty,
                FullName = dto.FullName ?? string.Empty,
                Email = dto.Email ?? string.Empty,
                Phone = dto.Phone ?? string.Empty,
                DateOfBirth = DateOnly.FromDateTime(dto.DateOfBirth),
                Address = dto.Address,
                CampusId = dto.CampusId,
                DepartmentId = dto.DepartmentId,
                CourseId = dto.CourseId,
                Batch = dto.Batch ?? string.Empty,
                AdmissionDate = DateOnly.FromDateTime(dto.AdmissionDate),
                Status = dto.Status ?? "Active",
                GuardianName = dto.GuardianName,
                GuardianPhone = dto.GuardianPhone,
                GuardianEmail = dto.GuardianEmail,
                CreatedAt = DateTime.UtcNow
            };

            _db.Students.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, StudentDto dto)
        {
            var entity = await _db.Students.FindAsync(id);
            if (entity is null) return;

            entity.FullName = dto.FullName ?? entity.FullName;
            entity.Phone = dto.Phone ?? entity.Phone;
            entity.Address = dto.Address;
            entity.CourseId = dto.CourseId;
            entity.Batch = dto.Batch ?? entity.Batch;
            entity.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(int id, string status)
        {
            var entity = await _db.Students.FindAsync(id);
            if (entity is null) return;

            entity.Status = status;
            entity.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Students.FindAsync(id);
            if (entity is null) return;

            _db.Students.Remove(entity);
            await _db.SaveChangesAsync();
        }

        private static StudentDto MapToDto(Student s)
        {
            return new StudentDto
            {
                StudentId = s.StudentId,
                StudentCode = s.StudentCode,
                FullName = s.FullName,
                Email = s.Email,
                Phone = s.Phone,
                DateOfBirth = s.DateOfBirth.ToDateTime(TimeOnly.MinValue),
                Address = s.Address,
                CampusId = s.CampusId,
                DepartmentId = s.DepartmentId,
                CourseId = s.CourseId,
                Batch = s.Batch,
                AdmissionDate = s.AdmissionDate.ToDateTime(TimeOnly.MinValue),
                Status = s.Status,
                GuardianName = s.GuardianName,
                GuardianPhone = s.GuardianPhone,
                GuardianEmail = s.GuardianEmail
            };
        }
    }
}