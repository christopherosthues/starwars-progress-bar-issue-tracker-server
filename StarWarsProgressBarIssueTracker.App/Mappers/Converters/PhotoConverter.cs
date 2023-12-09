using AutoMapper;

namespace StarWarsProgressBarIssueTracker.App.Mappers.Converters;

public class PhotoConverter : IValueConverter<string, byte[]>
{
    public byte[] Convert(string sourceMember, ResolutionContext context)
    {
        return System.Convert.FromBase64String(sourceMember);
    }
}
