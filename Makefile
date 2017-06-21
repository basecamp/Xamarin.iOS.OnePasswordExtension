## Requirements
# * Xcode
# * Sharpie

TARGET_BUILD_DIR=extern/build
CURRENT_PROJECT_VERSION=1.8.4
OTHER_CFLAGS="-fembed-bitcode"
BITCODE_GENERATION_MODE=bitcode

all : clean bind

pull :
	git clone git@github.com:agilebits/onepassword-app-extension.git extern/OnePasswordExtension
	cd extern/OnePasswordExtension; git checkout "${CURRENT_PROJECT_VERSION}"

buildExtern : pull
	cd extern; xcodebuild -sdk iphonesimulator -configuration Release; xcodebuild -sdk iphoneos -configuration Release

	# combine lib files for various platforms into one
	lipo -create "${TARGET_BUILD_DIR}/Release-iphoneos/OnePassword.framework/OnePassword" "${TARGET_BUILD_DIR}/Release-iphonesimulator/OnePassword.framework/OnePassword" -output "${TARGET_BUILD_DIR}/libOnePasswordExtension-${CURRENT_PROJECT_VERSION}.a"

	cp "${TARGET_BUILD_DIR}/libOnePasswordExtension-${CURRENT_PROJECT_VERSION}.a" Xamarin.iOS.OnePasswordExtension/libOnePasswordExtension.a

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

clean :
	rm -rf extern/OnePasswordExtension
	rm -rf extern/build
	rm -rf extern/xamarin-one-password