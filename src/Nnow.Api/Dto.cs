using Nnow.Application.Commands;
using Nnow.Domain.Entities;

namespace Nnow.Api.Dtos;

public class PermissionDto
{
    public int Id { get; set; }
    public string Surname { get; set; }
    public string Forename { get; set; }
    public int PermissionTypeId { get; set; }
    public DateTime Date { get; set; }
}

public class PermissionResponseDto
{
    public List<string> Errors { get; set; }
    public bool IsSuccess { get; set; }
    public PermissionDto Permission { get; set; }
}


//Es posible el uso de mappers automáticos como por ejemplo 
//AutoMapper, para ahorrar tiempo se hace un adapter.
public class AdapterHelper
{

    public static PermissionResponseDto Adapt(ApplicationResponse<Permission> response)
    {
        PermissionResponseDto dto = new()
        {
            Errors = response.Errors,
            IsSuccess = response.Valid,
        };

        if (response.Entity != null)
        {
            dto.Permission = new()
            {
                Id = response.Entity.Id,
                Forename = response.Entity.Forename,
                Surname = response.Entity.Surname,
                PermissionTypeId = response.Entity.PermissionTypeId,
                Date = response.Entity.Date,
            };
        }

        return dto;
    }

}
