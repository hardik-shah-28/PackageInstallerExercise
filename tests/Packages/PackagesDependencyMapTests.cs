using Microsoft.VisualStudio.TestTools.UnitTesting;
using PackageInstallerExercise;
using PackageInstallerExercise.Packages;
using PackageInstallerExercise.Packages.Exceptions;
using PackageInstallerExercise.Packages.Interfaces;

namespace PackageInstallerExercise.Test {

  [TestClass]
  public class PackagesDependencyMapTests {

    private PackagesDependencyMap<PackageMock> dependencyMap;

    [TestInitialize()]
    public void Initialize() {
      dependencyMap = new PackagesDependencyMap<PackageMock>();
    }

    #region Tests

      #region insertPackage

      [TestMethod]
      [Description("Should add a package to the dependency map.")]
      public void TestinsertPackageToDependencyMap() {

        // Arrange
        string nameOfPackage = "KittenService",
               nameOfDependency = "CamelCaser";

        var expectedPackage = new PackageMock() {
          Name = nameOfPackage,
          Dependency = new PackageMock() {
            Name = nameOfDependency
          }
        };

        // Act
        var actualPackage = dependencyMap.insertPackage(nameOfPackage, nameOfDependency);

        // Assert
        Assert.AreEqual(expectedPackage, actualPackage);

      }

      [TestMethod]
      [Description("Should add a package and link its dependency to an existing package.")]
      public void TestinsertPackageToDependencyMapAndLink() {

        // Arrange
        string nameOfPackage = "KittenService",
               nameOfDependency = "CamelCaser";

        var expectedPackage = new PackageMock() {
          Name = nameOfPackage,
          Dependency = new PackageMock() {
            Name = nameOfDependency
          }
        };

        dependencyMap.insertPackage(nameOfDependency); // Add package dependency first

        // Act
        var actualPackage = dependencyMap.insertPackage(nameOfPackage, nameOfDependency);

        // Assert
        Assert.AreEqual(expectedPackage, actualPackage);

      }

      [TestMethod]
      [Description("Should add a package and a new dependency to the package list when it doesn't already exist.")]
      public void TestinsertPackageToDependencyMapAndCreateDependencyPackage() {

        // Arrange
        string nameOfPackage = "KittenService",
               nameOfDependency = "CamelCaser";

        var expectedPackage = new PackageMock() {
          Name = nameOfPackage,
          Dependency = new PackageMock() {
            Name = nameOfDependency
          }
        };

        // Act
        var actualPackage = dependencyMap.insertPackage(nameOfPackage, nameOfDependency);

        // Assert
        Assert.AreEqual(expectedPackage, actualPackage);

      }

        #region Contains Cycle

        [TestMethod]
        [Description("Should throw PackageContainsCycleException Error.")]
        [ExpectedException(typeof(ExceptionForPackageHavingLoop))]
        public void TestinsertPackageThrowContainsCycleException() {

          // Arrange
          dependencyMap.insertPackage("A", "C");
          dependencyMap.insertPackage("B", "A");

          // Act & Assert
          dependencyMap.insertPackage("C", "B");

        }

        [TestMethod]
        [Description("Should throw PackageContainsCycleException Error. Scenario from exercise.")]
        [ExpectedException(typeof(ExceptionForPackageHavingLoop))]
        public void TestinsertPackageThrowContainsCycleException2() {

          // Arrange
          dependencyMap.insertPackage("A");
          dependencyMap.insertPackage("B", "C");
          dependencyMap.insertPackage("C", "F");
          dependencyMap.insertPackage("D", "A");
          dependencyMap.insertPackage("E");

          // Act & Assert
          dependencyMap.insertPackage("F", "B");

        }

        [TestMethod]
        [Description("Should throw PackageContainsCycleException Error when a package and its dependency are the same.")]
        [ExpectedException(typeof(ExceptionForPackageHavingLoop))]
        public void TestinsertPackageThrowContainsCycleExceptionWhenSamePackageAdded() {
          // Arrange, Act & Assert
          dependencyMap.insertPackage("A", "A");
        }

        #endregion

      [TestMethod]
      [Description("Should throw PackageDuplicateException Error package has already been added.")]
      [ExpectedException(typeof(ExceptionForPackageDuplication))]
      public void TestinsertPackageThrowPackageDuplicateException() {

        // Arrange
        dependencyMap.insertPackage("A", "B");

        // Act
        dependencyMap.insertPackage("a"); // Add same package but lowercase

      }

      #endregion

    [TestMethod]
    [Description("Should return a dependency map. Scenario from exercise.")]
    public void TestGetMap() {

      // Arrange
      var expected = new string[] { "A", "F", "C", "B", "D", "E" };
      
      dependencyMap.insertPackage("A");
      dependencyMap.insertPackage("B", "C");
      dependencyMap.insertPackage("C", "F");
      dependencyMap.insertPackage("D", "A");
      dependencyMap.insertPackage("E", "B");
      dependencyMap.insertPackage("F");

      // Act
      var actual = dependencyMap.GetMap();

      // Assert
      CollectionAssert.AreEqual(expected, actual);

    }

    [TestMethod]
    [Description("Should return a dependency map. Scenario 2.")]
    public void TestGetMap2() {

      // Arrange
      var expected = new string[] { "C", "A", "B" };
      dependencyMap.insertPackage("A", "C");
      dependencyMap.insertPackage("B", "C");
      dependencyMap.insertPackage("C");

      // Act
      var actual = dependencyMap.GetMap();

      // Assert
      CollectionAssert.AreEqual(expected, actual);

    }

    #endregion

    public class PackageMock : IPackage {

      public string Name { get; set; }
      public IPackage Dependency { get; set; }

      
      public override bool Equals(object obj) {

        // parameters cant be null If parameter is null return false.
        if (obj == null) {
          return false;
        }

        // parameter must be cast to package. return false if it cant.
        PackageMock p = obj as PackageMock;
        if ((System.Object)p == null) {
          return false;
        }

        // Return true if the fields matches.
        return Name == p.Name;

      }

        public override string ToString() {

        string value = this.Name;

        if (this.Dependency != null) {
          value += ":" + this.Dependency.Name;
        }

        return value;
      }

    }

  }

}
