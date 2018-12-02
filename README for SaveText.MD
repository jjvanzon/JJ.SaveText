# JJ.SaveText

*Jan-Joost van Zon, 2018-12-02*

The *SaveText prototype app* is an experiment in which I proved and refined my favorite software design patterns.
The goal was to make things compatible with many different platforms. It can run on:

* Databases
* XML files
* Mobile
* iOS
* Android
* Windows Phone
* MVC
* WCF
* Unity Game Engine

The folder structure is representative of the main layering:

* Data
* Business
* Presentation

Other folders:

* Database: Database initialization and migration
* Demos: Arbitrary demo code

## Set Up Dev Environment

* The solutions will only build if you clone another repository too: JJ.Framework. ([https://github.com/jjvanzon/JJ.Framework](https://github.com/jjvanzon/JJ.Framework))
* It must be cloned  to a folder 'JJ.Framework' that is right next to your 'JJ.SaveText' repository clone.
* First build JJ.Framework.sln or at least NuGet-restore it.
* Then build one of the solutions in this repository.

## Newer Tech

This project is from 2015. Newer tech has emerged or become freely available (e.g. .NET Standard, Xamarin, Angular, Unity Game Engine free in Visual Studio Community edition). This project has not been updated since then.