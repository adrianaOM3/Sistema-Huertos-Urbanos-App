namespace Api.Models.DTOs.Garden
{
  public class GardenDto
  {
     public int GardenId { get; set; }

    public int? UserId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }


    public string? UserName { get; set; }

  }
}