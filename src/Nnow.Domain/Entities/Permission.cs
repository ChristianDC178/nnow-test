namespace Nnow.Domain.Entities;

public class EntityBase
{
    public int Id { get; }
}

public class Permission : EntityBase
{

    public string Forename { get; set; }
    public string Surname { get; set; }
    public int PermissionTypeId{ get; set; }
    public PermissionType PermissionType { get; set; }
    public DateTime Date { get; set; }
}

public class PermissionType : EntityBase
{
    public string Description { get; set; }

    public List<Permission> Permissions { get; set;}
}