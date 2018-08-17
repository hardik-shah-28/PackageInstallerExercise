using Microsoft.VisualStudio.TestTools.UnitTesting;
using PackageInstallerExercise.Packages;

namespace PackageInstallerExercise.Test.Packages {

  [TestClass]
  public class PackageTests {

    [TestMethod]
    [Description("it must return a nameOfPackage:nameOfDependency string")]
    public void TestToStringWithDependency() {

      
      string nameOfPackage = "A",
          nameOfDependency = "B";

      var package = new Package() {
        Name = nameOfPackage,
        Dependency = new Package() {
          Name = nameOfDependency
        }
      };

      var actual = package.ToString();

      // Assert
      Assert.AreEqual(actual, nameOfPackage + ":" + nameOfDependency);

    }

    [TestMethod]
    [Description("it must return a nameOfPackage string.")]
    public void TestToStringWithoutDependency() {

      string nameOfPackage = "A";

      var package = new Package() {
        Name = nameOfPackage
      };

      var actual = package.ToString();

      // Assert
      Assert.AreEqual(actual, nameOfPackage);

    }

    [TestMethod]
    [Description("when both packages have same name and dependency than it Should be equal.")]
    public void TestEqualSameNameAndDependency() {
            
      string nameOfPackage = "A",
          nameOfDependency = "B";

      var package1 = new Package() {
        Name = nameOfPackage,
        Dependency = new Package() {
          Name = nameOfDependency
        }
      };

      var package2 = new Package() {
        Name = nameOfPackage,
        Dependency = new Package() {
          Name = nameOfDependency
        }
      };

      // Act & Assert
      Assert.AreEqual(package1, package2);

    }

    [TestMethod]
    [Description("if both packages have same name but has different dependencies than it should be equal.")]
    public void TestEqualSameNameDifferentDependency() {

      // Arrange
      string nameOfPackage = "A",
          nameOfDependency = "B";

      var package1 = new Package() {
        Name = nameOfPackage,
        Dependency = new Package() {
          Name = nameOfDependency
        }
      };

      var package2 = new Package() {
        Name = nameOfPackage
      };

      // Act & Assert
      Assert.AreEqual(package1, package2);

    }

    [TestMethod]
    [Description("if both packages have same name but different dependencies than it should not be equal.")]
    public void TestNotEqual() {
            
      var package1 = new Package() {
        Name = "A"
      };

      var package2 = new Package() {
        Name = "B"
      };

      // Act & Assert
      Assert.AreNotEqual(package1, package2);

    }

  }

}