using PackageInstallerExercise.Packages.Interfaces;
using System;

namespace PackageInstallerExercise.Packages.Exceptions {

  // Package Exception Base Class
  public abstract class BasePackageException: Exception {

    abstract public string Name { get; }

    public IPackage Package { get; private set; }

    public BasePackageException(IPackage package) {
      this.Package = package;
    }

    public override string Message {
      get {
        return string.Format(
        "{0} [{1}]", this.Name, this.Package.ToString()
        );
      }
    }

  }

}