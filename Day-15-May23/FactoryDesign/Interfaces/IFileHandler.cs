
namespace FactoryDesign.Interfaces
{
    public interface IFileHandler
    {
        void Handle(string filePath, FileAccess access, string? content = null);
    }
}