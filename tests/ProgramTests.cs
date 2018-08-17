using Microsoft.VisualStudio.TestTools.UnitTesting;
using PackageInstallerExercise.Interfaces;
using PackageInstallerExercise.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PackageInstallerExercise.Test {

  [TestClass]
  public class ProgramTests {

    private ConsoleOutputWriterMock writer;
    private PackageDependencyMapGeneratorMock generator;
    private Program program;

    [TestInitialize()]
    public void Initialize() {
      writer = new ConsoleOutputWriterMock();
      generator = new PackageDependencyMapGeneratorMock();
      program = new Program(writer, generator);
    }

    [TestMethod]
    [Description("it Should write successful output on console when proper arguments is passed")]
    public void TestMainOutput() {
            
      string[] input = { "KittenService: CamelCaser, CamelCaser:" };
      var expectedOutput = "CamelCaser, KittenService";
            
      program.Run(input);

      // Assert
      Assert.AreEqual(expectedOutput, writer.GetLastLine());

    }

    [TestMethod]
    [Description("when arguments are not passed than it should fail")]
    public void TestMainNoArguments() {
            
      string[] input = { };
            
      var result = program.Run(input);

      // Assert
      Assert.AreEqual(ConsoleReturnTypes.NoArguments, result);

    }

    [TestMethod]
    [Description("when more than one arguments are passed than it should fail.")]
    public void TestMainArgumentsMoreThanOne() {
            
      string[] input = { "argument1", "argument2" };
            
      var result = program.Run(input);

      // Assert
      Assert.AreEqual(ConsoleReturnTypes.TooManyArguments, result);

    }

    [TestMethod]
    [Description("colon should be present in the argument. if there is no colon than it should fail.")]
    public void TestMainArgumentNotContainColon() {
            
      string[] input = { "argument1 without colon" };
            
      var result = program.Run(input);

      // Assert
      Assert.AreEqual(ConsoleReturnTypes.ArgumentsNotInCorrectFormat, result);

    }

    [TestMethod]
    [Description("it Should contain a colon.")]
    public void TestMainArgumentContainsColon() {
            
      string[] input = { "KittenService:" };
            
      var result = program.Run(input);

      // Assert
      Assert.AreEqual(ConsoleReturnTypes.Success, result);

    }

    [TestMethod]
    [Description("there has to have arguments.Argument cannot be empty.")]
    public void TestMainArgumentEmptyString() {
            
      string[] input = { "" }; 
            
      var result = program.Run(input);

      // Assert
      Assert.AreEqual(ConsoleReturnTypes.ArgumentsNotInCorrectFormat, result);

    }

    [TestMethod]
    [Description("colon cant be alone. argument cant have just colon")]
    public void TestMainArgumentJustColon() {

      // Arrange
      string[] input = { ":" };
            
      var result = program.Run(input);
            
      Assert.AreEqual(ConsoleReturnTypes.ArgumentsNotInCorrectFormat, result);

    }

    [TestMethod]
    [Description("when something went wrong just inform user.")]
    public void TestRunInformUserOfFailure() {

    
      string[] input = { "" }; 

      var result = program.Run(input);

      // Assert
      Assert.IsTrue(writer.HasBeenCalled());

    }

    [TestMethod]
    [Description("Parse the dependency list string into an array of packages.")]
    public void TestRunParseDependencyList() {

      string[] input = { "KittenService: CamelCaser, CamelCaser:" };
      string[] expected = { "KittenService: CamelCaser", "CamelCaser:" };

      var result = program.Run(input);

      // Assert
      CollectionAssert.AreEqual(expected, generator.sample);

    }

    [TestMethod]
    [Description(" display a message to the user when it Catches & handles an unknown error")]
    public void TestRunHandleUnknownError() {

      // Arrange
      string[] input = { "KittenService: CamelCaser, CamelCaser:" };
      generator.ThrowError = true;

      // Act
      var result = program.Run(input);

      // Assert
      Assert.AreEqual(ConsoleReturnTypes.Rejected, result);
      Assert.IsTrue(writer.HasBeenCalled());

    }

  }

  // Stub ConsoleOutputWriter for mocking anything printed on the screen
  public class ConsoleOutputWriterMock : OutputWriterInterface {

    private List<string> printvalues = new List<string>();

    public void WriteLine(string s) {
      printvalues.Add(s);
    }

    public string GetLastLine() {
      return printvalues.Last();
    }

    public bool HasBeenCalled() {
      return printvalues.Count > 0;
    }

  }

  // Stub PackageDependencyMapGenerator for mocking the map creation
  public class PackageDependencyMapGeneratorMock : DependencyMapGeneratorInterface {

    public bool ThrowError { get; set; }
    public string[] sample { get; set; }

    public string[] CreateMap(string[] sample) {

      if (this.ThrowError) {
        throw new Exception("Exception");
      }
      else {
        this.sample = sample;
        return new string[] { "CamelCaser", "KittenService" };
      }

    }
    
  }

}