# workaround for bitcode generation problem with Xcode 7.3
# unset TOOLCHAINS

OTHER_CFLAGS="-fembed-bitcode"

BITCODE_GENERATION_MODE=bitcode

# configure directories for intermediate and final builds
# UNIVERSAL_OUTPUTFOLDER=${SRCROOT}/${PROJECT_NAME}-lib

# define output folder environment variable
# UNIVERSAL_OUTPUTFOLDER=${BUILD_DIR}/${CONFIGURATION}-universal

## Requirements
# * Xcode
# * Sharpie

# CC=clang # or gcc (tested with clang 3.2 and gcc 4.7.1)
# LIBRARIES:= -lobjc
# SOURCE=OnePasswordExtension.m
# CFLAGS=-Wall -Werror -g -v $(HEADERS) $(SOURCE)
# LDFLAGS=$(LIBRARIES)
# OUT=-o OnePasswordExtension.o

TARGET_BUILD_DIR=extern/build
CURRENT_PROJECT_VERSION=1.8.4

all : clean bind

pull :
	git clone git@github.com:agilebits/onepassword-app-extension.git extern/OnePasswordExtension
	cd extern/OnePasswordExtension; git checkout "${CURRENT_PROJECT_VERSION}"

buildExtern : pull
	cd extern; xcodebuild -sdk iphonesimulator -configuration Release; xcodebuild -sdk iphoneos -configuration Release

	# xcodebuild -workspace NetworkLib.xcworkspace -scheme  -sdk iphonesimulator -configuration Debug
	# xcodebuild -workspace NetworkLib.xcworkspace -scheme NetworkLib -sdk iphoneos -configuration Debug

	# make a new output folder
	mkdir -p ${TARGET_BUILD_DIR}/../xamarin-one-password

	# combine lib files for various platforms into one
	lipo -create "${TARGET_BUILD_DIR}/Release-iphoneos/OnePassword.framework/OnePassword" "${TARGET_BUILD_DIR}/Release-iphonesimulator/OnePassword.framework/OnePassword" -output "${TARGET_BUILD_DIR}/libOnePasswordExtension-${CURRENT_PROJECT_VERSION}.a"

	cp "${TARGET_BUILD_DIR}/libOnePasswordExtension-${CURRENT_PROJECT_VERSION}.a" Xamarin.OnePassword/libOnePasswordExtension.a

bind : buildExtern
	sharpie bind \
		-n Xamarin.OnePassword \
		-o Xamarin.OnePassword \
		-sdk iphoneos \
		extern/OnePasswordExtension/OnePasswordExtension.h

build : bind
	cd Xamarin.OnePassword && msbuild /p:Configuration=Release
	cd Xamarin.OnePassword && nuget pack Xamarin.OnePassword.nuspec

clean :
	rm -rf extern/OnePasswordExtension
	rm -rf extern/build