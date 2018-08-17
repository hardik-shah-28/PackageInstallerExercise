using PackageInstallerExercise.Packages.Interfaces;

namespace PackageInstallerExercise.Interfaces {

  // This is Dependency Map Interface
  public interface DependencyMapInterface {
    IPackage insertPackage(string nameOfPackage, string nameOfDependency);
    string[] GetMap();
  }

}
