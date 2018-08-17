/*

You suddenly have a curious aspiration to create a package installer that can handle dependencies. 
You want to be able to give the installer a list of packages with dependencies, and 
have it install the packages in order such that an install won’t fail due to a missing dependency.  
This exercise is to write the code that will determine the order of install.  
Please complete a solution in JavaScript or C#.
 
Usage:
This is the console application which accepts one command line argument. 
This argument will contain all the packages and their dependencies which is seperated by comma enclosed in quotes

Example of how to use:
C:\PackageInstallerExercise "KittenService: CamelCaser, CamelCaser:"

*/

using PackageInstallerExercise.Interfaces;
using PackageInstallerExercise.Packages;
using PackageInstallerExercise.Types;
using System;

namespace PackageInstallerExercise {


  public class Program {

    // Interface used to print the results
    private OutputWriterInterface outputWritter;
    private DependencyMapGeneratorInterface dependencyMapGenerator;
    private string[] sample;

    public Program(OutputWriterInterface writer, DependencyMapGeneratorInterface generator) {
      outputWritter = writer;
      dependencyMapGenerator = generator;
    }

    public static int Main(string[] args) {

      var dependencyMap = new PackagesDependencyMap<Package>();
            
      var program = new Program(
        new ConsoleOutputPrinter(),
        new PackagesDependencyMapGenerator(':', dependencyMap)
      );

      return (int)program.Run(args);

    }


    public ConsoleReturnTypes Run(string[] args) {

      ConsoleReturnTypes result;

      try {

        // accept the arguments from command line
        result = acceptArguments(args);

        //in case of failure stop execution and inform user
        if (result != ConsoleReturnTypes.Success) {
          HandleError(result);
          return result;
        }

        var dependencyMap = dependencyMapGenerator.CreateMap(this.sample);
        WriteLine(string.Join(", ", dependencyMap));

      }
      catch (Exception e) {
        result = ConsoleReturnTypes.Rejected;
        HandleError(result, e.Message);
      }

      return result;

    }
        
    // display failure message to the user
    private void HandleError(ConsoleReturnTypes failureType, string details = null) {

      switch (failureType) {
        case ConsoleReturnTypes.NoArguments:
        case ConsoleReturnTypes.ArgumentsNotInCorrectFormat:
          WriteLine("please Enter a list of dependencies.");
          WriteLine("Example of usage: packageinstallerexcercise \"KittenService: CamelCaser, CamelCaser:\"");
          break;

        case ConsoleReturnTypes.TooManyArguments:
          WriteLine("provide one argument only. not more tan one or less than one");
          break;

        default:
          string line = string.Format(
            "An error occurred: {0}. \nDetails: {1}.",
            Enum.GetName(typeof(ConsoleReturnTypes), failureType),
            details
          );

          WriteLine(line);
          break;

      }

    }

   //Accept the argument from the user through command line
    private ConsoleReturnTypes acceptArguments(string[] args) {

      // can not have more or less than 1 argument. should have only one argument.
      if (args.Length == 0) {
        return ConsoleReturnTypes.NoArguments;
      }

      // it cant handle more than one argument. can only handle 1 argument
      if (args.Length > 1) {
        return ConsoleReturnTypes.TooManyArguments;
      }

      // Get the combined packages
      var packagesList = args[0];
            
      // argument should not be empty and check whether it contains the seperator :
      if (string.IsNullOrEmpty(packagesList) ||
        !packagesList.Contains(":")) {
        return ConsoleReturnTypes.ArgumentsNotInCorrectFormat;
      }

      ParsePackagesList(packagesList);

      // it should have package name and seperator. cant have only seperator :
      if (this.sample.Length == 1 && this.sample[0] == ":") {
        return ConsoleReturnTypes.ArgumentsNotInCorrectFormat;
      }

      return ConsoleReturnTypes.Success;

    }

    
    private void ParsePackagesList(string packagesList) {

      // Split each package:dependency
      var splitsample = packagesList.Split(',');

      // remove white spaces
      for (int i = 0; i < splitsample.Length; i++) {
        splitsample[i] = splitsample[i].Trim();
      }

      // Set the sample for this program instance
      this.sample = splitsample;

    }

    // write output to the screen
    private void WriteLine(string s) {
      outputWritter.WriteLine(s);
    }

  }

}