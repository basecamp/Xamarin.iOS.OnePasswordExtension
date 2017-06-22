# Xamarin.iOS.OnePasswordExtension

Xamarin bindings for [1Password Extension](https://github.com/agilebits/onepassword-app-extension).

## Usage

The source paired with the [Agile Bits documentation](https://github.com/agilebits/onepassword-app-extension/tree/1.8.4#integrating-1password-with-your-app) will be your best resource, but some basic usage:

```csharp
using Xamarin.iOS.OnePasswordExtension;
// class {
    // Only Add if this iOS device has 1Password available
    // ?? because this isn't currently working on iphone-simulator.
    // Must run on Device
    if ((OnePasswordExtension.SharedExtension?.IsAppExtensionAvailable ?? false)) {
        // On Button press or something {
            OnePasswordExtension.SharedExtension.FindLoginForURLString("https://your-domain.com", viewController, buttonControl, (NSDictionary loginDictionary, NSError error) => {
                if (loginDictionary == null || loginDictionary.Count == 0) {
                    if (error.Code != AppExtension.ErrorCodeCancelledByUser) {
                        System.Diagnostics.Debug.WriteLine(@"Error invoking 1Password App Extension for find login: {0}", error);
                    }
                    return;
                }

                var loginString = ((NSString)loginDictionary[AppExtension.UsernameKey]).ToString();
                var passwordString = ((NSString)loginDictionary[AppExtension.PasswordKey]).ToString();

                if (loginString.Length > 0 && passwordString.Length > 0) {
                    // Set login/password inputs
                }
            });
        // }
    }
//}
```

## Rebuilding

All of the rebuilding mechanisms are within `make`. The Makefile specifies a version, which is the tag
from the Agile Bits repo it checks out. Once it's checked out, it will build that project and extract
an archive for binding purposes. That archive is bundled within the Nuget package.

To rebuild from scratch:

* `make all` to pull the extension repo, build an archive and write a binding file
* The binding file (`ApiDefinitions.cs`) needs to have `Verify` and `Unavailable` attributes commented out
* If you're planning to release a new version, bump the version in `Xamarin.iOS.OnePasswordExtension/Xamarin.iOS.OnePasswordExtension.nuspec`
* `make build` will build the C# project and build a nupkg
* `make publish` will push to nuget - you may need API key from nuget