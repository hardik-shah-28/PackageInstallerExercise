using PackageInstallerExercise.Packages.Interfaces;

namespace PackageInstallerExercise.Packages {

  public class Package: IPackage {
    
        //name of the package and dependency
    public string Name { get; set; }
    public IPackage Dependency { get; set; }
        
    /// Equals Method checks whether package has same name or not.
    public override bool Equals(object obj) {

      //return false if parameter is null. parameter cant be null
      if (obj == null) {
        return false;
      }
      
      //paramter should be caster(converted) to package if not thn return false.
      Package p = obj as Package;
      if ((System.Object)p == null) {
        return false;
      }

      // Return true if the fields match else return false
      return Name == p.Name;

    }


        
    // override the to string method and represent package as string.
    public override string ToString() {

      string value = this.Name;
       //if dependency is not null than return value as : dependency name
      if (this.Dependency != null) {
        value += ":" + this.Dependency.Name;
      }

      return value;
    }

  }

}
