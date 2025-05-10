namespace Api.Dtos.Pest;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class ExternalPestDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    public string common_name { get; set; }
    public string scientific_name { get; set; }
    public List<DescriptionBlock> description { get; set; }
    public List<SolutionBlock> solution { get; set; }
    public List<string> host { get; set; }
    public List<ImageBlock> images { get; set; }
}

public class DescriptionBlock
{
    public string subtitle { get; set; }
    public string description { get; set; }
}

public class SolutionBlock
{
    public string subtitle { get; set; }
    public string description { get; set; }
}

public class ImageBlock
{
    public string regular_url { get; set; }
}
public class ExternalPestWrapper
{
    public List<ExternalPestDto> data { get; set; }
}

