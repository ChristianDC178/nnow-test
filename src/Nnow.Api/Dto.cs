namespace Nnow.Api.Dtos;

public class PermissionTypeDto
{ 
    public int Id { get; set; }
    public string Description { get; set; }
}

public class ApplicationResponseDto<T>
{
    public List<string> Errors { get; private set; } = new List<string>();
    public T Entity { get; set; }
}

public class PermissionDto
{
    public int Id { get; set; }
    public string Surname { get; set; }
    public string Forename { get; set; }
    public int PermissionTypeId { get; set; }
    public DateTime Date { get; set; }
}

