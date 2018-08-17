using PackageInstallerExercise.Packages.Interfaces;

namespace PackageInstallerExercise.Packages.Exceptions {

  // Exception for package duplication
  public class ExceptionForPackageDuplication : BasePackageException {

    public ExceptionForPackageDuplication(IPackage package) 
      : base(package) { }

    public override string Name {
      get {
        return "Package already added, its duplication of package.";
      }
    }

  }
}
