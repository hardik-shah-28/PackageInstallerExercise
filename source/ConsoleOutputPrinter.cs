using PackageInstallerExercise.Interfaces;
using System;

namespace PackageInstallerExercise {

 // output printer class to the console
  class ConsoleOutputPrinter: OutputWriterInterface {
    public void WriteLine(string s) {
      Console.WriteLine(s);
    }
  }

}
