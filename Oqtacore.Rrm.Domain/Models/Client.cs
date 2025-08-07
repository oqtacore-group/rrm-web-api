using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Oqtacore.Rrm.Domain.Models;

/// <summary>
/// Represents a client in the system.
/// </summary>
public class Client
{
    public int id { get; set; }
    public string Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? SiteUrl { get; set; }
    public string? Email { get; set; }
    public DateTime Created { get; set; }
    public int? CreatedBy { get; set; }
    /// <summary>
    /// This flag is used to mark the client as deleted. 
    /// </summary>
    public bool? IsDeleted { get; set; }

    public void editProfile(Client edited)
    {
        this.Email = edited.Email;
        this.Name = edited.Name;
        this.PhoneNumber = edited.PhoneNumber;
        this.SiteUrl = edited.SiteUrl;
    }
}


public class ClientArchive
{
    public int id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string SiteUrl { get; set; }
    public string Email { get; set; }
    public System.DateTime Created { get; set; }
    public Nullable<int> CreatedBy { get; set; }
    public System.DateTime ActionDate { get; set; }
    public string ActionType { get; set; }
    public int ActionBy { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key, Column(Order = 0)]
    public int ActionId { get; set; }
}