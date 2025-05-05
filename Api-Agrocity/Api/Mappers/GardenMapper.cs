using Api.Models.DTOs.Garden;

using Api.Models;

namespace Api.Mappers
{
  public static class GardenMapper
  {
    public static GardenDto ToDto(this Garden gardenItem)
    {
      return new GardenDto
      {
        GardenId = gardenItem.GardenId,
        UserId = gardenItem.UserId,
        UserName = gardenItem.User != null ? gardenItem.User.Name : null,
        Name = gardenItem.Name,
        Description = gardenItem.Description,
        CreatedAt = gardenItem.CreatedAt
        
      };
    }

    public static Garden ToGardenFromCreateDto(this CreateGardenRequestDto gardenRequestDto)
    {
      return new Garden
      {
        UserId = gardenRequestDto.UserId,
        Name = gardenRequestDto.Name,
        Description = gardenRequestDto.Description,
        CreatedAt = gardenRequestDto.CreatedAt
      };
    }
  }
}