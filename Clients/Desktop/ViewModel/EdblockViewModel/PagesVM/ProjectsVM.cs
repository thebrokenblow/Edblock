using System.Windows.Input;
using System.Collections.ObjectModel;
using EdblockViewModel.CoreVM;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.Clients;
using Edblock.ProjectsManagementModel.Configurations;
using Edblock.ProjectsManagementModel.ProjectManagementService;
using Microsoft.Extensions.DependencyInjection;
using EdblockViewModel.Services.Interfaces;
using EdblockViewModel.Services;

namespace EdblockViewModel.PagesVM;

public class ProjectsVM : BaseViewModel
{
    public ICommand NavigateToEditor { get; }

    public ProjectsVM(INavigationService navigationService, UserViewModel userViewModel)
    {
        var serviceProvider = ConfigurationProjectManager.GetServiceProvider();
        var projectManager = serviceProvider.GetRequiredService<IProjectManager>();
        var projects = projectManager.Update("123", 1, new()
        { 
            Content = "kek",
            Description = "kek",
            Name = "e"
        });
        //NavigateToEditor = FactoryNavigateService<EditorVM>.Create(navigationService, true);
    }


    public ObservableCollection<CardProjectVM> Projects { get; } =
        [
        new()
            {
                Name = "Неравенство",
                Description = "Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description = "Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description = "Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description = "Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description = "Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description = "Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description = "Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description = "Уравнение ax + b > 0",
                Project = ""
            }
        ];
}