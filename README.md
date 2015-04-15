# Testify

Built upon a ‘best of breed’ open-source tool stack (including J/NUnit, FIT, Fitnesse and Selenium), the Testify wizard allows you to create a new project complete with file structure, skeleton code, running unit tests, acceptance tests, code coverage measures and automated one-button build-and-test process. All it requires is a project name, and the rest is handled by Testify.

Testify creates a running, fully portable, stand-alone project. It can also be easily integrated into source-code control systems and continuous integration environments.

The core Testify generation engine and the original Visual Studio 2005 solution template are based heavily on TreeSurgeon which was originally developed by Mike Roberts and Bil Simser. TreeSurgeon is maintained at http://www.codeplex.com/treesurgeon

 * The Testify presentation slides from Agile Testing Days are in the docs folder
 * Now features BDD type stories with easyb or FitNesse
 * Can now run FitNesse from Ant build (try "go accept")
 * Version 1.1.22 Introduces template import from a zip file. This will be the way forward and VS2010 support is delivered this way, see VS2010.zip in downloads. Future releases of Testify wizard will remove existing templates and repackage them into zips to reduce the testify installer size.
 * Added VS2010 Template with SpecFlow as acceptance test / BDD framework. N.B. You must install VS2010.zip template first because of .Net 4.0 library and tools dependencies.
 * Added VS2008 Template with SpecFlow. This targets .NET 3.5 Runtime (It also includes Selenium2 runtime and dot net bindings although there is still no sample project yet.)

Automatically exported from code.google.com/p/testifywizard

http://mikeascott.github.io/testifywizard/
