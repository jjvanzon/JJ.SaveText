JJ.SaveText
===========


Prototype App
-------------

The *SaveText prototype app* was an experiment, to test certain software programming techniques.


Multi-Platform
--------------

One goal might have been, to try and deploy .NET code on multiple platforms, like web or mobile, running on a database or perhaps on file system.


Design Patterns
---------------

Another goal could have been, to test if certain design patterns might work practically, or at all under these circumstances. Design patterns might be tested ways to structure your code, to make it better organized, to accomplish a certain technical value.


Tried Techniques
----------------

The following platforms, frameworks or techniques might have been considered:

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


Minimal Functionality, Maximal Architecture
-------------------------------------------

With *SaveText*, a code architecture was tested with an idea of 'minimal functionality, maximal architecture'. The idea might have been, that when certain patterns might work for simple situations, that might extend to more complex functionality. To see if the components can work together to accomplish technical desires. The results of that effort, may be techniques that might apply to more complex and perhaps more useful applications.


Saving a Text
-------------

As such, the only functionality *SaveText* might possess, would be to simply save a piece of text somewhere. Not much more than that.


Online / Offline Variations
---------------------------

Explored were several online / offline variations: for instance one where the application talks to an online web service upon all user actions. Another variation might be, where user actions might cause local code to go off and things to be locally stored, until a synchronization might go off in the background, to send things to an online service in the cloud.


Newer Tech
----------

This project may be from around 2015. Newer tech may have emerged or become (freely) available (e.g. .NET Standard, Xamarin, Angular, Unity Game Engine free in Visual Studio Community edition). This project might not have been updated since then.


Web Site (MVC)
--------------

- This may be a 'web site' variation on SaveText.
- It may use ASP.NET MVC technology for the user interface.
- It may connect to a database, using NHibernate as the underlying store technology.


App Service
-----------

- This is like a 'cloud' web service, to which some of the apps might connect.
- It may expose a SOAP web service using WCF.
- It may connect to a database, using NHibernate as the underlying store technology.


Windows Applications
--------------------

Several Windows applications might be found.  
They may test online/offline situations.  
And different types of storing the data too.

- *JJ.Presentation.SaveText.Unity.DotNet.Offline*
    - May works offline.
    - May store data in a local XML file, using System.Xml.Linq, as the underlying data storage technology.
- *JJ.Presentation.SaveText.Unity.DotNet.OfflineWithSync*
    - May store data in a local XML file, using System.Xml, as the underlying data storage technology.
    - May sync with SaveText's cloud service at certain intervals.
- *JJ.Presentation.SaveText.WinForms.Online*
    - Does not store locally.
    - May contact a SaveText's cloud service for each action a user may perform.
    - It may use WCF for that.
- *JJ.Presentation.SaveText.WinForms.Online.CustomSoapClient*
    - Does not store locally.
    - May contact a SaveText's cloud service for each action a user may perform.
    - It may use a custom-programmed SOAP client for this.
- *JJ.Presentation.SaveText.WinForms.OnlineOfflineSwitched*
    - Can be in online mode but switch to offline mode and back.
    - When online it may contact a SaveText's cloud service for each action a user may perform.
    - When offline it may store data locally in an XML file, using System.Xml, as the underlying data storage technology.


Mobile Apps
-----------

- The mobile deployments were tested once, but a while back, and may not be up to date with technology. 
- So it may be an idea, not to expect too much from them.
- They may not be compatible anymore with newer mobile devices.
- The precise way these deployment files are to be used might not be described here. Here is an impression of it.
- The Android variations might have been installed once by copying it to the right location on an Android phone.
- The iOS variation may have required an Apple Mac running XCode and a paid developer subscription with Apple.
- The WindowsPhone variation might have been deployed using a version of Visual Studio (2012?) specifically tailored for Windows Phone.
- The mobile deployments may not have all the variations (Online, Offline,OfflineWithSync, etc.) The possible cause might be, that testing did not get further than trying to connect to a web service from Unity Game Engine technology. Web service connections might not have worked using Unity Game Engine on Android, so it may not have gotten as far as being tested on other platforms, like iOS and Windows Phone 8.


Folders
-------

The folder structure may be representative of the main layering:

- Data
- Business
- Presentation

Other folders:

- Database: Database initialization and migration
- Demos: Arbitrary demo code


Setting Up Dev Environment
--------------------------

### JJ.Framework

- The solutions may only build if you clone another repository too: JJ.Framework. ([https://github.com/jjvanzon/JJ.Framework](https://github.com/jjvanzon/JJ.Framework))
- It might be cloned  to a folder "JJ.Framework" that is right next to the "JJ.SaveText" repository clone.
- First building JJ.Framework.sln or at least NuGet-restoring it might be desired.
- Then might be build, one of the solutions in this repository.

### Database

- The folder Database\SaveTextDB may contain a database backup.
- The file might be restored in SQL Server with name "DEV_SaveTextDB".
- The database user name "dev" may be expected with password "dev".