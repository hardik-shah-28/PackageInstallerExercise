namespace PackageInstallerExercise.Packages.Interfaces {

  /// <summary>
  /// Package Interface
  /// </summary>
  public interface IPackage {

    string Name { get; set; }
    IPackage Dependency { get; set; }

  }

}
