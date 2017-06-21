using Foundation;
using ObjCRuntime;
using UIKit;

namespace Xamarin.iOS.OnePasswordExtension
{
	// typedef void (^OnePasswordLoginDictionaryCompletionBlock)(NSDictionary * _Nullable, NSError * _Nullable);
	delegate void OnePasswordLoginDictionaryCompletionBlock ([NullAllowed] NSDictionary arg0, [NullAllowed] NSError arg1);

	// typedef void (^OnePasswordSuccessCompletionBlock)(BOOL, NSError * _Nullable);
	delegate void OnePasswordSuccessCompletionBlock (bool arg0, [NullAllowed] NSError arg1);

	// typedef void (^OnePasswordExtensionItemCompletionBlock)(NSExtensionItem * _Nullable, NSError * _Nullable);
	delegate void OnePasswordExtensionItemCompletionBlock ([NullAllowed] NSExtensionItem arg0, [NullAllowed] NSError arg1);

	// @interface OnePasswordExtension : NSObject
	[BaseType (typeof(NSObject))]
	interface OnePasswordExtension
	{
		// +(OnePasswordExtension * _Nonnull)sharedExtension;
		// [Verify (MethodToProperty)]
		[Static]
		[Export ("sharedExtension")]
		OnePasswordExtension SharedExtension { get; }

		// -(BOOL)isAppExtensionAvailable __attribute__((availability(ios_app_extension, unavailable)));
		// [Unavailable (PlatformName.iOSAppExtension)]
		// [Verify (MethodToProperty)]
		[Export ("isAppExtensionAvailable")]
		bool IsAppExtensionAvailable { get; }

		// -(void)findLoginForURLString:(NSString * _Nonnull)URLString forViewController:(UIViewController * _Nonnull)viewController sender:(id _Nullable)sender completion:(OnePasswordLoginDictionaryCompletionBlock _Nonnull)completion;
		[Export ("findLoginForURLString:forViewController:sender:completion:")]
		void FindLoginForURLString (string URLString, UIViewController viewController, [NullAllowed] NSObject sender, OnePasswordLoginDictionaryCompletionBlock completion);

		// -(void)storeLoginForURLString:(NSString * _Nonnull)URLString loginDetails:(NSDictionary * _Nullable)loginDetailsDictionary passwordGenerationOptions:(NSDictionary * _Nullable)passwordGenerationOptions forViewController:(UIViewController * _Nonnull)viewController sender:(id _Nullable)sender completion:(OnePasswordLoginDictionaryCompletionBlock _Nonnull)completion;
		[Export ("storeLoginForURLString:loginDetails:passwordGenerationOptions:forViewController:sender:completion:")]
		void StoreLoginForURLString (string URLString, [NullAllowed] NSDictionary loginDetailsDictionary, [NullAllowed] NSDictionary passwordGenerationOptions, UIViewController viewController, [NullAllowed] NSObject sender, OnePasswordLoginDictionaryCompletionBlock completion);

		// -(void)changePasswordForLoginForURLString:(NSString * _Nonnull)URLString loginDetails:(NSDictionary * _Nullable)loginDetailsDictionary passwordGenerationOptions:(NSDictionary * _Nullable)passwordGenerationOptions forViewController:(UIViewController * _Nonnull)viewController sender:(id _Nullable)sender completion:(OnePasswordLoginDictionaryCompletionBlock _Nonnull)completion;
		[Export ("changePasswordForLoginForURLString:loginDetails:passwordGenerationOptions:forViewController:sender:completion:")]
		void ChangePasswordForLoginForURLString (string URLString, [NullAllowed] NSDictionary loginDetailsDictionary, [NullAllowed] NSDictionary passwordGenerationOptions, UIViewController viewController, [NullAllowed] NSObject sender, OnePasswordLoginDictionaryCompletionBlock completion);

		// -(void)fillItemIntoWebView:(id _Nonnull)webView forViewController:(UIViewController * _Nonnull)viewController sender:(id _Nullable)sender showOnlyLogins:(BOOL)yesOrNo completion:(OnePasswordSuccessCompletionBlock _Nonnull)completion;
		[Export ("fillItemIntoWebView:forViewController:sender:showOnlyLogins:completion:")]
		void FillItemIntoWebView (NSObject webView, UIViewController viewController, [NullAllowed] NSObject sender, bool yesOrNo, OnePasswordSuccessCompletionBlock completion);

		// -(BOOL)isOnePasswordExtensionActivityType:(NSString * _Nullable)activityType;
		[Export ("isOnePasswordExtensionActivityType:")]
		bool IsOnePasswordExtensionActivityType ([NullAllowed] string activityType);

		// -(void)createExtensionItemForWebView:(id _Nonnull)webView completion:(OnePasswordExtensionItemCompletionBlock _Nonnull)completion;
		[Export ("createExtensionItemForWebView:completion:")]
		void CreateExtensionItemForWebView (NSObject webView, OnePasswordExtensionItemCompletionBlock completion);

		// -(void)fillReturnedItems:(NSArray * _Nullable)returnedItems intoWebView:(id _Nonnull)webView completion:(OnePasswordSuccessCompletionBlock _Nonnull)completion;
		// [Verify (StronglyTypedNSArray)]
		[Export ("fillReturnedItems:intoWebView:completion:")]
		void FillReturnedItems ([NullAllowed] NSObject[] returnedItems, NSObject webView, OnePasswordSuccessCompletionBlock completion);

		// -(void)fillLoginIntoWebView:(id _Nonnull)webView forViewController:(UIViewController * _Nonnull)viewController sender:(id _Nullable)sender completion:(OnePasswordSuccessCompletionBlock _Nonnull)completion __attribute__((deprecated("Use fillItemIntoWebView:forViewController:sender:showOnlyLogins:completion: instead. Deprecated in version 1.5")));
		[Export ("fillLoginIntoWebView:forViewController:sender:completion:")]
		void FillLoginIntoWebView (NSObject webView, UIViewController viewController, [NullAllowed] NSObject sender, OnePasswordSuccessCompletionBlock completion);
	}
}
