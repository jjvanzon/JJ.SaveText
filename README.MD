JJ Code Base
============

Larger code base with multiple applications:

- Question & Answer
- SaveText prototype app
- Some misc demos

These projects used to be in there, but were moved to the Archive folder, because they have their own separate repositories now:

- [JJ.Framework](https://github.com/jjvanzon/JJ.Framework)
- [JJ.Synthesizer](https://github.com/jjvanzon/JJ.Synthesizer)


Modularization
--------------

Circle3 was the previous code base, that started being developed around 2010. It was about a quite specific way of programming, not like mainstream .NET development. It had a first-order grouping by functionality.

The way of working was not portable between what I do at home and what I do at the office. My ideas about software architecture have changed.

JJ code base is newer. It started being developed around 2013. Its architecture is more agnostic about specific data models, specific programming models, specific API's and specific functionality. The first order grouping is now by technical aspects such as apps, services, framework, validation, security, etcetera. The idea is that that software architecture might accommodate different types of programming and different types of functionality in an orderly fashion.

I think a first-order grouping by technical aspects instead of by functional aspects may be of help, because technology can change fast, but functionality may change even faster, so for a main outline of things, technical aspects might be more stable as a subdivision, than functional aspects, which may change more frequently, in my opinion.


Setting Up Dev Environment
----------------------

### JJ.Framework

- Pre-release versions of *JJ.Framework* components could be used.
- They may require configuring *NuGet* to connect to an additional package source.
- The following info might be useful for that:
- Name: JJs-Pre-Release-Package-Feed
- Source: https://pkgs.dev.azure.com/jjvanzon/1de16010-421a-41a5-90f1-86e9513f2c5b/_packaging/JJs-Pre-Release-Package-Feed/nuget/v3/index.json

### Internet Information Services (IIS)

- Some web projects might like to run on IIS (Internet Information Services).
- Otherwise they may not load.
- Trying to *reload* a web project:
- The Output window in Visual Studio could show an expected web address.
- It is suggested to install IIS.
- Creating a web site.
- Point its *physical path* to the folder of the csproj that might not load.
- Having a *binding* that uses the web address previously found in the Visual Studio output as a *host name*.
- The file C:\Windows\System32\drivers\etc\hosts may need editing.
- Entries like the following might be added:

    ```
    127.0.0.1  dev.questionandanswer.jjvanzon.io
    ::1        dev.questionandanswer.jjvanzon.io
    127.0.0.1  dev.savetextappservice.jjvanzon.io
    ::1        dev.savetextappservice.jjvanzon.io
    127.0.0.1  dev.savetext.jjvanzon.io
    ::1        dev.savetext.jjvanzon.io
    ```

### Databases

#### QuestionAndAnswerDB

- The database backup can be found under "Database\QuestionAndAnswer"
- The file can be unzipped and restored in SQL Server with name DEV_QuestionAndAnswerDB
- And again with name DEV_QuestionAndAnswerDB_UnitTests
- The database user name "dev" could be expected with password "dev".

#### SaveTextDB

- The folder Database\SaveTextDB should contain a database backup.
- The file can be restored in SQL Server with name "DEV_SaveTextDB".
- The database user name "dev" could be expected with password "dev".