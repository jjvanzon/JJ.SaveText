*JJ van Zon, 2022*

💾 JJ.SaveText ☁
=================

<h2>📔 Contents</h2>

- [Prototype App](#prototype-app)
- [Multi-Platform](#multi-platform)
- [🧰 Design Patterns](#-design-patterns)
- [🛠 Techniques](#-techniques)
- [🏛 Minimal Functionality / Max Architecture](#-minimal-functionality--max-architecture)
- [💾 Saving a Text](#-saving-a-text)
- [☁ Online / Offline Variations](#-online--offline-variations)
- [🔮 Newer Tech](#-newer-tech)
- [🕹 Unity Game Engine](#-unity-game-engine)
- [🌐 web Web Site (MVC)](#-web-web-site-mvc)
- [👨‍💻 App Service](#-app-service)
- [💻 Windows Applications](#-windows-applications)
- [📱 Mobile Apps](#-mobile-apps)
- [📂 Folders](#-folders)
- [⚙ Dev Env](#-dev-env)
    - [Visual Studio](#visual-studio)
    - [Unity Game Engine](#unity-game-engine)
    - [Database](#database)
    - [JJ.Framework](#jjframework)
    - [Internet Information Services (IIS)](#internet-information-services-iis)
- [🕘 Checking Out Old Commits](#-checking-out-old-commits)

Prototype App
-----------------

The *SaveText prototype app* was an experiment from around 2015, to test certain software programming techniques.


Multi-Platform
--------------

One goal was to try and deploy .NET code on multiple platforms, like web or mobile, running on a database or saving a file on the file system instead.

<img src="images\save-text-1.jpg" width="750" />
<img src="images\save-text-2.jpg" width="750" />


🧰 Design Patterns
-------------------

Another goal was to test if certain design patterns might work practically, or at all under these circumstances. Design patterns are tested ways to structure your code, to make it better organized, to support a certain technical value like maintainability.


🛠 Techniques
--------------

The following platforms, frameworks and techniques were tried out:

- Databases
- XML files
- Mobile
- Web
- iOS
- Android
- Windows Phone
- MVC
- WCF
- Hand-written SOAP client
- Unity Game Engine
- Entity Framework
- NHibernate
- WinForms
- Online/offline sync


🏛 Minimal Functionality / Max Architecture
--------------------------------------------

*SaveText* was an attempt to test a code architecture with an idea of *minimal functionality, maximal architecture*. The idea was, that when certain patterns might work for simple situations, that might extend to more complex functionality. To see if the components can work together to accomplish technical desires. The results of that effort, are a proof of concept of techniques that might apply to more complex and perhaps more useful applications.

The patterns and practices in this project closely match the following guidelines for structuring software: [JJ's Reference Architecture](https://github.com/jjvanzon/JJs-Reference-Architecture)


💾 Saving a Text
-----------------

As such, the only functionality *SaveText* possesses, would be to simply save a piece of text somewhere. Not much more than that.


☁ Online / Offline Variations
------------------------------

Explored were several online / offline variations: for instance one where the application talks to an online web service upon all user actions. Another variation is one, where user actions would cause local code to go off and things to be locally stored, until a synchronization might go off in the background, to send things to an online service in the cloud.


🔮 Newer Tech
--------------

This project was from around 2015. Newer tech may have emerged or become (freely) available (e.g. .NET Standard, Xamarin, Angular, Unity Game Engine free in Visual Studio Community edition and .NET 5.0 and above). This project might not have been updated since then.


🕹 Unity Game Engine
---------------------

The choice of Unity Game Engine may seem random. But it was the first technology at that time, that would work for deploying my code on multiple mobile platforms and using the Mono compiler. Other tech wouldn't work, crashed on me, or simply did not exist at that time.


🌐 web Web Site (MVC)
---------------------

- This is a 'web site' variation on SaveText.
- It uses ASP.NET MVC technology for the user interface.
- It tries to connect to a database, using NHibernate as the underlying data access technology.


👨‍💻 App Service
---------------

- This is like a 'cloud' web service, to which some of the apps might connect.
- It exposes a SOAP web service using WCF communication technology.
- It can connect to a database, using NHibernate as the underlying data access technology.


💻 Windows Applications
------------------------

Several Windows applications were made.  
They may test different online/offline situations.  
And different types of storing the data too.

...

- *JJ.Presentation.SaveText.WinForms.Offline*
    - Works offline.
    - Stores data in a local XML file, using System.Xml.Linq, as the underlying data access technology.
- *JJ.Presentation.SaveText.WinForms.OfflineWithSync*
    - Stores data in a local XML file, using System.Xml, as the underlying data access technology.
    - May sync with SaveText's cloud service at certain intervals.
    - It uses WCF for that as the communication technology.
- *JJ.Presentation.SaveText.WinForms.Online*
    - Does not store locally.
    - Contacts a SaveText's cloud service for each action a user may perform.
    - It uses WCF for that as the communication technology.
- *JJ.Presentation.SaveText.WinForms.Online.CustomSoapClient*
    - Does not store locally.
    - Contacts a SaveText's cloud service for each action a user may perform.
    - It uses a custom-programmed SOAP client for a communication technology.
- *JJ.Presentation.SaveText.WinForms.OnlineOfflineSwitched*
    - Can be in online mode, but switch to offline mode as well and back.
    - When online it may contact a SaveText's cloud service for each action a user can perform.
    - When offline it would store data locally in an XML file, using System.Xml, as the underlying data access technology.


📱 Mobile Apps
--------------

- The mobile deployments were tested once, but a while back, and may not be up to date with technology. 
- So maybe do not to expect too much from them.
- They might not be compatible anymore with newer mobile devices.
- The precise way these deployment files are to be used, is not be described here. Here is just an impression of it.
- The *Android* variations might have been installed once, by copying it to the right location on an Android phone.
- The *iOS* variation required an Apple Mac running XCode and a paid Apple Developer Program subscription.
- The *Windows Phone* variation was deployed, using a version of Visual Studio specifically tailored for Windows Phone.
- The mobile deployments may not have all the variations (Online, Offline, OfflineWithSync, etc.) The possible cause of this is, that testing did not get further than trying to connect to a web service from Unity Game Engine technology. Web service connections might not have worked that well yet using Unity Game Engine on Android, so it might not have gotten further than that.


📂 Folders
-----------

The folder structure is representative of the main layering:

- Data
- Business
- Presentation

Another folder:

- Database: Database initialization and migration


⚙ Dev Env
-----------

Here you may find some pointers how to set up a developent environment to build and run `JJ.SaveText` projects.

### Visual Studio

- Visual Studio Community 2019 or 2022 may be needed to open the projects and solution file.

### Unity Game Engine

- Some projects were made using Unity Game Engine, version unknown, sorry.

### Database

- The folder Database\SaveTextDB may contain a database backup.
- The file might be restored in SQL Server with name "DEV_SaveTextDB".
- The database user name "dev" may be expected with password "dev".

### JJ.Framework

- Pre-release versions of `JJ.Framework` components may be used.
- Those are currently hosted in *Azure DevOps*:
- https://pkgs.dev.azure.com/jjvanzon/1de16010-421a-41a5-90f1-86e9513f2c5b/_packaging/JJs-Pre-Release-Package-Feed/nuget/v3/index.json
- Name: JJs-Pre-Release-Package-Feed
- It might require configuring *NuGet* in your dev environment to connect to this additional package source.

### Internet Information Services (IIS)

- Some web projects might like to run under Internet Information Services (IIS).
- They may not load otherwise.
- Trying to *reload* a web project:
- The Output window in Visual Studio might show an expected web address and host name, that might be useful for setting things up.
`-----`
- It is suggested to install IIS.
- Create web site for each of the relevant projects.
- Also the Windows hosts file might be changed. Each site added to IIS might need entries in the file `C:\Windows\System32\drivers\etc\hosts`.
- Here follow suggestions for each of the sites to create and their settings.
- (Physical path root folder `C:\Repositories\JJ.SaveText` may vary.)

`-----`

- __Web Site__

    - C# project:
        - `JJ.Presentation.SaveText.Mvc.csproj`
    - IIS site name:
        - `DEV_JJ.Presentation.SaveText.Mvc`
    - IIS site physical path:
        - `C:\Repositories\JJ.SaveText\Presentation\SaveText.Mvc`
    - IIS site binding host name:
        - `dev.savetext.jjvanzon.io`
    - hosts file entries:
        - `127.0.0.1  dev.savetext.jjvanzon.io`
        - `::1        dev.savetext.jjvanzon.io`

- __App Service__

    - C# project:
        - `JJ.Presentation.SaveText.AppService`
    - IIS site name:
        - `DEV_JJ.Presentation.SaveText.AppService`
    - IIS site physical path:
        - `C:\Repositories\JJ.SaveText\Presentation\SaveText.AppService`
    - IIS site binding host name:
        - `dev.savetextappservice.jjvanzon.io`
    - Additional IIS binding with port:
        - `6371`
    - hosts file entries:
        - `127.0.0.1  dev.savetextappservice.jjvanzon.io`
        - `::1        dev.savetextappservice.jjvanzon.io`
    - Unfortunately, some mobile apps may try to reach host `83.82.26.17:6371` instead.


🕘 Checking Out Old Commits
----------------------------

`JJ.SaveText` was once part of a larger code base. It was extracted to become a new Git repository with history in tact. Some quirks when checking out older history items, may have to do with that. The following may only be relevant when getting older commits from history (from before 2022-11-18).

- Commit messages:
    - May mention projects not part of the `JJ.SaveText` repo.
- Broken references to `JJ.Framework`:
    - `JJ.Framework` projects are no longer in the same code base.
    - It may help to try referencing the NuGet version of these depedencies.
    - Another option may be to clone the [JJ.Framework repository](https://github.com/jjvanzon/JJ.Framework) and fix the paths pointing to the `JJ.Framework` projects.
- References to other non-existent projects:
    - To fix this, try referencing the NuGet version of these depedencies.