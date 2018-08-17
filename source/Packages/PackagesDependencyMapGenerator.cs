using PackageInstallerExercise.Interfaces;
using System;

namespace PackageInstallerExercise.Packages {

  // Package Dependency Map Generator
  public class PackagesDependencyMapGenerator : DependencyMapGeneratorInterface {

    private char seperator;
    private DependencyMapInterface dependencyMap;

    public PackagesDependencyMapGenerator(char delimiter, DependencyMapInterface packageDependencyMap) {
      seperator = delimiter;
      dependencyMap = packageDependencyMap;
    }
        
    // Create Dependency Map
    public string[] CreateMap(string[] sample) {
      FillMap(sample);
      return this.dependencyMap.GetMap();
    }

    // this method Fills dependency map with the sample provided
    private void FillMap(string[] sample) {

      foreach (string definition in sample) {

        // Strip out package:dependency from string
        string[] packageAndDependency = definition.Split(this.seperator);

        if (packageAndDependency.Length != 2) {
          throw new FormatException("Dependency string has to be in specified format it is not in the correct format.");
        }

        string nameOfPackage = packageAndDependency[0].Trim();
        string nameOfDependency = packageAndDependency[1].Trim();

        this.dependencyMap.insertPackage(nameOfPackage, nameOfDependency);

      }

    }

  }
}
