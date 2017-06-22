## Requirements
# * xmllint (command)
# * Xcode
# * Sharpie

TARGET_BUILD_DIR=extern/build
CURRENT_ONE_PASSWORD_EXTENSION_VERSION=1.8.4
OTHER_CFLAGS="-fembed-bitcode"
BITCODE_GENERATION_MODE=bitcode

 # This changes when nuspec version is changed
CURRENT_PROJECT_VERSION=$(shell xmllint --xpath 'string(//version)' Xamarin.iOS.OnePasswordExtension/Xamarin.iOS.OnePasswordExtension.nuspec)

all : clean bind

pull :
	git clone git@github.com:agilebits/onepassword-app-extension.git extern/OnePasswordExtension
	cd extern/OnePasswordExtension; git checkout "${CURRENT_ONE_PASSWORD_EXTENSION_VERSION}"

buildExtern : pull
	cd extern; xcodebuild -sdk iphonesimulator -configuration Release; xcodebuild -sdk iphoneos -configuration Release

	# combine lib files for various platforms into one
	lipo -create "${TARGET_BUILD_DIR}/Release-iphoneos/OnePassword.framework/OnePassword" "${TARGET_BUILD_DIR}/Release-iphonesimulator/OnePassword.framework/OnePassword" -output "${TARGET_BUILD_DIR}/libOnePasswordExtension-${CURRENT_ONE_PASSWORD_EXTENSION_VERSION}.a"

	cp "${TARGET_BUILD_DIR}/libOnePasswordExtension-${CURRENT_ONE_PASSWORD_EXTENSION_VERSION}.a" Xamarin.iOS.OnePasswordExtension/libOnePasswordExtension.a

bind : buildExtern
	sharpie bind \
		-n Xamarin.iOS.OnePasswordExtension \
		-o Xamarin.iOS.OnePasswordExtension \
		-sdk iphoneos \
		extern/OnePasswordExtension/OnePasswordExtension.h

	@echo "\n\nThere were likely [Verify] attributes added to the ApiDefinitions.cs file."
	@echo "Please fix those or comment them out then run 'make build'."

build :
	cd Xamarin.iOS.OnePasswordExtension && msbuild /p:Configuration=Release
	cd Xamarin.iOS.OnePasswordExtension && nuget pack Xamarin.iOS.OnePasswordExtension.nuspec

publish :
	git tag -a "v${CURRENT_PROJECT_VERSION}" -m "Release v${CURRENT_PROJECT_VERSION}"
	git push origin "v${CURRENT_PROJECT_VERSION}"
	nuget push Xamarin.iOS.OnePasswordExtension/Xamarin.iOS.OnePasswordExtension.${CURRENT_PROJECT_VERSION}.nupkg -Source https://www.nuget.org/api/v2/package

clean :
	rm -rf extern/OnePasswordExtension
	rm -rf extern/build
	rm -rf extern/xamarin-one-password