JJ.SaveText
===========

Prototype App
-------------

The *SaveText prototype app* was an experiment to test certain software programming techniques.

Multi-Platform
--------------

One goal might have been to try and deploy .NET code on multiple platforms, like web or mobile, running on a database or perhaps on file system.

Design Patterns
---------------

Another goal could have been to test if certain design patterns might work practically or at all under these circumstances. Design patterns might be tested ways to structure your code, to make it well organized, to accomplish a certain technical value.

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

Maximal Architecture, Minimal Functionality
-------------------------------------------

With *SaveText* a code architecture was tested with an idea of 'maximal architecture, minimal functionality'. The idea might have been that when certain patterns might work for simple situations, that might extend to more complex functionality. To see if the components can work together to accomplish technical desires. The results of that effort may be techniques that might apply to more complex and perhaps more useful applications.

Saving a Text
-------------

As such, the only functionality SaveText might posses, is to simply save a piece of text somewhere. Not much more than that.

Online / Offline Variations
---------------------------

Explored were several online / offline variations: for instance one where the application talks to an online web service upon all user actions. Another variation might be where user actions might cause local code to go off and things to be locally stored, until a synchronization might go off in the back ground, to send things to an online service in the cloud.

Newer Tech
----------

This project may be from around 2015. Newer tech has emerged or become (freely) available (e.g. .NET Standard, Xamarin, Angular, Unity Game Engine free in Visual Studio Community edition). This project has not been updated since then.

Mobile Deployments
------------------

Some mobile deployment files might internally be named SetText instead of SaveText. (The namespace JJ.Presentation might also be JJ.Apps instead in that case.)

The mobile deployments were tested once, but a while back and may not be up to date with technology. So it may be an idea, not to expect too much from them.

The mobile deployments may not have all the variations (Online, Offline,OfflineWithSync, etc.) The possible cause might be that testing did not get further than trying to connect to a web service from Unity Game Engine technology. Web service connections might not have worked using Unity Game Engine even on Android, so it may not have gotten as far as ever being tested on other platforms like iOS and Windows Phone 8.

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

- The solutions may only build if you clone another repository too: JJ.Framework. ([https://github.com/jjvanzon/JJ.Framework](https://github.com/jjvanzon/JJ.Framework))
- It might be cloned  to a folder 'JJ.Framework' that is right next to the 'JJ.SaveText' repository clone.
- First building JJ.Framework.sln or at least NuGet-restoring it might be desired.
- Then might be build, one of the solutions in this repository.

Database
--------

...