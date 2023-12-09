using AutoMapper;

namespace StarWarsProgressBarIssueTracker.App.Mappers.Converters;

public class DbPhotoConverter : IValueConverter<byte[], string>
{
    public string Convert(byte[] sourceMember, ResolutionContext context)
    {
        return System.Convert.ToBase64String(sourceMember);
    }
}
