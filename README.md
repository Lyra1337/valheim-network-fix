# valheim-network-fix

## About

This little injectable DLL should fix the network bandwidth issue in valheim. It's based on [this](https://windowsreport.com/valheim-lag-server/) dnSpy patch method. The server also needs to get this DLL injected or patched by the dnSpy approach.

## Approach

It uses reflection to find the loaded `assembly_valheim` in the current `AppDomain` and tries to set the private field `m_dataPerSec` in the singleton `TDOMan.instance`.

The program gets the current value and multiplies it by the factor 10 as suggested by the referenced article above.

## Injection

I added a simple Injector Program using the [Reloaded.Injector](https://github.com/Reloaded-Project/Reloaded.Injector) Library.

After Injection the Initialize-Method will be called by the injector.

## Building

Just clone this repo and open the solution in visual studio. It's current target .NET Framework is 4.5 but you could change that to whatever framework you have installed. The referenced helper NuGet package `DllExport` may prompt you to apply some patches to the project file. Select the only project file in the solution and press apply.

## Disclaimer

I am not aware of any kind of anti cheat in this game. But if the developer decides to add one, you'll get a ban when using this.