using System.Threading.Tasks;

namespace EdblockViewModel.Project.Interfaces;

public interface IProjectVM
{
    void SaveProject(string filePath);
    Task LoadProject(string filePath);
}
