using System.Collections.Generic;
using System.Threading.Tasks;
using CMSAPI.Models;

namespace CMSAPI.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task<StudentDto?> GetByIdAsync(int id);
        Task CreateAsync(StudentDto dto);
        Task UpdateAsync(int id, StudentDto dto);
        Task UpdateStatusAsync(int id, string status);
        Task DeleteAsync(int id);
    }
}