using PackageInstallerExercise.Packages.Interfaces;

namespace PackageInstallerExercise.Packages.Exceptions {
  
  //Exception for checking whether Package Contains Loop
  public class ExceptionForPackageHavingLoop : BasePackageException {

    public ExceptionForPackageHavingLoop(IPackage package) 
      : base(package) { }

    public override string Name {
      get {
        return "Package Contains Loop Exception";
      }
    }

  }

}
