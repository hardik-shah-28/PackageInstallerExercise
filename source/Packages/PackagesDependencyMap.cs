using PackageInstallerExercise.Interfaces;
using PackageInstallerExercise.Packages.Interfaces;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using PackageInstallerExercise.Packages.Exceptions;

namespace PackageInstallerExercise.Packages {

  // this is Container for holding the mapping between package dependencies
  public class PackagesDependencyMap<P> : DependencyMapInterface
    where P : IPackage, new() {

    public List<IPackage> Packages { get; private set; }

   
    public PackagesDependencyMap() {
      this.Packages = new List<IPackage>();
    }

   
    /// Add Package to dependency map.
    public IPackage insertPackage(string nameOfPackage, string nameOfDependency = null) {

      // check for existance for package
      var isPackageExist = searchPackage(nameOfPackage);

      // If package already has a dependency, throw duplicate error
      if (isPackageExist != null && isPackageExist.Dependency != null) {
        throw new ExceptionForPackageDuplication(isPackageExist);
      }

      var newPackages = new List<IPackage>();

      IPackage dependency = default(P);
            
      //Find the dependency, if not found than create it when name of dependency is passed 
      if (!string.IsNullOrWhiteSpace(nameOfDependency)) {
        dependency = CreateOrsearchPackage(nameOfDependency, newPackages);
      }

     
      //Create the new package or get it from the list of package 
      var package = CreateOrsearchPackage(nameOfPackage, newPackages);
      package.Dependency = dependency;

      // Determine if it contains a loop
      if (PackageHasLoop(package, package.Name)) {
        throw new ExceptionForPackageHavingLoop(package);
      }

      //add the newly created pages to the package list
      this.Packages.AddRange(newPackages);
      return this.Packages.Last();

    }
        
    // create the package list if not found and add it to list of package list
    private IPackage CreateOrsearchPackage(string nameOfPackage, IList packages = null) {

      // ceck for package existance in the list
      var package = searchPackage(nameOfPackage);

      // package cant be null If package doesn't already exist, create a new instance
      if (package == null) {

        package = new P() {
          Name = nameOfPackage
        };

        // If no packages passed, default to that of the class
        if (packages == null) {
          packages = this.Packages;
        }

        // Add package to package list
        packages.Add(package);
        
      }

      return package;

    }

 
    // Find the package name within the list of package
    private IPackage searchPackage(string nameOfPackage) {

      if (string.IsNullOrWhiteSpace(nameOfPackage)) {
        throw new ArgumentException("Package has to have the name it cant be empty");
      }

      return this.Packages.Find(
        p => p.Name.Equals(
          nameOfPackage,
          StringComparison.CurrentCultureIgnoreCase
        ));

    }

    // this method Generates a dependency map
    public string[] GetMap() {

      var map = new List<string>();
     
      foreach (var package in this.Packages) {
        GetDependenciesOfPackage(package, map);
      }

      return map.ToArray();

    }
        
    //add dependency name in the order in the tree
    private void GetDependenciesOfPackage(IPackage package, IList map) {

      // Recurse through tree if dependency exists
      if (package.Dependency != null) {
        GetDependenciesOfPackage(package.Dependency, map);
      }

      // if package is already in te list than dont add it
      if (map.Contains(package.Name)) {
        return;
      }

      // finally Add the package Name to the list.
      map.Add(package.Name);

    }

    // Determines if the package has a a loop through recursion
    private bool PackageHasLoop(IPackage package, string originalnameOfPackage) {

      //dependency cant be null. When dependency is null, can't be a cycle
      if (package.Dependency == null) {
        return false;
      }

      // its loop When dependency Name is the same as the original package name
      if (package.Dependency.Name == originalnameOfPackage)
      {
                return true;
      }

      //one more condition to check loop is When package and its dependency have the same name
      if (package.Equals(package.Dependency)) {
        return true;
      }

     
      return PackageHasLoop(package.Dependency, originalnameOfPackage);

    }

  }

}
